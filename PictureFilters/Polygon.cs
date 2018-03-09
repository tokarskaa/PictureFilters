using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace PictureFilters
{
    class Polygon
    {
        public List<Edge> Edges { get; set; }
       
        public Polygon()
        {
            Edges = new List<Edge>();
        }

     

        public void AddLine(Edge e)
        {
            if (Edges.Count != 0)
            {
                e.Vertices[0] = Edges[Edges.Count - 1].Vertices[1];
                if (e.Vertices[1].Equals(Edges[0].Vertices[0]))
                    e.Vertices[1] = Edges[0].Vertices[0];
            }
            Edges.Add(e);
        }

        public bool IsPolygonFinished(Point p)
        {
            Edge e = GetEdgeFromPoint(p);
            if (IsPointInEdges(p) && e == Edges[0])
            {
                AddLastEgde();
                return true;
            }
            return false;
        }
         public void AddLastEgde()
        {
            Edges.Add(new Edge(Edges[Edges.Count - 1].Vertices[1], Edges[0].Vertices[0]));
        }
  

        public Edge GetEdgeFromPoint(Point point)
        {
            if (Edges.Count == 0)
                return null;
            foreach (var edge in Edges)
            {
                if (edge.IsPointInEdge(point))
                    return edge;
            }
            return null;
        }

       

        public bool IsPointInEdges(Point point)
        {
            if (GetEdgeFromPoint(point) == null)
                return false;
            return true;
        }

       

        public double GetMinY()
        {
            double min = int.MaxValue;
            foreach (Edge e in Edges)
                if (e.GetMinY() < min)
                    min = e.GetMinY();
            return min;
        }

        public double GetMaxY()
        {
            double max = 0;
            foreach (Edge e in Edges)
                if (e.GetMaxY() > max)
                    max = e.GetMaxY();
            return max;
        }

        public double GetMinX()
        {
            double min = int.MaxValue;
            foreach (Edge e in Edges)
                if (e.GetMinX() < min)
                    min = e.GetMinX();
            return min;
        }

        public double GetMaxX()
        {
            double max = 0;
            foreach (Edge e in Edges)
                if (e.GetMaxX() > max)
                    max = e.GetMaxX();
            return max;
        }
    }
}