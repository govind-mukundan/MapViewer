.. -*- coding: UTF-8 -*-

:Subtitle: Allowing rows to be disabled
		
.. _blog-disabledrows:

Enabling disabling
==================

2014-05-20

    *"What sort of stupid ListView doesn't allow rows to be disabled?
    I'm ditching your control and writing one myself."*

Not all ObjectListView's feature requests are phrased politely :) 

Other people have asked for the same feature in much more diplomatic terms, so I thought it would be nice to see if it was possible. Being able to disable rows would be a neat ability. Although it would be easy to misuse (from a UI point of view), I can easily imagine cases were it would be very useful.

Defining disabled
-----------------

A disabled row would have the following characteristics:

    * cannot be selected
    * cannot be activated (double-clicked, pressing Enter)
    * cannot be edited
    * cannot be checked
    * *can* be focused
    * must be visually distinct (often grey text with greyscale images)

Nothing very surprising, except for the focus bit. [Quick reminder: focus is the UI element that receive keyboard input. Several rows can be selected; only one can be focused] Disabled rows need to be able to be focused so that the user can use the UpArrow/DownArrow and PageUp/PageDown to move through the rows. If disabled rows could not be focused, using the arrows keys to move through the list would simply stop before a disabled row. We could try to skip over disabled rows, but that leads to nasty rabbit hole -- what happens on PageDown/PageUp when the next page contains only disabled items, what happens when all the rows are disabled?

While editing, the user can use Alt-Up or Alt-Down to start editing the rows above or below the current cell. If the row in that direction is disabled, editing will skip over that row until the next enabled row in that direction.

The first thing that is going to happen after I release this is that someone is going to want slightly different behaviour: to look disabled, but still be editable, or to be disabled but still be checkable. Just say no! For the moment, this is the behaviour of disabled rows and you cannot pick and choose :)

Make it so
----------

With this description of features, what seems the most technically difficult? My code controls editing, checking and (most of) displaying, so that shouldn't be troublesome. But how to you prevent rows from being selected?

For a `ListView`, there are several ways to select rows:

    * clicking (obviously)
    * shift clicking selects all rows between the anchor and the clicked row
    * `Ctrl-A` selects all rows
    * Lasso select

According to the MSDN, all these ways of selecting rows eventually result in the same `LVN_ITEMCHANGING` reflected notification. We can handle this notification and look for a change in selection state::

    bool isSelected = (nmlistviewPtr.uNewState & LVIS_SELECTED) == LVIS_SELECTED;

Better still, `LVN_ITEMCHANGING` is cancellable, so we just have to set the `m.Result = (IntPtr)1;` when the user tries to select a disabled row and the selecting will be cancelled.

Oh yeah! Half an hour and the most difficult part was done! Except... this worked fine for plain `ObjectListView` but didn't work at all for virtual listviews (`FastObjectListView` and `TreeListView`). `LVN_ITEMCHANGING` is never triggered for virtual lists :( The documentation even says so.

`LVN_ITEMCHANGED` (note the -ED) is triggered for both normal and virtual listviews, *but* it can't be cancelled. It is simply saying, "Hey. This row has changed, and there's nothing you can do about it!" I tried various ways of tweaking the notification, changing its data, skipping base processing, cancelling the event, but nothing worked reliably.

If the neat solution doesn't work, we can always use the sledgehammer! When we receive notification that a row has been selected AND we want that row to be disabled, we could just forcibly reverse the selection::

    OLVListItem olvi = this.GetItem(nmlistviewPtr.iItem);
    if (olvi != null && !olvi.Enabled)
        NativeMethods.SetItemState(this, nmlistviewPtr.iItem, LVIS_SELECTED, 0);

Surprisingly, this actually works. For both normal and virtual lists, this prevents selection for clicking and lasso selection. However...

Virtual listview complexities
-----------------------------

Virtual lists are the problem child of the ListView family. They take delight in making the programmers life challenging. If something is going to be difficult, it will always be with virtual lists. For example...

On virtual lists, selecting a range via Shift-Click or selecting all items via Ctrl-A present problems.

The problem is that for both Shift-Click and Ctrl-A the listview receives a single notification but multiple rows have been selected. The notification can only indicate a single item `nmlistviewPtr.iItem` but we need to know the other rows that were selected.
We can solve the Ctrl-A problem. When all items are selected via Ctrl-A, the notification has a special value -1 in `nmlistviewPtr.iItem`. When we see this value, we know that all rows have been selected, so we can loop through all our disabled objects and explicitly deselect them again::

    // -1 indicates that all rows are to be selected -- in fact, they already have been.
    // We now have to deselect all the disabled objects.
    if (nmlistviewPtr2.iItem == -1)
    {
        foreach (var disabledModel in this.DisabledObjects)
        {
            int modelIndex = this.IndexOf(disabledModel);
            if (modelIndex >= 0)
                NativeMethods.SetItemState(this, modelIndex, LVIS_SELECTED, 0);
        }
    }

Shift-Click is more troublesome. It's troublesome because it's difficult to explain exactly what shift-clicking does. When you shift click on a row in the listview, all the rows between the clicked row and the **anchored row** are selected. But which row is anchored? That's the difficult part. It's different depending on how the last selection was made and what OS you are on. Clicking, ctrl-click, lasso all set the anchor point in different ways. Another complexity is that "between" is calculated very differently if the list is grouped or ungrouped.

After struggling with all these complexities, I had my light bulb moment! I don't really care what was selected, I just want to make sure that all disabled rows are still deselected. Once I realised that, I could use the same processing as for Ctrl-A handling. Done!

Getting the colour right
------------------------

Ok. Now rows can be disabled and they can't be selected. But those rows still look the same. Now we have to make them look different.

Disabled items are normally greyed out in some way. That's easy -- just set the `ForeColor` on the items to grey. It does work, but it's not great. The text on the disabled rows is grey, but the images are still coloured and the checkboxes still look enabled.

.. image:: images/blog8-greytext-colourimages.png

For non-owner drawn lists, this is the best we can do. The underlying `ListView` control allows the text colour to be set, but the images are still going to be color. [Yes, we could create grey-scale versions of all images and then somehow remap the images when the row was disabled -- but that's too much fragile work even for me].

For owner drawn lists, we have much more control. Firstly, if a row is disabled, we can draw any images as grey scale. There is a very handy method `ControlPaint.DrawImageDisabled()` which does (almost) exactly what we want. It's only limitation is that it doesn't scale images automatically, so if the image needs scaling, we will have to do that manually (which I don't currently, so disabled images won't be scaled to fit into narrower spaces).

This doesn't work so well for images that come from `ImageLists`. At the moment, the majority of images are drawn using `ImageList_Draw()` WinApi function. This does nice things for us and is fast! But it doesn't do greyscale drawing.

It does have a big brother `ImageList_DrawIndirect()` which is the "airplane cockpit" version with 10 thousand switches and dials. But on the surface, it doesn't do grey scale drawing either! Humph! But let's look a little deeper.

`ImageList_DrawIndirect()` accepts a single (large) parameter block::

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGELISTDRAWPARAMS
    {
        public int cbSize;
        public IntPtr himl;
        public int i;
        public IntPtr hdcDst;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public int xBitmap;
        public int yBitmap;
        public uint rgbBk;
        public uint rgbFg;
        public uint fStyle;
        public uint dwRop;
        public uint fState;
        public uint Frame;
        public uint crEffect;
    }

It turns out that if you include `ILS_SATURATE` in the `fState` and set `rgbFg` to something dark (say, black or `CLR_DEFAULT`), it will actually draw an image in greyscale. 

So, tweaking our renderers to use these two technique (and remembering to draw check boxes as disabled), our disabled rows now look like this:

.. image:: images/blog8-allgrey.png

This actually looks pretty good.

Making it available
-------------------

The last piece in the puzzle is to make these features available to the programmer.

`ObjectListView.DisabledObjects`
    Gets or sets the entire collection of model objects whose rows should be disabled.

`ObjectListView.DisableObject()`, `ObjectListView.DisableObjects()`
    Disables all the model objects that are passed in. If the given model objects are not currently in the list, the control still remembers that they are disabled.

`ObjectListView.EnableObject()`, `ObjectListView.EnableObjects()`
    Enables one or more models objects, reversing the effects of `DisableObject()`. Does nothing if the given models are not currently disabled. 

`ObjectListView.IsDisabled()`
    Is the given model object disabled?

`ObjectListView.Reset()`
    This existing method now also resets which objects are disabled.

`OLVListItem.Enabled`
    Is this given item disabled? This can be read safely, but 
    like all `ListItem` stuff, you shouldn't mess with this. If you don't understand this, see :ref:`unlearn`.

`ObjectListView.DisabledItemStyle`
    Gets or sets the styling that will be used for formatting disabled rows.

`ObjectListView.DefaultDisabledItemStyle` (static property)
    Gets or sets the default disabled item style that will be used when nothing else has been given.
    Gets or sets the styling that will be used for formatting disabled rows.