.. -*- coding: UTF-8 -*-

:Subtitle: Nasty flickers! Hsss, me hates 'em

.. _blog-virtuallistflickers:

Flickering and the Virtual List
===============================

2012-05-04

One of the very first lines of code I wrote for this project was::

    this.DoubleBuffered = true; // kill nasty flickers. hiss... me hates 'em

I *hate* it when UI controls flicker. It looks unprofessional, and it simply annoys me.
One of my consistent goal for `ObjectListView` is that it does not flicker.
So when a couple of people reported that `FastObjectListView` and `TreeListView` would
sometimes flicker badly, I was irritated.

Now, I know you can make the `ObjectListView` controls flicker -- but you have to try hard. It's almost
always a programmer error that causes the flickering. However, I knew the people
who submitted the flickering reports and they were clever guys who were unlikely to make
silly mistakes. Armed with the conviction that these guys were probably right, I looked hard.

Flicker occurs when the underlying ListView control is told to do one thing, and then moments later
is told to do something different. So, for example, setting a cell's value to empty and then moments later
set it to not empty will cause a slight flicker. Setting the sort order to one thing and then to something
different will cause a major flicker, as will scrolling to one position and then to a different position.

After playing with the `TreeListView` for a while, I found that if the control was scrolled down to show
a later part of the list, it would sometimes jump/flicker when a branch was
collapsed or expanded.
You can see this jumpiness below:

.. raw:: html

    <div style="margin-top:8px;">
      <object type="application/x-shockwave-flash" data="_static\flicker.swf"  width="265" height="281">
        <param name="movie" value="flicker.swf" />
        <param name="quality" value="high" />
        <param name="bgcolor" value="#ffffff" />
        <p>You do not have the latest version of Flash installed. Please visit this link to download it: <a href="http://www.adobe.com/products/flashplayer/">    http://www.adobe.com/products/flashplayer/</a></p>
      </object>
    </div>

The annoying thing was that the control wouldn't always flicker.
If the control had a row selected and that row was visible, the control would not flicker.
But if the select row was offscreen, the control would flicker.

I tried clearing the selection so that no rows were selected, but the flickering remained.

I remembered that native ListView controls have the concept of 'focused' as well as 'selected'.
A focused row is the row that is going to handle any keyboard input.
The two are quite closely related. Very often, the selected row is also the focused row, but it
is not always the case. Even when nothing is selected, a row can be focused.

I tried removing the focus by setting `FocusedItem` to *null*, but that made no difference to the flickering.
[Later, when I was doing the disassembling mentioned below, I found that setting `FocusedItem` to nil is pointless
-- it literally does nothing. ]

Darn!

What's really going on here?
----------------------------

OK, let's think about this for a second.
Adding a new row to a virtual list is (fundamentally) a very simple operation -- it increments
the `VirtualListSize` property. `ObjectListView` does quite a bit of other work, depending on sorting and
grouping, but in its simplest case, it is not complicated. There was no reason that simply adding a new
row should cause flickering.

When all else fails, use the source (Luke). So, I pulled out my dissembler to see exactly what was happening
in .NET's `VirtualListSize` property setter.

The important part of that code looks something like this::

    bool flag = this.IsHandleCreated && this.VirtualMode && this.View == View.Details && !this.DesignMode;
    int num = -1;
    if (flag)
        num = this.SendMessage(LVM_GETTOPINDEX, 0, 0));
    this.virtualListSize = value;
    if (this.IsHandleCreated && this.VirtualMode && !this.DesignMode)
        this.SendMessage(LVM_SETITEMCOUNT, this.virtualListSize, 0);
    if (flag)
    {
        num = Math.Min(num, this.VirtualListSize - 1);
        if (num > 0)
        {
            ListViewItem topItem = this.Items[num];
            this.TopItem = topItem;
        }
    }

Hey! What is that messing around with `TopItem`? .NET finds the index of the item that is currently at the
(visible) top of the control, then changes the virtual list size, and then tries to scroll the control
back to the same item. Why does it do that?

I wrote a simple test case to just send the `LVM_SETITEMCOUNT` directly. Ahhhh... now I understand.
The behaviour of that message in regards to scroll position is unpredictable. It seems that it will
always try to show the focused row, which could be anywhere. If the focused row is already visible,
the control doesn't move at all. But if the focused row is offscreen, the control will scroll to somewhere
else (but not always to the focused row -- sigh)
The SDK documentation doesn't say anything
about moving the scroll position, so I guess this is a "feature".

So that explains the flicker. The native `LVM_SETITEMCOUNT` changes the scroll position to somewhere else,
and then the .NET `ListView` class tries to put it back to what it was.

Flicking the flicker
--------------------

Now I knew what was causing the problem. But how could I fix it? The problem is in the .NET library itself
(more specifically, in the interaction between the .NET ListView class
and the underlying Windows ListView control). I don't think waiting for Microsoft to fix it was going to work.

The `VirtualListSize` property is not declared as `virtual` so I could not just override it. However, it is never
used within the .NET classes themselves -- only by the ObjectListView project code. So, I could `new` it, then all
my code would call my version of that property.

In my implementation, I don't want to mess around with the `TopItem` property, since that will just cause the
same flickering I wanted to avoid. The .NET code only did that because the `LVM_SETITEMCOUNT` messed up the
scroll position. However, it's clear that MS did notice the problem, since in CommonControl v4.70 (which was
released with IE v3.0), that message now has a special flag `LVSICF_NOSCROLL` which makes the control maintain
its scroll position. Exactly what I wanted.

In addition to calling the `LVM_SETITEMCOUNT` with an extra parameter, we have to do one more thing -- a
plain and simple dirty hack. The `VirtualListSize` property also updates a private field with the value of the
size of the list. Since it is a private field, we cannot even see it, let alone change it.

This is a problem for compiled C#, but not for reflection! Reflection can reach into the bowel of `ListView`
and pull out the info about the private field. Once we have a `FieldInfo` we can use that to update the
private variable, thus keeping the rest of the ListView class happy.

So, `VirtualObjectListView` now has its own implementation of `VirtualListSize` which successfully changes
the size of the list without any flicker::

    protected new virtual int VirtualListSize {
        get { return base.VirtualListSize; }
        set {
            if (value == this.VirtualListSize || value < 0)
                return;

            // Get around the 'private' marker on 'virtualListSize' field using reflection
            if (virtualListSizeFieldInfo == null) {
                virtualListSizeFieldInfo = typeof(ListView).GetField("virtualListSize", BindingFlags.NonPublic | BindingFlags.Instance);
                System.Diagnostics.Debug.Assert(virtualListSizeFieldInfo != null);
            }

            // Set the base class private field so that it keeps on working
            virtualListSizeFieldInfo.SetValue(this, value);

            // Send a raw message to change the virtual list size *without* changing the scroll position
            if (this.IsHandleCreated && !this.DesignMode)
                NativeMethods.SendMessage(this.Handle, LVM_SETITEMCOUNT, value, LVSICF_NOSCROLL);
        }
    }
    static private FieldInfo virtualListSizeFieldInfo;

After, flicker free:

.. raw:: html

    <div style="margin-top:8px;">
      <object type="application/x-shockwave-flash" data="_static\flicker-gone.swf"  width="268" height="281">
        <param name="movie" value="flicker-gone.swf" />
        <param name="quality" value="high" />
        <param name="bgcolor" value="#ffffff" />
        <p>You do not have the latest version of Flash installed. Please visit this link to download it: <a href="http://www.adobe.com/products/flashplayer/">    http://www.adobe.com/products/flashplayer/</a></p>
      </object>
    </div>

Yeah! My controls are back to being flicker free :)

