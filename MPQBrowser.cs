using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mooege.Common.MPQ;
using Mooege.Core.GS.Common.Types.SNO;

namespace MPQBrowser
{
    public partial class MPQBrowser : Form
    {
        public MPQBrowser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds all assets grouped by asset group to the tree
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            AssetTree.Nodes.Clear();
            foreach (SNOGroup group in MPQStorage.Data.Assets.Keys)
            {
                var assetGroupNode = new TreeNode(group.ToString());
                foreach (var item in MPQStorage.Data.Assets[group].Values)
                    assetGroupNode.Nodes.Add(new AssetNode(item));

                AssetTree.Nodes.Add(assetGroupNode);
            }
        }

        /// <summary>
        /// Shows node selection details in first tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssetTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is AssetNode)
            {
                PropertyBrowser details = (e.Node as AssetNode).ShowDetails();
                details.SnoNodeSelected += new PropertyBrowser.OnSnoNodeClickHandler(ShowElementProperties);

                this.tabs.SelectedTab.Controls.Clear();
                this.tabs.SelectedTab.Controls.Add(details);
                this.tabs.SelectedTab.Text = System.IO.Path.GetFileName((e.Node as AssetNode).Asset.FileName);
            }
        }

        /// <summary>
        /// Shows details of an actor in a new tab
        /// </summary>
        /// <param name="sno"></param>
        private void ShowElementProperties(int sno)
        {
            AssetNode node = null;
            foreach (var key in MPQStorage.Data.Assets.Keys)
                if (MPQStorage.Data.Assets[key].ContainsKey(sno))
                    node = new AssetNode(MPQStorage.Data.Assets[key][sno]);

            if (node == null) return;

            TabPage newTab = new TabPage(System.IO.Path.GetFileName(node.Asset.FileName));
            PropertyBrowser details = node.ShowDetails();
            details.SnoNodeSelected += new PropertyBrowser.OnSnoNodeClickHandler(ShowElementProperties);
            
            newTab.Controls.Add(details);
            tabs.TabPages.Add(newTab);
            tabs.SelectedTab = newTab;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Closes a tag by doubleclicking on it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabs_DoubleClick(object sender, EventArgs e)
        {
            if (tabs.SelectedIndex != 0)
                tabs.TabPages.Remove((TabPage)tabs.SelectedTab);
        }

        private void lookupSNOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SNOLookup lookup = new SNOLookup();

            if (lookup.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                ShowElementProperties(lookup.Sno);
        }
    }
}
