.. -*- coding: UTF-8 -*-

:Subtitle: Recent improvements in loving the ListView

What's New?
===========

For the (mostly) complete change log, :ref:`see here <changelog>`.

November 2015 - Version 2.9.0
-----------------------------

New features
^^^^^^^^^^^^

* Buttons! :ref:`[More info] <recipe-buttons>` 
* Added `ObjectListView.CellEditUsesWholeCell` and `OLVColumn.CellEditUsesWholeCell` properties.
  If these are set to *true*, `ObjectListView` will use the whole width of a cell 
  when editing a cell's value. Setting the property on a `OLVColumn` impacts just that column,
  setting it on the `ObjectListView` impacts all columns.
* `TreeListViews` can now draw triangles as expansion glyphs, complete with hot behaviour.
  Set `TreeListView.TreeRenderer.UseTriangles` to *true*. It can also not draw any expansion
  glyph by setting `IsShowGlyphs` to *false*.
* Added `ObjectListView.FocusedObject`, `ObjectListView.DefaultHotItemStyle`, 
  and `ObjectListView.HeaderMinimumHeight` properties. Each is completely predictable.

Large-ish Improvements
^^^^^^^^^^^^^^^^^^^^^^

* Regularized the interaction of the various formatting options, in particular `FormatRow`,
  `FormatCell` and hyperlinks.
* Completely rewrote the demo. Each tab is now its own UserControl, and there are lots more 
  comments and explanations.
* `DescribedTaskRenderer` left experimental state and became a first class part of the project.
  See "Pretty Tasks" tab in the demo.
* Lists are now owner drawn by default. About one-quarter of all complaints I receive are
  from someone trying to use a feature that only works when `OwnerDraw` is true.
  This allows all the great features of ObjectListView to work correctly at the slight cost of 
  more processing at render time. It also avoids the annoying "hot item background ignored 
  in column 0" behaviour that the native ListView has. You can still set it back to *false* if you wish. 


Small improvments
^^^^^^^^^^^^^^^^^

* The normal `DefaultRenderer` is now a `HighlightTextRenderer`, since that seems more generally useful.
* Allow `ImageGetter` to return an `Image` (which I can't believe didn't work from the beginning!)
* Added `SimpleDropSink.EnableFeedback` to allow all the pretty and helpful user feedback 
  during drag operations to be turned off.
* `OLVColumn.HeaderTextAlign` became nullable so that it can be "not set", in which case the alignment
  of the header will follow the alignment of the column (this was always the intent)
* Made Unfreezing on data bound lists more efficient by removing a redundant `BuildList()` call
* When auto generating column, columns that do not have a `[OLVColumn]` attribute will auto size

Bug fixes
^^^^^^^^^

* Yet another attempt to disable ListView's "shift click toggles checkboxes" behaviour. The last
  attempt had nasty side effects -- I received the full set of "Hey! I got a right mouse click event 
  when I left clicked the control" emails, expressed in varying degrees of politeness. The 
  new strategy is (hopefully) more invisible.
* `FastObjectListView` now fires a `Filter` event when applying filters
* `SelectionChanged` event is now triggered when the filter is changed
* Setting `View` to `LargeIcon` in the designer is now persisted
* Check more rigourously for a dead control when performing animations
* Corrected small UI glitch in `TreeListView` when focus was lost and `HideSelection` was *false*.


November 2014 - Version 2.8.1
-----------------------------

New features
^^^^^^^^^^^^

* Allow string comparisons to be replaced via static properties `ModelObjectComparer.StringComparer` and `ColumnComparer.StringComparer`

Bug fixes
^^^^^^^^^

* Fixed issue that prevented single click editing from working
* Added `ClickOnceAlways` edit mode
* Trigger `Filter` events properly
* Fixed a couple of issues related to data-bound lists and `CurrencyManager.Position`
* Fixed some bugs in `TreeListView` where `RefreshObject()` or collapsing a branch could corrupt the internal state of the control.

October 2014 - Version 2.8
--------------------------

New features
^^^^^^^^^^^^

* Added the ability to disable rows. :ref:`[More info] <recipe-disabled-rows>` :ref:`[Implementation details] <blog-disabledrows>` 
* Added checkboxes in column headers.  :ref:`[More info] <recipe-checkbox-in-header>`

Other changes
^^^^^^^^^^^^^

* Added `CollapsedGroups` property
* Extended hit test information to include header components (header, divider, checkbox)
* `CellOver` events are now raised when the mouse moves over the header. Set `TriggerCellOverEventsWhenOverHeader` to `false` to disable this behaviour 
* `Freeze/Unfreeze`  now use `BeginUpdate()`/`EndUpdate()` to disable Window level drawing while frozen
* Changed default value of `ObjectListView.HeaderUsesThemes` from `true` to `false`. Too many people were being confused, trying to make something interesting appear in the header and nothing showing up
* Final attempt to fix the issue with multiple hyperlink events being raised. This involves turning a `NM_CLICK` notification into a `NM_RCLICK`. Thanks to aaron for the initial report and investigation.
* `TreeListView.CollapseAll()` now actually, you know, collapses all branches
* The pre-build ObjectListView.dll in the ObjectListView download is now built against .NET 4.0.
  This will make it able to be used directly in VS 2010 and later. For VS 2008 and 2005, the DLL will have to build
  from the included source.
* Added NuGet support. ObjectListView is now available as `ObjectListView.Official`.

Bug fixes
^^^^^^^^^

* Fixed various issues where calling `TreeListView.RefreshObject()` could throw an exception
* Fixed various issues regarding checkboxes on virtual lists
* Fixed issue where virtual lists containing a single row didn't update hyperlinks on `MouseOver`
* Fixed issue where calling `TreeListView.CollapseAll()` when a filter was installed could throw an exception
* Fixed some subtle bugs resulting from misuse of `TryGetValue()`
* Several minor Resharper complaints quiesced.

March 2014 - Version 2.7
------------------------

After a long break, the next release of ObjectListView is available. 

New features
^^^^^^^^^^^^

* Added `HierarchicalCheckBoxes` to `TreeListView` (so quick to say, but so much work to do)
* Added `TreeListView.Reveal()` to show deeply nested model objects, by expanding all its ancestors

Other changes
^^^^^^^^^^^^^

* Added `CellEditEventArgs.AutoDispose` to allow cell editors to be disposed after use. Defaults to `true`. This allows heavy controls to be cached for reuse, and light controls to be disposed without leaks.
* `ShowHeaderInAllViews` now works on virtual lists
* Added `TreeListView.TreeFactory` to allow the underlying `Tree` to be replaced by another implementation.
* `CollapseAll()` and `ExpandAll()` now trigger cancellable events
* Added static property `ObjectListView.GroupTitleDefault` to allow the default group title to be localised
* Added Visual Studio 2012 support
* Clicking on a non-groupable column header when showing groups will now sort the group contents by that column.
* `TreeListView` now honours `SecondarySortColumn` and `SecondarySortOrder`


Bugs fixed
^^^^^^^^^^

* `ClearObjects()` now actually, you know, clears objects :)
* Fixed some more issues/bugs/annoyances with `ShowHeaderInAllViews`.
* Fixed various bugs related to filters and list modifications.
* Fixed some bugs so that tree expansion events are always triggered, but only once per action.
* `RebuildChildren()` no longer checks if `CanExpand` is true before rebuilding.
* Fixed long standing bug in `RefreshObject()` would sometimes not work on objects which overrode `Equals()`

July 2012 - Version 2.6
-----------------------

New features
^^^^^^^^^^^^

* Added `DataTreeListView` -- a data bindable `TreeListView`. :ref:`[More info] <recipe-datatreelistview>`.

* Added `UseNotifyPropertyChanged` property to allow ObjectListViews to listen for `INotifyPropertyChanged` 
  events on models. :ref:`[More info] <features-inotifypropertychanged>`.

* `Generator` can now work on plain model objects without requiring properties to be marked with `[OLVColumn]` attribute.
  :ref:`[More info] <recipe-generator>`.

* Added `FlagClusteringStrategy` -- a new clustering strategy based off bit-xor’ed integer fields.

* Added `CellPadding`, `CellHorizontalAlignment` and `CellVerticalAlignment` properties to `ObjectListView` and
  `OLVColumn`. On owner drawn controls, these control the placement of cell contents within the cell.

* Added `OLVExporter` -- a utility to export data from `ObjectListView`.

Other changes
^^^^^^^^^^^^^

* Added `Reset()` method, which definitively removes all rows and columns from all flavours of `ObjectListView`.
* Renamed `GetItemIndexInDisplayOrder()` to `GetDisplayOrderOfItemIndex()` to better reflect its function.
* Changed the way column filtering works so that the same model object can now be in multiple clusters.
  This is useful for filtering on xor'ed flag fields or multi-value strings (e.g. hobbies that are stored as comma separated values).
* Added `SimpleDropSink.UseDefaultCursors` property. Set this to *false* to use custom cursors in drop operations.
* Added more efficient version of `FilteredObjects` property to `FastObjectListView`.
* Added `ObjectListView.EditModel()` convenience method
* Added `ObjectListView.AutoSizeColumns()` to resize all columns according to their content or header
* Added static property `ObjectListView.IgnoreMissingAspects`. If this is set to *true*, all 
  `ObjectListViews` will silently ignore missing aspect errors. Read the remarks to see why this would be useful.
* Don’t trigger selection changed events during sorting/grouping, add/removing columns, or expanding branches.
* Clipboard and drag-drop now includes CSV format.
* Reimplemented `Generator` to be subclassable. Added `IGenerator` to allow column generation to be be completely replaced.

Bugs fixed
^^^^^^^^^^

* Hit detection will no longer report check box hits on columns without checkboxes.
* Circumvent annoying bug in ListView control where changing selection would leave artefacts on the control.
* Renderers only create Timer when animating GIFs.
* Fixed bug with single click cell editing where the cell editing didn’t start until the first mouse move.
  This fixed a number of related bugs concerning cell editing and mouse moves.
* Fixed bug where removing a column from a LargeIcon or SmallIcon view would crash the control.
* Fixed bug where search-by-typing would not work correctly on a `FastObjectListView` when showing groups
* Fixed several bugs related to groups on virtual lists.
* Overlays now remember all the ObjectListView's parents so that we can explicitly unbind all those parents when disposing.
  This protects us against unexpected changes in the visual hierarchy (e.g. moving a parent `UserControl` from one tab to another)
* `TreeListView.RebuildAll()` will now preserve scroll position.


May 2012 - Version 2.5.1
------------------------

New features
^^^^^^^^^^^^

* Added better support for groups. This includes hit detection,
  cancellable group expand/collapse event (`GroupExpandingCollapsing`) and group state changed
  event (unsurprisingly `GroupStateChanged`). See :ref:`this blog <blog-listviewgroups>` for more details.

* Added `UsePersistentCheckboxes` property to allow `ObjectListView` to correctly remember checkbox
  values across list rebuilds. Without this, applying a filter to plain `ObjectListView` would always
  make the checkboxes lose their values. This is *true* by default. Set to *false* to return to v2.5 and earlier
  behaviour.

* Added `AdditionalFilter` property. Any `IModelFilter` installed through the `AdditionalFilter` property
  will be combined with any column based filter that the user specifies at runtime. This is different
  from the `ModelFilter` property, since setting that will *replace* any user given column filtering and vice versa.

* Added `CanUseApplicationIdle` property to cover cases where `Application.Idle` events are not triggered.
  In some contexts -- specifically VisualStudio and Office extensions -- the `Application.Idle` events
  are never triggered. If you set `CanUseApplicationIdle` to *false*, `ObjectListView` will correctly handle
  these situations.

* Support for :ref:`native background images <recipe-native-backgrounds>`.

Other Changes
^^^^^^^^^^^^^

* Vastly improved the runtime designer, based off information in
  `'Inheriting' from an Internal WinForms Designer`__ on `CodeProject`_.

.. __: http://www.codeproject.com/Articles/150801/Inheriting-from-an-Internal-WinForms-Designer

.. _CodeProject: http://www.codeproject.com

* Improved :ref:`TreeListView dragging example <blog-rearrangingtreelistview>`.
  Now also shows how to handle accepting drops from
  external sources.

Bugs fixed
^^^^^^^^^^

* Avoid bug/feature in .NET's `ListView.VirtualListSize` setter that causes flickering when the size of the list changes
  (:ref:`read this<blog-virtuallistflickers>` for the full details).

* Fixed a bug that forced groups to always have 20 or so pixels of extra space between them. This is now
  correctly controlled by the `SpaceBetweenGroups` property.

* Fixed a bug that caused decorations to not be drawn when the first group (olny) of a list was collapsed.

* Fixed bug that occurred when adding/removing items to a `VirtualObjectListView` (including `FastObjectListView`
  and `TreeListView`) while the view was grouped.

* Fixed bug where, on a `ObjectListView` with only a single editable column, tabbing to change rows would edit
  the cell above rather than the cell below the cell being edited.

* Fixed bug in `TreeListView.CheckedObjects` where it would return model objects that had been filtered out.

* Clicking the separator on the Column Select menu no longer crashes.

* Fixed rare bug that could occur when trying to group/clustering an empty list.

* Handle case where a model object has both an `Item` property and an `Item[]` accessor.

* Fixed filters to correctly handle searching for empty strings.

* Handle cases where a second tool tip is installed onto the ObjectListView.

* Correctly recolour rows after an Insert or Move.

* Removed `m.LParam` cast which could cause overflow issues on Win7/64 bit.

Supported systems
-----------------

Another hard drive crash and my last remaining XP machine is no more.
I no longer have access to XP or even Vista -- only Windows 7.

I may try to purchase a cheap laptop simply to run XP, but for the moment, I cannot test
ObjectListView on anything other than Windows 7.

May 2011 - Version 2.5
----------------------

New features
^^^^^^^^^^^^

* Excel like filtering. Right clicking on a header will show a "Filter" menu, which will allow you to select the values that will survive the filtering.

* `FastDataListView`. Just like a normal `DataListView`, only faster. On my laptop, it comfortably handles datasets of 100,000 rows without trouble. NOTE: This does not virtualize the data access part -- only the UI portion. So, if you have a query that returns one million rows, all the rows will still be loaded from the database. Once loaded, however, they will be managed by a virtual list.

* Fully customizable character map during cell edit mode.
  This was an overkill solution for the various flavours of "tab wraps to new line" requests.
  As convinence wrappers, `CellEditTabChangesRows` and `CellEditEnterChangesRows` properties have
  been added.

* Support for VS 2010. The target framework must be a "full" version of .Net. It will not work with a "Client Profile" (which is unfortunately the default for new projects in VS 2010).

* Columns can now disable sorting, grouping, searching and "hide-ability" (`Sortable`, `Groupable` `Searchable` and `Hideable` properties respectively).

Breaking changes
^^^^^^^^^^^^^^^^

* [Medium]: On `VirtualObjectListView`, `DataSource` was renamed to `VirtualListDataSource`. This was necessary to allow FastDataListView which is both a DataListView AND a VirtualListView -- which both used a 'DataSource' property :(

* [Small]: `GetNextItem()` and `GetPreviousItem()` now accept and return `OLVListView` rather than `ListViewItems`.

* [Small]: Renderer for tree column must now be a subclass of `TreeRenderer`, not just a general `IRenderer`

* [Small]: `SelectObject()` and `SelectObjects()` no longer deselect all other rows.
  This gives an much easier way to add objects to the selection. The properties `SelectedObject`
  and `SelectedObjects` *do* still deselect all other rows.

Minor features
^^^^^^^^^^^^^^

* `TextMatchFilter` was seriously reworked. One text filter can now match on multiple strings. `TextMatchFilter` has new factory methods (which make `TextMatchFilter.MatchKind` redundant).

* Revived support for VS 2005 after being provided with a new copy of VS 2005 Express.

* Column selection mechanism can be customised, through the `SelectColumnsOnRightClickBehaviour`. The default is `InlineMenu`, which behaves like previous versions. Other options are `SubMenu` and `ModalDialog`. This required moving the `ColumnSelectionForm` from the demo project into the ObjectListView project.

* Added `OLVColumn.AutoCompleteEditorMode` in preference to `AutoCompleteEditor`  (which is now just a wrapper). Thanks to Clive Haskins

* Added `ObjectListView.IncludeColumnHeadersInCopy`

* Added `ObjectListView.Freezing` event

* Added `TreeListView.ExpandedObjects` property.

* Added `Expanding`, `Expanded`, `Collapsing` and `Collapsed` events to `TreeListView`.

* Added `ObjectListView.SubItemChecking` event, which is triggered when a checkbox on subitem is checked/unchecked.

* Allow a delegate to owner draw the header

* All model object comparisons now use `Equals()` rather than `==` (thanks to vulkanino)

* Tweaked `UseTranslucentSelection` and `UseTranslucentHotItem` to look (a little) more like Vista/Win7.

* Added ability to have a gradient background on `BorderDecoration`

* Ctrl-C copying is now able to use the `DragSource` to create the data transfer object (controlled via `CopySelectionOnControlCUsesDragSource` property).

* While editing a cell, `Alt-[arrow]` will try to edit the cell in that direction
  (showing off what the cell edit character mapping can achieve)

* Added long, :ref:`tutorial-like walk-through <blog-rearrangingtreelistview>` of how to make a `TreeListView` rearrangeable.

* Reorganized files into folders


Bug fixes (not a complete list)
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

* Fixed (once and for all) `DisplayIndex` problem with `Generator`

* Virtual lists can (finally) set `CheckBoxes` back to *false* if it has been set to true. (This was a little hacky and may not work reliably).

* Preserve word wrap settings on `TreeListView`

* Resize last group to keep it on screen

* Changed the serializer used in `SaveState()`/`RestoreState()` so that it resolves classes on name alone

* When grouping, group comparer, collapsible groups and `GroupByOrder` being `None` are now all honoured correctly

* Trying to use animated gifs in a virtual list no longer crashes. It still doesn't work, but it doesn't crash.

* `GetNextItem()` and `GetPreviousItem()` now work on grouped virtual lists.

* Fixed bug in `GroupWithItemCountSingularFormatOrDefault`

* Fixed strange flickering in grouped, owner drawn OLV's using `RefreshObject()`

* Alternate colours are now only applied in `Details` view (as they always should have been)

* Alternate colours are now correctly recalculated after removing objects

* `CheckedObjects` on virtual lists now only returns objects that are currently in the list.

* `ClearObjects()` on a virtual list now resets all check state info.

* Filtering on grouped virtual lists no longer behaves strangely

* `ModelDropEventArgs.RefreshObjects()` now works correctly on `TreeListViews`.

* Dragging a column divider in the IDE Form Designer now correctly resizes the column.

* Removing objects from filtered or sorted `FastObjectListView` now works without clearing the filter or sorting.


14 September 2010 - Version 2.4.1
---------------------------------

New features
^^^^^^^^^^^^

* Column header improvements: they can be :ref:`rendered vertically <recipe-column-header-vertical>`;
  they can :ref:`show an image <recipe-column-header-image>`; they can be aligned differently to the cell's contents
  (use `OLVColumn.HeaderTextAlign` property).

* Group sorting can now be completely customised, as can item ordering within. See :ref:`this recipe <recipe-sorting-groups>`.

* Improved text filtering to allow for prefix matching and full regex expressions.

* Subitem checkboxes improvements: check boxes now obey `IsEditable` setting on column, can be hot, can be disabled.

* Added `EditingCellBorderDecoration` to make it clearer :ref:`which cell is being edited <recipe-showing-editing-cell>`.

* Added `OLVColumn.Wrap` to easily word wrap a columns cells.

Small tweaks
^^^^^^^^^^^^

* No more flickering of selection when tabbing between cells.

* Added `ObjectListView.SmoothingMode` to control the smoothing of all graphics operations.

* Dll's are now signed.

* Invalidate the control before and after cell editing to make sure it looks right.

* `BuildList(true)` now maintains vertical scroll position even when showing groups.

* CellEdit validation and finish events now have `NewValue` property.

* Moved `AllowExternal` from `RearrangableDropSink` up the hierarchy to `SimpleDropSink`
  since it could be generally useful.

* Added `ObjectListView.HeaderMaximumHeight` to limit how tall the header section can become

Bug fixes
^^^^^^^^^

* Avoid bug in standard `ListView` where virtual lists would send invalid item indicies for tool tip messages when in non-Details views.

* Fixed bug where `FastObjectListView` would throw an exception when showing hyperlinks in any view except Details.

* Fixed bug in `ChangeToFilteredColumns()` that resulted in column display order being lost when a column was hidden.

* Fixed long standing bug where having 0 columns caused an `InvalidCast` exception.

* Columns now cache their group item format strings so that they still work as grouping columns after they have been removed from the listview. This cached value is only used when the column is not part of the listview.

* Correctly trigger a `Click` event when the mouse is clicked.

* Right mouse clicks on checkboxes no longer confuses them

* Fixed bugs in `FastObjectListView` and `TreeListView` that prevented objects from being removed (or at least appeared to).

* Avoid checkbox munging bug in standard `ListView` when shift clicking on non-primary columns when `FullRowSelect` is `true`.

* `OLVColumn.ValueToString()` now always returns a `String` (as it always should have)


10 April 2010 - Version 2.4
---------------------------

New features
^^^^^^^^^^^^

* :ref:`Filtering <recipe-filtering>`.

* :ref:`Animations <animations-label>` on cells, rows, or the whole list.

* :ref:`Header styles <recipe-headerformatting>`. This makes `HeaderFont` and `HeaderForeColor` properties unnecessary. They will be marked obsolete in the next version and removed after that.

* [Minor] Ctrl-A now selects all rows (no surprises there). Set `SelectAllOnControlA` to `false` to disable.

* [Minor] Ctrl-C copies all selected rows to the clipboard (as it always did), but this can now be disabled by setting `CopySelectionOnControlC` to `false`.


Bug fixes
^^^^^^^^^

* Changed object checking so that objects can be pre-checked before they are added to the list. Normal ObjectListViews managed "checkedness" in the ListViewItem, so this won't work for them, unless check state getters and putters have been installed. It will work on on virtual lists (thus fast lists and tree views) since they manage their own check state.

* Overlays can be turned off (set `UseOverlays` to `false`). They also only draw themselves on 32-bit displays.

* ObjectListViews' overlays now play nicer with MDI, but it's still not great. When an ObjectListView overlay is used within an MDI
  application, it doesn't crash any more, but it still doesn't handle overlapping windows. Overlays from one ObjectListView are
  drawn over other controls too. Current advice: don't use overlays within MDI applications.

* `F2` key presses are no longer silently swallowed.

* `ShowHeaderInAllViews` is better but not perfect. Setting it before the control is created or setting it
  to `true` work perfectly. However, if it is set to `false`, the primary checkboxes disappear! I could just ignore changes once
  the control is created, but it's probably better to let people change it on the fly and just document the idiosyncracies.

* Fixed bug in group sorting so that it actually uses `GroupByOrder` as it should always have done (thank to Michael Ehrt).

* Destroying the `ObjectListView` during an mouse event (for example, closing a form in a double click handler)
  no longer throws a "disposed object" exception.

12 October 2009 - Version 2.3
-----------------------------

This release focused on formatting -- giving programmers more opportunity to play with the appearance
of the `ObjectListView`.

Decorations
^^^^^^^^^^^

Decorations allow you to put pretty images, text and effects over the top of your `ObjectListView`.
Here the love heart and the "Missing!" are decorations.

.. image:: images/decorations-example.png

See this recipe :ref:`recipe-decorations` for more details.

Group header formatting
^^^^^^^^^^^^^^^^^^^^^^^

Groups have been overhauled for this release. Groups under XP remain unchanged, but under Vista
and Windows 7, many more formatting options are now available.

.. image:: images/group-formatting.png

See :ref:`recipe-groupformatting` for how to make pretty groups like this.

Hyperlinks
^^^^^^^^^^

`ObjectListViews` can now have cells that are hyperlinks.

.. image:: images/hyperlinks.png

See :ref:`recipe-hyperlink`.

Header formatting
^^^^^^^^^^^^^^^^^

The font and text color of the `ObjectListView` header can now be changed.
You can also word wrap the header text.

.. image:: images/header-formatting.png

See :ref:`recipe-headerformatting`.


.. _whats-new-format-events:

`FormatRow` and `FormatCell` events
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

In previous version, `RowFormatter` was the approved way to change the
formatting (font/text color/background color) of a row or cell. But it had some
limitations:

1. It did not play well with `AlternateBackgroundColors` property

2. It was called before the `OLVListItem` had been added to the
   `ObjectListView`, so many of its properties were not yet initialized.

3. It was painful to use it to format only one cell.

4. Perhaps most importantly, the programmer did not know where in the
   `ObjectListView` the row was going to appear so they could not implement more
   sophisticated versions of the row alternate background colors scheme.

To get around all these problems, there is now a `FormatRow` event. This is
called *after* the `OLVListItem` has been added to the control. Plus it has a
`DisplayIndex` property specifying exactly where the row appears in the list
(this is correct even when showing groups).

There is also a `FormatCell` event. This allows the programmer to easily format
just one cell.

See :ref:`recipe-formatter`.

`Generator`
^^^^^^^^^^^

By using compiler attributes, `ObjectListViews` can now be generated directly
from model classes. See :ref:`recipe-generator` for details and provisos.

[Thanks to John Kohler for this idea and the original implementation]

Groups on virtual lists
^^^^^^^^^^^^^^^^^^^^^^^

When running on Vista and later, virtual lists can now be grouped!

`FastObjectListView` supports grouping out of the box. For your own
`VirtualObjectListView` you must do some more work yourself.

See :ref:`recipe-virtualgroups` for details.

[This was more of a technical challenge for myself than something I thought would
be wildly useful. If you do actually use groups on virtual lists, please let me know]

Small changes
^^^^^^^^^^^^^

* Added `UseTranslucentSelection` property which mimics the selection
  highlighting scheme used in Vista. This works fine on Vista and on XP when the
  list is `OwnerDrawn`, but only moderately well when non-`OwnerDrawn`, since
  the native control insists on drawing its normal selection scheme, in addition
  to the translucent selection.

* Added `ShowHeaderInAllViews` property. When this is *true*, the header is
  visible in all views, not just *Details*, and can be used to control the sorting
  of items.

* Added `UseTranslucentHotItem` property which draws a translucent area over the
  top of the current hot item.

* Added `ShowCommandMenuOnRightClick` property which is *true* shows extra commands
  when a header is right clicked. This is *false* by default.

* Added `ImageAspectName` which the name of a property that will be invoked to
  get the image that should be shown on a column.
  This allows the image for a column to be retrieved
  from the model without having to install an `ImageGetter` delegate.

* Added `HotItemChanged` event and `Hot*` properties to allow programmers to
  perform actions when the mouse moves to a different row or cell.

* Added `UseExplorerTheme` property, which when *true* forces the `ObjectListView`
  to use the same visual style as the explorer. On XP, this does nothing, but on
  Vista it changes the hot item and selection mechanisms.
  Be warned: setting this messes up several other properties. See
  :ref:`recipe-vistascheme`.

* Added `OLVColumn.AutoCompleteEditor` which allows you to turn off auto-completion
  on cell editors.

* `OlvHitTest()` now works correctly even when `FullRowSelect` is *false*. There
  is a bug in the .NET `ListView` where `HitTest()` for a point that is in
  column 0 but not over the text or icon will fail (i.e. fail to recognize that
  it is over column 0). `OlvHitTest()` does not have that failure.

* Added `OLVListItem.GetSubItemBounds()` which correctly calculates the bounds
  of cell even for column 0. In .NET `ListView` the bounds of any subitem 0 are
  always the bounds of the whole row.

* Column 0 now follows its `TextAlign` setting, but only when `OwnerDrawn`. On a
  plain `ListView`, column 0 is always left aligned. ** This feature is
  experimental. Use it if you want. Don't complain if it doesn't work :) **

* Renamed `LastSortColumn` to be `PrimarySortColumn`, which better indicates its use.
  Similar `LastSortOrder` became `PrimarySortOrder`.

* Cell editors are no longer forcibly disposed after being used to edit a cell.
  This allows them to be cached and reused.

* Reimplemented `OLVListItem.Bounds` since the base version throws an exception
  if the given item is part of a collapsed group.

* Removed even token support for Mono.

* Removed `IncrementalUpdate()` method, which was marked as obsolete in February 2008.

4 August 2009 - Version 2.2.1
-----------------------------

This is primarily a bug fix release.

New features
^^^^^^^^^^^^

* Added cell events (`CellClicked`, `CellOver`, `CellRightClicked`).

* Made `BuildList()`, `AddObject()` and `RemoveObject()` thread-safe.

Bug fixes
^^^^^^^^^

* Avoided bug in .NET framework involving column 0 of owner drawn listviews not being redrawn when the listview was scrolled horizontally (this was a *lot* of work to track down and fix!)

* Subitem edit rectangles always allowed for an image in the cell, even if there was none. Now they only allow for an image when there actually is one.

* The cell edit rectangle is now correctly calculated when the listview is scrolled horizontally.

* If the user clicks/double clicks on a tree list cell, an edit operation will no longer begin if the click was to the left of the expander. This is implemented in such a way that other renderers can have similar "dead" zones.

* `CalculateCellBounds()` messed with the `FullRowSelect` property, which confused the tooltip handling on the underlying control. It no longer does this.

* The cell edit rectangle is now correctly calculated for owner-drawn, non-Details views.

* Space bar now properly toggles checkedness of selected rows.

* Fixed bug with tooltips when the underlying Windows control was destroyed.

* `CellToolTipShowing` events are now triggered in all views.

May 2009 - Version 2.2
----------------------

The two big features in this version are overlays and drag and drop support.

Drag and drop support
^^^^^^^^^^^^^^^^^^^^^

`ObjectListViews` now have sophisticated support for drag and drop operations.

An `ObjectListView` can be made a source for drag operations by setting the
`DragSource` property. Similarly, it can be made a sink for drop actions by
setting the `DropSink` property. These properties accept an `IDragSource`
interface and an `IDropSink` interface respectively. `SimpleDragSource` and
`SimpleDropSink` provide reasonable default implementations for these
interfaces.

Since the whole goal of `ObjectListView` is to encourage slothfulness, for most
simple cases, you can ignore these details and just set the `IsSimpleDragSource`
and `IsSimpleDropSink` properties to *true*, and then listen for `CanDrop` and
`Dropped` events.

See :ref:`dragdrop-label` for more details.

The `RearrangeableDropSink` class gives an `ObjectListView` the ability to be rearranged by dragging.
See :ref:`dragdrop-rearranging`.

Image and text overlays
^^^^^^^^^^^^^^^^^^^^^^^

`ObjectListView` now have the ability to draw translucent images and text over the top
over the `ObjectListView` contents. These overlays do not scroll when the list
contents scroll. These overlays works in all Views. You can set an overlays
within the IDE using the `OverlayImage` and `OverlayText` properties.

The overlay design is extensible, and you can add arbitrary overlays through the `AddOverlay()` method.

See :ref:`recipe-overlays` for more details.

The "list is empty" message is now implemented as an overlay, and as such is heavily customisable.
See :ref:`recipe-emptymsg` for details.

Other new features
^^^^^^^^^^^^^^^^^^

* The most requested feature ever -- collapsible groups -- is now available. But it is for Vista only. Thanks to Crustyapplesniffer for his implementation of this feature. Set the `HasCollapsibleGroups` to *false* if you don't want this on your `ObjectListView` (it is *true* by default).

* Added `SelectedColumn` property, which puts a slight tint over that column. When combined with the `TintSortColumn` and `SelectedColumnTint` properties, the sorted column will automatically be tinted with whatever colour you want.

* Added `Scroll` event (thanks to Christophe Hosten who implemented this)
* Made several properties localizable.
* The project no longer uses `unsafe` code, and can therefore be used in a limited trust environment.
* `TreeListView` now has `GetParent()` and `GetChildren()` methods to allow tree traversal. It also has a
  `DiscardAllState()` method to collapse all branches and forget everything about all model objects.

Bug fixes (not a complete list)
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

* Fix a long standing problem with flickering on owner drawn virtual lists. Apart from now being flicker-free, this means that grid lines no longer get confused, and drag-select no longer flickers. This means that TreeListView now has noticeably less flicker (it is always an owner drawn virtual list).

* Double-clicking on a row no longer toggles the checkbox (Why did MS ever include that?).
* Double-clicking on a checkbox no longer confuses the checkbox.
* Correctly renderer checkboxes when `RowHeight` is non-standard.
* Checkboxes are now visible even when the `ObjectListView` does not have a `SmallImageList`.
* `AlwaysGroupByColumn` and `SortGroupItemsByPrimaryColumn` now work correctly (without messing up the column header sort indicators).
* Several Vista-only bugs were fixed

3 February 2009 - Version 2.1
-----------------------------

Complete overhaul of owner drawing
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

In the same way that 2.0 overhauled the virtual list processing, this version
completely reworks the owner drawn rendering process. However, this overhaul
was done to be transparently backwards compatible.

The only breaking change is for owner drawn non-details views (which I doubt
that anyone except me ever used). Previously, the renderer on column 0 was
double tasked for both rendering cell 0 and for rendering the entire item in
non-detail view. This second responsibility now belongs explicitly to the
`ItemRenderer` property.

* Renderers are now based on `IRenderer` interface.
* Renderers are now Components and can be created, configured, and assigned within the IDE.
* Renderers can now also do hit testing.
* Owner draw text now looks like native ListView
* The text AND bitmaps now follow the alignment of the column. Previously only the text was aligned.
* Added `ItemRenderer` to handle non-details owner drawing
* Images are now drawn directly from the image list if possible. 30% faster than previous versions.

Other significant changes
^^^^^^^^^^^^^^^^^^^^^^^^^

* Added hot tracking
* Added checkboxes to subitems

* AspectNames can now be used as indexes onto the model objects -- effectively something like this: `modelObject[this.AspectName]`. This is particularly helpful for `DataListView` since `DataRows` and `DataRowViews` support this type of indexing.

* Added `EditorRegistry` to make it easier to change or add cell editors

Minor Changes
^^^^^^^^^^^^^

* Added `TriStateCheckBoxes`, `UseCustomSelectionColors` and `UseHotItem` properties
* Added `TreeListView.RevealAfterExpand` property
* Enums are now edited by a ComboBox that shows all the possible values.
* Changed model comparisons to use `Equals()` rather than `==`. This allows the model objects to implement their own idea of equality.
* `ImageRenderer` can now handle multiple images. This makes `ImagesRenderer` defunct.
* `FlagsRenderer<T>` is no longer generic. It is simply `FlagsRenderer`.
* Virtual ObjectListViews now trigger `ItemCheck` and `ItemChecked` events

Bug fixes
^^^^^^^^^

* `RefreshItem()` now correctly recalculates the background color
* Fixed bug with simple checkboxes which meant that `CheckedObjects` always returned empty.
* `TreeListView` now works when visual styles are disabled
* `DataListView` now handles boolean types better. It also now longer crashes when the data source is reseated.
* Fixed bug with `AlwaysGroupByColumn` where column header clicks would not resort groups.

10 January 2009 - Version 2.0.1
-------------------------------

This version adds some small features and fixes some bugs in 2.0 release.

New or changed features
^^^^^^^^^^^^^^^^^^^^^^^

* Added `ObjectListView.EnsureGroupVisible()`
* Added `TreeView.UseWaitCursorWhenExpanding` property
* Made all public and protected methods virtual so they can be overridden in subclasses. Within `TreeListView`, some classes were changed from internal to protected so that they can be accessed by subclasses
* Made `TreeRenderer` public so that it can be subclassed
* `ObjectListView.FinishCellEditing()`, `ObjectListView.PossibleFinishCellEditing()` and `ObjectListView.CancelCellEditing()` are now public
* Added `TreeRenderer.LinePen` property to allow the connection drawing pen to be changed

Bug fixes
^^^^^^^^^

* Fixed long-standing "multiple columns generated" problem. Thanks to pinkjones for his help with solving this one!
* Fixed connection line problem when there is only a single root on a `TreeListView`
* Owner drawn text is now rendered correctly when `HideSelection` is true.
* Fixed some rendering issues where the text highlight rect was miscalculated
* Fixed bug with group comparisons when a group key was null
* Fixed bug with space filling columns and layout events
* Fixed `RowHeight` so that it only changes the row height, not the width of the images.
* `TreeListView` now works even when it doesn't have a `SmallImageList`

30 November 2008 - Version 2.0
------------------------------

Version 2.0 is a major change to ObjectListView.

Major changes
^^^^^^^^^^^^^

* Added `TreeListView` which combines a tree structure with the columns on a `ListView`.
* Added `TypedObjectListView` which is a type-safe wrapper around an `ObjectListView`.
* Major overhaul of `VirtualObjectListView` to now use `IVirtualListDataSource`. The new version of `FastObjectListView` and the new `TreeListView` both make use of this new structure.
* `ObjectListView` builds to a DLL, which can then be incorporated into your .NET project. This makes it much easier to use from other .NET languages (including VB).
* Large improvement in `ListViewPrinter's` interaction with the IDE. All `Pens` and `Brushes` can now be specified through the IDE.
* Support for tri-state checkboxes, even for virtual lists.
* Support for dynamic tool tips for cells and column headers, via the `CellToolTipGetter` and `HeaderToolTipGetter` delegates respectively.
* Fissioned ObjectListView.cs into several files, which will hopefully makes the code easier to approach.
* Added many new events, including `BeforeSorting` and `AfterSorting`.
* Generate dynamic methods from AspectNames using `TypedObjectListView.GenerateAspectGetters()`. The speed of hand-written AspectGetters without the hand-written-ness. This is the most experimental part of the release. Thanks to Craig Neuwirt for his initial implementation.

Minor changes
^^^^^^^^^^^^^

* Added `CheckedAspectName` to allow check boxes to be gotten and set without requiring any code.
* Typing into a list now searches values in the sort column by default, even on plain vanilla `ObjectListViews`. The behavior was previously on available on virtual lists, and was turned off by default. Set `IsSearchOnSortColumn` to false to revert to v1.x behavior.
* Owner drawn primary columns now render checkboxes correctly (previously checkboxes were not drawn, even when `CheckBoxes` property was true).

Breaking changes
^^^^^^^^^^^^^^^^

* `CheckStateGetter` and `CheckStatePutter` now use only `CheckState`, rather than using both `CheckState` and `booleans`. Use `BooleanCheckStateGetter` and `BooleanCheckStatePutter` for behavior that is compatible with v1.x.
* `FastObjectListViews` can no longer have a `CustomSorter`. In v1.x it was possible, if tricky, to get a `CustomSorter` to work with a `FastObjectListView`, but that is no longer possible in v2.0 In v2.0, if you want to custom sort a FastObjectListView, you will have to subclass FastObjectListDataSource and override the SortObjects() method. See here for an example.

24 July 2008 - Version 1.13
---------------------------

Major changes
^^^^^^^^^^^^^

* Allow check boxes on `FastObjectListViews`. .NET's ListView cannot support
  checkboxes on virtual lists. We cannot get around this limit for plain
  `VirtualObjectListViews`, but we can for `FastObjectListViews`. This is a
  significant piece of work and there may well be bugs that I have missed. This
  implementation does not modify the traditional `CheckedIndicies`/`CheckedItems`
  properties, which will still fail. It uses the new `CheckedObjects` property as
  the way to access the checked rows. Once `CheckBoxes` is set on a
  `FastObjectListView`, trying to turn it off again will throw an exception.

* There is now a `CellEditValidating` event, which allows a cell editor to be
  validated before it loses focus. If validation fails, the cell editor will
  remain. Previous versions could not prevent the cell editor from losing focus.
  Thanks to Artiom Chilaru for the idea and the initial implementation.

* Allow selection foreground and background colors to be changed. Windows does
  not allow these colours to be customised, so we can only do these when the
  `ObjectListView` is owner drawn. To see this in action, set the
  `HighlightForegroundColor` and `HighlightBackgroundColor` properties and then
  set `UseCustomSelectionColors` to true.

* Added `AlwaysGroupByColumn` and `AlwaysGroupBySortOrder` properties, which
  force the list view to always be grouped by a particular column.

Minor improvements
^^^^^^^^^^^^^^^^^^

* Added `CheckObject()` and all its friends, as well as `CheckedObject` and `CheckedObjects` properties
* Added `LastSortColumn` and `LastSortOrder` properties.
* Made `SORT_INDICATOR_UP_KEY` and `SORT_INDICATOR_DOWN_KEY` public so they can be used to specify the image used on column headers when sorting.
* Broke the more generally useful `CopyObjectsToClipboard()` method out of `CopySelectionToClipboard()`. `CopyObjectsToClipboard()` could now be used, for example, to copy all checked objects to the clipboard.
* Similarly, building the column selection context menu was separated from showing that context menu. This is so external code can use the menu building method, and then make any modification desired before showing the menu. The building of the context menu is now handled by `MakeColumnSelectMenu()`.
* Added `RefreshItem()` to `VirtualObjectListView` so that refreshing an object actually does something.
* Consistently use copy-on-write semantics with `AddObject(s)/RemoveObject(s)` methods. Previously, if `SetObjects()` was given an `ArrayList` that list was modified directly by the Add/RemoveObject(s) methods. Now, a copy is always taken and modifying, leaving the original collection intact.

Bug fixes (not a complete list)
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

* Fixed a bug with `GetItem()` on virtual lists where the item returned was not always complete .
* Fixed a bug/limitation that prevented `ObjectListView` from responding to right clicks when it was used within a `UserControl` (thanks to Michael Coffey).
* Corrected bug where the last object in a list could not be selected via `SelectedObject`.
* Fixed bug in `GetAspectByName()` where chained aspects would crash if one of the middle aspects returned null (thanks to philippe dykmans).

10 May 2008 - Version 1.12
--------------------------

* Added `AddObject/AddObjects/RemoveObject/RemoveObjects` methods. These methods allow the programmer to add and remove specific model objects from the `ObjectListView`. These methods work on `ObjectListView` and `FastObjectListView`. They have no effect on `DataListView` and `VirtualObjectListView` since the data source of both of these is outside the control of the ObjectListView.
* Non detail views can now be owner drawn. The renderer installed for primary column is given the chance to render the whole item. See BusinessCardRenderer in the demo for an example. In the demo, go to the Complex tab, turn on Owner Drawn, and switch to Tile view to see this in action.
* BREAKING CHANGE. The signature of `RenderDelegate` has changed. It now returns a `boolean` to indicate if default rendering should be done. This delegate previously returned `void`. This is only important if your code used `RendererDelegate` directly. Renderers derived from `BaseRenderer` are unchanged.
* The `TopItemIndex` property now works with virtual lists
* `MappedImageRenderer` will now render a collection of values
* Fixed the required number of bugs:
* The column select menu will now appear when the header is right clicked even when a context menu is installed on the `ObjectListView`
* Tabbing while editing the primary column in a non-details view no longer tries to edit the new column's value
* When a virtual list that is scrolled vertically is cleared, the underlying
  `ListView` becomes confused about the scroll position, and incorrectly renders
  items after that. ObjectListView now avoids this problem.

1 May 2008 - Version 1.11
-------------------------

* Added `SaveState()` and `RestoreState()`. These methods save and restore the user modifiable state of an `ObjectListView`. They are useful for saving and restoring the state of your ObjectListView between application runs. See the demo for examples of how to use them.
* Added `ColumnRightClick` event
* Added `SelectedIndex` property
* Added `TopItemIndex` property. Due to problems with the underlying `ListView` control, this property has several quirks and limitations. See the documentation on the property itself.
* Calling `BuildList(true)` will now try to preserve scroll position as well as the selection (unfortunately, the scroll position cannot be preserved while showing groups).
* ObjectListView is now CLS-compliant
* Various bug fixes. In particular, ObjectListView should now be fully functional on 64-bit versions of Windows.

18 March 2008 - Version 1.10
----------------------------

* Added space filling columns. A space filling column fills all (or a portion) of the width unoccupied by other columns.
* Added some methods suggested by Chris Marlowe: `ClearObjects()`, `GetCheckedObject()`, `GetCheckedObjects()`, a flavour of `GetItemAt()` that returns the item and column under a point. Thanks for the suggestions, Chris.
* Added minimal support for Mono. To create a Mono version, compile with conditional compilation symbol "MONO". The Windows.Forms support under Mono is still a work in progress -- the listview still has some serious problems (I'm looking at you, virtual mode). If you do have success with Mono, I'm happy to include any fixes you might make (especially from Linux or Mac coders). Please don't ask me Mono questions.
* Fixed bug with subitem colors when using owner drawn lists and a `RowFormatter`.

2 February 2008 - Version 1.9.1
-------------------------------

* Added `FastObjectListView` for all impatient programmers.
* Added `FlagRenderer` to help with drawing bitwise-OR'ed flags (search for `FlagRenderer` in the demo project to see an example)
* Fixed the inevitable bugs that managed to appear:
* Alternate row colouring with groups was slightly off
* In some circumstances, owner drawn virtual lists would use 100% CPU
* Made sure that sort indicators are correctly shown after changing which columns are visible

16 January 2008 - Version 1.9
-----------------------------

* Added ability to have hidden columns, i.e. columns that the ObjectListView
  knows about but that are not visible to the user. This is controlled by
  `OLVColumn.IsVisible`. I added `ColumnSelectionForm` to the demo project to show
  how it could be used in an application. Also, right clicking on the column
  header will allow the user to choose which columns are visible. Set
  `SelectColumnsOnRightClick` to false to prevent this behaviour.

* Added `CopySelectionToClipboard()` which pastes a text and HTML representation
  of the selected rows onto the Clipboard. By default, this is bound to Ctrl-C.

* Added support for checkboxes via `CheckStateGetter` and `CheckStatePutter`
  properties. See `ColumnSelectionForm` for an example of how to use.

* Added `ImagesRenderer` to draw more than one image in a column.

* Made `ObjectListView` and `OLVColumn` into partial classes so that others can
  extend them.

* Added experimental `IncrementalUpdate()` method, which operates like
  `SetObjects()` but without changing the scrolling position, the selection, or
  the sort order. And it does this without a single flicker. Good for lists that
  are updated regularly. [Better to use a `FastObjectListView` and the `Objects`
  property]

* Fixed the required quota of small bugs.

30 November 2007 - Version 1.8
------------------------------

* Added cell editing -- so easy to say, so much work to do
* Added `SelectionChanged` event, which is triggered once per user action regardless of how many items are selected or deselected. In comparison, `SelectedIndexChanged` events are triggered for every item that is selected or deselected. So, if 100 items are selected, and the user clicks a different item to select just that item, 101 SelectedIndexChanged events will be triggered, but only one SelectionChanged event. Thanks to lupokehl42 for this suggestion and improvements.
* Added the ability to have secondary sort column used when the main sort column gives the same sort value for two rows. See `SecondarySortColumn` and `SecondarySortOrder` properties for details. There is no user interface for these items -- they have to be set by the programmer.
* `ObjectListView` now handles `RightToLeftLayout` correctly in owner drawn mode, for all you users of Hebrew and Arabic (still working on getting `ListViewPrinter` to work, though). Thanks for dschilo for his help and input.

13 November 2007 - Version 1.7.1
--------------------------------

* Fixed bug in owner drawn code, where the text background color of selected items was incorrectly calculated.
* Fixed buggy interaction between `ListViewPrinter` and owner drawn mode.

7 November 2007 - Version 1.7
-----------------------------

* Added ability to print `ObjectListViews` using `ListViewPrinter`.

30 October 2007 - Version 1.6
-----------------------------

Major changes
^^^^^^^^^^^^^

* Added ability to give each column a minimum and maximum width (set the minimum
  equal to the maximum to make a fixed-width column). Thanks to Andrew Philips for
  his suggestions and input.

* Complete overhaul of `DataListView` to now be a fully functional, data-
  bindable control. This is based on Ian Griffiths' excellent example, which
  should be available here__, but unfortunately seems to have disappeared from the
  Web. Thanks to ereigo for significant help with debugging this new code.

* Added the ability for the listview to display a "this list is empty"-type
  message when the ListView is empty (obviously). This is controlled by the
  `EmptyListMsg` and `EmptyListMsgFont` properties. Have a look at the "File
  Explorer" tab in the demo to see what it looks like.

.. __: http://www.interact-sw.co.uk/utilities/bindablelistview

Minor changes
^^^^^^^^^^^^^

* Added the ability to preserve the selection when `BuildList()` is called. This is on by default.
* Added the `GetNextItem()` and `GetPreviousItem()` methods, which walk sequentially through the ListView items, even when the view is grouped (thanks to eriego for the suggestion).
* Allow item count labels on groups to be set per column (thanks to cmarlow for the idea).
* Added the `SelectedItem` property and the `GetColumn()` and `GetItem()` methods.
* Optimized aspect-to-string conversion. `BuildList()` is 15% faster.
* Corrected the bug with the custom sorter in `VirtualObjectListView` (thanks to mpgjunky).
* Corrected the image scaling bug in `DrawAlignedImage()` (thanks to krita970).
* Uses built-in sort indicators on Windows XP or later (thanks to gravybod for sample implementation).
* Plus the requisite number of small bug fixes.

3 August 2007 - Version 1.5
---------------------------

* `ObjectListViews` now have a `RowFormatter` delegate. This delegate is called whenever a `ListItem` is added or refreshed. This allows the format of the item and its sub-items to be changed to suit the data being displayed, like red colour for negative numbers in an accounting package. The DataView tab in the demo has an example of a `RowFormatter` in action. Include any of these words in the value for a cell and see what happens: red, blue, green, yellow, bold, italic, underline, bk-red, bk-green. Be aware that using RowFormatter and trying to have alternate coloured backgrounds for rows can give unexpected results. In general, `RowFormatter` and `UseAlternatingBackColors` do not play well together.
* `ObjectListView` now has a `RowHeight` property. Set this to an integer value and the rows in the `ListView` will be that height. Normal `ListViews` do not allow the height of the rows to be specified; it is calculated from the size of the small image list and the ListView font. The `RowHeight` property overrules this calculation by shadowing the small image list. This feature should be considered highly experimental. One known problem is that if you change the row height while the vertical scroll bar is not at zero, the control's rendering becomes confused.
* Animated GIF support: if you give an animated GIF as an `Image` to a column that has `ImageRenderer`, the GIF will be animated. Like all renderers, this only works in `OwnerDrawn` mode. See the DataView tab in the demo for an example.
* Sort indicators can now be disabled, so you can put your own images on column headers.
* Better handling of item counts on groups that only have one member: thanks to cmarlow for the suggestion and sample implementation.
* The obligatory small bug fixes.

30 April 2007 - Version 1.4
---------------------------

* Owner drawing and renderers.
* `ObjectListView` now supports all ListView.View modes, not just Details. The tile view has its own support built in.
* Column headers now show sort indicators.
* Aspect names can be chained using a "dot" syntax. For example, Owner.Workgroup.Name is now a valid `AspectName`. Thanks to OlafD for this suggestion and a sample implementation.
* `ImageGetter` delegates can now return ints, strings or Image objects, rather than just ints as in previous versions. ints and strings are used as indices into the image lists. Images are only shown when in OwnerDrawn mode.
* Added `OLVColumn.MakeGroupies()` to simplify group partitioning.

5 April 2007 - Version 1.3
--------------------------

* Added `DataListView`.
* Added `VirtualObjectListView`.
* Added `Freeze()`/`Unfreeze()`/`Frozen` functionality.
* Added ability to hand off sorting to a `CustomSorter` delegate.
* Fixed bug in alternate line coloring with unsorted lists: thanks to cmarlow for finding this.
* Handle null conditions better, e.g. `SetObjects(null)` or having zero columns.
* Dumbed-down the sorting comparison strategy. Previous strategy was classic overkill: user extensible, handles every possible situation and unintelligible to the uninitiated. The simpler solution handles 98% of cases, is completely obvious and is implemented in 6 lines.

5 January 2007 - Version 1.2
----------------------------

* Added alternate line colors.
* Unset sorter before building list. 10x faster! Thanks to aaberg for finding this.
* Small bug fixes.

26 October 2006 - Version 1.1
-----------------------------

* Added "Data Unaware" and "IDE Integration" article sections.
* Added model-object-level manipulation methods, e.g. `SelectObject()` and `GetSelectedObjects()`.
* Improved IDE integration.
* Refactored sorting comparisons to remove a nasty if...else cascade.

14 October 2006 - Version 1.0
-----------------------------
