using MazeDfs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeDfs.Memento; 

public class MazeMemento { 

    private List<Square> _maze;

    public MazeMemento(List<Square> maze) {
        _maze = maze;
    }

    public List<Square> GetMaze() { return _maze; }

    public void SetMaze(List<Square> maze) { _maze = maze; }
}
