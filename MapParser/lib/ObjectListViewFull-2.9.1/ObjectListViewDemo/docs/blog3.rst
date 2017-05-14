.. -*- coding: UTF-8 -*-

:Subtitle: ListViewSubItem.Bounds almost works

.. _blog-subitemboundsbug:

Technical Blog - Just a little bug
==================================

1 August 2009

How big is a bug? If you calculate the altitude of a rocket in feet rather than
meters, is that a big bug? If you always put up the x-ray shield properly except when
operator has quickly pressed X, Up, E and then Enter, is that a big bug? In software
engineering, small mistakes have ways of becoming bigger -- or at least biting you
where you aren't expecting.

Case in point. A long, long time ago, in a version far, far away, my beautiful owner drawn
list control was misbehaving. The control should have looked like this:

.. image:: images/blog3-listview1.png

When column 0 was dragged to some other position and things went wrong. Column 0
still drew itself at its original location, overwriting the columns that should be drawn there!

.. image:: images/blog3-listview1a.png

What's going on here?

Just a little bug
-----------------

The problem was a *little* bug in the
`ListViewSubItem.Bounds` property. The bug was that it almost always returns the
subitem's bounds. The "almost" is because it doesn't work for subitem 0.
You can see the problem in this debug output::

    this.listViewComplex.Items[0].Bounds
    {X = 0 Y = 20 Width = 1576 Height = 17}
    this.listViewComplex.Items[0].SubItems[0].Bounds
    {X = 0 Y = 20 Width = 1576 Height = 17}

The bounds of subitem 0 are the same as the bounds of the whole item. It's not a
big bug, really.

When .NET was preparing the `DrawSubItem` event, it used the subitem bounds. But
because of this bug, they were wrong for column 0. So I wrote a quick hack to
calculate where the bounds actually are and did the owner drawing within that.
It was just one small hack to fix one little bug. After the hack, column 0
appeared where it should have:

.. image:: images/blog3-listview2.png

Still not right
---------------

That wasn't too bad. Except it still didn't work -- but I didn't find that out
until just last week, when another owner drawn listview started misbehaving.

With my little hack in place to hide the subitem bounds bug, things mostly work.
But scroll the listview all the way to the right and a little bit back to the
left again. Hey! Where's my column 0? It's vanished:

.. image:: images/blog3-listview3.png

While the list is scrolled to the right, column 0 is not there. Forget the
slings and arrows of outrageous fortune, column 0 simply refuses to be.
Click on the rows, scroll vertically, rectangle select -- nothing will make
column zero appear. A little bit of debugging showed that the `DrawSubItem` event
wasn't being triggered for column 0. It's difficult to owner draw something when
you don't even get the chance.

After some major head scratching and one-too-many Red Bull's, the problem was
narrowed to `ListViewSubItem.Bounds` once again. Delving into the bowels of the
.NET framework (Reflector_ is *such* a wonderful tool) there is this code in
`ListView.CustomDraw` (a private method)::

    Rectangle subItemRect = this.GetSubItemRect(dwItemSpec, lParam->iSubItem);
    if ((lParam->iSubItem == 0) && (this.Items[dwItemSpec].SubItems.Count > 1)) {
        subItemRect.Width = this.columnHeaders[0].Width;
    }

.. _Reflector: http://www.red-gate.com/products/reflector

The lines that test the column and then modify the width of the bounds reek of
being another hack. Someone at MS realised there was a bug in the
`GetSubItemRect` method (which is what `ListViewSubItem.Bounds` uses), but
rather than fixing it, they simply coded around it. It's easier that way, isn't
it? I'd already done the same thing. But like my hack, it didn't completely work.

When the list is scrolled to the right, the bounds
of subitem 0 look like this::

    this.listViewComplex.Items[0].SubItems[0].Bounds
    {X = -147 Y = 20 Width = 1626 Height = 17}

Once `ListView.CustomDraw` method applies its width limitation (getting around
the `ListViewSubItem.Bounds` bug), it ends up with a bounds for subitem 0 of
something like::

    {X = -147 Y = 20 Width = 100 Height = 17}

Even my limited mathematical ability can see that this means that cell 0 has a
negative right edge. Quite rightly, the remainer of the `CustomDraw` method
doesn't draw the cell, since it "knows" that it's not on screen.

So, on an owner drawn `ListView` when column 0 is dragged to any position other
than position 0 AND the control is scrolled to the right, the cells for column 0
will simply not be drawn -- a `DrawSubItem` event will not be triggered for it.
And there was nothing I could do to fix it. I could hack the column 0 bounds within
the `DrawSubItem` event, but I can't hack not receiving the event at all. And
the problem code is buried within a .NET private method.

Never say never
---------------

Well, "nothing" is not quite right. Everything can be fixed -- it just depends on the amount of
effort you are willing to expend. Sensible people would say it can't be fixed and leave it at that.
But sometimes I am not particularly sensible :)

The trick to fixing this bug is duplicate .NET's behaviour -- just without the bug.

The culprit is .NET's handling of the `CustomDraw` message. So, the first step is
to intercept that notification::

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case 0x204E: // WM_REFLECT_NOTIFY
                    this.HandleReflectNotify(ref m);
                default:
                    base.WndProc(ref m);
            }
        }

        protected bool HandleReflectNotify(ref Message m) {
            NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
            switch (nmhdr.code) {
                case -12: //NM_CUSTOMDRAW
                    //System.Diagnostics.Debug.WriteLine("NM_CUSTOMDRAW");
                    if (!this.HandleCustomDraw(ref m))
                        base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
            }
        }

Once we have the custom draw notification, we have to decide if we are in the
bug triggering condition -- owner drawing column 0 in any column except 0. If we
are, we have to trigger our own `DrawListViewSubItem` event, and then prevent the default
processing from occuring::

        protected bool HandleCustomDraw(ref Message m) {
            const int CDDS_PREPAINT = 1;
            const int CDDS_ITEM = 0x00010000;
            const int CDDS_SUBITEM = 0x00020000;
            const int CDDS_SUBITEMPREPAINT = (CDDS_SUBITEM | CDDS_ITEM | CDDS_PREPAINT);

            NativeMethods.NMLVCUSTOMDRAW nmcustomdraw =
                (NativeMethods.NMLVCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.NMLVCUSTOMDRAW));
            switch (nmcustomdraw.nmcd.dwDrawStage) {
                case CDDS_SUBITEMPREPAINT:
                    // Are we owner drawing column 0 when it's in any column except 0?
                    if (!this.OwnerDraw)
                        return false;
                    int columnIndex = nmcustomdraw.iSubItem;
                    if (columnIndex != 0)
                        return false;
                    int displayIndex = this.Columns[0].DisplayIndex;
                    if (displayIndex == 0)
                        return false;
                    int rowIndex = (int)nmcustomdraw.nmcd.dwItemSpec;
                    if (rowIndex < 0 || rowIndex >= this.Items.Count)
                        return false;

                    // OK. We have to avoid .NET's buggy code.
                    // Trigger an event to draw column 0 when it is not at display index 0
                    using (Graphics g = Graphics.FromHdc(nmcustomdraw.nmcd.hdc)) {
                        // We can hardcode "0" here since we know we are only doing this for column 0
                        ListViewItem item = this.Items[rowIndex];
                        Rectangle r = this.GetSubItemZeroRect(rowIndex);
                        DrawListViewSubItemEventArgs args =
                            new DrawListViewSubItemEventArgs(g, r, item, item.SubItems[0], rowIndex, 0,
                            this.Columns[0], (ListViewItemStates)nmcustomdraw.nmcd.uItemState);
                        this.OnDrawSubItem(args);

                        // If the event handler wants to do the default processing
                        // (i.e. DrawDefault = true), we are stuck. There is no way
                        // can force the default drawing because of the bug in .NET we are trying to get around.
                        System.Diagnostics.Trace.Assert(!args.DrawDefault, "Default drawing is impossible in this situation");
                    }
                    m.Result = (IntPtr)4;

                    return true;
            }
            return false;
        }

Nothing surprising here. But we still have to face the whole cause of the
problem. How do we correctly calculate the bounds of cell 0? We *cannot* use the
`LVM_GETSUBITEMRECT` message since the actual bug is within that message [please
do not email me to say that it *should* behave like that. It should not. End of
story]. We have to try a different approach.

The .NET code use the column headers width to
decide how wide cell 0 should be. Why not just continue that trend and use the
column header to tell where the left edge is as well. To do that, we have to
prod the underlying control a bit::

        private Rectangle GetSubItemZeroRect(int rowIndex) {
            const int LVM_GETHEADER = 0x1000 + 31;
            const int HDM_GETITEMRECT = 0x1200 + 7;

            IntPtr header = NativeMethods.SendMessage(this.Handle, LVM_GETHEADER, 0, 0);
            NativeMethods.RECT headerCellBounds = new NativeMethods.RECT();
            NativeMethods.SendMessage(header, HDM_GETITEMRECT, 0, ref headerCellBounds);

            // Take the horizontal scroll position into account
            int scrollH = 0;
            NativeMethods.SCROLLINFO si = new NativeMethods.SCROLLINFO();
            si.fMask = 4 /*SIF_POS*/;
            if (NativeMethods.GetScrollInfo(this.Handle, 0 /*SB_HORZ*/, si))
                scrollH = si.nPos;

            Rectangle r = this.GetItemRect(rowIndex, ItemBoundsPortion.Entire);
            r.X = headerCellBounds.left - scrollH;
            r.Width = headerCellBounds.right - headerCellBounds.left;
            return r;
        }

The return of column 0
----------------------

With all that code finally in place, our column 0 is now happily on display again, scrolling
left and right without problem:

.. image:: images/blog3-listview4.png

A Plea
------

100+ lines of non-obvious code. A ridiculous amount of effort that no sensible
person would ever waste their time writing! All because someone didn't fix a bug
that they obviously knew about.

In your next project, when you next find a bug in the code base, don't just code around it.
Fix the bug when you find -- even if it's only just a little one.
