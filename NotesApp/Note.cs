using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp
{
    public class Note
    {
        public string Title { get; set; }
        public string Label { get; set; }
        public string NoteContent { get; set; }
        public bool IsChecked { get; set; }

        public Note(string title, string label, string noteContent)
        {
            Title = title;
            NoteContent = noteContent;
            Label = label;
        }
    }
}
