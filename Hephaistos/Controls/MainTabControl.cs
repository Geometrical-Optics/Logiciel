using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hephaistos.Controls
{
    public class MainTabControl : TabControl
    {
        private const string NAME = "MainTabControl";
        private ContextMenuStrip _contextMenuStrip;

        public MainTabControl()
        {
            _contextMenuStrip = new TabControlContextMenuStrip();
            Name = NAME;
            ContextMenuStrip = _contextMenuStrip;
            Dock = DockStyle.Fill;
        }
    }
}
