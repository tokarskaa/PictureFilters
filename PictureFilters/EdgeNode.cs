using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace PictureFilters
{
    class EdgeNode : IComparable<EdgeNode>
    {
        public double Ymax { get; set; }
        public double Xmin { get; set; }
        public double DX { get; set; }
        public EdgeNode NextEdge { get; set; }

        public EdgeNode(double ymax, double xmin, double dx, EdgeNode nextedge)
        {
            Ymax = ymax;
            Xmin = xmin;
            DX = dx;
            NextEdge = nextedge;
        }
        public EdgeNode(Edge e)
        {
            Ymax = e.GetMaxY();
            Point v = e.GetMinYVertex();
            Xmin = v.X;
            DX = ((e.Vertices[1].X - e.Vertices[0].X) / (e.Vertices[1].Y - e.Vertices[0].Y));
            NextEdge = null;
        }

        public void UpdateXmin()
        {
            Xmin += DX;
        }

        public EdgeNode DeepCopy()
        {
            return new EdgeNode(Ymax, Xmin, DX, NextEdge);
        }

        public int CompareTo(EdgeNode other)
        {
            if (Xmin < other.Xmin)
                return -1;
            else if (Xmin == other.Xmin)
                return 0;
            return 1;
        }
    }
}