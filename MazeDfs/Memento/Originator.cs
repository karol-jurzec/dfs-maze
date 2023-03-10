using MazeDfs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeDfs.Memento;
public class Originator {
    private List<Square> _mazeMemento;

    public Originator(List<Square> maze) { _mazeMemento = maze; }

    public MazeMemento Save() { return new MazeMemento(_mazeMemento); }
    public void Restore( List<Square> memento ) { _mazeMemento = memento; }
}
