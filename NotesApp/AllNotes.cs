using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp
{
    public class AllNotes
    {
        public List<Note> Notes { get; set; }
        public AllNotes()
        {
            Notes = new List<Note>();
        }
        public AllNotes(string json)
        {
            Notes = JsonConvert.DeserializeObject<AllNotes>(json).Notes;
        }
        public void AddNote(Note note)
        {
            Notes.Add(note);
        }
        public void AddNotes(List<Note> notes)
        {
            foreach (Note note in notes)
            {
                AddNote(note);
            }
        }
    }
}
