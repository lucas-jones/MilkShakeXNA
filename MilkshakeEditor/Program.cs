using System;
using System.Drawing;
using System.Windows.Forms;

namespace MilkshakeEditor
{
    public static class Program
    {
        private static Editor _editor;
        private static Ra.Ra _game;

        public static Control GameWindowControl
        {
            get { return Control.FromHandle((_game.Window.Handle)); }
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _editor = new Editor();

            _game = new Ra.Ra(_editor.getDrawSurface());

            Control.FromHandle((_game.Window.Handle)).VisibleChanged += SyncGameWindow;
            _editor.Move += SyncGameWindow;

            _editor.Show();
            _game.Run();
        }

        private static void SyncGameWindow(object sender, EventArgs e)
        {
            GameWindowControl.Location = _editor.PreviewPannel.PointToScreen(new Point(-4, -25));

            if (GameWindowControl.Visible) GameWindowControl.Visible = false;
        }
    }
}