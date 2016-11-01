using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cg_2_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


    }



    abstract class RoundMesh3D
    {
        protected int n = 10;
        protected int r = 20;
        protected Point3DCollection points;
        protected Int32Collection triangleIndices;

        public virtual int Radius
        {
            get { return r; }
            set { r = value; CalculateGeometry(); }
        }

        public virtual int Separators
        {
            get { return n; }
            set { n = value; CalculateGeometry(); }
        }

        public Point3DCollection Points
        {
            get { return points; }
        }

        public Int32Collection TriangleIndices
        {
            get { return triangleIndices; }
        }

        protected abstract void CalculateGeometry();
    }

    class SphereGeometry3D : RoundMesh3D
    {
        protected override void CalculateGeometry()
        {
            int e;
            double segmentRad = Math.PI / 2 / (n + 1);
            int numberOfSeparators = 4 * n + 4;

            points = new Point3DCollection();
            triangleIndices = new Int32Collection();

            for (e = -n; e <= n; e++)
            {
                double r_e = r * Math.Cos(segmentRad * e);
                double y_e = r * Math.Sin(segmentRad * e);

                for (int s = 0; s <= (numberOfSeparators - 1); s++)
                {
                    double z_s = r_e * Math.Sin(segmentRad * s) * (-1);
                    double x_s = r_e * Math.Cos(segmentRad * s);
                    points.Add(new Point3D(x_s, y_e, z_s));
                }
            }
            points.Add(new Point3D(0, r, 0));
            points.Add(new Point3D(0, -1 * r, 0));

            for (e = 0; e < 2 * n; e++)
            {
                for (int i = 0; i < numberOfSeparators; i++)
                {
                    triangleIndices.Add(e * numberOfSeparators + i);
                    triangleIndices.Add(e * numberOfSeparators + i +
                                        numberOfSeparators);
                    triangleIndices.Add(e * numberOfSeparators + (i + 1) %
                                        numberOfSeparators + numberOfSeparators);

                    triangleIndices.Add(e * numberOfSeparators + (i + 1) %
                                        numberOfSeparators + numberOfSeparators);
                    triangleIndices.Add(e * numberOfSeparators +
                                       (i + 1) % numberOfSeparators);
                    triangleIndices.Add(e * numberOfSeparators + i);
                }
            }

            for (int i = 0; i < numberOfSeparators; i++)
            {
                triangleIndices.Add(e * numberOfSeparators + i);
                triangleIndices.Add(e * numberOfSeparators + (i + 1) %
                                    numberOfSeparators);
                triangleIndices.Add(numberOfSeparators * (2 * n + 1));
            }

            for (int i = 0; i < numberOfSeparators; i++)
            {
                triangleIndices.Add(i);
                triangleIndices.Add((i + 1) % numberOfSeparators);
                triangleIndices.Add(numberOfSeparators * (2 * n + 1) + 1);
            }
        }
    }

    class BigPlanet : SphereGeometry3D
    {
        public BigPlanet()
        {
            Radius = 30;
            Separators = 5;
        }
    }
}
