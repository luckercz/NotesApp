using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NotesApp
{
    /// <summary>
    /// Interaction logic for WindowNote.xaml
    /// </summary>
    public partial class WindowNote : Window
    {
        bool pinned = false;
        public WindowNote()
        {
            InitializeComponent();
        }
        private void SaveNote(object sender, RoutedEventArgs e)
        {
            string title = Title.Text;
            string label = Label.Text;
            string content = Content.Text;

            Note note = new Note(title, content, label);
        }
        private void PinNote(object sender, RoutedEventArgs e)
        {
            if (pinned)
            {
                Topmost = false;
                pinned = false;
                PinImage.Source = new BitmapImage(new Uri (@"/png/pin.png", UriKind.Relative)); //https://stackoverflow.com/questions/3787137/change-image-source-in-code-behind-wpf
            }
            else
            {
                Topmost = true;
                pinned = true;
                PinImage.Source = new BitmapImage(new Uri(@"/png/pinFull.png", UriKind.Relative));
            }
        }
        private void CloseNote(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
