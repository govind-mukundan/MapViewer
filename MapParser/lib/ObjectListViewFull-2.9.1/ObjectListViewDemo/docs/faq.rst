.. -*- coding: UTF-8 -*-

:Subtitle: Just the FAQs, Ma'am

.. _faq:

Frequently Asked Questions
==========================

Some questions and issues surface regularly on the Forums or in emails. This section has
a selection of the most common questions. Please read the questions before asking questions on
the Forum. Several people have been known to have blood pressure problems.

.. contents::
   :depth: 1
   :backlinks: none
   :local:

.. _rows-of-different-heights:

Can an ObjectListView have rows of different heights?
-----------------------------------------------------

No.

ObjectListView is a wrapper for the underlying ListView. It makes a ListView much easier
to use, but it can't change the control's basic behaviour. One limitation of a ListView is
it that cannot have rows of different heights. There is no way to make one row be taller
than other rows. It's just not possible.

If being able to have rows of different heights is essential to you,
ObjectListView is not your solution.

You may want
to consider Matthew Hall's excellent XPTable_ and its `update project`_, as well as
Lee Paul Alexander's fantastic `Outlook-style list`__.

.. _XPTable: http://www.codeproject.com/KB/list/XPTable.aspx

.. _update project: http://www.codeproject.com/KB/list/XPTableListViewUpdate.aspx

.. __: http://www.codeproject.com/KB/list/outlooklistcontrol.aspx

Can an ObjectListView fix columns or rows so they don't scroll?
---------------------------------------------------------------

No.

The underlying ListView control doesn't allow anything like that.

Again, if that ability is essential, you will want to consider one
of the above projects.


How do I hide a column?
-----------------------

You hide a column by setting `IsVisible` to `false` on the column and then calling
`RebuildColumns()` on the `ObjectListView`::

    this.olvColumn2.IsVisible = false;
    this.olv1.RebuildColumns();

You cannot hide the primary column (i.e. column 0). If you want to hide the primary column,
make it not the primary column, but give it `DisplayIndex = 0`.

How do I add or remove a column programmatically?
-------------------------------------------------

Add (or remove) the column to the `AllColumns` collection and then call `RebuildColumns()`::

    OLVColumn aNewColumn = new OLVColumn();
    // ... configure it and finally ...
    this.olv1.AllColumns.Add(aNewColumn);
    this.olv1.RebuildColumns();

If you add the new column directly to the `Columns` collection, it will vanish
when the user switches to `Tile` view. It also won't appear in the list of columns
available when hiding or showing columns.


Why aren't the checkboxes responding to clicks ?!
-------------------------------------------------

This normally happens when the programmer tries to modify a checkbox
using .NET's normal mechanisms. In order to support virtual lists,
`ObjectListView` cannot use those mechanisms -- you have to do things
the `ObjectListView` way.

See :ref:`recipe-checkbox`, especially :ref:`using-checkboxes-programmatically`.


Can I use ObjectListView in a commercial application?
-----------------------------------------------------

The ObjectListView code is released under GPLv3. The GPL is a viral license,
meaning that if you use some GPL-covered code in your application, your
application must also be covered by GPL. This is generally not what commercial
developers want. (I am not a lawyer. This is not legal advice. It could be
completely wrong).

However, if you wish to use this code in a commercial application, please
contact me: phillip.piper@gmail.com .


Why doesn't the ObjectListView auto-update when I change my model objects?
--------------------------------------------------------------------------

    *I have ObjectListView that's showing my model objects. But when I change the values in my
    model, the ObjectListView doesn't update. What's going wrong here?*

Nothing. That's what it is supposed to do.

The `ObjectListView` knows nothing about your model objects. It does not
know when they have been changed. Only you know that. When you know that a model object
has changed and needs to be updated, you can either call `RefreshObject()` to
update just one object, or you can call `BuildList()` to rebuild everything at once.

`DataListView` is the exception to this rule. Any change you make to your `DataSource` should be
immediately reflected in the `DataListView`.

Why does VS 2010 complain that `BrightIdeasSoftware.ObjectListView` can't be found?
-----------------------------------------------------------------------------------

  *I've made a new project in Visual Studio 2010 and added the ObjectListView project to my solution.
  But when I build the solution, it complains that `BrightIdeasSoftware.ObjectListView` can't be found.
  What's going on here?*

The default framework for new projects in Visual Studio 2010 is a "Client Profile". That profile
does not contain support for design time components. `ObjectListView` currently uses design time
components to work within the IDE.

To fix this problem, change the target framework of your solution to a "Full" version.

Why doesn't it do *some-feature-I-really-want*?
-----------------------------------------------

It could be that I simply haven't thought of it. Or it could be that I have thought of
it but it just isn't possible.

Remember that `ObjectListView` is just a wrapper around `ListView`. It makes the `ListView`
a little easier to use, and adds some helper functions, but it can't change the basic
behaviour of the control.


Why is the text of the first column indented by about 20 pixels?
----------------------------------------------------------------

This shows up when you have a `ListView` that doesn't have an icon in the first column.
The control still leaves space for the icon, even when there isn't one.

If the `ListView` doesn't have a small image list, this indent disappears. But as soon as
the control has a small image list, even an empty one, the text of the first column will
be indented.

So, if you really want to get rid of this indent, make an ObjectListView which doesn't have
a `SmalImageList`. Be aware that some features *require* a small image list and will create
one if necessary. These features are:

   * sort indicators on column header on pre-XP systems
   * subitem checkboxes


I've compiled the project for the first time, but I don't see the ObjectListView in the toolbox
-----------------------------------------------------------------------------------------------

If you are using Visual Studio, check under the Tools>>Options, in the Windows
Form Designer category (turn on Advanced settings to see) for a setting
"AutoToolboxPopulate." This should be set to True.

Also, make sure you have a Windows Forms project. ObjectListView is a Windows
Forms control: it will not appear in the toolbox for a Windows Presentation
Framework project, nor for a WebForms project.


Is ObjectListView thread-safe?
------------------------------

"Thread-safe" is a notoriously slippery term.

If you mean, "Can I do whatever I like with an ObjectListView from various
threads at the same time?" the answer is "Probably not."

If you mean "Can I update the ObjectListView from a background thread?" then the
answer is, "Yes, so long as you stick to the primary commands." Specially, these
methods can all be called from background threads, and the control will work as
expected:

* `AddObject/s()`
* `BuildList()`
* `ClearObjects()`
* `InsertObjects()`
* `RefreshObjects()`
* `RemoveObject/s()`
* `SetObjects()`
* `Sort()`


Does it work with Mono?
-----------------------

As of September 2009, not any more.

It would be possible to make it work with
Mono again. Please let me know if you are interested.


Don't you have some class diagrams for all this stuff?
------------------------------------------------------

     *There seems to be an awful lot going on here. Don't you have some class
     diagram so I can see how things fit together?*

Some people love class diagrams. Other regard them as just one more piece of out
of date documentation. As of v2.1, these diagrams are accurate.

The basic classes are show here:

.. image:: images/ClassDiagram.png

More details about the relationship between virtual listviews and their data sources
can be seen in this diagram:

.. image:: images/ClassDiagram-VirtualList.png

How can I add drag and drop to a TreeListView?
----------------------------------------------

See :ref:`this blog <blog-rearrangingtreelistview>` for a detailed discussion of
how to add drag and drop support to a `TreeListView`.

