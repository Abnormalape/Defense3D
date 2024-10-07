using BHSSolo.DungeonDefense.Contruct;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

namespace BHSSolo.DungeonDefense.Controller
{
    public static class GridPathFinder
    {
        /// <summary>
        /// Reach next Crossroad.
        /// </summary>
        /// <param name="startGrid">Crossroad Grid.</param>
        /// <param name="excludeGrids">Grids to excule like grid heading to deadend.</param>
        /// <param name="gridPath">Path to use.</param>
        public static void FindPathWithNoGoal(
            DungeonGridData startGrid,
            List<DungeonGridData> excludeGrids,
            out List<DungeonGridData> gridPath
            ) //Find Path when Next Crossroad Appears.
        {
            List<DungeonGridData> tempPath = new();

            List<DungeonGridData> connectedRooms = new List<DungeonGridData>(
                startGrid.ConnectedRooms.Where(room => !excludeGrids.Contains(room))); //Find Forks which is not included in EcludeGrids.

            if (connectedRooms.Count == 0)
            {
                //You are on corssroad, but you've visited all forks of It.
                //You have to go back to previous crossroad.
                gridPath = tempPath; //Goto Previous crossRoad on other Method.
                Debug.Log("Find other path.");
                return;
            }
            else
            {
                //Find Next Path.
                tempPath.Add(startGrid);

                int pathNumber = UnityEngine.Random.Range(0, connectedRooms.Count);

                FindPathWithNoGoalRecursive(connectedRooms[pathNumber], startGrid, connectedRooms[pathNumber], excludeGrids, tempPath, out excludeGrids, out tempPath);
            }

            gridPath = tempPath;
        }

        private static void FindPathWithNoGoalRecursive(
            DungeonGridData startGrid,
            DungeonGridData prevGrid,
            DungeonGridData crossroadFork,
            List<DungeonGridData> excludeGridDatas,
            List<DungeonGridData> previousGridPath,
            out List<DungeonGridData> excludeGrids,
            out List<DungeonGridData> gridPath
            )
        {
            List<DungeonGridData> tempExcludeGridDatas = excludeGridDatas;

            List<DungeonGridData> tempGridPath = previousGridPath;
            tempGridPath.Add(startGrid);

            if (startGrid.ConnectedRooms.Count == 2)
            {
                if (startGrid.ConnectedRooms[0] == prevGrid)
                {
                    FindPathWithNoGoalRecursive(startGrid.ConnectedRooms[1], startGrid, crossroadFork, tempExcludeGridDatas, tempGridPath, out tempExcludeGridDatas, out tempGridPath);
                }
                else
                {
                    FindPathWithNoGoalRecursive(startGrid.ConnectedRooms[0], startGrid, crossroadFork, tempExcludeGridDatas, tempGridPath, out tempExcludeGridDatas, out tempGridPath);
                }

                excludeGrids = tempExcludeGridDatas;
                gridPath = tempGridPath;
            }
            else if (startGrid.ConnectedRooms.Count == 1) //DeadEnd. Add This Path's fork to excludeGrid.
            {
                tempExcludeGridDatas.Add(crossroadFork);
                excludeGrids = tempExcludeGridDatas;
                gridPath = tempGridPath;
                return;
            }
            else //You are on crossRoad now. Do next Action on other method.
            {
                excludeGrids = tempExcludeGridDatas;
                gridPath = tempGridPath;
                return;
            }
        }


        /// <summary>
        /// Eventualy reach Goal.
        /// </summary>
        /// <param name="startGrid"></param>
        /// <param name="endGrid"></param>
        /// <param name="gridPath"></param>
        public static void FindPathToGoal(DungeonGridData startGrid, DungeonGridData endGrid, out List<DungeonGridData> gridPath)
        {
            List<DungeonGridData> tempGridPath = new();

            List<DungeonGridData> crossroadGrids = new();
            List<DungeonGridData> excludeGrids = new();
            excludeGrids.Add(startGrid);

            Debug.Log("Start Grid : " + startGrid.namename);

            if (startGrid.ConnectedRooms.Count == 1)
            {
                tempGridPath.Add(startGrid);

                FindPathToGoalRecursive(startGrid.ConnectedRooms.First(), endGrid, startGrid, ref excludeGrids, ref crossroadGrids, ref tempGridPath);
            }
            else if (startGrid.ConnectedRooms.Count >= 2)
            {
                tempGridPath.Add(startGrid);
                crossroadGrids.Add(startGrid);

                int randomFork = UnityEngine.Random.Range(0, startGrid.ConnectedRooms.Count);
                DungeonGridData forkGrid = startGrid.ConnectedRooms[randomFork];

                excludeGrids.Add(forkGrid);

                FindPathToGoalRecursive(forkGrid, endGrid, startGrid, ref excludeGrids, ref crossroadGrids, ref tempGridPath);
            }
            else
            {
                Debug.Log("Connected Rooms Can't Be Under 1!!!");
            }

            gridPath = tempGridPath;
        }

        private static void FindPathToGoalRecursive(
            DungeonGridData startGrid,
            DungeonGridData endGrid,
            DungeonGridData prevGrid,
            ref List<DungeonGridData> excludeGrids,
            ref List<DungeonGridData> crossroadGrids,
            ref List<DungeonGridData> gridPath)
        {
            Debug.Log($"On Grid {startGrid.namename}.");

            gridPath.Add(startGrid);

            if (startGrid == endGrid) //Arrive to End.
            {
                return;
            }

            List<DungeonGridData> tempExcludeGrids = excludeGrids;
            tempExcludeGrids.Add(startGrid);

            List<DungeonGridData> connectedRooms
                = new List<DungeonGridData>(
                startGrid.ConnectedRooms.Where(room => !tempExcludeGrids.Contains(room)));

            if (connectedRooms.Count == 0)
            {
                if (crossroadGrids.Contains(startGrid))
                {
                    crossroadGrids.Remove(crossroadGrids.Last());
                }

                if (crossroadGrids.Last() == null)
                {
                    Debug.Log("Face DeadEnd and no way to get EndPoint. this Can't be.");
                    return;
                }

                DungeonGridData lastCrossRoad = crossroadGrids.Last();

                int lastCrossRoadIndex = gridPath.IndexOf(lastCrossRoad);
                Debug.Log($"Moving to crossroad name {gridPath[lastCrossRoadIndex].namename}");
                gridPath.RemoveRange(lastCrossRoadIndex, gridPath.Count - lastCrossRoadIndex);

                FindPathToGoalRecursive(crossroadGrids.Last(), endGrid, startGrid, ref excludeGrids, ref crossroadGrids, ref gridPath);
            }
            else if (connectedRooms.Count == 1)
            {
                FindPathToGoalRecursive(connectedRooms.First(), endGrid, startGrid, ref excludeGrids, ref crossroadGrids, ref gridPath);
            }
            else if (connectedRooms.Count >= 2)
            {
                int randomFork = UnityEngine.Random.Range(0, connectedRooms.Count);

                crossroadGrids.Add(startGrid);
                excludeGrids.Add(connectedRooms[randomFork]);

                FindPathToGoalRecursive(connectedRooms[randomFork], endGrid, startGrid, ref excludeGrids, ref crossroadGrids, ref gridPath);
            }
            else
            {
                Debug.Log("Connected rooms can't be minus");
            }
        }


        /// <summary>
        /// Find Shortcut To Goal
        /// </summary>
        /// <param name="startGrid"></param>
        /// <param name="endGrid"></param>
        /// <param name="gridPath"></param>
        public static void FindShortcutToGoal(DungeonGridData startGrid, DungeonGridData endGrid, out List<DungeonGridData> gridPath)
        {
            List<DungeonGridData> tempGridPath = new();

            FindShortcutToGoalRecursive(startGrid, endGrid, ref tempGridPath);

            gridPath = tempGridPath;
        }

        private static void FindShortcutToGoalRecursive(DungeonGridData startGrid, DungeonGridData endGrid, ref List<DungeonGridData> gridPath)
        {
            if (startGrid.ConnectedRooms.Contains(endGrid))
            {
                Debug.Log("Found!");
                return;
            }
            else
            {
                foreach (DungeonGridData grid in startGrid.ConnectedRooms)
                {
                    FindShortcutToGoalRecursive(grid, endGrid, ref gridPath);
                }
            }
        }


        public static void SetNodeGrids(List<DungeonGridData> Grids)
        {
            Dictionary<DungeonGridData, GridNode> gridNodeDictionary = new(300);
            List<GridNode> NODES = new(300);

            foreach (DungeonGridData grid in Grids)
            {
                if (!grid.IsContructed) { continue; }

                if (grid.ConnectedRooms.Count != 2)
                {
                    gridNodeDictionary.Add(grid, new(grid));
                }
                else if (!grid.IsRoad)
                {
                    gridNodeDictionary.Add(grid, new(grid));
                }
            }

            foreach (var e in gridNodeDictionary)
            {
                GridNode node;
                node = e.Value;

                List<DungeonGridData> exclude = new(1);
                exclude.Add(node.NodeGrid);
                int loops = 0;
                
                foreach (DungeonGridData connectedGrid in node.NodeGrid.ConnectedRooms)
                {

                    List<DungeonGridData> nodePath = new(30);

                    if (connectedGrid.ConnectedRooms.Count != 2) //연결된 그리드가 바로 교차로일때
                    {
                        nodePath.Add(connectedGrid);
                    }
                    else if (!connectedGrid.IsRoad) //연결된 그리드가 바로 방일때
                    {
                        nodePath.Add(connectedGrid);
                    }
                    else
                    {
                        FindPathWithNoGoal(connectedGrid, exclude, out nodePath);
                    }
                    //위의 메소드는 시작점과 도착점의 그리드를 전부 반환한다.
                    //마지막 grid는 crossRoad이며, Node이다.
                    //마지막 grid가 Room이면 가중치가 추가된다.


                    DungeonGridData nextNode = nodePath.Last();

                    node.ConnectedNode[loops] = gridNodeDictionary[nextNode];
                    node.ConnectedNodePath[loops] = new List<DungeonGridData>(nodePath);

                    if (!node.ConnectedNode[loops].NodeGrid.IsRoad) //isRoom
                    {
                        node.ConnectedNodePathWeight[loops] = node.ConnectedNodePath[loops].Count + 2;
                    }
                    else
                    {
                        node.ConnectedNodePathWeight[loops] = node.ConnectedNodePath[loops].Count;
                    }
                    loops++;
                }

                NODES.Add(node);
            }

            FindShortestWay(NODES[NODES.Count - 3], NODES[NODES.Count - 2], NODES);
        }

        private static void FindShortestWay(GridNode startNode, GridNode endNode, List<GridNode> nodes)
        {
            Dictionary<GridNode, int> nodeDistance = new();
            Dictionary<GridNode, GridNode> prevNodes = new();
            List<GridNode> unvisitedNodes = new();
            List<GridNode> tempVisitedNodes = new();

            foreach (var e in nodes)
            {
                nodeDistance.Add(e, int.MaxValue);
                prevNodes.Add(e, null);
            }

            nodeDistance[startNode] = 0;

            for (int i = 0; i < startNode.ConnectedNode.Length; i++)
            {
                nodeDistance[startNode.ConnectedNode[i]] = startNode.ConnectedNodePathWeight[i];
                unvisitedNodes.Add(startNode.ConnectedNode[i]);
            }

            tempVisitedNodes.Add(startNode);

            foreach (var e in unvisitedNodes)
            {
                tempVisitedNodes.Add(e);

                FindShortestWayRecursive(
                    e,
                    endNode,
                    tempVisitedNodes,
                    nodeDistance[e],
                    ref nodeDistance,
                    ref prevNodes);

                prevNodes[e] = startNode;

                tempVisitedNodes.Remove(e);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            foreach (var e in nodeDistance)
            {
                sb.Append(e.Value.ToString());
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");

            Debug.Log(sb.ToString());

            List<DungeonGridData> path =
                FindGridPath(endNode, startNode, prevNodes);

            foreach (var e in path)
            {
                Debug.Log(e.namename);
            }
        }

        private static void FindShortestWayRecursive(
            GridNode startNode,
            GridNode endNode,
            List<GridNode> visitedNodes,
            int currentDistance,
            ref Dictionary<GridNode, int> nodeDistance,
            ref Dictionary<GridNode, GridNode> linkedNodes)
        {
            if (startNode == endNode) { return; }

            List<GridNode> tempVisitedNodes = new List<GridNode>(visitedNodes);

            for (int i = 0; i < startNode.ConnectedNode.Length; i++)
            {
                if (visitedNodes.Contains(startNode.ConnectedNode[i])) { continue; }

                if (currentDistance + startNode.ConnectedNodePathWeight[i] < nodeDistance[startNode.ConnectedNode[i]])
                {
                    nodeDistance[startNode.ConnectedNode[i]] = currentDistance + startNode.ConnectedNodePathWeight[i];
                    linkedNodes[startNode.ConnectedNode[i]] = startNode;

                    tempVisitedNodes.Add(startNode.ConnectedNode[i]);

                    FindShortestWayRecursive(
                        startNode.ConnectedNode[i],
                        endNode,
                        tempVisitedNodes,
                        nodeDistance[startNode.ConnectedNode[i]],
                        ref nodeDistance,
                        ref linkedNodes);

                    tempVisitedNodes.Remove(startNode.ConnectedNode[i]);
                }
            }
        }

        private static List<DungeonGridData> FindGridPath(GridNode endNode, GridNode startNode, Dictionary<GridNode, GridNode> linkedNodes)
        {
            GridNode currentNode = endNode;
            List<DungeonGridData> path = new();

            while (currentNode != startNode)
            {
                for (int i = 0; i < linkedNodes[currentNode].ConnectedNode.Length; i++)
                {
                    if (linkedNodes[currentNode].ConnectedNode[i] == currentNode)
                    {
                        List<DungeonGridData> tempPath = new();
                        tempPath.AddRange(linkedNodes[currentNode].ConnectedNodePath[i]);
                        tempPath.AddRange(path);
                        path = tempPath;
                        currentNode = linkedNodes[currentNode];
                        break;
                    }
                }
            }

            return path;
        }
    }

    public class GridNode
    {
        public GridNode(DungeonGridData grid)
        {
            NodeGrid = grid;
            int connections = NodeGrid.ConnectedRooms.Count;
            ConnectedNode = new GridNode[connections];
            ConnectedNodePath = new List<DungeonGridData>[connections];
            ConnectedNodePathWeight = new int[connections];
        }

        public DungeonGridData NodeGrid { get; private set; }

        public GridNode[] ConnectedNode;
        public List<DungeonGridData>[] ConnectedNodePath;
        public int[] ConnectedNodePathWeight;
    }
}


