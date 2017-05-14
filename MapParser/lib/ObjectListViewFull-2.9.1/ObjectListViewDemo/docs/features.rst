.. -*- coding: UTF-8 -*-

:Subtitle: Why should I learn to love the ListView?

.. _features:

Features of an ObjectListView
=============================

Why take the time to learn how to use an ObjectListView? What's the benefit? The
return on investment? This page tries to document the useful features of an
ObjectListView. Not all features are equally useful, but it's better to be aware
of what's available so that you can use it when the need arises.

* `Animations`_
* `Alternate rows background colors`_
* `Automatic grouping`_
* `Automatic sorting`_
* `Automatically create the ListView from model objects`_
* `Buttons in cells`_
* `Checkboxes in any column`_
* `Cell events`_
* `Collapsible groups`_
* `Copy selected rows to clipboard`_
* `Custom row and cell formatting`_
* `Custom selection colours`_
* `Data binding`_
* `Different flavours of ObjectListView for different purposes`_
* `Displays a "list is empty" message`_
* `Drag and drop`_
* `Ease of use`_
* `Editing cell values`_
* `Filtering`_
* `Groups on virtual lists`_
* `Header check boxes`_
* `Header formatting`_
* `Header images`_
* `Header drawn vertically`_
* `Header visible in all views`_
* `Hierarchical checkboxes`_
* `Hot item tracking`_
* `In-place modifications of the list`_
* `INotifyPropertyChanged support`_
* `Model object level operations`_
* `More control over column width`_
* `Owner drawing`_
* `Overlays and decorations`_
* `Row height can be changed`_
* `Save and restore state`_
* `Searching on the sort column`_
* `SelectionChanged event`_
* `Selected Column`_
* `Supports all ListView views`_
* `Tool Tips`_
* `User-selection of visible columns`_


Ease of use
-----------

**The** major goal of an ObjectListView is to make your life easier. All  common
ListView tasks  should be  easier --  or at  least no  more difficult -- with an
ObjectListView. For the investment of  configuration the Columns, you receive  a
great deal of convenience and value added functions.

See :ref:`getting-started-label` for an introduction to the basics.


Automatically create the ListView from model objects
----------------------------------------------------

The major way  in which the  ObjectListView makes your  life easier is  by being
able to  automatically build  the ListView  from a  collection of model objects.
Once the columns are defined, an ObjectListView is able to build the rows of the
ListView  without  any  other  help.  It  only  takes  a  single  method   call:
`SetObjects()`.


Automatic grouping
------------------

If  `ShowGroups`  is true,  the  control will  automatically  create groups  and
partition the rows into those groups.

This grouping can be customised in several ways:

*  the  way a  row  is assigned  to  a group  can  be changed  by  installing  a
   `GroupKeyGetter` on the column.

* the name for a groups can be changed by installing a `GroupKeyToTitleConverter`
  on the oolumn

For values  that form  a continous  range (like  salaries, height,  grades), the
`MakeGroupies` utility method can easily create more meaningful groupings.

Groups normally change according to the  sort column. You can "lock" the  groups
to be on a particular column via the `AlwaysGroupByColumn` property.

See :ref:`grouping-label` for more details.


Collapsible groups
------------------

This is the most requested feature ever. Under Vista, groups are now collapsible.
This is enabled by default. If you don't want your groups to be collapsible, set
`HasCollapsibleGroups` to *false*.

Under XP, groups cannot be collapsed.


Data binding
------------

Each flavour of control (`ObjectListView`, `FastObjectListView`, `TreeListView`)
has a data bindable version: `DataListView`, `FastDataListView`, `DataTreeListView`.

Each data bindable version has two additional properties: `DataSource` and `DataMember`.
These let you control which data set is bound to the control.

Groups on virtual lists
-----------------------

Grouping on virtual lists  is normally impossible. The  SDK says so. But  with a
little exploring of `undocumented features`__, it can be made to work. Thanks to
Geoff Chapell for all his hard work in finding out what is possible.

.. __: http://www.geoffchappell.com/viewer.htm?doc=studies/windows/shell/comctl32/controls/listview/index.htm&tx=23

So, as  of v2.3,  virtual lists  can show  groups. `FastObjectListViews` support
grouping without any further effort -- simple set `ShowGroups` to *true*. If you
have a `VirtualObjectListView` and have done the work of implementing a  virtual
data source, you will need to implement the `IVirtualGroups` interface, and then
set `GroupingStrategy` property to an object that implements that interface.

Group formatting
----------------

Under Vista and later, groups can now have icons, sub titles, footers and tasks.

.. image:: images/group-formatting.png

See :ref:`recipe-groupformatting`

Automatic sorting
-----------------

ObjectListView will automatically sort the rows when the user clicks on a column
header. This  sorting understands  the data  type of  the column,  so sorting is
always correct  according to  the data  type. Sorting  does not  use the  string
representation.

Sorting can be customised either  by listening for the `BeforeSorting`  event or
by installing a `CustomSorter`.


Different flavours of ObjectListView for different purposes
-----------------------------------------------------------

An `ObjectListView` is the plain vanilla version of the control. It accepts a list of
model objects, and builds the control from those model objects.

A `DataListView` is a data bindable version of an ObjectListView. Give it a data source,
and it automatically keep itself in sync with the data source, propagating changes to and fro.
It will even create the columns of the list view for you, if you don't want to do it yourself.

A `FastObjectListView` is a faster version of an ObjectListView.
Typically, it can build a list of 10,000 objects in less than 0.1 seconds.

A `VirtualObjectListView` does not require a list of model objects. Instead, it asks for
model objects as it requires them. In this way, it can support an unlimited number of rows.
Most simply, a `VirtualObjectListView` can be given a `RowGetter` delegate, which is called when
the list needs to display a particular model object. This gives a functional, but limited ListView.
It's better to implement the `IVirtualListDataSource` interface to give a fully functional virtual
ListView.

A `TreeListView` combines the tree structure of a TreeView with the multi-column display of a
ListView.


Editing cell values
-------------------

ListViews normally allow only the primary cell (column 0) to be edited.
An ObjectListView allows all cells to be edited. This editing knows to use different
editors for different data types. It also allows auto-completion based on existing values
for that column.

See :ref:`cell-editing-label` for more details.


Owner drawing
-------------

Sometimes, you want to show more than just text and an icon in your ListView. ObjectListView
has extensive support for owner drawing, providing a collection of useful renderers, and making
it easy to develop your own renderers.

There is even a renderer provided that draws animations within a cell (if
anyone ever actually uses this feature in a real application please let me
know).

See :ref:`owner-draw-label` for more information.


Drag and drop
-------------

`ObjectListView` supports dragging rows to other places, including other application. It also
supports accepting drops from other sources including cross-application drops. Special support
is provide for drops from other `ObjectListViews` in the same application.

In many cases, an `ObjectListView` becomes a full drag source by setting
`IsSimpleDragSource` to *true*.

Similarly, to accept drops, it is usually enough to set `IsSimpleDropSink` to *true*, and
then handle the `CanDrop` and `Dropped` events (or the `ModelCanDrop` and `ModelDropped`
events, if you only want to handle drops from other `ObjectListViews` in your application).

See :ref:`dragdrop-label` for more information.


Supports all ListView views
---------------------------

An `ObjectListView` supports all views: report, tile, list, large and small icons. All functions
should work equally in all views: drag and drop, editing, check state, icons, selection.

You can even owner draw the non-details views (through the `ItemRenderer` property).


More control over column width
------------------------------

An `ObjectListView` allows the programmer to have control over the width of columns after
the ListView is created.

When a column is created, it is normally given a width in pixels. This is the width of the
column when the ListView is first shown. After creation, the user can resize that column
to be something else.

By using the `MinimumWidth` and `MaximumWidth` properties, the programmer can control the
lower and upper limits of a column. Combining these two properties can give a fixed width
column.

Finally, the programmer can specify that a column should resize automatically to be wider
when the ListView is made wider and narrower when the ListView is made narrower.
This type of column is a space filling column, and is created by setting `IsSpaceFilling` to
true.

See these recipes:

* :ref:`recipe-column-width`
* :ref:`recipe-fixed-column`
* :ref:`recipe-column-filling`


Displays a "list is empty" message
----------------------------------

An empty ListView can be confusing to the user: did something go wrong?
Do I need to wait longer and then something will appear?

An `ObjectListView` can show a "this list is empty" message when there is nothing
to show in the list, so that the user knows the control is supposed to be empty.
You can even apply some fairly customizations to the appearance of the message.

See this recipe: :ref:`recipe-emptymsg`


Buttons in cells
----------------

An `ObjectListView` can put clickable buttons into cells.

See this recipe for more details: :ref:`recipe-buttons`.


Checkboxes in any column
------------------------

An `ObjectListView` supports checkboxes on rows. In fact, it supports checkboxes in
subitems, if you are really keen. These work even in virtual mode (which a normal
ListView cannot do).

See this recipe for more details: :ref:`recipe-checkbox`.


Alternate rows background colors
--------------------------------

Having subtly different row colours for even and odd rows can make a ListView easier
for users to read. `ObjectListView` supports this alternating of background colours.
It is enabled by setting `UseAlternatingBackColors` to true (the default). The background
of odd numbered rows will be `AlternateRowBackColor`.


Custom row and cell formatting
------------------------------

An `ObjectListView` allows rows and even cells to be formatted with custom colours and fonts. For example,
you could draw clients with debts in red, or big spending customers could be given a gold
background. See here: :ref:`recipe-formatter`


Model object level operations
-----------------------------

The `ObjectListView` allows operations at the level that makes most sense to the
application: at the level of model objects. Properties like `SelectedObjects` and
`CheckedObjects` and operations like `RefreshObjects()` provide a high-level
interface to the ListView.


Searching on the sort column
----------------------------

When a user types into a normal ListView, the control tries to find the first row where
the value in cell 0 begins with the character that the user typed.

`ObjectListView` extends this idea so that the searching can be done on the column by which
the control is sorted (the "sort column"). If your music collection is sorted by "Album"
and the user presses "z", ObjectListView will move the selection to the first track of the
"Zooropa" album, rather than find the next track whose title starts with "z".

In many cases, this is behaviour is quite intuitive. iTunes works in this fashion on its
string value columns (e.g. Name, Artist, Album, Genre).


Hot item tracking
-----------------

It sometimes useful to emphasis the row that the mouse is currently over. This is called
"hot tracking." The normal ListView can underline the text of the hot item. In an `ObjectListView`,
the font, font style, text color, and background color can all be set for the hot item.

See this recipe for details: :ref:`recipe-hottracking`


Copy selected rows to clipboard
-------------------------------

When one or more rows are selected and the user pressed Ctrl-C, a text representation and
a HTML representation of the selected rows is pasted into the clipboard. This allows users
to easily copy information from your application into their word processing documents.


Save and restore state
----------------------

If the user makes adjustments to the size, order and selection of columns in one of your
ListViews, it would be good manners to make sure those changes are still there when the user
runs your application tomorrow. The methods `SaveState()` and `RestoreState()` let you
do this effortlessly.


User-selection of visible columns
---------------------------------

it is sometimes nice to let the user choose which columns they wish to see in a ListView.
`ObjectListView` allows you to define many columns for a particular ListView but only
have some of them initially visible. The user can right click on the column headers
and be presented with a menu of all defined columns from which they can choose which
columns they wish to see.

The programmer can also control which columns are visible via the `IsVisible` property.
To hide a column, set `IsVisible` to false and then call `RebuildColumns()` to
make the change take effect.


SelectionChanged event
----------------------

With a normal ListView, the `SelectedIndexChanged` event is the normal way of detecting
when the selection has changed. This event is triggered whenever a row is selected or
deselected. Although this sounds obvious, it can be quite annoying. If the user selects
100 rows and then clicks on another row, you will received 101 `SelectedIndexChanged` events:
1 for each row deselected and 1 for the new row selected.

ObjectListView has a `SelectionChanged` event which is triggered once, no matter how many
rows are selected or deselected. This is normally far more convenient.


Selected Column
---------------

If you set the `SelectedColumn` property, that column will be lightly tinted to show
that it is selected.

If you set the `TintSortColumn` property, the sort column will automatically be
selected.


Row height can be changed
-------------------------

With a normal ListView, the row height is calculated from a combination of the control
font and the `SmallImageList` height. It cannot be changed. But, an `ObjectListView` has a
`RowHeight` property which allows the height of all rows to be specified.

N.B. Every row has the same height. No variable height rows are allowed.
See :ref:`rows-of-different-heights` to understand why.


Custom selection colours
------------------------

The colours used to indicate a selected row are governed by the operating system and
cannot be changed. However, if you set `UseCustomSelectionColors` to true, the
ObjectListView will use `HighlightBackgroundColor` and `HighlightForegroundColor` as
the colours for the selected rows.


Tool Tips
---------

A standard `ListView` cannot display tooltips on individual cells (apart from showing
truncated cell values when FullRowSelect is true).

But an ObjectListView can show arbitrary tool tips for both cells and headers.
See :ref:`recipe-tooltips` for details.


In-place modifications of the list
----------------------------------

ObjectListView supports `AddObjects()` and `RemoveObjects()` method which modify
the contents of the list in place. Use the `Objects` property to fetch the
current contents of the list.

Not all flavours of ObjectListView support this capacity equally. Plain `ObjectListViews`
support it fully, as do `FastObjectListViews`. `VirtualObjectListViews` simply hand off these
methods to their data source, so whether these methods work depends on the implementor
of the data source.

`DataListViews` do *not* support these methods since they are controlled by
their `DataSource`.

`TreeListViews` interpret these operations as modifying the top level item
("roots") of their list.


Overlays and decorations
------------------------

`ObjectListViews` can draw images and text over the top of the control. This drawing
is normally done transluscently, so that the drawn images/text do not completely
obscure the underlying information.

Overlays are drawing over the control itself. Decorations are added to rows and
subitems.

See:
* :ref:`recipe-overlays` for how to use them;
* :ref:`overlays-label` for some gotchas
* :ref:`blog-overlays` for the history of their development.


Cell events
-----------

A normal `ListView` triggers mouse events for the list as a whole.
`ObjectListView` can trigger events for cells:
`CellClick`, `CellOver` and `CellRightClick`. These events allow you
to easily know what the user clicked (or what the mouse is over), and
then react appropriately.

Header check boxes
------------------

Each column header can show a checkbox. Checking or uncheckeding this checkbox
can change the checkedness of all checkboxes in the column.

See :ref:`recipe-checkbox-in-header`.

Header formatting
-----------------

Each column header can be given its own font, text color, background color
and border. These can be set per column or on
all headers at once using `HeaderFormatStyle` on the `ObjectListView` itself.

The text of column header can also be word wrapped, rather than truncated.

See :ref:`recipe-headerformatting`.

Header images
-------------

Each column can have an image drawn to the left of the text by setting
the `HeaderImageKey` property.

See :ref:`recipe-column-header-image`.

Header drawn vertically
-----------------------

To save horizonal space, a header can be drawn vertically.

See :ref:`recipe-column-header-vertical`.

Header visible in all views
---------------------------

In *Details* view, the user can click on a column heading to sort the items
according to that column. But in other views, there is no way to specify
how to sort the items. To help with this, `ObjectListView` has a
`ShowHeaderInAllView` property, which if set to *true*, shows the header
in all views, not just *Details*, so that the items can be sorted however
the user wants.

Animations
----------

`ObjectListView` integrates with the `Sparkle` library to allow animations
on cells, rows, or whole lists.

See :ref:`animations-label`.

Filtering
---------

`ObjectListViews` (in all flavours) support filtering of their contents.
This filtering is done by installing `ModelFilters` (or less often `ListFilters`)
which select which rows will be shown in the list. See :ref:`recipe-filtering`.

`ObjectListView` comes with some standard filters, most usefully a text match
filter which includes only rows that contains a certain string in any cell.
This can be combined with a special renderer to highlight the text matches.
See :ref:`recipe-text-filtering`.

These filters are of course extensible.

.. _features-inotifypropertychanged:

INotifyPropertyChanged support
------------------------------

Yet more slothful!

If you set `UseNotifyPropertyChanged` to *true*, then `ObjectListView` will listen
for changes on your model classes, and automatically update the rows when
properties on the model classes changed.

Obviously, your model objects have to implement `INotifyPropertyChanged`.

Hierarchical checkboxes
-----------------------

`TreeListView` has hierarchical check boxes, which is where the checkedness of the parent
summaries the checkedness of the all the subitems. Checking the parent checks all the descendent items.
Similarly, unchecking the parent unchecks all the descendent items.

See :ref:`recipe-hierarchical-checkboxes` and this blog entry: :ref:`blog-hierarchicalcheckboxes`