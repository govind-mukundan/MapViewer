.. -*- coding: UTF-8 -*-

:Subtitle: What's underneath the Overlays

.. _overlays-label:

Overlays and Decorations
========================

Overlays grew out of the desire to put a background image on a ListView.
(See :ref:`blog-overlays` for the details).

`ObjectListView` provides a normal set of operations to manage overlays::

    public void AddOverlay(IOverlay overlay);
    public bool HasOverlay(IOverlay overlay);
    public void RemoveOverlay(IOverlay overlay);

The `IOverlay` interface is very simple::

    public interface IOverlay {
        void Draw(ObjectListView olv, Graphics g, Rectangle r);
    }

Within the `Draw()` method, your implementation can draw whatever it likes.

In the Overlays.cs file, you can find several examples of overlays that
can serve as your model for implementation. The two most common overlays
are image overlays and text overlays.

* `ImageOverlay` draws an image over the `ObjectListView`

* `TextOverlay` is a highly configurable overlay that can draw bordered, colored
  and backgrounded text. The text can be rotated.

These two overlays are so common that `ObjectListView` comes with one of each
predefined. These predefined overlays are exposed to the IDE, so that they
can be configured directly from there. So, for the majority of cases, this
is the only understanding of overlays that you will need.

Disabling overlays
------------------

Though they seem simple, overlays are actually quite tricky underneath.
If they are causing problems (for example, if you are seeing `GlassPanelForms`
turn up where you are not expecting them), you can completely disable them
by setting `UseOverlays` to *false*.

Implementation
--------------

Overlays are implemented using the Layered Window API. This API allows a
toplevel windows to be translucent and partially transparent. Internally,
the `ObjectListView` creates a transparent window, positions it above the
control, and then paints the overlays onto that window.

.. image:: images/overlay.png

So, when displaying overlays, the `ObjectListView` creates a new
top-level window, and puts that above the control. In this way,
it looks like the overlays are drawn onto the list, but in reality,
they are completely separate.

And the two shall become one
----------------------------

The tricky bits of the overlay implementation are making the
overlay window acts as if it is actually part of the `ObjectListView`.

Obviously, when the `ObjectListView` moves, the overlay window must
move too. If we could make the overlay window a child window of the
`ObjectListView`, then Windows would make that happen for us. But
layered windows *have* to be top level windows. So we have to
manually move the overlay window when the `ObjectListView` moves.

Similarly, when the `ObjectListView` is resized, we have to manually
resize the overlay window to match the new size. If we do this resizing
in realtime as the user is resizing the window, it slows down the resizing
considerably. Apparently, resizing a large Layered window is an expensive
operation. So, we simply hide the overlays while the window is resized,
and then show them again once the resizing is complete.

The most difficult bit of making the overlay behaves as if it
were part of the `ObjectListView` is when the `ObjectListView` is hidden.
Simply being hidden (i.e. Visible = false) isn't a problem, but hidden
by placing other controls over the top of the `ObjectListView` is a major
problem.

Remember the overlay window is a separate top level window. Changing the
ordering of child windows within another top level window doesn't affect the
overlay window - it thinks it is still directly over the `ObjectListView`. Which
it is, in a way, it's just that other controls are now over the top of the
`ObjectListView`. The overlay window will still be visible, but the
`ObjectListView` will not be, so the overlays will be drawn on top of whatever
controls are over the `ObjectListView`.

If you think putting controls over the top of other control sounds like a
very unlikely scenario, this is exactly how tab controls work. They play with
the z-ordering of their TabPanels, moving the current TabPanel in front of the
other tabs. As currently implemented, `ObjectListView` understand the standard
Window Forms TabControl, and will correctly hide their overlays when a parent
TabControl switches tabs.

But if you are using a non-standard TabControl-like control, you will have to
do this hiding of overlay windows yourself. It is enough to call `HideOverlays()`
at the right time. `ObjectListView` will show their overlays when they are needed.

So, if you find in your app, that a parent control is hiding your `ObjectListView`
(as it should), but you can still see the overlays (when you shouldn't),
you have to call `HideOverlays()` when the `ObjectListView` is hidden.
