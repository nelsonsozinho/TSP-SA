using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Playground
{
    public partial class View : Form
    {
        private readonly ViewModel _vm;

        internal View(ViewModel vm)
        {
            _vm = vm;
            InitializeComponent();

            OutputPictureBox.Paint += OutputPictureBox_Paint;
            OutputPictureBox.MouseDown += OutputPictureBox_MouseDown;
            Resize += (s, e) => OutputPictureBox.Invalidate();

        }

        void OutputPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            var x = (int)Math.Round((e.Location.X - UtilLeft) / CellWidth);
            var y = (int)Math.Round((e.Location.Y - UtilTop) / CellHeight);
            _vm.PickAt(x, y);
            using (var g = OutputPictureBox.CreateGraphics()) Render(g);
        }

        void OutputPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Render(e.Graphics);
        }

        private void Render(Graphics g)
        {
            Text = _vm.PlanningGeneration.ToString();

            g.Clear(SystemColors.Control);
            for (var i = 0; i <= SizeOfGrid; i++)
            {
                g.DrawLine(
                    Pens.LightGray,
                    new Point(UtilLeft, GetY(i)),
                    new Point(UtilRight, GetY(i))
                    );

                g.DrawLine(
                    Pens.LightGray,
                    new Point(GetX(i), UtilTop),
                    new Point(GetX(i), UtilBottom)
                    );
            }

            var cities = _vm.Route.ToArray();
            using (var pen = new Pen(Color.Green, 2))
            {
                for (int i = 0; i < cities.Length; i++)
                {
                    var j = (i + 1)%cities.Length;
                    g.DrawLine(pen,
                        new Point(GetX((int) cities[i].LocationX), GetY((int) cities[i].LocationY)),
                        new Point(GetX((int) cities[j].LocationX), GetY((int) cities[j].LocationY))
                        );
                }
            }

            foreach (var city in cities)
            {
                g.FillEllipse(Brushes.Red,
                    GetX((int)city.LocationX) - 5,
                    GetY((int)city.LocationY) - 5,
                    10,
                    10
                    );
            }

        }

        private const int SizeOfGrid = 20;

        int GetX(int original)
        {
            return (int)(UtilLeft + original*(((double)UtilWidth)/SizeOfGrid));
        }

        int GetY(int original)
        {
            return (int)(UtilTop + original * (((double)UtilHeight) /SizeOfGrid));
        }

        private const int Margin = 5;
        int UtilTop { get { return Margin; } }
        int UtilLeft { get { return Margin;  } }
        int UtilBottom { get { return OutputPictureBox.ClientSize.Height - Margin; } }
        int UtilRight { get { return OutputPictureBox.ClientSize.Width - Margin; } }
        int UtilWidth { get { return UtilRight - UtilLeft; } }
        int UtilHeight { get { return UtilBottom - UtilTop; } }
        double CellWidth {get { return (double) UtilWidth/SizeOfGrid; }}
        double CellHeight { get { return (double)UtilHeight / SizeOfGrid; } }

        private void RunButton_Click(object sender, EventArgs e)
        {
            _vm.Complete();
            OutputPictureBox.Invalidate();
        }

        private void IterateButton_Click(object sender, EventArgs e)
        {
            _vm.Iterate();
            OutputPictureBox.Invalidate();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            _vm.Reset();
            OutputPictureBox.Invalidate();

        }
    }
}
