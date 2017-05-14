.. -*- coding: UTF-8 -*-

:Subtitle: Separating the wheat from the chaff

.. _filtering-label:

Filtering
=========

[v2.4] `ObjectListView` supports filtering on lists.

To enable filtering
on a list, you must set `UseFiltering` to `true`. To ensure backward
compatibility, it is `false` by default.

Once filtering is enabled, `ObjecListView` supports two sorts of filtering:
  (1) whole list filtering;
  (2) model filtering.

Whole list filtering
--------------------

Whole list filtering is useful for filtering that applies to the entire list.
Tail and head filtering are classic examples of such filtering. If you have a log file
viewer and you only want to show the last 500 lines, you can set a filter
to simply show the last 500 objects::

   this.olvLogView.ListFilter = new TailFilter(500);

Since this filter operates on the list as a whole, it runs quickly. By cutting down
the number of objects shown in the list, it can speed up the list considerably.

Model based filtering
---------------------

Model filtering considers each object in turn and decides if it should be included.
Because it considers every object individually, it is slower than a whole list filter,
but it is still fast enough for normal use. On my mid-range laptop, model filtering on
a list of 50,000 objects still has a sub-second response time.

Implementing a filter
---------------------

You can make your own filters by implmenting the `IModelFilter` or `IListFilter`
interface, or by subclassing `AbstractModelFilter` or `AbstractListFilter`.
These interfaces are just about as simple as they could be::

    public interface IModelFilter
    {
        bool Filter(object modelObject);
    }

    public interface IListFilter
    {
        IEnumerable Filter(IEnumerable modelObjects);
    }

If you only wanted to show emergency calls, you could make a filter like this::

    public class OnlyEmergenciesFilter : IModelFilter
    {
        public bool Filter(object modelObject) {
            return ((PhoneCall)x).IsEmergency;
        }
    }
    ...
    this.olv1.ModelFilter = new OnlyEmergenciesFilter();

Rather than subclassing, you will often be able to use an instance of `ModelFilter` directly.
This accepts a delegate that decides if the given model should be included. Again, to only
show emergency calls, you could install a filter like this::

   this.olv1.ModelFilter = new ModelFilter(delegate(object x) {
       return ((PhoneCall)x).IsEmergency;
   });

To remove a filter, simply set `ModelFilter` (or `ListFilter`) to `null`.

Filters and virtual lists
-------------------------

As always, since virtual lists keep their data to themselves, `ObjectListView` cannot filter
their contents directly. However, if the `VirtualListDataSource` behind the virtual list
implements the new `IFilterableDataSource` interface, then the virtual list can be filtered too.

The `IFilterableDataSource` is very simple::

    public interface IFilterableDataSource
    {
        /// <summary>
        /// All subsequent retrievals on this data source should be filtered
        /// through the given filters. null means no filtering of that kind.
        /// </summary>
        void ApplyFilters(IModelFilter modelFilter, IListFilter listFilter);
    }

The data source should store the given filters, and apply them to all subsequent operations.

Filtering and TreeListViews
---------------------------

Filtering and `TreeListViews` interact in a predictable but perhaps unexpected fashion.

Filtering considers only rows that are currently exposed (that is, all their ancestorss
are expanded).

Within those rows, rows will be included by the filtering process if they **or any of their
descendents** will be included by the filtering. (Yes, this is recursive). If a bottom level
child matches the filtering criteria, then all its ancestors will be considered to have
matched as well and thus will be shown in the control.

In the majority of situations, this gives the most predictable and useful visual results.

Filtering and RefreshObject()
-----------------------------

`RefreshObject()` does *not* change the "filtered-ness" of an object: an object that was
visible before `RefreshObject()` will still be visible after, and an object that was
filtered before `RefreshObject()` was called will still be filtered afterwards.

There are a main reason for this behaviour is that `RefreshObject()` has always had
the contract that it will not rebuild, resort or regroup the list. If the filtered-ness
of an object changes, `RefreshObject()` would have to break one or more of these
conditions.

1. For a grouped list, `RefreshObject()` may have to create a new group or remove an
   existing group. This would break the "no rebuild"
   and "no regrouping" contract.

2. If there is a `ListFilter` installed, `RefreshObject()` would have to rebuild the whole list.

3. In a `TreeListView`, the newly hidden (or revealed) object might be a deeply nest child row,
   which would require all the parents to also be hidden or revealed, again breaking the contracts.

If you make a change to model objects and want filters to be reconsidered for them, you have
to reapply the filters (you can just assign the same filter again). `RefreshObject()` will not work.

.. _column-filtering-label:

Excel-like Filtering
--------------------

[2.5] ObjectListView provides an Excel-like user interface to allow users to dynamically create
filters. The user can choose which values should be matched by the filter, and ObjectListView
will create and install a filter to do just that.

.. image:: images/excel-filtering.png


Selecting "Clear all filtering" removes all column-based filtering from the control.

Deselecting all clusters effectively removes any filtering associated with the column.

When the user chooses an Excel filter, any previously installed `ModelFilter` will be replaced.
If the programmer wants their filter to be combined with the user chosen Excel filter, the
programmer should set the `AdditionalFilter` property instead of the `ModelFilter`.

Turning off
^^^^^^^^^^^

To disable this ability, set `ObjectListView.ShowFilterMenuOnRightClick` to *false*.

To hide the 'Filter' menu item for a particular column, set `UsesFiltering` to *false* on that column.

To clear all installed Excel-like filters, use `ObjectListView.ResetColumnFiltering()`.

Changing the clustering
^^^^^^^^^^^^^^^^^^^^^^^

"Clustering" is the process of grouping the model objects into sets, where each set
shares the same data value for the attribute of interest.

Not very clear? Let's try an example.

Imagine we have a column that shows the birthdate of a collection of people.
The clustering for that column might put all the people with a birthday in January into
one set, all those with a birthday in February into a different set, and so on.

==========   ================================
Person       Birthdate
==========   ================================
Alex         DateTime(1981, February, 29)
Bob          DateTime(1979, January, 29)
Fred         DateTime(1970, August, 12)
Gerald       DateTime(1977, August, 27)
Margy        DateTime(1947, February, 7)
Phillip      DateTime(1969, January, 24)
==========   ================================

would produce these clusters:

===========  =============================  ============= ================
Cluster      Key                            Label         Members
===========  =============================  ============= ================
1            DateTime(1900, January, 1)     "January"     "Phillip, "Bob"
2            DateTime(1900, February, 1)    "February"    "Alex", "Margy"
3            DateTime(1900, August, 1)      "August"      "Fred", "Gerald"
===========  =============================  ============= ================


The filtering is controlled by the `ClusteringStrategy` that is installed on a column.
By default, the clustering strategy copies the grouping behaviour of that column. To change
this, you must set `ClusteringStrategy` to a strategy that does what you want.

To create your own strategy, you must implement `IClusteringStrategy` or subclass
the safe base class, `ClusteringStrategy`.

Date/Time Clustering
^^^^^^^^^^^^^^^^^^^^

If you are showing dates or times in a column, an instance of `DateTimeClusteringStrategy`
can probably be configured to do exactly what you want.


Customising the menu
^^^^^^^^^^^^^^^^^^^^

The menu is constructed by an instance of `FilterMenuBuilder`.

As it stands, there are two small customisations you can make to its behaviour:

   1. You can change the treatment
   of nulls. By setting `TreatNullAsDataValue` to *true* (the default), *null* will be treated
   as a data value and listed as one of the selectable clusters. If this is *false*, *null*
   will be treated as "Not a real value" and that object will not be included in clusters.

   2. You can specify how many rows the menu builder should consider when constructing
   the clusters for the menu. The property `MaxObjectsToConsider` gives an upper limit
   on the number of model objects that will be considered when constructing the clusters.
   The default is 10,000 -- which should be more than enough for most applications.

You can localize the strings used in the Filter menu setting the various static properties
on the `FilterMenuBuilder` class::

    FilterMenuBuilder.SELECT_ALL_LABEL = "Selecionar tudo";

To change the way the
menu looks or works, you must subclass `FilterMenuBuilder` and then install an instance
of your own class as the builder on your ObjectListView::

    public class MyFilterMenuBuilder : FilterMenuBuilder {
	    // override whatever methods you like
    }

Once you have defined your menu builder, you need to assign it to your ObjectListView::

	this.olv1.FilterMenuBuilder = new MyFilterMenuBuilder();
