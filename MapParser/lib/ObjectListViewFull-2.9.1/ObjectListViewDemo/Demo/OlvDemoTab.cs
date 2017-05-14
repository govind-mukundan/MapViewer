using System;
using System.ComponentModel;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace ObjectListViewDemo {
    public class OlvDemoTab : UserControl {

        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OLVDemoCoordinator Coordinator
        {
            get { return coordinator; }
            set
            {
                coordinator = value;
                if (value != null) {
                    this.InitializeTab();
                    this.SetupGeneralListViewEvents();
                }
            }
        }
        private OLVDemoCoordinator coordinator;
        private ObjectListView listView;

        protected virtual void InitializeTab() { }

        public ObjectListView ListView {
            get { return this.listView; }
            protected set { this.listView = value; }
        }

        private void SetupGeneralListViewEvents() {
            if (this.ListView == null || this.Coordinator == null)
                return;

            this.ListView.SelectionChanged += delegate(object sender, EventArgs args) {
                this.Coordinator.HandleSelectionChanged(this.ListView);
            };

            this.ListView.HotItemChanged += delegate(object sender, HotItemChangedEventArgs args) {
                this.Coordinator.HandleHotItemChanged(sender, args);
            };

            this.ListView.GroupTaskClicked += delegate(object sender, GroupTaskClickedEventArgs args) {
                Coordinator.ShowMessage("Clicked on group task: " + args.Group.Name);
            };

            this.ListView.GroupStateChanged += delegate(object sender, GroupStateChangedEventArgs e) {
                System.Diagnostics.Debug.WriteLine(String.Format("Group '{0}' was {1}{2}{3}{4}{5}{6}",
                    e.Group.Header,
                    e.Selected ? "Selected" : "",
                    e.Focused ? "Focused" : "",
                    e.Collapsed ? "Collapsed" : "",
                    e.Unselected ? "Unselected" : "",
                    e.Unfocused ? "Unfocused" : "",
                    e.Uncollapsed ? "Uncollapsed" : ""));
            };
        }
    }
}