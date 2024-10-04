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

        private List<DungeonGridData> searchedPath = new(20);


        private List<DungeonGridData> excludeGrids = new(10);


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
                if (dungeonConstructManager.GridDatas.ContainsKey(hit.transform.position))
                {
                    DungeonGridData standingGrid = dungeonConstructManager.GridDatas[hit.transform.position];

                    GridPathFinder.FindPathWithNoGoal(standingGrid, excludeGrids, out searchedPath);

                    if (searchedPath.Count > 0)
                    {
                        foreach (var eachGrid in searchedPath)
                        {
                            Debug.Log("Path Searched");
                            if (eachGrid != null)
                            { Debug.Log(eachGrid.ConstructedPosition); }
                        }

                    }
                    else
                    {
                        Debug.Log("No Path Found");
                    }
                }
                else
                {
                    Debug.Log("No Grid Data Standing On");
                }
            }

            //Todo:
            enemyController.SetSearchedPath(searchedPath);
            enemyController.ChangeControllerState(EnemyStates.Moving);
        }
    }
}
