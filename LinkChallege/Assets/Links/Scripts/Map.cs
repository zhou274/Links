using System;
using System.Collections.Generic;
using System.Linq;
using GameTemplate.Scripts;
using Random = System.Random;

namespace Links.Scripts
{
    public class Map : IClonable<Map>
    {
        private Random m_random;

        public Node[,] nodes = new Node[5,5];

        public List<Node> solution = new List<Node>();

        public List<Node> nodesList = new List<Node>();
        public string name;
        public int index;
        public int collectionIndex;

        public int moves3stars;
        public int moves2stars;

        private Random Randomizer
        {
            get
            {
                if (m_random != null)
                {
                    return m_random;
                }
                m_random = new Random(name.GetHashCode() + collectionIndex * 1000 + index);
                return m_random;
            }
        }

        public Map Clone()
        {
            var newMap = new Map();
            newMap.name = name;
            newMap.index = index;
            newMap.collectionIndex = collectionIndex;
            newMap.moves3stars = moves3stars;
            newMap.moves2stars = moves2stars;

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    var node = nodes[x, y];
                    if (node == null)
                    {
                        continue;
                    }

                    var newMapNode = node.Clone();
                    newMap.nodes[x, y] = newMapNode;
                    newMap.nodesList.Add(newMapNode);
                }
            }

            foreach (var solutionNode in solution)
            {
                newMap.solution.Add(solutionNode.Clone());
            }

            return newMap;
        }

        public void GenerateRandom(int numNodes)
        {
            nodesList = new List<Node>();

            for (int i = 0; i < numNodes && nodesList.Count <= 25; i++)
            {
                Node existingNode = null;
                int x = 0, y = 0;
                int dir = -1;
                if (i == 0)
                {
                    var index = Randomizer.Next(25);
                    x = index / 5;
                    y = index % 5;
                }
                else
                {
                    int attempts = 0;
                    bool found = false;
                    while (attempts < 1000)
                    {
                        existingNode = nodesList[Randomizer.Next(nodesList.Count)];
                        x = existingNode.x;
                        y = existingNode.y;
                        dir = Randomizer.Next(4);
                        switch (dir)
                        {
                            case 0: y--; break;
                            case 1: y++; break;
                            case 2: x--; break;
                            case 3: x++; break;
                        }

                        if (x >= 0 && y >= 0 & x < 5 && y < 5 && nodes[x, y] == null)
                        {
                            found = true;
                            break;
                        }

                        attempts++;
                    }

                    if (!found)
                    {
                        return;
                    }
                }

                var newNode = new Node {x = x, y = y};
                nodesList.Add(newNode);
                nodes[x, y] = newNode;

                if (existingNode != null)
                {
                    var links = Randomizer.Next(1, 4);
                    switch (dir)
                    {
                        case 0:
                        {
                            existingNode.linksTop = newNode.linksBottom = links;
                            break;
                        }
                        case 1:
                        {
                            existingNode.linksBottom = newNode.linksTop = links;
                            break;
                        }
                        case 2:
                        {
                            existingNode.linksLeft = newNode.linksRight = links;
                            break;
                        }
                        case 3:
                        {
                            existingNode.linksRight = newNode.linksLeft = links;
                            break;
                        }
                    }
                }
            }

            foreach (var solutionNode in nodesList)
            {
                solution.Add(solutionNode.Clone());
            }
        }

        public void RandomizeMap(int numChanges, List<NodeType> allowedNodeTypes)
        {
            if (numChanges > nodesList.Count)
            {
                throw new Exception("Invalid number of changes requested");
            }


            int nonStaticNodesCount = numChanges;
            while (nonStaticNodesCount > 0)
            {
                var existingNode = nodesList[Randomizer.Next(nodesList.Count)];
                if (existingNode.nodeType != NodeType.Static)
                {
                    continue;
                }
                
                var type = allowedNodeTypes[Randomizer.Next(allowedNodeTypes.Count)];
                existingNode.nodeType = type;

                var matchingSoltuonNode = solution.Single(solutionNode => solutionNode.x == existingNode.x && solutionNode.y == existingNode.y);
                matchingSoltuonNode.nodeType = type;

                nonStaticNodesCount--;
            }

            var randomizableNodes = nodesList
                .Where(n => n.nodeType != NodeType.Static)
                .OrderBy(x => (int)x.nodeType)
                .ToList();

            foreach (var existingNode in randomizableNodes)
            {
                bool succeededRandomizing = false;
               
                if ((existingNode.nodeType & NodeType.Rotatable) == NodeType.Rotatable)
                {
                    var numRotations = 1 + Randomizer.Next(3);
                    for (int i = 0; i < numRotations; i++)
                    {
                        existingNode.Rotate();
                    }
                    succeededRandomizing = true;

                    moves3stars += 4 - numRotations;
                }

                if ((existingNode.nodeType & NodeType.Moveable) != 0)
                {
                    for (int attempts = 0; attempts < 1000; attempts++)
                    {
                        var changeX = ((existingNode.nodeType & NodeType.MoveHorizontal) != 0);
                        var changeY = ((existingNode.nodeType & NodeType.MoveVertical) != 0);
                        var newX = changeX ? Randomizer.Next(5) : existingNode.x;
                        var newY = changeY ? Randomizer.Next(5) : existingNode.y;
                        if (nodes[newX, newY] != null)
                        {
                            continue;
                        }

                        nodes[existingNode.x, existingNode.y] = null;
                        existingNode.x = newX;
                        existingNode.y = newY;
                        nodes[existingNode.x, existingNode.y] = existingNode;
                    
                        succeededRandomizing = true;
                        moves3stars++;

                        break;
                    }
                }

                if (succeededRandomizing)
                {
                    // Don't randomize the same node twice
                    numChanges--;
                }
            }

            // One extra move for getting 3 stars
            moves3stars++;
            // 5 extra moves for getting 2 stars
            moves2stars = moves3stars + 5;
        }

        public bool CheckLevelCompleted()
        {
            foreach (var node in nodesList)
            {
                if (node.linksTop > 0)
                {
                    if (node.y <= 0)
                    {
                        return false;
                    }
                    if (nodes[node.x, node.y - 1]?.linksBottom != node.linksTop)
                    {
                        return false;
                    }
                }
                if (node.linksRight > 0)
                {
                    if (node.x >= 4)
                    {
                        return false;
                    }
                    if (nodes[node.x + 1, node.y]?.linksLeft != node.linksRight)
                    {
                        return false;
                    }
                }
                if (node.linksBottom > 0)
                {
                    if (node.y >= 4)
                    {
                        return false;
                    }
                    if (nodes[node.x, node.y + 1]?.linksTop != node.linksBottom)
                    {
                        return false;
                    }
                }

                if (node.linksLeft > 0)
                {
                    if (node.x <= 0)
                    {
                        return false;
                    }
                    if (nodes[node.x - 1, node.y]?.linksRight != node.linksLeft)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}