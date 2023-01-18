using MazeDfs.Memento;
using MazeDfs.Models;
using MazeDfs.Source;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace MazeDfs {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        public static Color backgroundColor = Color.DeepSkyBlue;

            
        Graphics g;
        Pen p;
        Bitmap bmp;

        static List<Square> squares = new();
       
        static Originator originator = new(squares);
        static Caretaker caretaker = new(originator);


        private int _width = 512;
        private int _height = 512;

        private static int _n = 100; //wymiary szerokosc
        private static int _m = 100; //wymiary wysokosc

        public static float cellWidth = 8.0F;


        private bool _drawBoard;
        private bool _solveMaze;

        private void InitBoard(int width, int height) {
            _drawBoard = false;
            _solveMaze = false;
            bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            panel1.BackgroundImage = bmp;
            g = Graphics.FromImage(panel1.BackgroundImage);
            cellWidth = width * 8.0F / 512; 
            g.Clear(backgroundColor);
        }

        private void Form1_Load(object sender, EventArgs e) {
            InitBoard(512, 512);
        }

        private void ConnectSquares() {
            Square visitedSquare = new Square(-100, -100);
            visitedSquare.Visited = true;

            //top left corner
            squares[0].LeftNeighbour = visitedSquare;
            squares[0].TopNeighbour = visitedSquare; 
            squares[0].RightNeighbour = squares[1];
            squares[0].BottomNeighbour = squares[_n * 1];
           
            //top right corner 
            squares[_n - 1].LeftNeighbour = squares[_n - 2]; 
            squares[_n - 1].TopNeighbour = visitedSquare; 
            squares[_n - 1].RightNeighbour = visitedSquare; 
            squares[_n - 1].BottomNeighbour = squares[_n * 2 - 1];

            //bottom left corner 
            squares[_n * (_m - 1)].LeftNeighbour = visitedSquare;
            squares[_n * (_m - 1)].TopNeighbour = squares[_n * (_m - 2)];
            squares[_n * (_m - 1)].RightNeighbour = squares[_n * (_m - 1) + 1];
            squares[_n * (_m - 1)].BottomNeighbour = visitedSquare;

            //bottom right corner 
            squares[_n * _m - 1].LeftNeighbour = squares[_n * _m  - 2];
            squares[_n * _m - 1].TopNeighbour = squares[_n * (_m - 1) - 1];
            squares[_n * _m - 1].RightNeighbour = visitedSquare;
            squares[_n * _m - 1].BottomNeighbour = visitedSquare;


            //top 
            for(int i = 1; i < _n - 1; ++i) {
                squares[i].LeftNeighbour = squares[i - 1];
                squares[i].TopNeighbour = visitedSquare;
                squares[i].RightNeighbour = squares[i + 1];
                squares[i].BottomNeighbour = squares[i + _n * 1];
            }

            //bottom
            for(int i = _n * (_m - 1) + 1; i < _n * _m - 1; ++i) {
                squares[i].LeftNeighbour = squares[i - 1];
                squares[i].TopNeighbour = squares[i - _m * 1]; 
                squares[i].RightNeighbour = squares[i + 1];
                squares[i].BottomNeighbour = visitedSquare; 
            }

            //left
            for (int i = _n; i < _n * (_m - 1); i += _n) {
                squares[i].LeftNeighbour = visitedSquare; 
                squares[i].TopNeighbour = squares[i - _n * 1 ]; 
                squares[i].RightNeighbour = squares[i + 1];
                squares[i].BottomNeighbour = squares[i + _n * 1]; 
            }

            //right
            for (int i = 2 * _n - 1; i < _n * _m - 1; i += _n) {
                squares[i].LeftNeighbour = squares[i - 1];
                squares[i].TopNeighbour = squares[i - _n * 1 ];
                squares[i].RightNeighbour = visitedSquare; 
                squares[i].BottomNeighbour = squares[i + _n * 1]; 
            }
            
            //inside
            for( int i = 1; i < _m - 1; ++i ) {
                for( int j = i * _n + 1; j < ( i + 1 ) * _n - 1; ++j ) {
                    squares[j].LeftNeighbour = squares[j - 1];
                    squares[j].TopNeighbour = squares[j - _n * 1 ];
                    squares[j].RightNeighbour = squares[j + 1]; 
                    squares[j].BottomNeighbour = squares[j + _n * 1]; 
                }   
            }

        }

        private void PrepareToSolve() {
            foreach(var square in squares) {
                square.Visited = false;
            }
        }

        private void DrawSolvePath( List<Square> solvePath, PaintEventArgs e ) {
            Pen paint = new(Color.DarkRed, 1);
            PointF[] solvePoints = solvePath.Select(solvePoint => solvePoint.Coordiantes).ToArray();
            solvePoints = solvePoints.Select(solvePoint => { solvePoint.X += cellWidth/2; solvePoint.Y += cellWidth/2; return solvePoint; }).ToArray();
            e.Graphics.DrawLines(paint, solvePoints);
        }

        private void CreateBoard(PaintEventArgs e) {
            float x = 0, y = 0;

            for(int i = 0; i < _n; i++) {
                for(int j = 0; j < _m; j++) {
                    var square = new Square(x, y);
                    square.Draw(e);
                    squares.Add(square);
                    x += cellWidth;
                }
                x = 0.0F;
                y += cellWidth;
            }
        }

        private void DisposeElements() {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {

            if( _drawBoard ) {
                CreateBoard(e);
                ConnectSquares();
                MazeDfsGenerator.CreateMaze(squares, e);


                PrepareToSolve();
                var solvePath = MazeSolver.SolveMaze(squares);
                DrawSolvePath(solvePath, e);
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            _solveMaze = true;
            
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void GenereteMazeButton_Click(object sender, EventArgs e) {
            _n = Int32.Parse(textBox2.Text);
            _m = Int32.Parse(textBox1.Text);

            cellWidth = 512 / _n;

            

            _drawBoard = true;
            squares.Clear();
            panel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e) {
            MazeMemento selected = (MazeMemento)listBox1.SelectedItem;

            if( selected != null ) {
                caretaker.Restore(selected);
                squares = selected.GetMaze();
                squares.Clear();
                panel1.Invalidate();
            }
        }

        private void button1_Click_1(object sender, EventArgs e) {
            listBox1.Items.Add(caretaker.Save());
        }
    }
}