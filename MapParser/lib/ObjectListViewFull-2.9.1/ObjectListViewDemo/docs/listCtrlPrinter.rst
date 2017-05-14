.. -*- coding: UTF-8 -*-

:Subtitle: The slothful way to love reports

.. _using-listctrlprinter:

Using a ListCtrlPrinter
=======================

A ListCtrlPrinter takes an ``ObjectListView`` (or plain old ``wx.ListCtrl``) and
effortlessly turns it into a pretty report. With only two lines of code, you can produce a
nice report like this:

.. image:: images/listctrlprinter-example1.png

When would I want to use it?
----------------------------

Your shiny new application is finished to perfection — and it's a week ahead of schedule
(we're imagining here, so let's be completely unrealistic). After your demonstration to
The Management, the CEO says, "It's great! I love it! But I want to have all that
information as a report. And I want to be able to fiddle with the columns in the report,
resize them, any way I want. It has to be sortable, and groupable, just like we can see on
the screen there. And of course, we have to be able to print preview it before we print
it. And can we have it finished by tomorrow?" You consider briefly whether you should
mention the PrintScreen key, but you doubt he would be enthused with that solution.

Management love reports. Programmers aren't so keen. They often appear as an afterthought
to the requirement. Yet producing nice looking reports is not a trivial task. If you using
other languages, you can buy a commercial product (if you have Blizzard's budget), but
with wxPython, there are not that many options. You normally have to write it all
yourself, which can be frustrating. wx's printing scheme can cause even strong
programmers to go weak in the knees -- even with Robin's wonderful book for reference.

For a lazy and vain programmer like myself, what I really want is something that takes no
effort to implement, yet produces wonderful results. I want to be able to go back to my
CEO the same day, show him the nice looking reports that do exactly what he wanted, and
then remind him about my overdue raise.

The ListCtrlPrinter is designed to be just such a solution.

How do I use it in my project?
-------------------------------

As always, the goal is for this to be as easy to use as possible. A typical
usage should be as simple as::

   printer = ListCtrlPrinter(self.myListCtrl, "My Report Title")
   printer.PrintPreview()

This code create a new ``ListCtrlPrinter``, telling it what ``ListCtrl`` it should print
and what the report should be titled. Then a print preview of the report is opened.

A more complete example might look like this::

    printer = ListCtrlPrinter(self.lv, "Graviton Collision Statistics")
    printer.ReportFormat = ReportFormat.Normal("Lucida Bright")
    printer.ReportFormat.IsColumnHeadingsOnEachPage = True

    printer.PageHeader("Monthly Bad Science Report")
    printer.PageFooter("Bright Ideas Software", "%(date)s", "%(currentPage)d of %(totalPages)d")
    printer.Watermark("Work hard!")

    printer.PrintPreview(self)

Primary commands
----------------

The ``ListCtrlPrinter`` has commands that match the normal printing commands, which should
be hooked into the menu structure of your application.

* ``PageSetup()``
    Opens the normal "Page Setup" dialog that allows the user to choose the
    page orientation and margins, as well as choose the printer for output.

* ``PrintPreview()``
    Open a print preview dialog, from which the user can also print the report.

* ``Print()``
    Opens the "Print" dialog that lets the user choose which printer, the pages and
    other details before printing the report.


Working in a structured environment
-----------------------------------

A report consists of several blocks, each of which stacks vertically. The structure of a report
is like this:

.. image:: images/listctrlprinter-structure.png


The PageHeader and PageFooter repeat on each page (obviously). The ColumnHeader
repeats on each page, depending on the ReportFormat options.


Controlling the appearance
--------------------------

The formatting of a report is controlled completely by the ``ReportFormat`` object of the
``ListCtrlPrinter``. To change the appearance of the report, you change the settings in
this object.

These properties control the appearance of the report as a whole:

* ``IncludeImages``
    Should images from the ``ListCtrl`` be included in the report?
* ``IsColumnHeadingsOnEachPage``
    If this is True, the column headers will be repeated at the top of each page.
* ``IsShrinkToFit``
    If this is True, the report will be shrunk so that all the column of the ``ListCtrl`` can fit within
    the width of page.
* ``UseListCtrlTextFormat``
    If this is True, the format (text font and color) of the rows will be taken
    from the ``ListCtrl`` itself, rather than the ``Cell.BlockFormat`` object. Useful if your
    ``ListCtrl`` has fancy formatting on the rows that you want to replicate in the printed
    version.

As was illustrated above, a report consists of various sections (called "blocks"). Each
of these blocks has a matching ``BlockFormat`` object in the ``ReportFormat``. To modify
the appearance of a block, you modify its matching ``BlockFormat`` object. So, to
modify the format of the page header, you change the ``ReportFormat.PageHeader`` object.

A ``ReportFormat`` object has the following properties which control the appearance of the matching
sections of the report:

* PageHeader
* ListHeader
* ColumnHeader
* GroupTitle
* Row
* ListFooter
* PageFooter


These properties return ``BlockFormat`` objects, which have the following properties:

* ``CanWrap``
    If the text for this block cannot fit horizontally, should be wrap to a new line (True)
    or should it be truncated (False)?
* ``Font``
    What font should be used to draw the text of this block
* ``Padding``
    How much padding should be applied to the block before the text or other decorations
    are drawn? This can be a numeric (which will be applied to all sides) or it can be
    a collection of the paddings to be applied to the various sides: (left, top, right, bottom).
* ``TextAlignment``
    How should text be aligned within this block? Can be *wx.ALIGN_LEFT*, *wx.ALIGN_CENTER*, or
    *wx.ALIGN_RIGHT*.
* ``TextColor``
    In what color should be text be drawn?

The blocks that are based on cells (PageHeader, ColumnHeader, Row, PageFooter) can also
have the following properties set:

* ``AlwaysCenter``
    Will the text in the cells be center aligned, regardless of other settings?
* ``CellPadding``
    How much padding should be applied to this cell before the text or other decorations
    are drawn? This can be a numeric (which will be applied to all sides) or it can be a
    collection of the paddings to be applied to the various sides: (left, top, right,
    bottom).
* ``GridPen``
    What Pen will be used to draw the grid lines of the cells?

In addition to these properties, there are some methods which add various decorations to
the blocks:

* ``Background(color=wx.BLUE, toColor=None, space=0)``
    This gives the block a solid color background (or a gradient background if *toColor*
    is not None). If *space* is not 0, *space* pixels will be subtracted from all sides
    from the space available to the block.

* ``Frame(pen=None, space=0)``
    Draw a rectangle around the block in the given pen

* ``Line(side=wx.BOTTOM, color=wx.BLACK, width=1, toColor=None, space=0, pen=None)``
    Draw a line on a given side of the block. If a pen is given, that is used to draw the
    line (and the other parameters are ignored), otherwise a solid line (or a gradient
    line is *toColor* is not None) of *width* pixels is drawn.

Can't you just show me what these things do?
--------------------------------------------

.. image:: images/listctrlprinter-formatting.png


Understanding the process
-------------------------

Use The Source Luke (at least until I write this part of the docs)



Other things to be aware of
---------------------------

* A ``ListCtrlPrinter`` only works on ListCtrls that are in report view. It will ignore
  any ListCtrl that is in any other view.

* You can set the left *Padding* of the *ColumnHeader* format and *Row* format to different
  values. This results in the column headers not lining up with the rows. This should be
  understood as a feature.

* For reasons that are still not clear to me, images that come from BMP files will not
  print on some (most?) printers. Images that come from PNG and other formats work fine.

* The ``ListCtrlPrinter`` is not designed to be general purpose reporting solution. There are
  no running totals, macro language, or ODBC data sources. It just prints ListCtrls.
