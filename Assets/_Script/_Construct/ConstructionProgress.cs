using BHSSolo.DungeonDefense.Contruct;
using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class ConstructionProgress
    {
        public ConstructionProgress(DungeonConstructManager dungeonConstructManager)
        {
            DungeonConstructManager = dungeonConstructManager;
            CursorManager = dungeonConstructManager.OwnerManager.CursorManager_;
            AllGrid = dungeonConstructManager.GridDatas;
        }
        private DungeonConstructManager DungeonConstructManager { get; set; }
        private CursorManager CursorManager { get; set; }
        private Dictionary<Vector3, DungeonGridData> AllGrid { get; set; }

        public void SetHoldingBuildData(RoomBuildType holdingType, int width, int depth)
        {
            HoldingRoomBuildType = holdingType;
            HoldingRoomWidth = width;
            HoldingRoomDepth = depth;
        }
        public void ResetHoldingBuildData()
        {
            HoldingRoomBuildType = RoomBuildType.None;//Todo: None
            HoldingRoomWidth = 0;
            HoldingRoomDepth = 0;
        }
        public RoomBuildType HoldingRoomBuildType { get; private set; }
        public int HoldingRoomWidth { get; private set; }
        public int HoldingRoomDepth { get; private set; }
        

        public void ResetTempRoomData()
        {
            TempRoomPlaceGrids.Clear();
            TempRoomSideGrids.Clear();
            TempRoomAroundGrids.Clear();
            TempRoomCoreGrid = null;
            BasePosition = Vector3.zero;
        }
        public List<DungeonGridData> TempRoomPlaceGrids { get; private set; } = new(49);
        public List<DungeonGridData> TempRoomSideGrids { get; private set; } = new(24);
        public List<DungeonGridData> TempRoomAroundGrids { get; private set; } = new(28);
        public DungeonGridData TempRoomCoreGrid { get; private set; }
        public Vector3 BasePosition { get; private set; }

        public void JudgePositionIsBuildable(Vector3 gridTargetPosition)
        {
            BasePosition = gridTargetPosition;

            SetTempRoomGrids(gridTargetPosition);

            if (HoldingRoomBuildType == RoomBuildType.Passage)
            {
                if (JudgePassageBuildable())
                {
                    //Can Build
                    CursorManager.ChangeManagerState(CursorState.OnManage_Grid_PassageBuildAfterJudge);
                }
                else
                {
                    Debug.Log("Can't Build Passage.");
                    ResetTempRoomData();
                }
            }
            else if (HoldingRoomBuildType == RoomBuildType.Room)
            {
                if (JudgeRoomBuildable())
                {
                    //Can Build
                    CursorManager.ChangeManagerState(CursorState.OnManage_Grid_RoomBuildAfterJudge);
                }
                else
                {
                    Debug.Log("Can't Build Room.");
                    ResetTempRoomData();
                }
            }
        }

        private void SetTempRoomGrids(Vector3 gridTargetPosition)
        {
            Vector3 basePosition = new Vector3(gridTargetPosition.x, gridTargetPosition.y, gridTargetPosition.z);

            if (HoldingRoomWidth == 0 || HoldingRoomDepth == 0) { Debug.Log("RoomSize Can't Be Zero"); return; } //==> Error.

            List<int> xList = new List<int>(HoldingRoomWidth);
            List<int> zList = new List<int>(HoldingRoomDepth);
            List<Vector3> mainPositions = new(HoldingRoomWidth * HoldingRoomDepth);

            //XPositions.
            if (HoldingRoomWidth % 2 == 0) //IsEven
            {
                int xEvenCount = (HoldingRoomWidth / 2);
                for (int ix = -xEvenCount; ix <= xEvenCount; ix++)
                {
                    if (ix == 0) continue;

                    int multiplier;
                    if (ix < 0) multiplier = -1;
                    else multiplier = 1;

                    xList.Add((int)(basePosition.x + ((ix - (multiplier)) * 5) + (multiplier * 2.5f)));
                }
            }
            else //IsOdd
            {
                int xOddCount = (HoldingRoomWidth - 1) / 2;
                for (int ix = -xOddCount; ix <= xOddCount; ix++)
                {
                    xList.Add((int)(basePosition.x + (ix * 5)));
                }
            }

            //ZPositions.
            if (HoldingRoomDepth % 2 == 0) //IsEven
            {
                int zEvenCount = (HoldingRoomDepth / 2);
                for (int iz = -zEvenCount; iz <= zEvenCount; iz++)
                {
                    if (iz == 0) continue;

                    int multiplier;
                    if (iz < 0) multiplier = -1;
                    else multiplier = 1;

                    zList.Add((int)(basePosition.z + ((iz - (multiplier)) * 5) + (multiplier * 2.5f)));
                }
            }
            else //IsOdd
            {
                int zOddCount = (HoldingRoomDepth - 1) / 2;
                for (int iz = -zOddCount; iz <= zOddCount; iz++)
                {
                    zList.Add((int)(basePosition.z + (iz * 5)));
                }
            }

            //mainPositions
            foreach (int x in xList)
            {
                foreach (int z in zList)
                {
                    mainPositions.Add(new(x, basePosition.y, z));
                }
            }

            //Todo: Exception to out of boundary.
            foreach (Vector3 e in mainPositions)
            {
                if (!AllGrid.ContainsKey(e)) //예외처리 사항.
                {
                    Debug.Log("No Exsisting Grid There");
                    ResetTempRoomData();
                    return;
                }
                else
                {
                    //TempRoomPlaceGrids
                    TempRoomPlaceGrids.Add(AllGrid[e]);

                    //TempRoomSideGrids
                    if (e.x == xList.First() || e.x == xList.Last() || e.z == zList.First() || e.z == zList.Last())
                    {
                        TempRoomSideGrids.Add(AllGrid[e]);

                        //TempRoomAroundGrids
                        if (e.x == xList.First())
                        {
                            Vector3 tempAroundPosition = e - new Vector3(5, 0, 0);
                            DungeonGridData tempGridData;
                            AllGrid.TryGetValue(tempAroundPosition, out tempGridData);
                            if (tempGridData == null)
                            {; }
                            else
                            { TempRoomAroundGrids.Add(tempGridData); }
                        }
                        if (e.x == xList.Last())
                        {
                            Vector3 tempAroundPosition = e + new Vector3(5, 0, 0);
                            DungeonGridData tempGridData;
                            AllGrid.TryGetValue(tempAroundPosition, out tempGridData);
                            if (tempGridData == null)
                            {; }
                            else
                            { TempRoomAroundGrids.Add(tempGridData); }
                        }
                        if (e.z == zList.First())
                        {
                            Vector3 tempAroundPosition = e - new Vector3(0, 0, 5);
                            DungeonGridData tempGridData;
                            AllGrid.TryGetValue(tempAroundPosition, out tempGridData);
                            if (tempGridData == null)
                            {; }
                            else
                            { TempRoomAroundGrids.Add(tempGridData); }
                        }
                        if (e.z == zList.Last())
                        {
                            Vector3 tempAroundPosition = e + new Vector3(0, 0, 5);
                            DungeonGridData tempGridData;
                            AllGrid.TryGetValue(tempAroundPosition, out tempGridData);
                            if (tempGridData == null)
                            {; }
                            else
                            { TempRoomAroundGrids.Add(tempGridData); }
                        }
                    }
                }
            }

            //TempRoomCoreGrid
            int coreXPos;
            int coreZPos;
            if (HoldingRoomWidth % 2 == 0) { coreXPos = xList[(HoldingRoomWidth / 2) - 1]; }
            else { coreXPos = xList[(HoldingRoomWidth - 1) / 2]; }
            if (HoldingRoomDepth % 2 == 0) { coreZPos = zList[(HoldingRoomDepth / 2) - 1]; }
            else { coreZPos = zList[(HoldingRoomDepth - 1) / 2]; }
            TempRoomCoreGrid = AllGrid[new Vector3(coreXPos, basePosition.y, coreZPos)];
        }

        private bool JudgePassageBuildable()
        {
            foreach (var e in TempRoomPlaceGrids)
            {
                if (!e.IsContructed)
                {
                    Debug.Log("Must Click Passage On Constructure.");
                    ResetTempRoomData();
                    return false;
                }
            }

            bool atLeastOneSideIsEmpty = false;
            foreach (var e in TempRoomAroundGrids)
            {
                if (!e.IsContructed)
                {
                    atLeastOneSideIsEmpty = true;
                    break;
                }
            }
            if (!atLeastOneSideIsEmpty)
            {
                Debug.Log("There is no empty Side.");
                ResetTempRoomData();
                return false;
            }

            //Can Build Passage.
            return true;
        }

        private bool JudgeRoomBuildable()
        {
            foreach (var e in TempRoomPlaceGrids)
            {
                if (e.IsContructed)
                {
                    Debug.Log("Room Can't build on other.");
                    ResetTempRoomData();
                    return false;
                }
            }

            bool passageFound = false;
            foreach (var e in TempRoomAroundGrids)
            {
                if (e.GridType == GridType.Passage)
                {
                    Debug.Log("Passage Found");
                    passageFound = true;
                    break;
                }
            }
            if (!passageFound)
            {
                Debug.Log("There is no Passage near Room.");
                ResetTempRoomData();
                return false;
            }

            //Can Build Room.
            return true;
        }

        public void CompleteRoomBuild(DungeonGridData roomEntrance, DungeonGridData nearPassage)
        {
            //TempRoomCoreGrid.SetRoomCore();

            foreach (var e in TempRoomPlaceGrids)
            {
                e.SetRoom();
            }

            roomEntrance.ConnectedGrids.Add(nearPassage);
            nearPassage.ConnectedGrids.Add(roomEntrance);

            ResetHoldingBuildData();
            ResetTempRoomData();
        }

        public void CompletePassageBuild()
        {

        }
    }
}
