using System;
using System.Collections.Generic;
using System.Linq;

namespace Links.Scripts
{
    public class LevelsScript : GameTemplate.Scripts.LevelsScript<Map, MapScript>
    {
        private List<NodeType> allChanges = new List<NodeType> { NodeType.Rotatable, NodeType.Moveable, NodeType.MoveHorizontal, NodeType.MoveVertical, NodeType.MoveableRotatable };

        public override Map PrepareLevel(int collectionIndex, int levelIndex)
        {
            Map level;
            if (collectionIndex == 0)
            {
                var prepareLevel = PrepareLevelCollectionEasy(levelIndex);
                if (prepareLevel != null)
                {
                    return prepareLevel;
                }
            }
            if (collectionIndex == 1)
            {
                var prepareLevel = PrepareLevelCollectionNormal(levelIndex);
                if (prepareLevel != null)
                {
                    return prepareLevel;
                }
            }
            if (collectionIndex == 2)
            {
                var prepareLevel = PrepareLevelCollectionHard(levelIndex);
                if (prepareLevel != null)
                {
                    return prepareLevel;
                }
            }

            // Calculate number of nodes, up to 24 (leaving one empty node for the randomization)
            var nodesCount = Math.Min(levelIndex < 2 ? 2 : levelIndex < 5 ? 3 : 3 + (levelIndex / 8), 24);
            // Calculate the available changes, based on the level
            var availableChangesCount = (levelIndex < 2 ? 1 : levelIndex < 10 ? 2 : 2 + ((levelIndex + 10) / 40));
            var availableChanges = allChanges.Take(availableChangesCount).ToList();

            level = new Map();
            level.index = levelIndex;
            level.name = "关卡 " + (levelIndex + 1);
            level.GenerateRandom(nodesCount);
            level.RandomizeMap(nodesCount - 1, availableChanges);

            return level;
        }

        private Map PrepareLevelCollectionEasy(int levelIndex)
        {
            var level = new Map();
            level.index = levelIndex;
            level.collectionIndex = 0;
            level.name = "关卡 " + (levelIndex + 1);
            
            if (levelIndex < 5)
            {
                // Levels 1 - 5 - Rotatable only

                level.GenerateRandom(levelIndex + 2);
                level.RandomizeMap(levelIndex + 1, new List<NodeType> {NodeType.Rotatable});
            }

            else if (levelIndex < 10)
            {
                // Levels 6 - 10 - Moveable only
                level.GenerateRandom(levelIndex - 5 + 2);
                level.RandomizeMap(levelIndex - 5 + 1, new List<NodeType> {NodeType.Moveable});
            }

            else if (levelIndex < 20)
            {
                // Levels 11 - 20 - Rotatable & Moveable, 6 nodes
                level.GenerateRandom(6);
                level.RandomizeMap(5, new List<NodeType> {NodeType.Rotatable, NodeType.Moveable});
            }

            else if (levelIndex < 25)
            {
                // Levels 21 - 25 - MoveableRotatable only
                level.GenerateRandom(levelIndex - 20 + 2);
                level.RandomizeMap(levelIndex - 20 + 1, new List<NodeType> {NodeType.MoveableRotatable});
            }

            else if (levelIndex < 40)
            {
                // Levels 26 - 40 - Rotatable, Moveable, MoveableRotatable, 6 nodes
                level.GenerateRandom(6);
                level.RandomizeMap(5, new List<NodeType> {NodeType.Rotatable, NodeType.Moveable, NodeType.MoveableRotatable});
            }

            else if (levelIndex < 45)
            {
                // Levels 41 - 45 - MoveHorizontal only
                level.GenerateRandom(levelIndex - 40 + 2);
                level.RandomizeMap(levelIndex - 40 + 1, new List<NodeType> {NodeType.MoveHorizontal});
            }

            else if (levelIndex < 50)
            {
                // Levels 46 - 50 - MoveVertical only
                level.GenerateRandom(levelIndex - 45 + 2);
                level.RandomizeMap(levelIndex - 45 + 1, new List<NodeType> {NodeType.MoveVertical});
            }

            else if (levelIndex < 60)
            {
                // Levels 51 - 60 - MoveHorizontal, MoveVertical only
                level.GenerateRandom(6);
                level.RandomizeMap(5, new List<NodeType> {NodeType.MoveHorizontal, NodeType.MoveVertical});
            }

            return level;
        }

        private Map PrepareLevelCollectionNormal(int levelIndex)
        {
            var numNodes = Math.Min(8 + (levelIndex / 10), 20);
            var level = new Map();
            level.index = levelIndex;
            level.collectionIndex = 1;
            level.name = "关卡 " + (levelIndex + 1);
            level.GenerateRandom(numNodes);
            level.RandomizeMap(numNodes, allChanges);

            return level;
        }

        private Map PrepareLevelCollectionHard(int levelIndex)
        {
            var numNodes = Math.Min(12 + (levelIndex / 10), 20);
            var level = new Map();
            level.index = levelIndex;
            level.collectionIndex = 2;
            level.name = "关卡 " + (levelIndex + 1);
            level.GenerateRandom(numNodes);
            level.RandomizeMap(numNodes, allChanges);

            return level;
        }
    }
}