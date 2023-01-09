using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeDfs.Models;
public class Square {
    public PointF Coordiantes { get; set; } 

    public Square(float x, float y) {
        Coordiantes = new PointF(x, y);

        Visited = false;
        RightSide = true;
        LeftSide = true;
        TopSide = true;
        BottomSide = true;
    }

    public bool Visited { get; set; }
    public bool RightSide { get; set; }
    public bool LeftSide { get; set; }
    public bool BottomSide { get; set; }
    public bool TopSide { get; set; }


    public Square LeftNeighbour { get; set; }
    public Square RightNeighbour { get; set; }
    public Square TopNeighbour { get; set; }
    public Square BottomNeighbour { get; set; }

    public float width = Form1.cellWidth;

    public void Draw(PaintEventArgs e) {
        Pen blackPen = new Pen(Color.Black, 1);

        PointF point1 = new PointF(Coordiantes.X, Coordiantes.Y);
        PointF point2 = new PointF(Coordiantes.X + width, Coordiantes.Y);

        e.Graphics.DrawLine(blackPen, point1, point2); //top

        point1 = new PointF(Coordiantes.X, Coordiantes.Y);
        point2 = new PointF(Coordiantes.X, Coordiantes.Y + width);

        e.Graphics.DrawLine(blackPen, point1, point2); //left

        point1 = new PointF(Coordiantes.X + width, Coordiantes.Y + width);
        point2 = new PointF(Coordiantes.X, Coordiantes.Y + width);

        e.Graphics.DrawLine(blackPen, point1, point2); //bottom

        point1 = new PointF(Coordiantes.X + width, Coordiantes.Y + width);
        point2 = new PointF(Coordiantes.X + width, Coordiantes.Y);

        e.Graphics.DrawLine(blackPen, point1, point2); //right
    }

    public void RemoveLeftSide(PaintEventArgs e) {
        Pen paint = new(Form1.backgroundColor);
        PointF point1 = new PointF(Coordiantes.X, Coordiantes.Y);
        PointF point2 = new PointF(Coordiantes.X, Coordiantes.Y + width);

        LeftSide = false;
        e.Graphics.DrawLine(paint, point1, point2);
    }
    public void RemoveRightSide(PaintEventArgs e) {
        Pen paint = new(Form1.backgroundColor);
        PointF point1 = new PointF(Coordiantes.X + width, Coordiantes.Y + width);
        PointF point2 = new PointF(Coordiantes.X + width, Coordiantes.Y);

        RightSide = false;    
        e.Graphics.DrawLine(paint, point1, point2); 
    }

    public void RemoveBottomSide(PaintEventArgs e) {
        Pen paint = new(Form1.backgroundColor);
        PointF point1 = new PointF(Coordiantes.X + width, Coordiantes.Y + width);
        PointF point2 = new PointF(Coordiantes.X, Coordiantes.Y + width);

        BottomSide = false;    
        e.Graphics.DrawLine(paint, point1, point2); 
    }

    public void RemoveTopSide(PaintEventArgs e) {
        Pen paint = new(Form1.backgroundColor);
        PointF point1 = new PointF(Coordiantes.X, Coordiantes.Y);
        PointF point2 = new PointF(Coordiantes.X + width, Coordiantes.Y);

        TopSide = false;    
        e.Graphics.DrawLine(paint, point1, point2); 
    }
}

