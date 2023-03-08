using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hephaistos.Controls
{
    public class TabControlContextMenuStrip : ContextMenuStrip
    {
        private const string NAME = "TabControlContextMenuStrip";

        public TabControlContextMenuStrip()
        {
            Name = NAME;

            ToolStripMenuItem closeTab = new ToolStripMenuItem("Fermer");
            ToolStripMenuItem closAllTabsExceptThis = new ToolStripMenuItem("Fermer tout sauf ce fichier");
            ToolStripMenuItem openFileInExplorer = new ToolStripMenuItem("Ouvrir le répertoire du fichier en cours dans l'explorateur");

            Items.AddRange(new ToolStripItem[] { closeTab, closAllTabsExceptThis, openFileInExplorer });
        }
    }
}
