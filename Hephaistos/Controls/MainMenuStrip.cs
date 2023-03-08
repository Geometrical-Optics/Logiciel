using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hephaistos.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        private const string NAME = "MainMenuStrip";
        public MainMenuStrip()
        {
            Name = NAME;
            Dock = DockStyle.Top;
            FileDropDownMenu();
            EditDropDownMenu();
            FormatDropDownMenu();
            ViewDropDownMenu();
        }

        public void FileDropDownMenu()
        {
            ToolStripMenuItem fileDropDownMenu = new ToolStripMenuItem("Fichier");


            ToolStripMenuItem newMenu = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            ToolStripMenuItem openMenu = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);
            ToolStripMenuItem saveMenu = new ToolStripMenuItem("Enregister", null, null, Keys.Control | Keys.S);
            ToolStripMenuItem saveAsMenu = new ToolStripMenuItem("Enregister sous...", null, null, Keys.Control | Keys.Shift | Keys.N);
            ToolStripMenuItem quitMenu = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            fileDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { newMenu, openMenu, saveMenu, saveAsMenu, quitMenu });

            Items.Add(fileDropDownMenu);
        }
        
        public void EditDropDownMenu()
        {
            ToolStripMenuItem editDropDownMenu = new ToolStripMenuItem("Edition");


            ToolStripMenuItem cancelMenu = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            ToolStripMenuItem restoreMenu = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);

            editDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { cancelMenu, restoreMenu }) ;

            Items.Add(editDropDownMenu);
        }

        public void FormatDropDownMenu()
        {
            ToolStripMenuItem formatDropDownMenu = new ToolStripMenuItem("Format");


            ToolStripMenuItem fontMenu = new ToolStripMenuItem("Police...");

            formatDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { fontMenu});

            Items.Add(formatDropDownMenu);
        }

        public void ViewDropDownMenu()
        {
            ToolStripMenuItem viewDropDownMenu = new ToolStripMenuItem("Format");


            ToolStripMenuItem alwaysInFrontMenu = new ToolStripMenuItem("Toujours devant", null, null, Keys.Control | Keys.N);
            ToolStripMenuItem zoomDropDownMenu = new ToolStripMenuItem("Zoom", null, null, Keys.Control | Keys.O);

            ToolStripMenuItem zoomInMenu = new ToolStripMenuItem("Zoom avant", null, null, Keys.Control | Keys.Add);
            ToolStripMenuItem zoomOutMenu = new ToolStripMenuItem("Zoom arrière", null, null, Keys.Control | Keys.Control | Keys.Subtract);
            ToolStripMenuItem zoomResetMenu = new ToolStripMenuItem("Réinitialiser zoom par défaut", null, null, Keys.Control | Keys.Divide);

            zoomInMenu.ShortcutKeyDisplayString = "Ctrl+Num +";
            zoomInMenu.ShortcutKeyDisplayString = "Ctrl+Num -";
            zoomInMenu.ShortcutKeyDisplayString = "Ctrl+Num /";

            zoomDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { zoomInMenu, zoomOutMenu, zoomResetMenu });

            viewDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { alwaysInFrontMenu, zoomDropDownMenu });

            Items.Add(viewDropDownMenu);
        }
    }
}
