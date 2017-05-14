using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ObjectListViewDemo.Models;
using ObjectListViewDemo.Properties;

namespace ObjectListViewDemo {
    public partial class TabSimpleExample : OlvDemoTab {

        public TabSimpleExample() {
            InitializeComponent();
            this.ListView = this.olvSimple;

            // If you wanted the lists checkboxes to behave like radio buttons, you could do something like this:

//            this.ListView.ItemChecked += delegate(object sender, ItemCheckedEventArgs e) {
//                OLVListItem item = e.Item as OLVListItem;
//                if (item != null && e.Item.Checked) {
//                    ArrayList objects = ObjectListView.EnumerableToArray(olvSimple.Objects, true);
//                    objects.Remove(item.RowObject);
//                    olvSimple.UncheckObjects(objects);
//                }
//            };
        }

        protected override void InitializeTab() {
            this.comboBoxEditable.SelectedIndex = 0;

            // Uncomment this to allow editing of salary column
//            SetupEditingOnSalary();

            // Uncomment this to see a fancy cell highlighting while editing
//            SetupEditDecoration();

            // Uncomment to see debug output as the mouse moves
//            SetupMouseMoveTracking();

            // Uncomment to see how to do per-cell formatting
//            SetupFormatCellHandling();

            // Uncomment to allow drag and drop when grouped
//            SetupDragToGroup();

            // Just one line of code make everything happen.
            this.olvSimple.SetObjects(Coordinator.PersonList);
        }

        private void SetupEditingOnSalary() {
            // The Salary column is read-only since its value comes from a method call -- GetRate() --
            // rather than a settable property.

            this.columnHeaderSalaryRate.IsEditable = true;

            // To allow it to be edited, it some specific code to put the new value back into the model
            this.columnHeaderSalaryRate.AspectPutter = delegate(object model, object newValue) {
                Person p = (Person)model;
                p.SetRate((double)newValue);
            };

            // We could also use a TypedColumn to add type safety
//            TypedColumn<Person> tcol = new TypedColumn<Person>(this.columnHeaderSalaryRate);
//            tcol.AspectPutter = delegate(Person x, object newValue) { x.SetRate((double) newValue); };
        }

        private void SetupEditDecoration() {
            this.olvSimple.AddDecoration(new EditingCellBorderDecoration(true));
        }

        private void SetupMouseMoveTracking() {
            // Print debug messages as the mouse moves.
            // This uses MouseHover to lessen the flood of output
            this.olvSimple.MouseHover += delegate(object sender, EventArgs e) {
                OLVColumn column;
                Point pt = this.olvSimple.PointToClient(Cursor.Position);
                OLVListItem item = this.olvSimple.GetItemAt(pt.X, pt.Y, out column);
                System.Diagnostics.Debug.WriteLine(String.Format("Hover: {0}, {1}", item, column));
            };
        }

        private void SetupFormatCellHandling() {
            // Need to indicate that we want CellFormat events
            this.olvSimple.UseCellFormatEvents = true;

            // We will format cells in the "Cooking Skill" column, such that people with
            // a rating of < 10 will get a red-ish background, whilst those with a rating
            // of >= 40 will get a greenish background.
            this.olvSimple.FormatCell += delegate(object sender, FormatCellEventArgs args) {

                // CellFormat is triggered for *every* cell. You must filter for only the column(s) you want
                if (args.Column.Text != "Cooking Skill")
                    return;

                // We will get the cooking skill rating from the model directly. 
                // We could also use args.CellValue.
                Person p = args.Model as Person;
                if (p == null)
                    return;

                if (p.CulinaryRating < 10)
                    args.SubItem.BackColor = Color.PaleVioletRed;
                if (p.CulinaryRating >= 40)
                    args.SubItem.BackColor = Color.PaleGreen;
            };
        }

        private void SetupDragToGroup() {

            // Indicate that the OLV supports rows being dragged out
            this.olvSimple.IsSimpleDragSource = true;

            // Indicate that the OLV supports items being dropped on it. 
            this.olvSimple.IsSimpleDropSink = true;

            // To do drag and drop, you must handle two events:
            //     - ModelCanDrop -- Can the dragged items be dropped where they currently are
            //     - ModelDropped -- The models were dropped, do whatever you want
            // These events are triggered when the drag starts in another OLV. For drags
            // that start elsewhere, you should listen for CanDrop and Dropped.

            this.olvSimple.ModelCanDrop += delegate(object sender, ModelDropEventArgs e) {
                e.Effect = DragDropEffects.None;

                // Only allow dropping if we are grouped on the cooking column
                // Setting the InfoMessage property gives the user some feedback while dragging
                if (!olvSimple.ShowGroups || olvSimple.PrimarySortColumn != olvSimpleCookingColumn) {
                    e.InfoMessage = olvSimple.ShowGroups
                        ? "Group by Cooking Skill column to see dragging working"
                        : "Turn on groups and group by Cooking Skill column to see dragging working";
                    return;
                }

                Person modelUnderDrag = e.TargetModel as Person;
                if (modelUnderDrag != null && (
                    e.DropTargetLocation == DropTargetLocation.Item ||
                    e.DropTargetLocation == DropTargetLocation.AboveItem ||
                    e.DropTargetLocation == DropTargetLocation.BelowItem)) {
                    e.InfoMessage = "Drop here to move to the same cooking skill group as " + modelUnderDrag.Name;
                    e.Effect = DragDropEffects.Move;
                }
            };

            this.olvSimple.ModelDropped += delegate(object sender, ModelDropEventArgs e) {
                // What item was dropped on?
                Person target = e.TargetModel as Person;
                if (target == null)
                    return;

                // Since groups are calculated, there isn't a direct way to move models into a particular group.
                // The right way to do it is to change the data (culinary rating, in this case) of the target
                // so that those targets now belong to the desired group, and then rebuild the list.
                foreach (Person source in e.SourceModels)
                    source.CulinaryRating = target.CulinaryRating;

                olvSimple.BuildList(true);
            };
        }


        #region UI event handling

        private void buttonRebuild_Click(object sender, EventArgs e) {
            Coordinator.TimedRebuildList(this.ListView);
        }

        private void textBoxFilterSimple_TextChanged(object sender, EventArgs e) {
            Coordinator.TimedFilter(this.ListView, ((TextBox)sender).Text);
        }

        private void buttonCopy_Click(object sender, EventArgs e) {
            this.ListView.CopyObjectsToClipboard(this.ListView.SelectedObjects);
        }

        private void buttonRemove_Click(object sender, EventArgs e) {
            this.ListView.RemoveObjects(this.ListView.SelectedObjects);
        }

        private void buttonAdd_Click(object sender, EventArgs e) {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) {
                // Add a visible and invisible column at the same time
                this.olvSimple.AllColumns.Add(new OLVColumn("New column: " + Environment.TickCount, null));
                OLVColumn col = new OLVColumn("Second new: " + Environment.TickCount, null);
                col.IsVisible = false;
                this.olvSimple.AllColumns.Add(col);
                this.olvSimple.RebuildColumns();
            } else {
                Person person = new Person("Some One Else " + System.Environment.TickCount);
                this.olvSimple.AddObject(person);
                this.olvSimple.EnsureModelVisible(person);
            }
        }

        private void comboBoxEditable_SelectedIndexChanged(object sender, EventArgs e) {
            Coordinator.ChangeEditable(this.ListView, (ComboBox)sender);
        }

        private void checkBoxHotItem_CheckedChanged(object sender, EventArgs e) {
            this.ListView.UseHotItem = ((CheckBox)sender).Checked;
        }

        private void checkBoxItemCount_CheckedChanged(object sender, EventArgs e) {
            Coordinator.ShowLabelsOnGroupsChecked(this.ListView, (CheckBox)sender);
        }

        private void checkBoxGroups_CheckedChanged(object sender, EventArgs e) {
            Coordinator.ShowGroupsChecked(this.ListView, (CheckBox)sender);
        }

        #endregion

        #region Animations

#if WITHOUT_ANIMATION
        private void buttonListAnimation_Click(object sender, EventArgs e) {
            Coordinator.ShowMessage("Animations do not work on VS 2005");
        }

        private void buttonRowAnimation_Click(object sender, EventArgs e)
        {
            Coordinator.ShowMessage("Animations do not work on VS 2005");
        }

        private void buttonCellAnimation_Click(object sender, EventArgs e)
        {
            Coordinator.ShowMessage("Animations do not work on VS 2005");
        }
#else

        private void buttonListAnimation_Click(object sender, EventArgs e) {
            AnimatedDecoration listAnimation = new AnimatedDecoration(this.olvSimple);
            Animation animation = listAnimation.Animation;

            //Sprite image = new ImageSprite(Resource1.largestar);
            //image.FixedLocation = Locators.SpriteAligned(Corner.MiddleCenter);
            //image.Add(0, 2000, Effects.Rotate(0, 360 * 2f));
            //image.Add(1000, 1000, Effects.Fade(1.0f, 0.0f));
            //animation.Add(0, image);

            Sprite image = new ImageSprite(Resource1.largestar);
            image.Add(0, 500, Effects.Move(Corner.BottomCenter, Corner.MiddleCenter));
            image.Add(0, 500, Effects.Rotate(0, 180));
            image.Add(500, 1500, Effects.Rotate(180, 360 * 2.5f));
            image.Add(500, 1000, Effects.Scale(1.0f, 3.0f));
            image.Add(500, 1000, Effects.Goto(Corner.MiddleCenter));
            image.Add(1000, 900, Effects.Fade(1.0f, 0.0f));
            animation.Add(0, image);

            Sprite text = new TextSprite("Animations!", new Font("Tahoma", 32), Color.Blue, Color.AliceBlue, Color.Red, 3.0f);
            text.Opacity = 0.0f;
            text.FixedLocation = Locators.SpriteAligned(Corner.MiddleCenter);
            text.Add(900, 900, Effects.Fade(0.0f, 1.0f));
            text.Add(1000, 800, Effects.Rotate(180, 1440));
            text.Add(2000, 500, Effects.Scale(1.0f, 0.5f));
            text.Add(3500, 1000, Effects.Scale(0.5f, 3.0f));
            text.Add(3500, 1000, Effects.Fade(1.0f, 0.0f));
            animation.Add(0, text);

            animation.Start();
        }

        private void buttonRowAnimation_Click(object sender, EventArgs e) {
            object modelObject = this.olvSimple.GetModelObject(Math.Min(5, this.olvSimple.GetItemCount()));
            if (modelObject == null) {
                Coordinator.ShowMessage("There aren't any rows to be animated");
                return;
            }
            AnimatedDecoration animatedDecoration = new AnimatedDecoration(this.olvSimple, modelObject);
            Animation animation = animatedDecoration.Animation;

            // Animate the same star several times to make ghosting
            this.AddStarAnimation(animation, 250, 0.4f);
            this.AddStarAnimation(animation, 200, 0.5f);
            this.AddStarAnimation(animation, 150, 0.6f);
            this.AddStarAnimation(animation, 100, 0.7f);
            this.AddStarAnimation(animation, 50, 0.8f);
            this.AddStarAnimation(animation, 0, 1.0f);

            animation.Start();
        }

        private void AddStarAnimation(Animation animation, int start, float opacity) {
            Sprite sprite = new ImageSprite(Resource1.star32);
            sprite.Opacity = opacity;
            sprite.Add(0, 4000, Effects.Walk(Locators.AnimationBounds(), WalkDirection.Anticlockwise));
            sprite.Add(0, 4000, Effects.Rotate(0, 1480));
            animation.Add(start, sprite);
        }

        private void buttonCellAnimation_Click(object sender, EventArgs e) {
            object modelObject = this.olvSimple.GetModelObject(Math.Min(5, this.olvSimple.GetItemCount()));
            if (modelObject == null || this.olvSimple.Columns.Count < 2) {
                Coordinator.ShowMessage("There aren't any cells to be animated");
                return;
            }

            AnimatedDecoration animatedDecoration = new AnimatedDecoration(this.olvSimple, modelObject, this.olvSimple.GetColumn(1));
            Animation animation = animatedDecoration.Animation;

            ShapeSprite sprite = ShapeSprite.RoundedRectangle(5.0f, Color.Firebrick, Color.FromArgb(48, Color.Firebrick));
            sprite.Opacity = 0.0f;
            sprite.CornerRounding = 14;
            sprite.FixedBounds = Locators.AnimationBounds(3, 3);
            sprite.Add(0, 4500, Effects.Blink(3));
            animation.Add(0, sprite);

            animation.Start();
        }
#endif

        #endregion
    }
}
