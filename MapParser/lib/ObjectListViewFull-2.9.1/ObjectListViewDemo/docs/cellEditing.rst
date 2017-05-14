.. -*- coding: UTF-8 -*-

:Subtitle: How to edit the cell values in an ObjectListView

.. _cell-editing-label:

ObjectListView Cell Editing
===========================

ListViews are normally used for displaying information. The standard ListView
allows the value at column 0 (the primary cell) to be edited, but nothing beyond
that. ObjectListView allows all cells to be edited. Depending on how the data
for a cell is sourced, the edited values can be automagically written back into
the model object.

Starting a cell edit
--------------------

The "editability" of an ObjectListView is controlled by the `CellEditActivation`
property. This property can be set to one of the following values:

* `CellEditActivateMode.None`
   Cell editing is not allowed on the control This is the default.

* `CellEditActivateMode.SingleClick`
   Single clicking on any subitem cell begins an edit operation on that cell.
   Single clicking on the primary cell does *not* start an edit operation.
   It simply selects the row. Pressing :kbd:`F2` edits the primary cell.

* `CellEditActivateMode.SingleClickAlways`
   Single clicking on any cell begins an edit operation on that cell,
   even the primary cell (unlike this mode above).

* `CellEditActivateMode.DoubleClick`
   Double clicking any cell starts an edit operation on that cell, including
   the primary cell. Pressing :kbd:`F2` edits the primary cell.

* `CellEditActivateMode.F2Only`
   Pressing :kbd:`F2` edits the primary cell. :kbd:`Tab`/:kbd:`Shift-Tab` can be used to
   edit other cells. Clicking does not start any editing.

Individual columns can be marked as editable via the `IsEditable` property
(default value is True), though this only has meaning once the `ObjectListView`
itself is editable. If you know that the user should not be allowed to change
cells in a particular column, set `IsEditable` to False. Be aware, though, that
this may create some surprises, resulting in user complaints like "How come I
can't edit this value by clicking on it like I can on all the other cells?".

Once a cell editor is active, the normal editing conventions apply:

    * :kbd:`Enter` or :kbd:`Return` finishes the edit and commits the new value to the model object.
    * :kbd:`Escape` cancels the edit.
    * :kbd:`Tab` commits the current edit, and starts a new edit on the next editable cell.
    * :kbd:`Shift-Tab` commits the current end, and starts a new edit on the previous
      editable cell.


Deciding on a cell editor
-------------------------

When a cell is to be edited, we need to decide what sort of editor to use.

There are two ways this decision can be made:

1. Registry based decision [v2.1]

In general, editors are created based on the type of value in the cell. Deciding what
editor to use based on the type of the value is the responsibility of the `EditorRegistry`.

Without extra work, the EditorRegistry knows how to edit booleans, integers (signed
and unsigned of all sizes), floats, doubles, DateTime, and strings. It also handles all
flavours of enums.

If you wish to edit a different type of object (or change the editor for one of the
above types), you can do this through `EditorRegistry.Register()` methods.

For example, there is no standard editor for a Color. To handle the editing of
colours, we would need a Control which can edit an instance of Color, and then to register it with the
`EditorRegistry`. Which migh look something like this::

    ObjectListView.EditorRegistry.Register(type(Color), type(SuperColourEditor));

You can also register a delegate with the registry to give you more flexibility when
configuring what exact control to use for a given value::

    ObjectListView.EditorRegistry.Register(typeof(DateTime), delegate(Object model, OLVColumn column, Object value) {
        DateTimePicker c = new DateTimePicker();
        c.Format = DateTimePickerFormat.Short;
        return c;
    });

2. Event based decision

If you want something more surgical, you can have complete control over the
process by listening for a cell editing starting event, CellEditStarting. Within
the handler for this event, the programmer can create and configure any sort of
widget they like and then return this widget via the `Control` property of the
event.

See `How Can You Customise The Editing`_ for more details.


How are Cells Edited
--------------------

Once the cell editor has been created, it is given the cell's value via
the controls `Value` property (if it has one and it is writable). If it doesn't
have a writable `Value` property, its `Text` property will be set with a text
representation of the cells value.

When the user has finished editing the value in the cell, the new value will be
written back into the model object (if possible). To get the modified value, the
default processing tries to use the `Value` property again. It that doesn't work,
the `Text` property will be used instead.

This use of `Value` and `Text` properties applies to custom editor (created by event
handlers) as well to the standard ones.

Updating the Model Object
-------------------------

Once the user has entered a new value into a cell and pressed Enter, the
ObjectListView tries to store the modified value back into the model object.
There are three ways this can happen:

1. CellEditFinishing Event Handler

You can create an event handler for the `CellEditFinishing` event (see below). In
that handler, you would write the code to get the modified value from the
control, put that new value into the model object, and set `Cancel` to true so
that the `ObjectListView` knows that it doesn't have to do anything else. You will
also need to call at least `RefreshItem()` or `RefreshObject()`, so that the changes
to the model object are shown in the `ObjectListView`.

There are cases where this is necessary, but as a general solution, it doesn't
fit my philosophy of slothfulness.

2. AspectPutter Delegate

You can install an `AspectPutter` delegate on the corresponding `OLVColumn`. If this
delegate is installed, it will be invoked with the model object and the new
value that the user entered. This is a neat solution.

3. Writable AspectName Property

If the columns `AspectName` is the name of a writable property, the
`ObjectListView` will try to write the new value into that property. This requires
no coding and certainly qualifies as the most slothful solution. But it only
works if `AspectName` contains the name of a writable property. If the `AspectName`
is dotted (e.g. "Owner.Address.Postcode") only the last property needs to be
writable.

If none of these three things happen, the user's edit will be discarded. The
user will enter her or his new value into the cell editor, press :kbd:`Enter`, and the
old value will be still be displayed. If it seems as if the user cannot update a
cell, check to make sure that one of the three things above is occurring.

How Can You Customise The Editing
---------------------------------

To do something other than the default processing, you can listen for three
events: `CellEditStarting`, `CellEditValidating` and `CellEditFinishing`.

CellEditStarting event
^^^^^^^^^^^^^^^^^^^^^^

The `CellEditStarting` event is triggered after the user has requested to edit a
cell but before the cell editor is placed on the screen. This event passes a
`CellEditEventArgs` object to the event handlers. In the handler for this event,
if you set `e.Cancel` to True , the cell editing operation will not begin. If you
don't cancel the edit operation, you will almost certainly want to play with the
`Control` property of `CellEditEventArgs`. You can use this to customise the default
editor, or to replace it entirely.

For example, if your `ObjectListView` is showing a `Color` in a cell, there is no
default editor to handle a `Color`. You could make your own `ColorCellEditor`, set
it up correctly, and then set the `Control` property to be your color cell editor.
The `ObjectListView` would then use that control rather than the default one. If
you do this, you must fully configure your control, since the `ObjectListView`
will not do any further configuration of the editor. So, to listen for the
event, you would do something like this::

    this.myObjectListView.CellEditStarting += new CellEditEventHandler(this.HandleCellEditStarting);

And your handler method might look something like this::

    private void HandleCellEditStarting(object sender, CellEditEventArgs e) {
        if (e.Value is Color) {
            ColorCellEditor cce = new ColorCellEditor();
            cce.Bounds = e.CellBounds;
            cce.Value = e.Value;
            e.Control = cce;
        }
    }

With this code in place, your spiffy `ColorCellEditor` will be shown whenever the
user tries to edit a color in your `ObjectListView`.

CellEditValidating event
^^^^^^^^^^^^^^^^^^^^^^^^

The `CellEditValidating` event is triggered when the user tries to leave the cell editor.

CellEditFinishing event
^^^^^^^^^^^^^^^^^^^^^^^

When the user wants to finish the edit operation, a `CellEditFinishing` event is
triggered. If the user has cancelled the edit (e.g. by pressing :kbd:`Escape`), the
`Cancel` property will already be set to True. In that case, you should simply
cleanup without updating any model objects. If the user hasn't cancelled the
edit, you can by setting `Cancel` to True -- this will force the `ObjectListView` to
ignore any value that the user has entered into the cell editor.

No prizes for guessing that you can refer to the `Control` property to extract the
value that the user has entered and then use that value to do whatever you want.
During this event, you should also undo any event listening that you have setup
during the `CellEditStarting` event.

You can look in the demo at `listViewComplex_CellEditStarting()`,
`listViewComplex_CellEditValidating()` and `listViewComplex_CellEditFinishing()` to
see an example of handling these events.
