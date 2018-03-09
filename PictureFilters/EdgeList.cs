using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PictureFilters
{
    class EdgeList
    {
        public EdgeNode Head { get; set; }
        public EdgeNode Tail { get; set; }
        public int EdgeCount { get; set; }

        public EdgeList()
        {
            EdgeCount = 0;
        }

        public EdgeNode this[int i]
        {
            get
            {
                if (i >= EdgeCount)
                    return null;
                EdgeNode tmp = Head;
                for (int j = 0; j < i; j++)
                    tmp = tmp.NextEdge;
                return tmp;
            }
            set
            {
                if (i >= EdgeCount)
                    return;
                EdgeNode tmp = Head;
                for (int j = 0; j < i; j++)
                    tmp = tmp.NextEdge;
                tmp = value;
            }
        }
        public void Add(EdgeNode e)
        {
            if (Head == null)
                Head = Tail = e;
            else
                Tail.NextEdge = e;
            Tail = e;
            Tail.NextEdge = null;
            EdgeCount++;
            return;
        }

        public void Append(EdgeList list)
        {
            if (Head == null)
            {
                Head = Tail = list.Head;
            }
            else
                Tail.NextEdge = list.Head;
            Tail = list.Tail;
            EdgeCount += list.EdgeCount;
        }

        private void DeleteAtIndex(int index)
        {
            if (index >= EdgeCount)
                return;
            if (index == 0)
                Head = Head.NextEdge;
            EdgeNode tmp = Head;
            EdgeNode previous = tmp;
            for (int i = 0; i <= index; i++)
            {
                previous = tmp;
                tmp = tmp.NextEdge;
            }
            previous.NextEdge = tmp.NextEdge;
            tmp.NextEdge = null;
            EdgeCount--;
        }

        public void Delete(EdgeNode e)
        {
            EdgeList deleted = new EdgeList();
            EdgeNode tmp = Head;
            while (tmp != null)
            {
                if (tmp != e)
                    deleted.Add(tmp);
                tmp = tmp.NextEdge;
            }
            EdgeCount = deleted.EdgeCount;
            Head = deleted.Head;
            Tail = deleted.Tail;
        }
        public void DeleteFinishedLines(int y)
        {
            List<EdgeNode> list = new List<EdgeNode>(EdgeCount);
            EdgeNode p = Head;
            while (p != null)
            {
                list.Add(p);
                p = p.NextEdge;
            }
            List<EdgeNode> del = list.FindAll(x => ((int)Math.Floor(x.Ymax)) <= y);
            foreach (var item in del)
                list.Remove(item);
            Head = Tail = null;
            EdgeCount = 0;
            foreach (var item in list)
                Add(item);

        }

        public void UpdateXmins()
        {
            for (int i = 0; i < EdgeCount; i++)
                this[i].UpdateXmin();
        }
        public void SortByX()
        {
            List<EdgeNode> list = new List<EdgeNode>(EdgeCount);
            EdgeNode p = Head;
            while (p != null)
            {
                list.Add(p);
                p = p.NextEdge;
            }
            list.Sort();
            Head = Tail = null;
            EdgeCount = 0;
            foreach (var item in list)
                Add(item);
        }
    }
}