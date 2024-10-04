using BHSSolo.DungeonDefense.Contruct;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    }
}
