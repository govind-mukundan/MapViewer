.. -*- coding: UTF-8 -*-

:Subtitle: What's wrong with the ToolTip class?

.. _blog-tooltip:

Technical Blog - Tool tips
==========================

27 May 2009

I wanted to be able to customise the tool tip that shows when the mouse is
hovered over a listview cell. Primarily, I wanted to be able to make it look
like a balloon, but any other customisations would be a bonus.

As always, the simplest approach is the best, so I created a `ToolTip` and installed it on the `ObjectListView`::

        protected override void OnCreateControl() {
            this.toolTip = new ToolTip();
            this.toolTip.BackColor = Color.Red;
            this.toolTip.SetToolTip(this, "using normal tooltip");
        }

This works and gives tooltips with a red background. You can even make the hover
tooltip show a balloon like this::

        protected override void OnCreateControl() {
            this.toolTip = new ToolTip();
            this.toolTip.IsBalloon = true;
            this.toolTip.SetToolTip(this, "using normal tooltip");
        }

.. image:: images/blog2-balloon1.png

Winner! Less than an hour and I can customise the tool tip.


Reality bites back
------------------

But then I noticed
one small problem -- it didn't actually work.
If I did this::

        protected override void OnCreateControl() {
            this.toolTip = new ToolTip();
            this.toolTip.SetToolTip(this, "using normal tooltip");
            this.toolTip.IsBalloon = true;
        }

No tool tip appeared at all. In fact, changing one of several properties
(*IsBalloon*, *ShowAlways*, *StripAmpersands*, *UseAnimation*, *UseFading*) after the tool
tip was installed AND the listview was created caused the tooltip to disappear.

The problem is that when these properties are changed on a `ToolTip`, the
underlying handle is destroyed and then recreated. However, when the underlying
control handle is recreated, it is not reinstalled into the `ListView` (or any
other control that uses a `ToolTip`).

We could get around this by subclassing `ToolTip`, and whenever any of those
properties are changed, we could call `SetToolTip()` to force the new handle to
be installed.

This almost works except that I wanted to be able to change these settings
during a `TTN_GETDISPINFO` callback (this is the notification message that is sent
when a tooltip control wants to know what text it should show). I want to do
this so that each cell can decide how it wants its tooltip to appear. If
something in a cell is important, it should be able to be in red with an icon
and title.

But it seems that this is just impossible. Any trick I tried during that
callback simply resulted in the tooltip disappearing for good.

Not getting a Handle on things
------------------------------

It should be possible to get around all these problems and just change the
`IsBalloon` setting by changing the `WindowStyle`. To do that, all we need is
the handle to the tooltip control. But `ToolTip` doesn't make that available: it
doesn't have a `Handle` property. It also doesn't expose a `WndProc` method that can
be overridden. At this point, I began to be seriously displeased with the
`ToolTip` class.

Another ToolTipControl
----------------------

To get around all these problems, I wrote `ToolTipControl`, a `NativeWindow`
which wraps a tooltip control. It is not a replacement for a `ToolTip`, but that
does all the tool tip shown by a `ListView` or its header to be customised in just
about any way you would like.

.. image:: images/blog2-balloon2.png

The Vista from here is not so good
----------------------------------

But it's still not perfect. It works flawlessly on XP, but under Vista, it has some
limits. Most significantly, changing to balloon style during an event does not work
reliably. It works mostly, but occassionally, the tool tip doesn't recalculate
its display rectangle correctly. It displays the balloon tip using the same bounds
as if it wasn't a balloon. If you set `IsBalloon` outside of an event, it works fine.

One final problem is that setting the `ForeColor` and `BackColor` simply doesn't work.
I suspect that these colours are controlled by the system theme, but I'm not sure about
that. It's certainly not documented anywhere.
