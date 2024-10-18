using BHSSolo.DungeonDefense.Contruct;
using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BHSSolo.DungeonDefense.Controller
{
    public class OnManage_Grid_PassageBuildAfterJudge : IState_<CursorState, CursorManager>, ICursorState
    {
        public CursorManager BlackBoard { get; set; }
        public CursorState StateType { get; set; } = CursorState.OnManage_Grid_PassageBuildAfterJudge;
        public void InitializeState(CursorManager blackBoard)
        {
            BlackBoard = blackBoard;
        }


        public CursorManager CursorManager_ { get; set; }
        public CursorState CursorState { get; set; } = CursorState.OnManage_Grid_PassageBuildAfterJudge;
        private DungeonConstructManager dungeonConstructManager { get; set; }
        private GameObject gridTarget { get; set; }
        private Vector3 gridTargetPosition { get; set; }
        private Vector3 startPosition { get; set; }

        private List<DungeonGridData> pathGrid = new(20);

        private bool clicked;

        public void InitialzieCursorState(CursorManager cursorManager)
        {
            CursorManager_ = cursorManager;
            dungeonConstructManager = cursorManager.GameManager.DungeonConstructManager_;
        }

        public void StateEnter()
        {
            gridTarget = CursorManager_.SummonGridTarget();
            gridTargetPosition = gridTarget.transform.position;
            startPosition = dungeonConstructManager.ConstructionProgress.BasePosition;

            dungeonConstructManager.HideAllGrids();
        }

        public void StateExit()
        {
            CursorManager_.DestroyGridTarget();
        }

        public void StateUpdate()
        {
            if (Input.GetMouseButtonDown(0) && !clicked
                && !EventSystem.current.IsPointerOverGameObject())
            {
                clicked = true;
            }

            if (gridTargetPosition != gridTarget.transform.position)
            {
                if (startPosition != dungeonConstructManager.ConstructionProgress.BasePosition) //When Start Position Changed.
                { startPosition = dungeonConstructManager.ConstructionProgress.BasePosition; }

                pathGrid.Clear();

                dungeonConstructManager.HideAllGrids();

                gridTargetPosition = gridTarget.transform.position;

                float xMovement = gridTargetPosition.x - startPosition.x;
                float zMovement = gridTargetPosition.z - startPosition.z;

                List<Vector3> path = new();
                Vector3 movement;
                int countsTogo;

                if (Mathf.Abs(xMovement) > Mathf.Abs(zMovement))
                {
                    countsTogo = Convert.ToInt32(MathF.Abs(xMovement));
                    if (xMovement > 0) { movement = new Vector3(5, 0, 0); }
                    else { movement = new Vector3(-5, 0, 0); }
                }
                else
                {
                    countsTogo = Convert.ToInt32(MathF.Abs(zMovement));
                    if (zMovement > 0) { movement = new Vector3(0, 0, 5); }
                    else { movement = new Vector3(0, 0, -5); }
                }

                countsTogo /= 5;

                path.Add(startPosition);
                for (int i = 1; i < countsTogo + 1; i++)
                {
                    path.Add((movement * i) + startPosition);
                }

                foreach (var e in path)
                {
                    pathGrid.Add(dungeonConstructManager.GridDatas[e]);
                }

                dungeonConstructManager.ShowGrids(pathGrid);
            }

            if (Input.GetMouseButtonUp(0) && clicked)
            {
                clicked = false;
                dungeonConstructManager.ConstructionProgress.JudgeLinkedPassage(pathGrid);
            }
        }
    }
}