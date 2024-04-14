using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeNotes();
        }

        private void NewNote(object sender, RoutedEventArgs e)
        {
            WindowNote window2 = new WindowNote(this);
            window2.Show();
        }
        private void CloseNote(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void InitializeNotes()
        {
            stackPanel.Children.Clear();
            string json = File.ReadAllText("AllNotes.json");
            AllNotes allNotes = new AllNotes(json);
            foreach (Note note in allNotes.Notes)
            {
                AddNoteToStackPanel(note);
            }
        }
        private void AddNoteToStackPanel(Note note)
        {
            Grid grid = new Grid();
            grid.MinHeight = 100;
            grid.Background = (Brush)new BrushConverter().ConvertFrom("#dbddda");
            Thickness margin = grid.Margin;
            margin.Top = 10;
            grid.Margin = margin;

            ColumnDefinition colDef1 = new ColumnDefinition();
            colDef1.Width = new GridLength(8, GridUnitType.Star);   
            ColumnDefinition colDef2 = new ColumnDefinition();
            ColumnDefinition colDef3 = new ColumnDefinition();
            colDef3.Width = new GridLength(0.5, GridUnitType.Star);
            grid.ColumnDefinitions.Add(colDef1);
            grid.ColumnDefinitions.Add(colDef2);
            grid.ColumnDefinitions.Add(colDef3);

            RowDefinition rowDef1 = new RowDefinition();
            rowDef1.Height = new GridLength(30, GridUnitType.Pixel);
            RowDefinition rowDef2 = new RowDefinition();
            grid.RowDefinitions.Add(rowDef1);
            grid.RowDefinitions.Add(rowDef2);

            TextBlock textBlock1 = new TextBlock();
            textBlock1.Text = note.Title;
            textBlock1.FontSize = 23;
            Grid.SetColumn(textBlock1, 0);
            Grid.SetRow(textBlock1, 0);

            TextBlock textBlock2 = new TextBlock();
            textBlock2.Text = note.NoteContent;
            Debug.WriteLine(note.NoteContent);
            textBlock2.FontSize = 20;
            Grid.SetColumn(textBlock2, 0);
            Grid.SetRow(textBlock2, 1);

            CheckBox checkBox1 = new CheckBox();
            checkBox1.Height = 28;
            Grid.SetColumn(checkBox1, 1);
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = 1.8;
            scaleTransform.ScaleY = 1.8;
            checkBox1.LayoutTransform = scaleTransform;

            grid.Children.Add(textBlock1);
            grid.Children.Add(textBlock2);
            grid.Children.Add(checkBox1);

            grid.ShowGridLines = true;

            stackPanel.Children.Add(grid);
        }
    }
}
