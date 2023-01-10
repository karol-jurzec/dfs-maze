using MazeDfs.Models;

namespace MazeDfs.Source;

public static class MazeDfsGenerator {
    private static bool HasNotVisitedNeigbours( Square square ) {
        return (square.LeftNeighbour.Visited && 
                square.TopNeighbour.Visited && 
                square.RightNeighbour.Visited && 
                square.BottomNeighbour.Visited ) ? false : true;
    }

    private static Square ChooseRandomNotVisitedNeighbour ( Square square ) {
        List<Square> notVisitedNeighbours = new();
        
        if(square.LeftNeighbour.Visited == false) notVisitedNeighbours.Add(square.LeftNeighbour);
        if (square.TopNeighbour.Visited == false) notVisitedNeighbours.Add(square.TopNeighbour);
        if (square.RightNeighbour.Visited == false) notVisitedNeighbours.Add(square.RightNeighbour);
        if (square.BottomNeighbour.Visited == false) notVisitedNeighbours.Add(square.BottomNeighbour);

        Random random = new();

        return notVisitedNeighbours[random.Next(0, notVisitedNeighbours.Count)];
    }

    private static void RemoveWallBetweenTwoSquares(Square curr, Square chosen, PaintEventArgs e) {
        if(curr.TopNeighbour == chosen) {
            curr.TopSide = false;
            chosen.BottomSide = false;
            curr.RemoveTopSide(e);
        } else if (curr.LeftNeighbour == chosen) {
            curr.LeftSide = false;
            chosen.RightSide = false;
            curr.RemoveLeftSide(e);
        } else if (curr.RightNeighbour == chosen) {
            curr.RightSide = false;
            chosen.LeftSide = false;
            curr.RemoveRightSide(e);
        } else {
            curr.BottomSide = false;
            chosen.TopSide = false;
            curr.RemoveBottomSide(e);
        }
    }

    public static void CreateMaze(List<Square> squares, PaintEventArgs e) {
        Stack<Square> squareStack = new();
        squares[0].Visited = true;
        squareStack.Push(squares[0]);

        Square curr;

        while (squareStack.Count > 0) {
            curr = squareStack.Pop();
            if (HasNotVisitedNeigbours(curr)) {
                squareStack.Push(curr);
                Square chosen = ChooseRandomNotVisitedNeighbour(curr);
                RemoveWallBetweenTwoSquares(curr, chosen, e);
                chosen.Visited = true;
                squareStack.Push(chosen);
            }
        }
    }
}
