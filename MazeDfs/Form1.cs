using MazeDfs.Models;
using System.Drawing.Drawing2D;

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

        private int _n = 10; //wymiary 
        private int _m = 10; //wymiary

        private void Form1_Load(object sender, EventArgs e) {
            bmp = new Bitmap(_width, _height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            panel1.BackgroundImage = bmp;
            g = Graphics.FromImage(panel1.BackgroundImage);
            g.Clear(backgroundColor);
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            float x = 0, y = 0;

            for(int i = 0; i < _n; i++) {
                for(int j = 0; j < _m; j++) {
                    var square = new Square() { Coordiantes = new PointF(x, y) };
                    square.Draw(e);
                    squares.Add(square);
                    x += cellWidth;
                }
                x = 0.0F;
                y += cellWidth;
            }

            squares[0].RemoveRightSide(e); 
            squares[16].RemoveBottomSide(e); 
            squares[26].RemoveLeftSide(e); 

        }
    }
}