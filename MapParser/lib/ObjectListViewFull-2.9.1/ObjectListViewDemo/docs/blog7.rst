.. -*- coding: UTF-8 -*-

:Subtitle: Taking care of the hierarchy
	
.. _blog-hierarchicalcheckboxes:

Hierarchy-aware checkboxes
==========================

2014-02-26

Hierarchical checkboxes is that neat ability where the checked-ness of a branch summarizes the checked-ness of all its subitems. If the branch is checked, you know that everything under that branch is checked. If the branch is unchecked, then similarly everything under that branch is unchecked. If the branch is indeterminate, you know that there is a mix of checked and unchecked items within that branch.

Logic wise, the task is simple. When a TreeListView is first shown, examine all subitems recursively of every top level branch: if all the subitems (recursively) are checked, the branch should be checked; if all the subitems (again, recursively) are unchecked, the branch should be unchecked. Otherwise, the branch is indeterminate. Once displayed, when the user checks a branch, check all items recursively under that branch. When the user unchecks a branch, uncheck all the subitems. 

For small trees, this would work fine. But for large trees, that would be terrible. In the ObjectListView demo, there is a `TreeListView` that browses the C: drive. On my dev machine, to determine if the top level branch should be checked or not, the control would have to traverse more than 600,000 files in 80,000 directories! Checking or unchecking the c: branch would require checking/unchecking about 700,000 subitems. It doesn't matter how fast your computer is, processing that many items is going to take a good chunk of time. Worse, all the processing is UI related, so it's going to happen on the UI thread, freezing the interface while it's going on. We have to find a better way.

Break the problem into two parts: deciding if a branch is checked or not, marking a branch as checked or unchecked.

One major problem is that we don't know the checkedness of all the subitems. When an ObjectListView has a `CheckStateGetter` installed, the only way we can know if an item is checked is by calling the `CheckStateGetter` on that item. We can't reason about what is checked or unchecked -- we always have to ask. In our disk browser example, we would have to ask all 700,000 items if it was checked. That's never going to work, so with hierarchical checkboxes, we don't allow `CheckStateGetters` to be installed.

If nothing is checked, the problem is simple.

If the complete sets of checked items is given to the control (setting the `CheckedObjects` property), we can solve the problem:

#. Clear the checkedness of all items.
#. Check all the given items
#. For each ancestor of the given items, calculate the ancestors checkedness by looking at the checkedness of its immediate subitems.

To calculate the ancestors of an item, we need a new delegate: `ParentGetter`. This is only used when using hierarchical checkboxes.

Normally, the TreeListView calculates the checkedness of an item just before it is displayed. But for hierarchical checkboxes, we need to know that checkedness of each subitem to know the checkedness of a branch.

Hierarchy and `CheckedObjects`
------------------------------

The next problem is how does the `TreeListView` tell the programmer which objects have been checked. This is done through the `CheckedObjects` collection. This collection will return:

* all objects which were specifically checked by the user
* all objects that were set in the `CheckedObjects` collection, and that have not been unchecked by the user
* all objects whose ancestor was checked by the user AND that have been made visible in the control

What happens if you check an object that doesn't exist in the tree? Should it still be returned in `CheckedObjects`? 

Yes. There is no easy way for the control to know the difference between an object that doesn't exist and an object that hasn't yet been revealed. So, if an object is checked (either through calling `CheckObject()` or through setting `CheckedObjects`), it will be returned in `CheckedObjects` property.

Filtering
---------

Filtering and `TreeListView` is always tricky. 

Hierarchical checkboxes ignores filtering. If the user checks a parent, all descendents will also be checked, regardless of whether or not they pass any installed filtering.
