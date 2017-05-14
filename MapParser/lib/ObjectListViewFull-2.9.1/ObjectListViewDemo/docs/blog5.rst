.. -*- coding: UTF-8 -*-

:Subtitle: Lifting up the downtrodden ListViewGroup

.. _blog-listviewgroups:

Promoting ListView Groups
=========================

2012-04-20

`ListViewGroups` are strange, second class citizens in the ListView world.

They exist, but there is almost nothing you can do with them -- you can't
draw them yourself, you can't change their colours, you can't tell when
the user clicks on them (apart from the Group Task text). You can't even
find out when the user does something to them (selects, expands or collapses),
let alone stop them from doing it.

Naturally, I find all these limitations annoying. So, I wanted to see how
many of these limitations I could remove.


Did something hit the group?
----------------------------

A fundamental ability that `ListViewGroups` lack is hit detection. There is no way to know, for example,
if the mouse is over a group. .NET's `ListView` doesn't include groups in its hit detection.

OK, let's drop down to the Windows SDK again. From Vista onwards, the ListView hit test messages allows
some form of hit detection on groups.
The `iGroup` member is supposed to be the index of the group under the point::

    struct _LVHITTESTINFO {
        POINT pt;
        UINT flags;
        int iItem;
        int iSubItem;
        int iGroup;
    } LVHITTESTINFO;

We can use this to do this::

    NativeMethods.LVHITTESTINFO lParam = new NativeMethods.LVHITTESTINFO();
    lParam.pt_x = x;
    lParam.pt_y = y;
    int index = NativeMethods.SendMessage(this.Handle, this.View == View.Details ? LVM_SUBITEMHITTEST : LVM_HITTEST, -1, ref lParam);

When the point is over a group, `flags` has the value `LVHT_EX_GROUP_HEADER` but the `iGroup` is... always 0 :(

Hmmm... Digging a little more, the SDK says that this field is only filled in
when the listview is owner data (i.e. virtual). So, trying this hit test on a `FastObjectListView`
shows that the `iGroup` is reliable. In fact, it is always filled in if the virtual list is showing groups,
even when the hit point is on a list item, not a group itself.

But what about on a normal `ObjectListView`? After some more work, it turns out that
when a group is hit on a normal `ListView`, the value returned is the *id* (not the *index* as with
a virtual list) of the group. Putting all these variations together gives us code like this::

    // Figure out which group is involved in the hit test. This is a little complicated:
    // If the list is virtual:
    //   - the returned value is list view item index
    //   - iGroup is the *index* of the hit group.
    // If the list is not virtual:
    //   - iGroup is always -1.
    //   - if the point is over a group, the returned value is the *id* of the hit group.
    //   - if the point is not over a group, the returned value is list view item index.
    OLVGroup group = null;
    if (this.ShowGroups && this.OLVGroups != null) {
        if (this.VirtualMode) {
            group = lParam.iGroup >= 0 && lParam.iGroup < this.OLVGroups.Count ? this.OLVGroups[lParam.iGroup] : null;
        } else {
            bool isGroupHit = (lParam.flags & (int)HitTestLocationEx.LVHT_EX_GROUP) != 0;
            if (isGroupHit) {
                foreach (OLVGroup olvGroup in this.OLVGroups) {
                    if (olvGroup.GroupId == index) {
                        group = olvGroup;
                        break;
                    }
                }
            }
        }
    }

With all this, we can figure out which group is under a point. I've added some new information
to `OlvListViewHitTestInfo` class:

    * `HitTestLocationEx` holds all the low level flags returned by the Windows message
    * `HitTestLocation` can now also be `HitTestLocation.Group` or `HitTestLocation.GroupExpander`,
      so that it is easy to see when a group or the expand/collapse button of a group is hit

Knowing when a group is expanded/collapsed
------------------------------------------

Several people asked if there was a way to know when a group was expanded or collapsed.

There is no documented way to do this. But `this blog`_ shows that there is an
undocumented notification -- `LVN_FIRST - 88` --  whenever the state of a group changes
-- including when its 'collapsed-ness' changes. The notification block looks
like this::

    [StructLayout(LayoutKind.Sequential)]
    public struct NMLVGROUP
    {
        public NMHDR hdr;
        public int iGroupId; // which group is changing
        public uint uNewState; // LVGS_xxx flags
        public uint uOldState;
    }

.. _this blog: http://something.com

Combine this with the notification with the data block and we can start work.
In our method to handle ReflectNotify, we add a switch value of `LVN_FIRST - 88`
and in response to that notification, we do this::

    protected virtual bool HandleGroupInfo(ref Message m)
    {
        NativeMethods.NMLVGROUP nmlvgroup = (NativeMethods.NMLVGROUP)m.GetLParam(typeof(NativeMethods.NMLVGROUP));

        // Ignore state changes that aren't related to selection, focus or collapsedness
        const uint INTERESTING_STATES = (uint) (GroupState.LVGS_COLLAPSED | GroupState.LVGS_FOCUSED | GroupState.LVGS_SELECTED);
        if ((nmlvgroup.uOldState & INTERESTING_STATES) == (nmlvgroup.uNewState & INTERESTING_STATES))
            return false;

        foreach (OLVGroup group in this.OLVGroups) {
            if (group.GroupId == nmlvgroup.iGroupId) {
                GroupStateChangedEventArgs args = new GroupStateChangedEventArgs(group, (GroupState)nmlvgroup.uOldState, (GroupState)nmlvgroup.uNewState);
                this.OnGroupStateChanged(args);
                break;
            }
        }

        return false;
    }

Nothing very difficult here. We're only interested in selection, focus or collapsedness state changes.
If the state change is interesting, find the group and trigger an event.

This is better than nothing -- but not very much. We know that the state changed, but we can't stop the state
changing. We can't stop the group being selected, and most importantly, we can't stop the group being
expanded or collapsed.

Cancelling state change
-----------------------

If we can't cancel the actual state change notification, and there doesn't seem to be a GroupStateChang-*ing* message from Windows,
what can we do?

We can be sneaky :)

The only way to expand/collapse a group is to click on the expander button. So, one approach would be:

    * Intecept the click event, see if it is *going* to click the group expand/button
    * Trigger a cancellable event. If the programmer wants to stop the expand/collapse, they
      could listen for that event and then set `IsCanceled` to *true*
    * If the event was cancelled, simply swallow the click event. The underlying ListView control would
      never see the click event, so wouldn't expand/collapse the group.

Let's make it so.

We can't just override the `OnMouseDown()` method since that happens too late -- the control has already seen the Windows message
and we can't stop it. Instead, we need to intercept the `WM_LBUTTONDOWN` message in the `WndProc()` message pump.
Actually... the group expand/collapse work off the mouse *up* event, so we really have to intercept the `WM_LBUTTONUP` message::

    protected virtual bool HandleLButtonUp(ref Message m) {
        if (this.MouseMoveHitTest == null)
            return false;

        // If we don't have collapsible groups, we don't need to do anything else
        if (!ObjectListView.IsVistaOrLater || !this.HasCollapsibleGroups)
            return;

        // If the user is trying to expand/collapse a group, give the program a chance to veto it
        if (this.MouseMoveHitTest.HitTestLocation == HitTestLocation.GroupExpander) {
            if (this.TriggerGroupExpandCollapse(this.MouseMoveHitTest.Group))
                return true;
        }

        // We have to call the default WndProc here, otherwise the group won't collapse/expand.
        base.DefWndProc(ref m);

        return false;
    }

Quite simple in the end. If the listview doesn't have collapsible groups, we don't need to do anything special.

Otherwise, if the user clicked in the expander, trigger an event to let the programmer veto the user's action.
`ObjectListView` calculates hit test information every time the mouse moves, so we can just
use that hit test information.

Finally, if the action wasn't veto'ed, we have to call the default WndProc, otherwise the group won't collapse.

Somewhat better
---------------

After this work, groups are somewhat better off than before. We do hit tests; we know when their state changes;
we can stop them expanding/collapsing.

It's still not perfect. I'd love to be able to owner draw them, or at least change their colours, but
at least they are not quite so down trodden as before.

