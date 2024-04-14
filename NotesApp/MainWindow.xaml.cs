using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Note> Notes {  get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            InitializeWindow();
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
        private void SelectImportNotes(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".json";
            dialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string path = dialog.FileName;
                string json = File.ReadAllText(path);
                AllNotes allNotes = new AllNotes(json);
                ImportNotes(allNotes);
            }
        }
        private void ImportNotes(AllNotes allNotes)
        {
            string json = File.ReadAllText("AllNotes.json");
            AllNotes currentNotes = new AllNotes(json);
            currentNotes.AddNotes(allNotes.Notes);
            File.WriteAllText("AllNotes.json", JsonConvert.SerializeObject(currentNotes));
            InitializeWindow();
        }
        private void DeleteCheckedNotes(object sender, RoutedEventArgs e)
        {
            foreach (var item in Notes.Where(i => i.IsChecked).ToList())
            {
                Notes.Remove(item);
                Debug.WriteLine(item.Title);
            }
            AllNotes allNotes = new AllNotes();
            allNotes.Notes = Notes.ToList();
            File.WriteAllText("AllNotes.json", JsonConvert.SerializeObject(allNotes));
            InitializeWindow();
        }
        private void ExportCheckedNotes(object sender, RoutedEventArgs e)
        {
            if (!Notes.Any(i => i.IsChecked))
            {
                return;
            }
            AllNotes notesToExport= new AllNotes();
            notesToExport.AddNotes(Notes.Where(i => i.IsChecked).ToList());
            string jsonNotes = JsonConvert.SerializeObject(notesToExport);

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.FileName = "Document";
            saveFileDialog.DefaultExt = ".json";
            saveFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string path = saveFileDialog.FileName;
                File.WriteAllText(path, jsonNotes);
            }
        }
        private void SearchChange(object sender, RoutedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            Search(searchText);
        }
        private void Search(string text)
        {
            if (text == "hledat")
            {
                return;
            }
            List<Note> searchResult = Notes.Where(item =>
                item.Title.ToLower().Contains(text) ||
                item.Label.ToLower().Contains(text)
            ).ToList();
            AllNotes allSearchNotes = new AllNotes();
            allSearchNotes.Notes = searchResult;
            InitializeNotes(allSearchNotes);
        }
        public void InitializeWindow()
        {
            string json = File.ReadAllText("AllNotes.json");
            AllNotes allNotes = new AllNotes(json);
            UpdateObservableCollection(allNotes);
            InitializeNotes(allNotes);
        }
        private void UpdateObservableCollection(AllNotes allNotes)
        {
            Notes = new ObservableCollection<Note>(allNotes.Notes);
        }
        private void InitializeNotes(AllNotes allNotes)
        {
            stackPanel.Children.Clear();
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

            grid.DataContext = note;

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
            margin.Top = 5;
            checkBox1.Margin = margin;

            checkBox1.SetBinding(CheckBox.IsCheckedProperty, new Binding("IsChecked"));

            grid.Children.Add(textBlock1);
            grid.Children.Add(textBlock2);
            grid.Children.Add(checkBox1);

            //grid.ShowGridLines = true;

            stackPanel.Children.Add(grid);
        }
        private void RemoveTextOnFirstFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = string.Empty;
            ((TextBox)sender).GotFocus -= RemoveTextOnFirstFocus;
        }
    }
}
