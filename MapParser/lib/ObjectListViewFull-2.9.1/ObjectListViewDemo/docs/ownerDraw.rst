.. -*- coding: UTF-8 -*-

:Subtitle: Owner drawn and quartered

.. _owner-draw-label:

Owner drawing a ListView
========================

By itself, a ListView can draw a piece of text with an optional image (details
view has more than one piece of text). For the majority of cases, this is more
than enough. But once in a while, there's a situation where a picture is worth
more than one thousand hours of development. Hopefully, with an ObjectListView,
it will take much less time than that.

With an ObjectListView, owner drawing can be accomplished in two ways:

1) By installing a delegate.

2) By implementing the `IRenderer` interface

Installing a delegate
---------------------

To install a delegate, you set the `RendererDelegate` property on a column, and
then, inside the renderer delegate, you can draw whatever you like::

    this.occupationColumn.RendererDelegate = delegate(EventArgs e,
    		  Graphics g, Rectangle r, Object rowObject) {
        using (LinearGradientBrush gradient =
            new LinearGradientBrush(r, Color.Gold, Color.Fuchsia, 0.0)) {
            g.FillRectangle(gradient, r);
        }
        StringFormat fmt = new StringFormat(StringFormatFlags.NoWrap);
        fmt.LineAlignment = StringAlignment.Center;
        fmt.Trimming = StringTrimming.EllipsisCharacter;
        fmt.Alignment = StringAlignment.Near;
        g.DrawString(((Person)rowObject).Occupation, listViewComplex.Font, Brushes.Black, r, fmt);
        return false;
    };

This delegate produces this spectacularly garish result:

.. image:: images/ownerdrawn-example1.png

Implementing IRenderer
----------------------

All rendereres implement the `IRenderer` interface (even those which use the
delegate mechanism). This interface looks like this::

    public interface IRenderer
    {
        /// Render the whole item within an ObjectListView.
        bool RenderItem(DrawListViewItemEventArgs e, Graphics g, Rectangle itemBounds, Object rowObject);

        /// Render one cell within an ObjectListView when it is in Details mode.
        bool RenderSubItem(DrawListViewSubItemEventArgs e, Graphics g, Rectangle cellBounds, Object rowObject);

        /// What is under the given point?
        void HitTest(OlvListViewHitTestInfo hti, int x, int y);

        /// When the value in the given cell is to be edited, where should the edit rectangle be placed?
        Rectangle GetTextEditRectangle(Graphics g, Rectangle cellBounds, OLVColumn column, Object rowObject);
    }

There is an `AbstractRenderer` which is a safe, do-nothing implementation of this interface. The very useful `BaseRenderer` (see below) is a
subclass of `AbstractRenderer`.

Suppose that you have an abiding love of garish gradients, and you really want
to be able to inflict them easily and repeatedly. You could implement them like this::

    public class GradientRenderer : IRenderer
    {
        public virtual bool RenderItem(DrawListViewItemEventArgs e, Graphics g, Rectangle itemBounds, object rowObject) {
            return true;
        }

        public virtual bool RenderSubItem(DrawListViewSubItemEventArgs e, Graphics g, Rectangle cellBounds, object rowObject) {
            using (LinearGradientBrush gradient = new LinearGradientBrush(r, Color.Gold, Color.Fuchsia, 0.0)) {
                g.FillRectangle(gradient, cellBounds);
            }
            StringFormat fmt = new StringFormat(StringFormatFlags.NoWrap);
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Trimming = StringTrimming.EllipsisCharacter;
            fmt.Alignment = StringAlignment.Near;
            g.DrawString(this.GetText(), e.ListView.Font, Brushes.Black, cellBounds, fmt);
            return false;
        }

        public virtual void HitTest(OlvListViewHitTestInfo hti, int x, int y) {
        }

        public virtual Rectangle GetEditRectangle(Graphics g, Rectangle cellBounds, OLVListItem item, int subItemIndex) {
            return cellBounds;
        }
    }

[Doing this really makes no sense -- it would be much better to subclass `BaseRenderer` -- but this is just
to give an example]

To actually use your custom renderer (or one of the standard ones), you
assign an instance of them to a column's `Renderer` property, like this::

    this.occupationColumn.Renderer = new GradientRenderer();

BaseRenderer - A renderer's utility belt
----------------------------------------

Installing a delegate or using the raw `IRenderer` works fine, but there are numerous utility methods that would be useful within a renderer:

* What `ObjectListView` are we are drawing within?
* What font should be used for the text?
* Is the row currently selected?
* What colour should the background be? What colour should the text be?
* What value is being rendered?

The `BaseRenderer` class encapsulates these utilities. You can use this class
directly, use one of the existing subclasses (see below) or roll your own
subclass to do whatever you want.

Continuing with our love of garish gradients, it's better to subclass `BaseRenderer` than to
implement `IRenderer` directly. With a
`BaseRenderer`, you only need to override the `Render(Graphics g, Rectangle r)` method::

    public class GradientRenderer : BaseRenderer
    {
        public override void Render(Graphics g, Rectangle r) {
            using (LinearGradientBrush gradient = new LinearGradientBrush(r, Color.Gold, Color.Fuchsia, 0.0)) {
                g.FillRectangle(gradient, r);
            }
            StringFormat fmt = new StringFormat(StringFormatFlags.NoWrap);
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Trimming = StringTrimming.EllipsisCharacter;
            switch (this.Column.TextAlign) {
                case HorizontalAlignment.Center: fmt.Alignment = StringAlignment.Center; break;
                case HorizontalAlignment.Left: fmt.Alignment = StringAlignment.Near; break;
                case HorizontalAlignment.Right: fmt.Alignment = StringAlignment.Far; break;
            }
            g.DrawString(this.GetText(), this.Font, this.TextBrush, r, fmt);
        }
    }

With this renderer, you can now render any column on any `ObjectListView` with
your colourful background. By using a `StringFormat`, this renderer will
automatically align the text according to the alignment of the column, truncate
too long strings, and center the text vertically when necessary.

Obviously, if this was a real class, you would replace `Color.Gold`,
`Color.Fuchsia`, and `0.0` with properties like `StartColor`, `EndColor`, and
`GradientAngle` respectively, but you get the idea.


What flavours does it come in?
------------------------------

There are a couple of flavours of `BaseRenderer` already available for use.

BarRenderer
^^^^^^^^^^^

.. image:: images/bar-renderer.png
    :align: left
    :class: left-padded

This is a simple-minded horizontal bar. The row's data value is used to
proportionally fill a "progress bar." The manner of drawing the progress bar is
customisable. This example shows using the default system theme renderer.

A `BarRenderer` can be initialised in a couple of different ways. The following
code creates a renderer that will draw a progress bar in the range 0-2 (used for
a person's height in metres) with a standard themed bar::

    this.olvHeight.Renderer = new BarRenderer(0, 2);

And this code creates a renderer that will draw the same data values as above,
but will do so with a hideous horizontal gradient, framed in black::

    this.olvHeight.Renderer = new BarRenderer(0, 2, Pens.Black, Brushes.Gold, Brushes.Blue);

MultiImageRenderer
^^^^^^^^^^^^^^^^^^

.. image:: images/multiimage-renderer.png
    :align: left
    :class: left-padded

This renderer draws 0 or more of the same image based on the row's data value.
The 5-star "My Rating" column on iTunes is an example of this type of renderer.

Initialising one of these renderers is a little more complicated. It needs to
know that image that should be drawn, the minimum and maximum values to be
considered, and the maximum number of images that the renderer should draw. If
the value to be drawn is less than the minimum, no images will be drawn. If the
value is greater than the maximum, then the maximum number of images will be
drawn. For values between the minimum and maximum, a proportionally number of
images will be drawn::

    this.olvCookingSkill.Renderer = new MultiImageRenderer(Resource1.star16, 5, 0, 40);

This says that when drawing the cooking skill column, 0 or more copies of the
star16 image will be drawn. At most, 5 images will be drawn. If the person's
cooking skill is 0 or less, no images will be drawn. If their skill is 40 or
greater, 5 stars will be drawn. Otherwise, 1 + skill / ((maximum - minimum) /
maxNumberImages) images will be drawn.

One gotcha: If you have some data whose value can be 0 to 5, and you want to
draw that many stars, the maximum value has to be 6, not 5. Counter intuitive but true.

MappedImageRenderer
^^^^^^^^^^^^^^^^^^^

.. image:: images/mappedimage-renderer.png
    :align: left
    :class: left-padded

This renderer draws an image decided from the row's data value. Each data value
has its own image. A simple example would be a `bool` renderer that draws a
tick for true and a cross for false. This renderer also works well for enums or
domain-specific codes.

This type of renderer is initialised to map a particular value to the image that should drawn for the value::

    this.olvRank.Renderer = new MappedImageRenderer(new Object[] {
        Rank.Private, Resource1.PrivateInsignia,
        Rank.Corporal, Resource1.CorporalInsignia,
        Rank.Sergeant, Resource1.SergeantInsignia,
    });

This renderer can also render a collection of values. If the `Aspect` returns a
collection, rather than just a simple value, then each item in that collection
will be drawn side by side.

ImageRenderer
^^^^^^^^^^^^^

.. image:: images/image-renderer.png
    :align: left
    :class: left-padded

This renderer tries to interpret its row's data value as an image or a collection
of images. Most
typically, if you have stored `Images` in your database, you would use this
renderer to draw the images from the database. It also works with paths that
lead to local image files.

As the result of a burst of misguided enthusiasm, this renderer will also draw
animated graphics. Of what possible use that would ever be in a real world
application, I'm not sure, but it's comforting to know that the option is always
there should you choose to use it. The code works on GIFs and may well work on
other frame-based animation formats, but I have not had the opportunity (or
inclination) to try.

This is initialised very simply::

    this.olvImage.Renderer = new ImageRenderer();

If the `Aspect` is a collection of images, they will each be drawn horizontally
one after the other, seperated by `Spacing` pixels.

ImagesRenderer
^^^^^^^^^^^^^^

.. image:: images/images-renderer.png
    :align: left
    :class: left-padded

This renderer draws zero or more images horizontally. The `Aspect` for this
renderer must return a collection of imageSelectors, where an imageSelector can
be an integer, a string, or an Image object. The integer and string will be used
as indexes into the SmallImageList, while the Image will be rendered directly.

This too is initialised very simply::

    this.olvTellsJokes.Renderer = new ImagesRenderer();

NOTE: This functionality has been assumed by the plain `ImageRenderer` so this class
is now defunct. It will continue to work.

FlagRenderer
^^^^^^^^^^^^

.. image:: images/flags-renderer.png
    :align: left
    :class: left-padded

A `FlagRenderer` is similar to an `ImagesRenderer` in that it draws zero or more
images horizontally. It differs in how it decides which images to show. A
FlagRenderer expects that it's `Aspect` will be a collection of flags, that is, a
bitwise OR'ed collection of exclusive values. When the `FlagRenderer` is created,
it is initialised to say, "When you see this bit set, draw this image".

So for this example, a `FlagRenderer` was created to render `FileAttributes` values.
The renderer was then configured so that when it saw a `FileAttributes.Archive`
flag, it draw the "archive" image from the SmallImageList. Similarly for the
`FileAttributes.ReadOnly` flag and the other flags::

    FlagRenderer attributesRenderer = new FlagRenderer();
    attributesRenderer.Add(FileAttributes.Archive, "archive");
    attributesRenderer.Add(FileAttributes.ReadOnly, "readonly");
    attributesRenderer.Add(FileAttributes.System, "system");
    attributesRenderer.Add(FileAttributes.Hidden, "hidden");
    this.olvColumnAttributes.Renderer = attributesRenderer;

The images are drawn in the order in which the flag/image pairs are added to the renderer. If `FileAttributes.System` was added first, the "system" image would be drawn first.


Owner drawing in non-Details view
---------------------------------

If you are keen, you can also owner draw the other views too, the non-Details views.

To do this, you install an `ItemRenderer` on your ObjectListView, and do your
rendering like normal. The only slight difference is that your renderer will
have to check which view the ObjectListView is currently using before doing your
rendering. Suppose you only want to inflict your gradients on your users when the
ObjectListView was in Tile view. You would change your above renderer to be
something like this::

    public class TileViewGradientRenderer : BaseRenderer
    {
        public override bool OptionalRender(Graphics g, Rectangle r)
        {
            if (this.ListView.View != View.Tile)
                return false;

            using (LinearGradientBrush gradient = new LinearGradientBrush(r, Color.Gold, Color.Fuchsia, 0.0)) {
                g.FillRectangle(gradient, r);
            }
            g.DrawRectangle(Pens.Black, r);

            StringFormat fmt = new StringFormat(StringFormatFlags.NoWrap);
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Trimming = StringTrimming.EllipsisCharacter;
            fmt.Alignment = StringAlignment.Near;
            g.DrawString(this.GetText(), this.Font, this.TextBrush, r, fmt);

            return true;
        }
    }

Notice that here we've overridden a different base method: `OptionalRender()`.
This method allows the renderer to decide if it wants to do the rendering itself
or to fall back on the default rendering. Returning true says that the rendering
has been done and no further processing is needed.

To see an extreme example of owner drawing in a non-detail view, go to the
Complex tab of the ObjectListView demo, turn on Owner Drawn and switch to Tile
view. There, you should be able to see something like this (which is probably
taking owner drawing to places it really does not want to go):

.. image:: images/tileview-ownerdrawn.png


And if that's not enough...
---------------------------

Renderers can do more than just render. They can be used for hit detection and for calculating the
location of cell's editor.


Things to Remember About Owner Drawing
--------------------------------------

1. Owner drawing only happens when you turn on the `OwnerDrawn` mode. So, you can
   only see your custom renderer when the `ObjectListView` is in owner-drawn mode. [I
   spent one very frustrating hour during the development of this code because I
   forgot about this].

2. Rows in an `ObjectListView` are always of fixed height. Row height can now be
   set for the whole `ObjectListView` using the `RowHeight` property, but it cannot be
   changed for individual rows.

3. It is obvious, but easily overlooked, that owner drawing is slower than non-
   owner drawing. The framework has to do a lot more work for owner drawing than it
   does for native drawing. A reasonable amount of optimization has been done to
   minimise this overhead, but it is still there. For small lists, the difference
   is not significant. However, it can be noticeable when a large number of redraws
   is necessary.