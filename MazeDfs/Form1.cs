using MazeDfs.Models;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;

namespace MazeDfs {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        public static Color backgroundColor = Color.DeepSkyBlue;
        public static float cellWidth = 40.0F;

        Graphics g;
        Pen p;
        Bitmap bmp;
        List<Square> squares = new();

        private int _width = 256;
        private int _height = 256;

        private int _n = 25; //wymiary szerokosc
        private int _m = 25; //wymiary wysokosc

        private void Form1_Load(object sender, EventArgs e) {
            this.panel1.Width = _n * (int)cellWidth + 1;
            this.panel1.Height = _m * (int)cellWidth + 1;
            this.panel1.Location = new Point(
                this.ClientSize.Width / 2 - panel1.Size.Width / 2,
                this.ClientSize.Height / 2 - panel1.Size.Height / 2); // center panel
            this.panel1.Anchor = AnchorStyles.None;

            this.Width = panel1.Width + 50;
            this.Height = panel1.Height + 50;

            bmp = new Bitmap(_width, _height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            panel1.BackgroundImage = bmp;
            g = Graphics.FromImage(panel1.BackgroundImage);
            g.Clear(backgroundColor);
        }

        private void ConnectSquares() {

            Square visitedSquare = new Square(-100, -100);
            visitedSquare.Visited = true;

            //top left corner
            squares[0].LeftNeighbour = null;
            squares[0].TopNeighbour = null; 
            squares[0].RightNeighbour = squares[1];
            squares[0].BottomNeighbour = squares[_n * 1];
           
            //top right corner 
            squares[_n - 1].LeftNeighbour = squares[_n - 2]; 
            squares[_n - 1].TopNeighbour = null; 
            squares[_n - 1].RightNeighbour = null; 
            squares[_n - 1].BottomNeighbour = squares[_n * 2 - 1];

            //bottom left corner 
            squares[_n * (_m - 1)].LeftNeighbour = null;
            squares[_n * (_m - 1)].TopNeighbour = squares[_n * (_m - 2)];
            squares[_n * (_m - 1)].RightNeighbour = squares[_n * (_m - 1) + 1];
            squares[_n * (_m - 1)].BottomNeighbour = null;

            //bottom right corner 
            squares[_n * _m - 1].LeftNeighbour = squares[_n * _m  - 2];
            squares[_n * _m - 1].TopNeighbour = squares[_n * (_m - 1) - 1];
            squares[_n * _m - 1].RightNeighbour = null;
            squares[_n * _m - 1].BottomNeighbour = null;


            //top 
            for(int i = 1; i < _n - 1; ++i) {
                squares[i].LeftNeighbour = squares[i - 1];
                squares[i].TopNeighbour = null;
                squares[i].RightNeighbour = squares[i + 1];
                squares[i].BottomNeighbour = squares[i + _n * 1];
            }

            //bottom
            for(int i = _n * (_m - 1) + 2; i < _n * _m - 1; ++i) {
                squares[i].LeftNeighbour = squares[i - 1];
                squares[i].TopNeighbour = squares[i - _m * 1]; 
                squares[i].RightNeighbour = squares[i + 1];
                squares[i].BottomNeighbour = null; 
            }

            //left
            for (int i = _n; i < _n * (_m - 1); i += _n) {
                squares[i].LeftNeighbour = null; 
                squares[i].TopNeighbour = squares[i - _n * 1 ]; 
                squares[i].RightNeighbour = squares[i + 1];
                squares[i].BottomNeighbour = squares[i + _n * 1]; 
            }

            //right
            for (int i = 2 * _n - 1; i < _n * _m - 1; i += _n) {
                squares[i].LeftNeighbour = squares[i - 1];
                squares[i].TopNeighbour = squares[i - _n * 1 ];
                squares[i].RightNeighbour = null; 
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

        private void panel1_Paint(object sender, PaintEventArgs e) {
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

            ConnectSquares();
            Console.WriteLine();

        }
    }
}