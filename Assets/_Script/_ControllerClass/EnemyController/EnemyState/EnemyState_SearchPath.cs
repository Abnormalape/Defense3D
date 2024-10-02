using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.Contruct;
using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.State
{
    public class EnemyState_SearchPath : IState_, IEnemyState
    {
        public EnemyController_ enemyController { get; set; }
        public EnemyStates EnemyState { get; set; } = EnemyStates.SearchPath;

        private DungeonConstructManager dungeonConstructManager;

        private List<DungeonGridData> searchedPath = new(20); //Todo: 

        public void InitializeEnemyState(EnemyController_ enemyController_)
        {
            enemyController = enemyController_;
            dungeonConstructManager = enemyController.EnemyManager.OwnerManager.DungeonConstructManager_;
        }

        public void StateEnter()
        {
            SetDestination();
        }

        public void StateExit()
        {
        }

        public void StateUpdate()
        {
            Debug.Log("I'm Searching Path!");
        }

        private void SetDestination()
        {
            Ray ray = new Ray(enemyController.transform.position + (Vector3.up * 0.5f), Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1f, 1 << LayerMask.NameToLayer("GridFloor")))
            {
                Vector3 floorGridPosition = hit.transform.position;

                if (dungeonConstructManager.GridDatas.ContainsKey(floorGridPosition))
                {
                    Debug.Log($"{dungeonConstructManager.GridDatas[floorGridPosition].ConnectedRooms.Count} Path To Go.");

                    //Todo: Redo Until (ConnectedRooms.Count != 2) or (RoomType == RoomType.Room)
                    //Todo: Then execute EnemyController.SetSearchedPath(searchedPath);

                    SearchPath(dungeonConstructManager.GridDatas[floorGridPosition]);
                }
                else
                {
                    Debug.Log("Enemy is standing on grid floor which not found in GridManager");
                    Debug.Log($"Enemy is now standing on ({floorGridPosition})");
                }
            }
            else
            {
                Debug.Log("No Grid Floor Found");
            }
        }

        private void SearchPath(DungeonGridData startGrid)
        {
            searchedPath.Add(startGrid);

            if(startGrid.ConnectedRooms.Count == 0)
            {
                Debug.Log("It can't be [Connected Room Count] Equals Zero.");
                return;
            }
            RecursiveSearchPath(startGrid.ConnectedRooms[0]);
        }

        private void RecursiveSearchPath(DungeonGridData grid)
        {
            searchedPath.Add(grid);

            if (!grid.IsRoad) //If Room
            {
                //traveled rooms. Add
                EndSearchPath();
                return;
            }
            else if (grid.ConnectedRooms.Count == 2)
            {
                DungeonGridData tempGridData
                    = searchedPath.Find(DungeonGridData => DungeonGridData == grid.ConnectedRooms[0]);

                if(tempGridData == null) //{grid.ConnectedRooms[0]} Not On List.
                {
                    RecursiveSearchPath(tempGridData);
                }
                else //{grid.ConnectedRooms[0]} On List.
                {
                    RecursiveSearchPath(grid.ConnectedRooms[1]);
                }
            }
            else if(grid.ConnectedRooms.Count == 1) //Dead End
            {
                EndSearchPath();
                return;
            }
            else if(grid.ConnectedRooms.Count == 0)
            {
                Debug.Log("This Is Error");
                EndSearchPath();
            }
            else //Connected Room >= 3. Equals CrossRoad.
            {
                EndSearchPath();
                return;
            }
        }

        private void EndSearchPath()
        {
            Debug.Log("Search Path Ended");
            Debug.Log($"{searchedPath.Count} Passages to Go.");
            enemyController.SetSearchedPath(searchedPath);
            enemyController.ChangeControllerState(EnemyStates.Moving);
        }
    }
}
