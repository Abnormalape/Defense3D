using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System.Linq;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class OnManage_Grid_PassageBuildAfterJudge : IState_, ICursorState
    {
        public CursorManager CursorManager_ { get; set; }
        public CursorState CursorState { get; set; } = CursorState.OnManage_Grid_PassageBuildAfterJudge;
        private DungeonConstructManager dungeonConstructManager { get; set; }
        private GameObject gridTarget { get; set; }
        private Vector3 gridTargetPosition { get; set; }
        private Vector3 startPosition { get; set; }

        public void InitialzieCursorState(CursorManager cursorManager)
        {
            CursorManager_ = cursorManager;
            dungeonConstructManager = cursorManager.OwnerManager.DungeonConstructManager_;
        }

        public void StateEnter()
        {
            gridTarget = CursorManager_.SummonGridTarget();
            gridTargetPosition = gridTarget.transform.position;
            startPosition = dungeonConstructManager.ConstructionProgress.BasePosition;
        }

        public void StateExit()
        {
            CursorManager_.DestroyGridTarget();
        }

        public void StateUpdate()
        {
            if (gridTargetPosition != gridTarget.transform.position)
            {
                gridTargetPosition = gridTarget.transform.position;

                Vector3 direction = (gridTargetPosition - startPosition);
                float distance = Vector3.Distance(startPosition, gridTargetPosition);


                RaycastHit[] hits = Physics.SphereCastAll(startPosition, 0.1f, direction, distance, 1 << LayerMask.NameToLayer("GridFloor"));


                Debug.Log("Start: " + startPosition);
                Debug.Log("Distance: " + distance);
                Debug.Log("Vector: " + direction);
                if (hits.Length > 0)
                {
                    foreach (RaycastHit hit in hits)
                    {
                        Debug.Log("Path: " + hit.transform.position);
                    }
                }
                Debug.Log("End: " + gridTargetPosition);
            }
        }
    }
}