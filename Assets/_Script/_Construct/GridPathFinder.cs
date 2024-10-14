using BHSSolo.DungeonDefense.Contruct;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                startGrid.ConnectedGrids.Where(room => !excludeGrids.Contains(room))); //Find Forks which is not included in EcludeGrids.

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

            if (startGrid.ConnectedGrids.Count == 2 && startGrid.GridType == GridType.Passage)
            {
                if (startGrid.ConnectedGrids[0] == prevGrid)
                {
                    FindPathWithNoGoalRecursive(startGrid.ConnectedGrids[1], startGrid, crossroadFork, tempExcludeGridDatas, tempGridPath, out tempExcludeGridDatas, out tempGridPath);
                }
                else
                {
                    FindPathWithNoGoalRecursive(startGrid.ConnectedGrids[0], startGrid, crossroadFork, tempExcludeGridDatas, tempGridPath, out tempExcludeGridDatas, out tempGridPath);
                }

                excludeGrids = tempExcludeGridDatas;
                gridPath = tempGridPath;
            }
            else if (startGrid.ConnectedGrids.Count == 1) //DeadEnd. Add This Path's fork to excludeGrid.
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

            if (startGrid.ConnectedGrids.Count == 1)
            {
                tempGridPath.Add(startGrid);

                FindPathToGoalRecursive(startGrid.ConnectedGrids.First(), endGrid, startGrid, ref excludeGrids, ref crossroadGrids, ref tempGridPath);
            }
            else if (startGrid.ConnectedGrids.Count >= 2)
            {
                tempGridPath.Add(startGrid);
                crossroadGrids.Add(startGrid);

                int randomFork = UnityEngine.Random.Range(0, startGrid.ConnectedGrids.Count);
                DungeonGridData forkGrid = startGrid.ConnectedGrids[randomFork];

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
            gridPath.Add(startGrid);

            if (startGrid == endGrid) //Arrive to End.
            {
                return;
            }

            List<DungeonGridData> tempExcludeGrids = excludeGrids;
            tempExcludeGrids.Add(startGrid);

            List<DungeonGridData> connectedRooms
                = new List<DungeonGridData>(
                startGrid.ConnectedGrids.Where(room => !tempExcludeGrids.Contains(room)));

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

        public static bool CanReachGoal(GridNode startNode, GridNode endNode)
        {
            Debug.Log("Start: " + startNode.NodeGrid.ConstructedPosition);
            Debug.Log("End: " + endNode.NodeGrid.ConstructedPosition);
            if (startNode == endNode)
                return true;

            Stack<GridNode> stack = new Stack<GridNode>();
            Dictionary<GridNode, bool> visited = new Dictionary<GridNode, bool>();

            stack.Push(startNode);
            visited[startNode] = true;

            while (stack.Count > 0)
            {
                GridNode current = stack.Pop();

                if (current == endNode)
                    return true;

                foreach (GridNode neighbor in current.ConnectedNode)
                {
                    if (!visited.ContainsKey(neighbor))
                    {
                        stack.Push(neighbor);
                        visited[neighbor] = true;
                    }
                }
            }

            return false;
        }

        public static void SetNodeGrids(List<DungeonGridData> Grids, out List<GridNode> Nodes)
        {
            Dictionary<DungeonGridData, GridNode> gridNodeDictionary = new(300);
            List<GridNode> NODES = new(300);

            foreach (DungeonGridData grid in Grids)
            {
                if (!grid.IsContructed) { continue; }
                if (grid.ConnectedGrids.Count == 0) { continue; }


                if (grid.ConnectedGrids.Count != 2 && grid.GridType == GridType.Passage)
                {
                    gridNodeDictionary.Add(grid, new(grid));
                }
                else if (grid.GridType == GridType.RoomCore || grid.GridType == GridType.Entrance)
                {
                    gridNodeDictionary.Add(grid, new(grid));
                }
            }

            foreach (var e in gridNodeDictionary)
            {
                GridNode node;
                node = e.Value;

                List<DungeonGridData> exclude = new(1)
                    { node.NodeGrid };

                if (node.NodeGrid.GridType == GridType.RoomCore)
                {
                    foreach (var partGrid in node.NodeGrid.RoomCorePartGrid)
                    { exclude.Add(partGrid); }
                }

                int loops = 0;

                foreach (DungeonGridData connectedGrid in node.NodeGrid.ConnectedGrids)
                {
                    List<DungeonGridData> nodePath = new(30);

                    if (connectedGrid.ConnectedGrids.Count != 2 && connectedGrid.GridType == GridType.Passage) //연결된 그리드가 바로 교차로이거나 막다른 길일때
                    {
                        nodePath.Add(connectedGrid);
                    }
                    else if (connectedGrid.GridType == GridType.Room || //If Grid is Room
                        connectedGrid.GridType == GridType.RoomCore || //If Grid is RoomCore
                        connectedGrid.GridType == GridType.Entrance) //If Grid is Entrance
                    {
                        nodePath.Add(connectedGrid);
                    }
                    else
                    {
                        FindPathWithNoGoal(connectedGrid, exclude, out nodePath); //Find Next Node Following Path.
                        //This method ends with
                        //1. current Grid is Passage and connected count is not 2
                        //2. or not passage
                    }

                    DungeonGridData lastGrid = nodePath.Last(); //경로의 마지막의 격자를 참조한다
                    DungeonGridData nextNode;

                    if (lastGrid.GridType == GridType.Passage) //마지막 격자가 교차로, 혹은 막다른 길일 경우, 해당 격자를 다음 노드로 설정한다.
                    {
                        nextNode = lastGrid;
                    }
                    else if (lastGrid.GridType == GridType.Room) //마지막 격자가 방일 경우
                    {
                        //1. 해당 격자가 어느 방에 속하는지 확인한후 //Todo: 현재 GameObject를 임시로 사용 중이라, 격자 참조가 안된다.
                        //2. 해당 방을 참조하여 다음노드로 경로의 다음 노드로 설정한다.
                        nextNode = lastGrid.RoomCoreGrid;
                    }
                    else if (lastGrid.GridType == GridType.RoomCore) //마지막 격자가 방 중심일 경우
                    {
                        nextNode = lastGrid;
                    }
                    else //예외 처리용 //Todo: 수정필요
                    {
                        Debug.Log($"Exception request need for {lastGrid.GridType}");
                        nextNode = lastGrid;
                    }

                    //노드 집합의 loops번째 노드의 연결 노드는 nextNode이다.
                    node.ConnectedNode.Add(gridNodeDictionary[nextNode]);
                    node.ConnectedNodePath.Add(new List<DungeonGridData>(nodePath));

                    if (node.ConnectedNode[loops].NodeGrid.GridType == GridType.RoomCore) //방과 연결 되었다면
                    {
                        node.ConnectedNodePathWeight.Add(node.ConnectedNodePath[loops].Count + 10); //추가 가중치를 받는다.
                    }
                    else
                    {
                        node.ConnectedNodePathWeight.Add(node.ConnectedNodePath[loops].Count); //길이라면 길의 수만큼 가중한다.
                    }
                    loops++;
                }
                NODES.Add(node);
            }

            //foreach (var e in NODES)
            //{
            //    Debug.Log("Node Position: " + e.NodeGrid.ConstructedPosition);
            //    int i = 0;
            //    foreach (var ee in e.ConnectedNodePath)
            //    {
            //        i++;
            //        Debug.Log("Path " + i + ": ");
            //        foreach (var eee in ee)
            //        {
            //            Debug.Log(eee.ConstructedPosition);
            //        }
            //    }
            //}

            Nodes = NODES;
        }

        /// <summary>
        /// This Method should run when you [Select a Room GameObject]. In else case, it might not run properly.
        /// </summary>
        /// <param name="nodes">Nodes Already Exsist.</param>
        /// <param name="disconnection">Selected Room's Grid.</param>
        /// <returns></returns>
        public static bool CanRemoveRoom
            (List<GridNode> nodes, DungeonGridData disconnection)
        {
            if (disconnection.GridType == GridType.Passage && disconnection.ConnectedGrids.Count == 1)
            { return true; } //Can remove grid.

            List<GridNode> tempResult = new(nodes);
            List<DungeonGridData> tempDisConnections = new(disconnection.ConnectedGrids);
            DungeonGridData tempDisconnection = disconnection;

            GridNode nodeToDisconnect = tempResult.Find(node => node.NodeGrid == tempDisconnection);

            List<GridNode> nodesToCheck = new(4);

            bool isNode = false;

            //if Grid is node
            if (nodeToDisconnect != null)
            {
                isNode = true;
                foreach (var e in nodeToDisconnect.ConnectedNode)
                {
                    e.ConnectedNode.Remove(nodeToDisconnect);
                    nodesToCheck.Add(e);
                }
            }
            else //If Grid is not node, It must have [Two Connection].
            {
                isNode = false;
                List<DungeonGridData> tempExclusion = new(1);
                tempExclusion.Add(tempDisconnection);

                List<DungeonGridData> tempPath = new(10);
                FindPathWithNoGoal(tempDisConnections[0], tempExclusion, out tempPath);
                nodesToCheck.Add(tempResult.Find(node => node.NodeGrid == tempPath.Last()));
                tempPath.Clear();
                FindPathWithNoGoal(tempDisConnections[1], tempExclusion, out tempPath);
                nodesToCheck.Add(tempResult.Find(node => node.NodeGrid == tempPath.Last()));

                nodesToCheck[0].ConnectedNode.Remove(nodesToCheck[1]);
                nodesToCheck[1].ConnectedNode.Remove(nodesToCheck[0]);
            }

            foreach (var e in nodesToCheck)
            {
                if (!CanReachGoal(tempResult[0], e))//하나라도 경로 검색에 실패시 삭제시도를 초기화, false
                {
                    if (isNode)
                    {
                        foreach (var f in nodeToDisconnect.ConnectedNode)
                        {
                            f.ConnectedNode.Add(nodeToDisconnect);
                        }
                    }
                    else
                    {
                        nodesToCheck[0].ConnectedNode.Add(nodesToCheck[1]);
                        nodesToCheck[1].ConnectedNode.Add(nodesToCheck[0]);
                    }
                    return false;
                }
            }

            //Disconnected!! Data Adjusted!!
            return true;
        }

        public static void FindShortestWay(GridNode startNode, GridNode endNode, List<GridNode> nodes)
        {
            List<GridNode> internalNodes = nodes;

            Dictionary<GridNode, int> nodeDistance = new();
            Dictionary<GridNode, GridNode> prevNodes = new();
            List<GridNode> unvisitedNodes = new();
            List<GridNode> tempVisitedNodes = new();

            foreach (var e in internalNodes)
            {
                nodeDistance.Add(e, int.MaxValue);
                prevNodes.Add(e, null);
            }

            nodeDistance[startNode] = 0;

            for (int i = 0; i < startNode.ConnectedNode.Count; i++)
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

            Debug.Log($"Starts On: {startNode.NodeGrid.ConstructedPosition}");
            Debug.Log($"Ends On: {endNode.NodeGrid.ConstructedPosition}");
            foreach (var e in path)
            {
                Debug.Log(e.ConstructedPosition);
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

            for (int i = 0; i < startNode.ConnectedNode.Count; i++)
            {
                if (visitedNodes.Contains(startNode.ConnectedNode[i])) { continue; } //Todo: Find Grid in List => O(n)

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
                for (int i = 0; i < linkedNodes[currentNode].ConnectedNode.Count; i++)
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
            int connections = NodeGrid.ConnectedGrids.Count;
            ConnectedNode = new(connections);
            ConnectedNodePath = new(connections);
            ConnectedNodePathWeight = new(connections);
        }

        public DungeonGridData NodeGrid { get; private set; }

        public List<GridNode> ConnectedNode;
        public List<List<DungeonGridData>> ConnectedNodePath { get; set; }
        public List<int> ConnectedNodePathWeight;
    }
}