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
            excludeGrids = enemyController.ExcludeGrids;
            dungeonConstructManager = enemyController.EnemyManager_.OwnerManager.DungeonConstructManager_;
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
        }

        private void SetDestination()
        {
            Ray ray = new Ray(enemyController.transform.position + (Vector3.up * 0.5f), Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1f, 1 << LayerMask.NameToLayer("GridFloor")))
            {
                DungeonGridData standingGrid = dungeonConstructManager.GridDatas[hit.transform.position];

                if (standingGrid.GridType == GridType.Room)
                    standingGrid = standingGrid.RoomCoreGrid;

                foreach (var e in dungeonConstructManager.NodeDatas)
                {
                    if (e.NodeGrid == standingGrid)
                    {
                        int rand = UnityEngine.Random.Range(0, e.ConnectedNode.Count);

                        List<DungeonGridData> tempPath = new List<DungeonGridData>(e.ConnectedNodePath[rand]);

                        enemyController.SetSearchedPath(tempPath);
                        enemyController.ChangeControllerState(EnemyStates.Moving);
                        return;
                    }
                }
                Debug.Log("Where Do I Go??!?!?!?!?!!");
            }
        }
        //    dungeonConstructManager.NodeDatas.Contains(sta)

        //    if (dungeonConstructManager.GridDatas.ContainsKey(hit.transform.position))
        //    {

        //        if (standingGrid.GridType == GridType.Room) //RoomCore의 경우엔 자동으로 Grid연결을 찾는다.
        //        {
        //            standingGrid = standingGrid.RoomCoreGrid;
        //        }

        //        //적어도 방금 지나온방은 다시 탐색하지 않는다.
        //        GridPathFinder.FindPathWithNoGoal(standingGrid, excludeGrids, out searchedPath);

        //        //여기서 적의 AI에 따라 지나온 경로를 저장하거나, 저장하지 않는다.
        //        //저장하면 같은 방에 들렀을 때, 지나온 경로는 탐색하지 않는다.
        //        //저장하지 않으면 지나온 경로를 탐색한다.

        //        if (searchedPath.Count > 0)
        //        {
        //            foreach (var eachGrid in searchedPath)
        //            {
        //                Debug.Log("Path Searched");
        //                if (eachGrid != null)
        //                { Debug.Log(eachGrid.ConstructedPosition); }
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("No Path Found");
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("No Grid Data Standing On");
        //    }
        //}

        ////Todo:
        //enemyController.SetSearchedPath(searchedPath);
        //enemyController.ChangeControllerState(EnemyStates.Moving);
    }
}
