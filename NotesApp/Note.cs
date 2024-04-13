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
        public string NoteContent { get; set; }
        public List<string> Label { get; set; }

        public Note(string title, string noteContent, List<string> label)
        {
            Title = title;
            NoteContent = noteContent;
            Label = label;
        }
    }
}
