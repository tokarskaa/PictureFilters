using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PictureFilters
{
    class EdgeTable
    {
        private EdgeList[] edgeTable;
        private int size;
        public int EdgeCount { get; set; }

        public EdgeTable(int size)
        {
            edgeTable = new EdgeList[size + 1];
            this.size = size;
            EdgeCount = 0;
        }

        public EdgeList this[int i]
        {
            get { return edgeTable[i]; }
            set { edgeTable[i] = value; }
        }

        public void AddAtIndex(int index, EdgeNode node)
        {
            if (edgeTable[index] == null)
                edgeTable[index] = new EdgeList();
            edgeTable[index].Add(node);
            EdgeCount++;
        }

        public EdgeList GetListAtIndex(int index)
        {
            if (index >= size)
                return null;
            EdgeList tmp = edgeTable[index];
            if (tmp == null)
                return null;
            EdgeCount -= edgeTable[index].EdgeCount;
            edgeTable[index] = null;
            return tmp;
        }

    }
}