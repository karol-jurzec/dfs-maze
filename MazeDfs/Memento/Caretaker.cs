using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeDfs.Memento;
public class Caretaker {
    private Originator _originator;
    private List<MazeMemento> _history;

    public Caretaker(Originator originator) {
        _originator = originator; 
        _history= new List<MazeMemento>();
    }

    public void Restore(MazeMemento m) {
        MazeMemento mem = _history.Find(x => m.GetMaze() == x.GetMaze());
        _originator.Restore(mem.GetMaze());
    }

    public MazeMemento Save() {
        MazeMemento m = _originator.Save();
        _history.Add(m);
        return m;
    }
}
