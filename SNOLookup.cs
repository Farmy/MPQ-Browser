using System;
using System.Windows.Forms;

namespace MPQBrowser
{
    public partial class SNOLookup : Form
    {
        public int Sno { get { try { return Int32.Parse(SnoInput.Text); } catch (Exception) { return 0; } } }

        public SNOLookup()
        {
            InitializeComponent();
        }
    }
}
