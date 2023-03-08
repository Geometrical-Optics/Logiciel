using Hephaistos.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hephaistos
{
    public partial class Hephaistos : Form
    {
        public Hephaistos()
        {
            InitializeComponent();
            MainMenuStrip menuStrip = new MainMenuStrip();
            MainTabControl mainTabControl = new MainTabControl();

            mainTabControl.TabPages.Add("Onblet 1");

            Controls.AddRange(new Control[] { mainTabControl, menuStrip});
        }
    }
}
