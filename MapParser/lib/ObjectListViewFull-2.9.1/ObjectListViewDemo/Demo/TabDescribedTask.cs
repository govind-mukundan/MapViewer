using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace ObjectListViewDemo
{
    public partial class TabDescribedTask : OlvDemoTab {
        public TabDescribedTask() {
            InitializeComponent();
            this.ListView = this.olvTasks;
        }

        protected override void InitializeTab() {

            this.SetupDescibedTaskColumn();
            this.SetupColumns();
            this.SetupColumnWithButton();

            // How much space do we want to give each row? Obviously, this should be at least
            // the height of the images used by the renderer
            this.olvTasks.RowHeight = 54;
            this.olvTasks.SmallImageList = imageListSmall;
            this.olvTasks.EmptyListMsg = "No tasks match the filter";
            this.olvTasks.UseAlternatingBackColors = false;
            this.olvTasks.UseHotItem = false;

            // Make and display a list of tasks
            List<ServiceTask> tasks = CreateTasks();
            this.olvTasks.SetObjects(tasks);
        }

        private void SetupColumnWithButton() {

            // Tell the columns that it is going to show buttons.
            // The label that goes into the button is the Aspect that would have been
            // displayed in the cell.
            this.olvColumnAction.IsButton = true;

            // How will the button be sized? That can either be:
            //   - FixedBounds. Each button is ButtonSize in size
            //   - CellBounds. Each button is as wide as the cell, inset by CellPadding
            //   - TextBounds. Each button resizes to match the width of the text plus ButtonPadding
            this.olvColumnAction.ButtonSizing = OLVColumn.ButtonSizingMode.FixedBounds;
            this.olvColumnAction.ButtonSize = new Size(80, 26);

            // Make the buttons clickable even if the row itself is disabled
            this.olvColumnAction.EnableButtonWhenItemIsDisabled = true;
            this.olvColumnAction.AspectName = "NextAction";
            this.olvColumnAction.TextAlign = HorizontalAlignment.Center;

            // Listen for button clicks -- which for the purpose of the demo will cycle the state of the service task
            this.olvTasks.ButtonClick += delegate(object sender, CellClickEventArgs e) {
                Coordinator.ToolStripStatus1 = String.Format("Button clicked: ({0}, {1}, {2})", e.RowIndex, e.SubItem, e.Model);

                // We only have one column with a button, but if there was more than one, you would have to check ColumnIndex to see which button was clicked
                ServiceTask task = (ServiceTask)e.Model;
                task.AdvanceToNextState();

                // Just to show off disabled rows, make tasks that are frozen be disabled.
                if (task.Status == ServiceTask.TaskStatus.Frozen)
                    this.olvTasks.DisableObject(e.Model);
                else
                    this.olvTasks.EnableObject(e.Model);

                this.olvTasks.RefreshObject(e.Model);
            };
        }

        private void SetupDescibedTaskColumn() {
            // Setup a described task renderer, which draws a large icon
            // with a title, and a description under the title.
            // Almost all of this configuration could be done through the Designer
            // but I've done it through code that make it clear what's going on.

            // Create and install an appropriately configured renderer 
            this.olvColumnTask.Renderer = CreateDescribedTaskRenderer();

            // Now let's setup the couple of other bits that the column needs

            // Tell the column which property should be used to get the title
            this.olvColumnTask.AspectName = "Task";

            // Tell the column which property holds the identifier for the image for row.
            // We could also have installed an ImageGetter
            this.olvColumnTask.ImageAspectName = "ImageName";

            // Put a little bit of space around the task and its description
            this.olvColumnTask.CellPadding = new Rectangle(4, 2, 4, 2);
        }

        private void SetupColumns()
        {
            // Draw the priority column as a collection of coins (first parameter).
            // We want the renderer to draw at most 4 stars (second parameter).
            // Priority has a value range from 0-5 (the last two parameters).
            this.olvColumnPriority.TextAlign = HorizontalAlignment.Center;
            MultiImageRenderer multiImageRenderer = new MultiImageRenderer("Lamp", 4, 0, 5);
            multiImageRenderer.Spacing = -12; // We want the coins to overlap
            this.olvColumnPriority.Renderer = multiImageRenderer;

            this.olvColumnStatus.AspectToStringConverter = delegate(object model) {
                ServiceTask.TaskStatus status = (ServiceTask.TaskStatus)model;
                switch (status) {
                    case ServiceTask.TaskStatus.InProgress:
                        return "In progress";
                    case ServiceTask.TaskStatus.NotStarted:
                        return "Not started";
                    case ServiceTask.TaskStatus.Complete:
                        return "Complete";
                    case ServiceTask.TaskStatus.Frozen:
                        return "Frozen";
                    default:
                        return "";
                }
            };
            this.olvColumnStatus.ImageGetter = delegate(object model) {
                ServiceTask task = (ServiceTask)model;
                switch (task.Status) {
                    case ServiceTask.TaskStatus.InProgress:
                        return "Heart";
                    case ServiceTask.TaskStatus.NotStarted:
                        return "Add";
                    case ServiceTask.TaskStatus.Complete:
                        return "Tick";
                    case ServiceTask.TaskStatus.Frozen:
                        return "Cancel";
                    default:
                        return "";
                }
            };
        }

        private DescribedTaskRenderer CreateDescribedTaskRenderer() {

            // Let's create an appropriately configured renderer.
            DescribedTaskRenderer renderer = new DescribedTaskRenderer();

            // Give the renderer its own collection of images.
            // If this isn't set, the renderer will use the SmallImageList from the ObjectListView.
            // (this is standard Renderer behaviour, not specific to DescribedTaskRenderer).
            renderer.ImageList = this.imageListTasks;

            // Tell the renderer which property holds the text to be used as a description
            renderer.DescriptionAspectName = "Description";

            // Change the formatting slightly
            renderer.TitleFont = new Font("Tahoma", 11, FontStyle.Bold);
            renderer.DescriptionFont = new Font("Tahoma", 9);
            renderer.ImageTextSpace = 8;
            renderer.TitleDescriptionSpace = 1;

            // Use older Gdi renderering, since most people think the text looks clearer
            renderer.UseGdiTextRendering = true;

            // If you like colours other than black and grey, you could uncomment these
//            renderer.TitleColor = Color.DarkBlue;
//            renderer.DescriptionColor = Color.CornflowerBlue;

            return renderer;
        }

        private static List<ServiceTask> CreateTasks() {
            List<ServiceTask> tasks = new List<ServiceTask>();

            tasks.Add(new ServiceTask("Setup spy cameras", "Install spy cameras in several locations to collect interesting footage", "film", ServiceTask.TaskStatus.NotStarted, 5));
            tasks.Add(new ServiceTask("Check printer status", "Ensure that the printer is turned on and has toner", "printer", ServiceTask.TaskStatus.NotStarted, 2));
            tasks.Add(new ServiceTask("Check circuit boards", "Ensure that the circuit boards are properly seated and have not be stolen ", "electronics", ServiceTask.TaskStatus.Complete, 4));
            tasks.Add(new ServiceTask("Swap local gossip", "Spent some time in rec room to pick up any juicy gossip that could be useful", "backandforth", ServiceTask.TaskStatus.InProgress, 3));
            tasks.Add(new ServiceTask("Answer any questions", "Politely and informatively respond to all tech questions the employees may have", "faq", ServiceTask.TaskStatus.InProgress, 1));
            tasks.Add(new ServiceTask("Check Windows licenses", "Make sure that each Windows machine is running an authorized copy of Windows", "windows", ServiceTask.TaskStatus.NotStarted, 5));
            tasks.Add(new ServiceTask("Download new games", "Check to see if anyone has installed an good new games and copy them onto the portable hard drive", "download", ServiceTask.TaskStatus.NotStarted, 1));

            return tasks;
        }

        private void RebuildFilters() {

            // Build a composite filter that unify the three possible filtering criteria

            List<IModelFilter> filters = new List<IModelFilter>();

            if (this.checkBoxHighPriority.Checked)
                filters.Add(new ModelFilter(delegate(object model) { return ((ServiceTask)model).Priority > 3; }));

            if (this.checkBoxIncomplete.Checked)
                filters.Add(new ModelFilter(delegate(object model) { return ((ServiceTask)model).Status != ServiceTask.TaskStatus.Complete; }));

            if (!String.IsNullOrEmpty(this.textBoxFilter.Text))
                filters.Add(new TextMatchFilter(this.olvTasks, this.textBoxFilter.Text));

            // Use AdditionalFilter (instead of ModelFilter) since AdditionalFilter plays well with any
            // extra filtering the user might specify via the column header
            this.olvTasks.AdditionalFilter = filters.Count == 0 ? null : new CompositeAllFilter(filters);
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e) {
            this.RebuildFilters();
        }

        private void checkBoxHighPriority_CheckedChanged(object sender, EventArgs e) {
            this.RebuildFilters();
        }

        private void checkBoxIncomplete_CheckedChanged(object sender, EventArgs e) {
            this.RebuildFilters();
        }
    }

    /// <summary>
    /// Dumb model class
    /// </summary>
    public class ServiceTask {
        private string task;
        private TaskStatus status;
        private string description;
        private string imageName;
        private int priority;

        #region Life and death

        public ServiceTask(string task, string description, string imageName, TaskStatus status, int priority)
        {
            this.Task = task;
            this.ImageName = imageName;
            this.Description = description;
            this.Status = status;
            this.Priority = priority;
        }

        #endregion

        #region Properties

        public string Task
        {
            get { return this.task; }
            set { this.task = value; }
        }

        public string ImageName
        {
            get { return this.imageName; }
            set { this.imageName = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public TaskStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public int Priority {
            get { return this.priority; }
            set { this.priority = value; }
        }

        public string NextAction {
            get {
                switch (Status) {
                    case ServiceTask.TaskStatus.InProgress:
                        return "Complete";
                    case ServiceTask.TaskStatus.NotStarted:
                        return "Start";
                    case ServiceTask.TaskStatus.Complete:
                        return "Freeze";
                    case ServiceTask.TaskStatus.Frozen:
                        return "Restart";
                    default:
                        return "[unknown]";
                }
            }
        }

        #endregion

        public enum TaskStatus {
            NotStarted,
            InProgress,
            Complete,
            Frozen
        }

        public void AdvanceToNextState() {
            switch (Status)
            {
                case ServiceTask.TaskStatus.NotStarted:
                    Status = ServiceTask.TaskStatus.InProgress;
                    break;
                case ServiceTask.TaskStatus.InProgress:
                    Status = ServiceTask.TaskStatus.Complete;
                    break;
                case ServiceTask.TaskStatus.Complete:
                    Status = ServiceTask.TaskStatus.Frozen;
                    break;
                case ServiceTask.TaskStatus.Frozen:
                    Status = ServiceTask.TaskStatus.NotStarted;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}
