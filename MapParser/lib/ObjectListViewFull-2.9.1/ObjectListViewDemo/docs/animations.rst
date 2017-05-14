.. -*- coding: UTF-8 -*-

:Subtitle: Pretty distractions

.. _animations-label:

Sparkle Animations
==================

In v2.4, ObjectListView integrated with the Sparkle_ animation library to
allow animations to be drawn onto its controls.

.. _Sparkle: http://www.codeproject.com/KB/gdiplus/sparkle.aspx

.. raw:: html

  <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="599" height="378" id="FlashID">
    <param name="movie" value="_static/objectListView-animation.swf">
    <param name="quality" value="high">
    <param name="wmode" value="opaque">
    <param name="swfversion" value="6.0.65.0">
    <!-- Next object tag is for non-IE browsers. So hide it from IE using IECC. -->
    <!--[if !IE]>-->
    <object data="_static/objectListView-animation.swf" type="application/x-shockwave-flash" width="599" height="378">
      <!--<![endif]-->
      <param name="quality" value="high">
      <param name="wmode" value="opaque">
      <param name="swfversion" value="6.0.65.0">
      <!-- The browser displays the following alternative content for users with Flash Player 6.0 and older. -->
      <div>
        <h4>Content on this page requires a newer version of Adobe Flash Player.</h4>
        <p><a href="http://www.adobe.com/go/getflashplayer"><img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" width="112" height="33" /></a></p>
      </div>
      <!--[if !IE]>-->
    </object>
    <!--<![endif]-->
  </object>

Google Envier's Anonymous
-------------------------

"I'd like to welcome you all to this weeks meeting of Google Enviers'
Anonymous. Hi. My name is Phillip and I'm a Google envier. I've
managed to control my condition for almost six days. But this morning I
fell off the wagon in a big way. I was working normally and then I saw
it! You all know what happens next. My breathing was suddenly rapid and
shallow. I reached for my paper bag to keep calm. But it was too late.
Waves of jealousy swept over me. In desperation, I speed-dialed my
Google Envier buddies, Steve and Bill, and they talked me through it.
After a minute or so, the spasm passed and I returned to (more or less)
normal, though the bitter taste of envy lingered for the rest of the
day.

In my self defense, the source of my envy is nothing as crass as their
billion dollar development budget, their wonderful working conditions,
or their thousand plus dollar share price. All these things fall into
insignificance in comparison with the true object of my obsession: their
animations! The way they effortlessly add little spinning stars, glowing
text or fading sparkles to their applications. My applications sit there
fully functional and obedient -- but static and passive, lacking the
moving eye candy that make Google apps cute and cool.

But no more! To you, fellow enviers, I present the Sparkle animation
framework. With this framework, you too can put animations into your
applications and free yourself from the shackles of Google envy."


Understanding Sparkle in 30 seconds or less
-------------------------------------------

OK. A little more seriously this time. The Sparkle library's purpose is
to allow (almost) any Control to show animations.

The design goals of the Sparkle library are:

* Short-lived animations on any control. The Sparkle library is designed
  to draw short animations over the top of existing Controls.

* Declarative. The Sparkle library is declarative. You say what you want
  the animation to do, and then you run the animation. The animation is
  completely defined before it begins.

* Non-interactive. The Sparkle library does not do user interaction --
  it does not listen to mouse moves, clicks or drags. It doesn't do
  collision detection or physics models. It just draws eye candy.

To use the library itself, you'll need to grasp its four major concepts:

* Animations. An animation is the canvas upon which sprite are placed.
  It is the white board upon which things are drawn.

* Sprites. Sprites are things that can be drawn. There are several
  flavours of sprites -- one for images, another for text, still another
  for shapes. It is normal to make your own types of sprites by
  subclassing Sprite (or implementing ISprite).

* Effects. Effects are things that make changes to sprites over time.
  They are the "movers and shakers" in the library, who actually do
  things. Sprites sit there, completely passive, looking pretty, but the
  effects push them around, change their visibility, spin or size. Again,
  you can use existing Effects, or implement your own through the IEffect
  interface.

* Locators. Locators are things that known how to calculate a point or a
  rectangle. Rather than saying "Put this sprite at (10, 20)," they allow
  you to say "Put this sprite at the bottom right corner of this other
  sprite." This idea can be tricky to get your mind around, but once you
  have grasped it, it is powerful.

How to use it
--------------

Adding any sort of animation to an application is a multi-step process.
With Sparkle, the workflow for creating an animation is:


1. Decide where the animation will appear. That is your `Animation`.

2. Think about what you want to show. They are your `Sprites`.

3. Think about what you want each `Sprite` to do. They are your `Effects`.

4. Whenever an `Effect` needs a "where", that's when you need `Locators`.



Simple Example
--------------

To get a feeling for how to use the library, there's no substitute for
seeing code. So let's put a spinning, fading star in the middle of an
ObjectListView.

To put animations onto an ObjectListView, you use `AnimatedDecoration`
class. Depending on the constructor used, this will create an animation
for the whole list, for a row, or for just one cell::


    AnimatedDecoration listAnimation = new AnimatedDecoration(this.olvSimple);
    AnimatedDecoration rowAnimation = new AnimatedDecoration(this.olvSimple, myModel);
    AnimatedDecoration cellAnimation = new AnimatedDecoration(this.olvSimple, myModel, olvColumnName);

We want an animation that can draw on the whole list so we use this form::

    AnimatedDecoration listAnimation = new AnimatedDecoration(this.olvSimple);
    Animation animation = listAnimation.Animation;

Now we have an `Animation`, we can make our Sprites. We only need one
sprite and we want it to show an image, so we use an ImageSprite. There
is also a TextSprite for drawing bordered/backgrounded text, and
ShapeSprite for drawing regular shapes::

    Sprite image = new ImageSprite(Resource1.largestar);

We have our sprite. Now we want it to do something. Whenever we want a
sprite to do something, we need an Effect. For this example, we want the
image to spin in the centre of the list, and to fade out while it does
so. We add the Effects to the Sprite, saying when the effect should
start and how long it will last::

    image.Add(0, 2000, Effects.Rotate(0, 360 * 2.0f));
    image.Add(1000, 1000, Effects.Fade(1.0f, 0.0f));

This says, during the first 2000 milliseconds after the sprite begins in
the animation, the image should spin completely twice. The second
statement says that 1000 milliseconds after the sprite begins, the
sprite should take 1000 milliseconds to gradually fade completely from
sight.

Most Sprites have some sort of MoveEffect given to them to move them
around. However, we want this sprite to just stay in the centre of the
list, so we give it a FixedLocation::

    image.FixedLocation = Locators.SpriteAligned(Corner.MiddleCenter);

This says the images MiddleCenter will always be aligned to the
MiddleCenter of the animation (which in this case is on the whole of the
list).

The final steps are to add the Sprite to the Animation::

    animation.Add(0, image);

This says to begin running the image sprite 0 milliseconds after the
animation starts (i.e. immediately). Often the sprites would not start
until some time after the animation begins, but in this case, there's
only one sprite so it may as well start immediately.

The animation is now fully configured and all that remains is to run it::

    animation.Start();

All being well, this should produce an animation on the ObjectListView
that looks something like this:


.. raw:: html

  <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="597" height="336" id="FlashID2">
    <param name="movie" value="_static/objectListView-simple-animation.swf">
    <param name="quality" value="high">
    <param name="wmode" value="opaque">
    <param name="swfversion" value="6.0.65.0">
    <!-- Next object tag is for non-IE browsers. So hide it from IE using IECC. -->
    <!--[if !IE]>-->
    <object data="_static/objectListView-simple-animation.swf" type="application/x-shockwave-flash" width="597" height="336">
      <!--<![endif]-->
      <param name="quality" value="high">
      <param name="wmode" value="opaque">
      <param name="swfversion" value="6.0.65.0">
      <!-- The browser displays the following alternative content for users with Flash Player 6.0 and older. -->
      <div>
        <h4>Content on this page requires a newer version of Adobe Flash Player.</h4>
        <p><a href="http://www.adobe.com/go/getflashplayer"><img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" width="112" height="33" /></a></p>
      </div>
      <!--[if !IE]>-->
    </object>
    <!--<![endif]-->
  </object>
  
In eight lines of code, you've put a spinning, fading star (a la Picasa) onto
your otherwise static `ListView.`


Animations
----------

An animation has two distinct functions.

1.  It implements a timer tick based animation system. At each tick of a
    clock, it advances the state of the animation: it decides which sprites
    should become active/inactive, gives effects the opportunity to do their
    magic. This portion does not perform any rendering -- it simply changes
    the state of parts of the animation. If anything needs to be rendered,
    the animation triggers a Redraw event.

2.  It draws the sprites according to their current state. Normally,
    something would listen for the Redraw event on an Animation, and in
    response to that event, it would redraw the animation. To do that, it
    calls the Animation.Draw(Graphics g) method. This will render the
    animation, in its current state, onto the given Graphics object. This
    operation does not change the state of the Animation.

In addition to animating sprites and rendering them, an animation
supports the basic set of commands to control its execution:

* Start()

* Pause()/Unpause()

* Stop()

Repeat behaviour
^^^^^^^^^^^^^^^^

Animations have a Repeat property, which controls the animation's
behaviour when it reaches the end of the animation.

* Repeat.None - The animation simply quits. All sprites disappear. This is
  the default.

* Repeat.Pause - The animation pauses. All sprites that were visible at
  the end of the animation remain visible and motionless.

* Repeat.Loop - The animation begins again.


Sprites
-------

Sprite are the actual eye candy -- the pretty do-nothing things that the
user can see. They keep whatever state information they require --
location, size, color, transparency -- and then use that state
information to draw themselves when asked. They don't change their own
state -- that's the responsibility of Effects.

There are several flavours of sprites that come with the Sparkle
library:

* ImageSprite

This takes an Image and draws it according to the sprites
state. If the given Image is a frame animation itself, the Sparkle
framework will animate that image automatically. I think it is only
animated GIFs that Microsoft supports as frame animations.

* TextSprite. 

TextSprites draw text (no prizes). But they can do a bit
more formatting than just that. The text can be colored (ForeColor
property), they can be drawn with a background (BackColor property).
They can draw a border around the text (BorderWidth and BorderColor
properties). The border can be either a rectangle (set CornerRounding to
0) or a round cornered rectangle (set CornerRounding to greater than 0
-- 16 is normally nice).

* ShapeSprites. 

These draw regular shapes (square, rectangles, round
cornered rectangle, triangles, ellipses/circles). Like TextSprites,
ShapeSprites can have a ForeColor (color of frame of the shape),
BackColor (used for the filled part of the shape), and PenWidth (width
of the frame).

Remember, all colors can have alpha values set for them, which will
allow varying levels of transparency when drawing the sprites. 

Effects
-------

Effects are the movers and shakers of the Sparkle library. They push
Sprites around, moving them here or there, making them visible or
invisible, spinning them around. Any time you want a Sprite to change,
you need an Effect.

Effects are given to a Sprite, and told when they should start and how
long they will run for::

    this.imageSprite.Add(100, 250, new FadeEffect(0.0f, 0.8f));

This says, "100 milliseconds after imageSprite starts in the animation,
this FadeEffect should, during 250 milliseconds, fade the sprite from
hidden (0.0 opacity) to 80% visible (0.8 opacity)."

Many Effects work by "tweening" - they are given an initial value and a
target value, and as the effect progresses, the effect gradually change
a property on their Sprite from the initial value to the end value. In
the above example, the FadeEffect's initial value is 0.0 and its end
value is 0.8. As the animation progresses, the FadeEffect would
gradually change the Opacity property of its Sprite from 0.0 to 0.8. So,
100 milliseconds after the sprite starts, the imageSprite will be
hidden; after 225 milliseconds, it will be 40% visible; after 350
milliseconds, it will be 80% visible, and then the effect will stop.

Effects factory
^^^^^^^^^^^^^^^

Effects factory contains static methods to create many commonly used
effects.

* Move(Corner to)

Move the sprite from it's current location to a corner of the animation.

* Move(Corner from, Corner to)

Move the sprite from one corner of the animation to another. This has a
zillion variations which allow different ways of saying where to start
and where to end.

* Goto(Corner to)

Go to (as in Monopoly) the given corner without any transition.

* Fade(float from, float to)

Change the Opacity of the sprite from the start to the end value,
effectively fading it in or out.

* Rotate(float from, float to)

Change the Spin of the sprite from the start to the end value (both in
degrees).

* Scale(float from, float to)

Change the Scale of the sprite, effectively making it bigger or smaller.

* Bounds(IRectangleLocator locator)

Change the Bounds of the sprite.

* Walk(IRectangleLocator locator)

This is the first interesting effect. This changes the location of the
sprite so that it "walks" around the perimeter of the given rectangle.
This has several flavours saying which exact point of the sprite will be
walked, which direction the walk should take, and where the walking
should start.

* Blink(int repetitions)

Another interesting effect. This changes the Opacity of the sprite so
that it blinks a number of times. There are a couple of variations that
allow the characteristics of the "blink" to be changed: how long it
takes to fade in, stay visible, fade out, stay invisible.

* Repeater(int repetitions, IEffect effect)

This applies the given Effect several times to the Sprite.

Locators
--------

In some ways, locators are the most difficult concept to grasp. If you
can get this concept, everything else normally falls into place.

A Locator is a point or a rectangle that can calculate itself whenever
needed. A plain Point is fixed, but a PointLocator can be different
every time it is called. By using a Locator, "how" a point is calculate
can be replaced at runtime to use any strategy it likes.

For example, the MoveEffect changes the Location of a Sprite. It could
be coded to move a Sprite to the TopLeft of an Animation::

    this.Sprite.Location = this.Animation.Bounds.Location;

This is nice and obvious solution, but not very flexible. If we then
wanted to move the sprite to the centre of the Animation, we'd have to
write a separate line of code, and then give some way to choose which
line to execute. And another line of code for ever other possible
location we could want.

But with Locators, the MoveEffect simply says::

    this.Sprite.Location = this.Locator.GetPoint();

By using this extra layer of abstraction, the intelligence of
calculating the "where" is placed into a separate object, and becomes
reusable from there.


Standard locators
^^^^^^^^^^^^^^^^^

`Locators` is a factory that has static methods to produce many common
locators. You can of course create the locators directly -- these are
just a convenience.

* IPointLocator At(int, int)

Create a PointLocator for a fixed point.

* IPointLocator SpriteAligned(Corner corner)

Create a PointLocator which is where a Sprite must be moved to so that
the given Corner is located at the corresponding corner of the
Animation. So, Locators.SpriteAligned(Corner.BottomRight) calculates
where a sprite must be moved to so that its BottomRight corner is at the
BottomRight corner of the Animation.

* IPointLocator SpriteAligned(Corner corner, Point offset)

Same as above, but the point is offset by the given fixed amount.

* IPointLocator SpriteAligned(Corner corner, float proportionX, float proportionY)

Create a PointLocator which is where a Sprite must be moved to so that
the given Corner is located at a point proportional across and down the
bounds the Animation. So, Locators.SpriteAligned(Corner.BottomRight,
0.6f, 07.7) calculates where a sprite must be moved to so that its
BottomRight corner is 60% across the Animation and 70% down.

* IPointLocator SpriteBoundsPoint(Corner corner)

Create a PointLocator which calculates the given corner of the sprite's
bounds.

* IPointLocator SpriteBoundsPoint(float proportionX, float proportionY)

Create a PointLocator which calculates a given proportion across and
down the sprite's bounds.

* IRectangleLocator At(int, int, int, int)

Create a RectangleLocator for a fixed rectangle.

* IRectangleLocator AnimationBounds()

Create a RectangleLocator for the bounds of the animation.

* IRectangleLocator AnimationBounds(int x, int y)

Create a RectangleLocator for the bounds of the animation inset by the
given amount

* IRectangleLocator SpriteBounds()

Create a RectangleLocator for the bounds of the sprite.

* IRectangleLocator SpriteBounds(int x, int y)

Create a RectangleLocator for the bounds of the sprite inset by the
given amount


Performance
-----------

The Sparkle library performs fairly well when used in accordance with
its design goals. Animating dozens of sprites with dozens of effects has
a minimal impact on performance. On my laptop, 20 or so sprites with a
variety of effects uses only about 2-3% of the CPU. The limiting factor
is not the animation but the redrawing of the underlying control.
Currently, the whole control is redrawn every frame. For simple
controls, like Buttons or UserControls, this is not a problem, but for
complicated control, like DataGridView, this redrawing quickly becomes
taxing.

Later versions might be optimized by invalidating only the smallest
possible area of the control.

I haven't tried using Sparkle with thousands of sprites. That really wasn't its purpose.

Status and stability
--------------------

Sparkle is a new library. It has worked well for me, but I'm sure there 
are bugs in it. Please report them and I will fix them. 

The interfaces and major classes are stable, but not yet fixed 
(unchangeable). It's possible that I will add a few more properties to 
the ISprite interface (I think it needs Skew). 

