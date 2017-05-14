.. -*- coding: UTF-8 -*-

:Subtitle: Implementing an image background in a ListView

.. _blog-overlays:

Technical Blog - Overlays
=========================

6 May 2009

I like pictures. I think it's neat that in Explorer you can put a little graphic
in the bottom right of the listview. I wanted to do the same thing with an
`ObjectListView`. Surely, it can't be that difficult.

First step: be clear on what I want. I want a background image on the ListView. It has
to stay fixed in place, not scrolling when the listview scrolls. It has to work
on XP and Vista. It has to be easy to customise, ideally just setting an image
within the IDE. If the image could be positioned in whatever corner, or have a
varying level of transparency, those would be bonuses.

And obviously, I want it to work flawlessly -- though I will be content with working
spectacularly well.

WM_ERASEBKGROUND
----------------

The classic solution is to intercept the `WM_ERASEBKGROUND` message, erase the
*ClientRectangle*, draw whatever you want, and the rest of the control then draws
over what you've already drawn. Easy.

But it doesn't work. Actually, it works, so long as you don't double buffer the
`ListView`. While the `ListView` is unbuffered, the image drawn in the
`WM_ERASEBKGROUND` handler appears fine. But, when the control is double buffered,
it doesn't work.

This is because, when *DoubleBuffered* is set to *true*, it also sets the style
`AllPaintingInWmPaint` style, which means: don't use `WM_ERASEBKGROUND`, the paint
handler will do everything, including erase the background. So, for a double-
buffered `ListView` (which is what I want), drawing in the `WM_ERASEBKGROUND`
handler doesn't work.

LVM_SETBKIMAGE
--------------

The second try was to use `LVM_SETBKIMAGE`. This WinSDK message tells a listview control
to draw an image under the control. Exactly what I wanted. But life is rarely
that easy.

The first difficulty was actually making it work. TortoiseSVN_ sometimes has a
background image on their list views, and Stefan had `kindly documented some of
his troubles`__ in getting it to work. Using the information there, I managed
to put an image under the control! Excellent... well not really.

.. _TortoiseSVN: http://tortoisesvn.net

.. __: http://tortoisesvn.net/listcontrol_watermark

.. image:: images/blog-setbkimage.png

As can be seen, this did put an image under the ListView, but with a number of unpleasant side-effects:

* With a background image in place, row background colours no longer worked.

* The background image was always hard pressed in the bottom right corner. The
  `LVBKIMAGE` structure has fields, *xOffset* and *yOffset*, which supposedly allow you
  to change this, but as far as I could see, they had no effect.

* Under XP, images with transparent areas do not draw transparently - the
  transparent area is always coloured blue. This is even when
  `LVBKIF_FLAG_ALPHABLEND` flag is set.

* Grid lines were drawn over the top of the image, which looked odd.

* Icons on subitems made the subitem cell draw over the image.

* Owner drawn cells always erased the image (I suspect this could be fixed).

The show stopper was with Details view. Column 0 always erased the image. I
could live with the other problems but what's the good of a underlay image when
column 0 wipes it out? I checked to see if Stefan had found a solution for this
one, but he hadn't.

Custom Draw
-----------

Another possibility would be to tap into the custom draw facility that the
Windows list view control offers. This is not the same as *OwnerDraw* in .NET,
though they are related.

Michael Dunn wrote `a great introduction to custom drawing`__ for CodeProject_, which is
required reading for anyone wanting to understand or use custom drawing.

.. __: http://www.codeproject.com/KB/list/lvcustomdraw.aspx

.. _CodeProject: http://www.codeproject.com/

The basic structure of custom drawing is (or at least should be)::

    -> WmPaint
        NmCustomDraw: PreErase
        NmCustomDraw: PostErase
        NmCustomDraw: PrePaint
            [For each visible item]
            NmCustomDraw: ItemPreErase
            NmCustomDraw: ItemPostErase
            NmCustomDraw: ItemPrePaint
            NmCustomDraw: ItemPostPaint
        NmCustomDraw: PostPaint
    <- WmPaint

In actuality, none of the Erase notifications are actually sent (`see here`__). This
isn't a problem for me, but it's something to be aware of in any case.

.. __: http://www.tech-archive.net/Archive/VC/microsoft.public.vc.mfc/2006-08/msg00220.html

So, if we intercept the PostPaint stage, we should be able to draw our image
over the top of the already painted control. This is slightly different from the
other two solutions: there, the image is drawn first and then the control
painted over the top; but here, the control is painted first and the image
painted on top. As long as the image is drawn in a translucent/transparent
manner, the contents of the ListView would still be legible.

So the basic structure of *HandleCustomPaint* method will be like this::

    unsafe protected void HandleCustomDraw(ref Message m) {
        const int CDDS_PREPAINT = 1;
        const int CDDS_POSTPAINT = 2;
        const int CDDS_PREERASE = 3;
        const int CDDS_POSTERASE = 4;
        const int CDDS_ITEM = 0x00010000;
        const int CDDS_ITEMPREPAINT = (CDDS_ITEM | CDDS_PREPAINT);
        const int CDDS_ITEMPOSTPAINT = (CDDS_ITEM | CDDS_POSTPAINT);
        const int CDDS_ITEMPREERASE = (CDDS_ITEM | CDDS_PREERASE);
        const int CDDS_ITEMPOSTERASE = (CDDS_ITEM | CDDS_POSTERASE);

        const int CDRF_NOTIFYPOSTPAINT = 0x10;
        const int CDRF_NOTIFYPOSTERASE = 0x40;

        NativeMethods.NMLVCUSTOMDRAW* lParam = (NativeMethods.NMLVCUSTOMDRAW*)m.LParam;

        switch (lParam->nmcd.dwDrawStage) {
            case CDDS_PREPAINT:
                base.WndProc(ref m);
                // Make sure that we get postpaint notifications
                m.Result = (IntPtr)((int)m.Result | CDRF_NOTIFYPOSTPAINT | CDRF_NOTIFYPOSTERASE);
                break;
            case CDDS_POSTPAINT:
                base.WndProc(ref m);
                break;
            case CDDS_ITEMPREPAINT:
                base.WndProc(ref m);
                m.Result = (IntPtr)((int)m.Result | CDRF_NOTIFYPOSTPAINT | CDRF_NOTIFYPOSTERASE);
                break;
            case CDDS_ITEMPOSTPAINT:
                base.WndProc(ref m);
                break;
            // We could listen for the erase events too, but they are never sent
        }
    }

The only interesting thing here is that in the PREPAINT stages, we have to
specifically tell Windows that we also want to receive post paint and post erase
messages.

In our case, we are interested in the post paint stage. We want to draw our
image overlay after everything else has been painted::

    case CDDS_POSTPAINT:
        base.WndProc(ref m);
        using (Graphics g = Graphics.FromHdc(lParam->nmcd.hdc)) {
            this.DrawOverlay(g);
        }
        break;

In the post paint stage, we cannot use a normal *CreateGraphic()* method. That
would make a new DC, and we need to get the DC that the listview is using
internally to double buffer the control. We can get a handle to that DC via the
custom draw notification member, *hdc*. We create a `Graphics` from that handle using
the *Graphic.FromHdc()* method. Now we can draw our overlay directly into the
listview control's own double buffered DC.

The *DrawOverlay* does the work of translucently drawing the image::

    private void DrawOverlay(Graphics g) {
        if (this.OverlayImage == null)
            return;

        Point pt = this.CalculateAlignedLocation(this.ClientRectangle, this.OverlayImage.Size);

        ImageAttributes imageAttributes = new ImageAttributes();
        if (this.OverlayTransparency != 255) {
            float a = (float)this.OverlayTransparency / 255.0f;
            float[][] colorMatrixElements = {
                new float[] {1,  0,  0,  0, 0},
                new float[] {0,  1,  0,  0, 0},
                new float[] {0,  0,  1,  0, 0},
                new float[] {0,  0,  0,  a, 0},
                new float[] {0,  0,  0,  0, 1}};

            imageAttributes.SetColorMatrix(new ColorMatrix(colorMatrixElements));
        }

        g.DrawImage(this.OverlayImage,
           new Rectangle(pt, this.OverlayImage.Size),
           0, 0, image.Size.Width, this.OverlayImage.Size.Height,
           GraphicsUnit.Pixel,
           imageAttributes);
    }

After a few more tweaks and false starts, I could successful paint the image
translucently over the list view. It all work perfectly and hubris reigned
supreme... until I scrolled the listview.

Scrolling does a bitblt of the scrolled region of the listview, and then redraws
the little bit that is revealed by the scrolling. This is excellent for
appearances, but dreadful for me. The bitblt moves the image as well, and then
redraws part of the image. So we ended up with two images.

.. image:: images/blog-badscroll.png

This was less than ideal.

There is no way to tell the bitblt not to scroll the image. The image is part of
the DC for the listview. The only way to prevent the image from scrolling is to
remove it from the listview, do the scrolling, and then draw it again. This
works but flickers annoyingly. This was far from my "works flawlessly" goal.

Now what?

Transparent Form
----------------

The fourth attempted solution was to make use of the Layered Windows API, which
.NET exposes through the *Opacity* and *TransparencyKey* properties of `Form`.

The idea there would be to place a completely transparent form over the top of
`ListView`, and then draw onto that form (Mathieu Jacques did the same thing with his
LoadingCurtain_ idea). From the user's point of view, the image appeared to be draw
onto the `ObjectListView`, but from the `ObjectListView` point of view, the image was not
there, so only the contents of the control itself was scrolled.

.. _LoadingCurtain: http://www.codeproject.com/KB/cs/LoadingCurtain.aspx

The *GlassPanelForm* implemented this transparent overlay::

    public partial class GlassPanelForm : Form
    {
        public GlassPanelForm() {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
            FormBorderStyle = FormBorderStyle.None;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.Manual;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            this.Opacity = 0.5f;
            this.BackColor = Color.FromArgb(255, 254, 254, 254);
            this.TransparencyKey = this.BackColor;
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
                return cp;
            }
        }
        ...
    }

We set the *TransparencyKey* to be the *BackgroundColor* so that any pixels that
isn't painted is treated as transparent. The *BackgroundColor* should be a colour
that isn't common since any pixel painted with that colour will be treated as
transparent. Since we are going to be drawing translucently, the background
colour should also be a value near white, since all drawing operations are going
to be alpha combined with the background before being painted.

We also override the *CreateParams* property, so that the Form is created with
the `WX_EX_TRANSPARENT` style. This has nothing to do with visible transparency --
this means that the window should not be considered as a target for
mouse actions. This is what we want since we are putting this form over the top
of our list view, but we still want our list view to receive mouse events.

Once this panel is created and correctly positioned, it can now call the
*DrawOverlay(Graphics g)* method from within its *OnPaint()* method, which now draw
the image overlays.

.. image:: images/blog-overlayimage.png

Finally, success! But, sadly, no!

Houston, we (still) have a problem
----------------------------------

The `GlassPanelForm` is a separate top-level window. It sits in front of our
listview, but in completely separate window. If the listview is hidden, we have
to make sure that the `GlassPanelForm` is hidden too, otherwise we will see the
image overlays even when the listview is no longer there.

When the listview is specifically hidden, we can catch the `VisibleChanged` event.
But if the listview is on a tab control, and that tab control changes visible
tab, the listview doesn't receive any notification. The tab control works by
changing the z-order of the children windows. All the controls on a Tab control
are technically still visible, but the z-ordering ensures that only the controls
on the top most tab are visible.

For my case, this was a tricky dilemma. How can I know if the overlay
should be visible? When a control is made invisible by its owning tab being sent
to the back, the control does not receive any notification. The control doesn't
know that it is no longer visible.

I could do complicated things look for tab controls in the parent window chain,
but they would all be clunky and error prone. What happens if someone uses
something like tab control, but that isn't a TabControl (almost all commercial
WinForm control libraries would qualify).

Producing a hybrid
------------------

Maybe I could combine the CustomDrawing solution for the majority of cases, and
use the Transparent form just while the control is scrolling. That is, normally,
the overlays would be drawn during the Custom draw cycle, but just before scrolling,
we would switch to using the transparent form. When the scrolling finished, we
would revert to the custom drawing.

This should give the best of all worlds. But the devil is always in the details.

With this hybrid, the problem was with the transitions: removing the custom drawn
overlays and showing the transparent form. Since the two controls are on two
separate forms, they cannot be both updated atomically. There is a slight delay
between when the `ObjectListView` is redrawn and when the transparent form is redrawn.

If we remove the custom drawn overlays before showing the transparent form, there is a
brief flicker when there are no overlays. If we show the transparent form before removing
the custom drawn overlays, there is a brief flicker where two alpha-channeled images
are drawn over the top of each other, making it look completely solid.

At first, I didn't think the flicker of the second case was too bad. But the flicker
happens every time the list was scrolled, mostly annoyingly when the mouse wheel
rolled. After a couple of days, I was thoroughly sick of the flickering and ready
to throw the whole thing out. There had to be another way.

Conclusion
----------

There wasn't another way. So I eventually decided to ditch the hybrid and return
to the transparent form. In 90% of cases, it will works exactly as the programmer
expects. By listening to various events, we can catch and handle almost all of
the problem cases mentioned above, including when an `ObjectListView` is reparented.

The only thing we can't handle automatically is when an `ObjectListView` is
contained in a non-standard TabControl-like container. When an `ObjectListView`
is included in a TabControl-like container, the programmer must call *HideOverlays()*
explicitly when an ObjectListView is hidden by the container.

You will know when you need to call *HideOverlays()* because the overlays from
`ObjectListView` that are not currently visible, will be shown over the top
of your TabControl-like container.