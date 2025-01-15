using System;

namespace Links.Scripts
{
    [Serializable]
    public class Node
    {
        public int x;
        public int y;

        public int linksTop = -1;
        public int linksRight = -1;
        public int linksBottom = -1;
        public int linksLeft = -1;

        public NodeType nodeType;

        public void Rotate()
        {
            var temp = linksLeft;
            linksLeft = linksBottom;
            linksBottom = linksRight;
            linksRight = linksTop;
            linksTop = temp;
        }

        public Node Clone()
        {
            return MemberwiseClone() as Node;
        }
    }
}