using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WK.Libraries.SharpClipboardNS;
using static WK.Libraries.SharpClipboardNS.SharpClipboard;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        SharpClipboard clipboard = new SharpClipboard();
        private GlobalKeyboardHook _globalKeyboardHook;

        List <Keys> keys = new List<Keys>();
        Boolean ctrlPressed = false;
        Boolean vPressed = false;

        public Form1()
        {
            //InitializeComponent();
            clipboard.ObservableFormats.Texts = true;
            clipboard.ObservableFormats.Files = true;
            clipboard.ObservableFormats.Images = true;
            clipboard.ObservableFormats.Others = true;
            clipboard.ClipboardChanged += ClipboardChanged;
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }


        //Bắt sự kiện keyhook (ctr + V)


        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            // EDT: No need to filter for VkSnapshot anymore. This now gets handled
            // through the constructor of GlobalKeyboardHook(...).
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.WM_PASTE)
            {
                MessageBox.Show("done");
            }
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                if (ctrlPressed && e.KeyboardData.Key == Keys.V)
                {
                    MessageBox.Show("Catch");
                }
                if (e.KeyboardData.Key == Keys.LControlKey || e.KeyboardData.Key == Keys.RControlKey)
                {
                    ctrlPressed = true;
                }
                // Now you can access both, the key and virtual code
            }
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
            {
                if (e.KeyboardData.Key == Keys.V)
                    vPressed = false;
                if (e.KeyboardData.Key == Keys.LControlKey || e.KeyboardData.Key == Keys.RControlKey)
                {
                    ctrlPressed = false;
                }
            }
        }

        //Bắt sự kiện copy vào clipboard
        private void ClipboardChanged(Object sender, ClipboardChangedEventArgs e)
        {
            // Is the content copied of text type?
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                // Get the cut/copied text.
                //MessageBox.Show(clipboard.ClipboardText);
            }

            // Is the content copied of image type?
            else if (e.ContentType == SharpClipboard.ContentTypes.Image)
            {
                // Get the cut/copied image.
                //Image img = clipboard.ClipboardImage;
            }

            // Is the content copied of file type?
            else if (e.ContentType == SharpClipboard.ContentTypes.Files)
            {
                // Get the cut/copied file/files.
                //MessageBox.Show(clipboard.ClipboardFiles.ToString());

                // ...or use 'ClipboardFile' to get a single copied file.
                //MessageBox.Show(clipboard.ClipboardFile);
            }

            // If the cut/copied content is complex, use 'Other'.
            else if (e.ContentType == SharpClipboard.ContentTypes.Other)
            {
                // Do something with 'e.Content' here...
            }
            var list = clipboard.ClipboardFiles.ToList();
            foreach (string str in list)
            {
                MessageBox.Show(str);
            }
            // Gets the application's executable name.
            //MessageBox.Show(e.SourceApplication.Name);
            // Gets the application's window title.
            //MessageBox.Show(e.SourceApplication.Title);
            // Gets the application's process ID.
            //MessageBox.Show(e.SourceApplication.ID.ToString());
            // Gets the application's executable path.
            //MessageBox.Show(e.SourceApplication.Path);
        }

    }
}
