using MazeDfs.Models;

namespace MazeDfs.Source;

public static class MazeSolver {
    private static List<Square> GetNotVisitedNeighbours(Square square) {
        List<Square> notVisitedNeighbours = new();
        
        if (square.LeftNeighbour.Visited == false && !square.LeftSide ) notVisitedNeighbours.Add(square.LeftNeighbour);
        if (square.TopNeighbour.Visited == false && !square.TopSide ) notVisitedNeighbours.Add(square.TopNeighbour);
        if (square.RightNeighbour.Visited == false && !square.RightSide ) notVisitedNeighbours.Add(square.RightNeighbour);
        if (square.BottomNeighbour.Visited == false && !square.BottomSide ) notVisitedNeighbours.Add(square.BottomNeighbour);

        return notVisitedNeighbours;
    }

    public static List<Square> SolveMaze(List<Square> squares) {
        HashSet<Square> visited = new();
        List<Square> path = new();

        path.Add(squares[0]);
        Square curr = squares[0];
        visited.Add(curr);
        curr.Visited = true;

        while (path[path.Count - 1] != squares[squares.Count - 1] && path.Count != 0) {
            List<Square> notVisited = GetNotVisitedNeighbours(curr); 
            var iter = notVisited.GetEnumerator();
            bool foundOutlet = false;
            bool movedToNext = iter.MoveNext();


            while ( movedToNext && !foundOutlet ) {
                if( visited.Count(m => m == iter.Current ) == 0) {
                    foundOutlet = true; 
                }else {
                    movedToNext = iter.MoveNext();
                }
            }

            if ( foundOutlet ) {
                path.Add(iter.Current);
                visited.Add(iter.Current);
                iter.Current.Visited = true;
            } else {
                path.RemoveAt(path.Count - 1);
            }

            curr = path[path.Count - 1];
        }

        return path;
    }
}
