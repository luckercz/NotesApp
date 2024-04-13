using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp
{
    public class AllNotes
    {
        public List<Note> Notes {  get; set; }

        public void AddNote(Note note)
        {
            Notes.Add(note);
        }
    }
}
