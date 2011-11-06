using System;
using Mooege.Common.MPQ;
using System.Windows.Forms;
using System.IO;

namespace MPQBrowser
{
    class AssetNode : TreeNode
    {
        public Asset Asset { get; private set; }

        public AssetNode(Asset asset)
        {
            this.Asset = asset;
            this.Text = Path.GetFileName(asset.FileName);
        }

        public PropertyBrowser ShowDetails()
        {
            if (Asset != null && Asset.Data != null)
                return new PropertyBrowser(Asset.Data);
            else
                return new PropertyBrowser(Asset);
        }
    }
}
