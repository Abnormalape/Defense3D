using BHSSolo.DungeonDefense.Contruct;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DungeonConstructManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ OwnerManager { get; set; }


        private GameStateManager_ gameStateManager; //Todo:
        private RoomManager_ RoomManager;
        private CursorManager cursorManager;
        private DungeonGridSpwner dungeonGridSpwner;
        public Dictionary<Vector3, DungeonGridData> GridDatas = new(10000);

        private bool showingGrid = false;

        private GameObject constructurePlaceHolder;
        public GameObject ConstructurePlaceHolder
        {
            get => constructurePlaceHolder;
            set => constructurePlaceHolder = value;
        }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;

            this.RoomManager = OwnerManager.RoomManager_;
            this.cursorManager = OwnerManager.CursorManager_;
            gameStateManager = OwnerManager.GameStateManager_;
            gameStateManager.OnGameStateChanged += GameStateReaction;

            dungeonGridSpwner = FindFirstObjectByType<DungeonGridSpwner>();

            for (int ix = -50; ix < 51; ix++)
            {
                for (int iz = 0; iz < 101; iz++)
                {
                    bool tempIsBuilt = false;

                    if (ix == 0 && iz == 0)
                        tempIsBuilt = true;


                    float tempX = ix;
                    float tempZ = iz;

                    GridDatas.Add(
                        new Vector3(tempX, 0.01f, tempZ) * 5f
                        , new DungeonGridData(
                            tempIsBuilt
                            , new Vector3(tempX, 0.01f, tempZ) * 5f));
                }
            }

            MakeGrid(GridDatas);
        }

        private void GameStateReaction(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Dungeon_ConstructionState:
                    ShowGrid(GridDatas);
                    return;

                default:
                    HideGrid();
                    return;
            }
        }

        public void MakeGrid(Dictionary<Vector3, DungeonGridData> gridDatas)
        {
            dungeonGridSpwner.MakeGrid(gridDatas);
        }

        public void ShowGrid(Dictionary<Vector3, DungeonGridData> gridDatas)
        {
            if (!showingGrid)
            {
                Debug.Log("Show Grid");
                showingGrid = true;
                dungeonGridSpwner.ShowGrid(gridDatas);
            }
        }

        public void HideGrid()
        {
            if (showingGrid)
            {
                Debug.Log("Hide Grid");
                showingGrid = false;
                dungeonGridSpwner.HideGrid();
            }
        }

        /// <summary>
        /// Find constructure's size by Name in DataManager.
        /// Then Judge Constructure can Constuct.
        /// </summary>
        /// <param name="constructionPoint">Center Point a.k.a Grid Cursor Position </param>
        /// <param name="attachedConstructureName">Constructure Name. Find Data In DataManager</param>
        public void JudgeConstruction(Vector3 constructionPoint, string attachedConstructureName)
        {
            //Find Constructure Data By Name In DataManager.
            //Use Construture's xSize, zSize

            int xSize = 3; //Todo: Use Data
            int zSize = 3; //Todo: Use Data
            int tempXSize = 0;
            int tempZSize = 0;

            if (xSize % 2 == 1)
                tempXSize = (xSize - 1) / 2;
            if (zSize % 2 == 1)
                tempZSize = (zSize - 1) / 2;

            Vector3 tempPosition = constructionPoint;

            for (int ix = -tempXSize; ix < tempXSize + 1; ix++)
            {
                for (int iz = -tempZSize; iz < tempZSize + 1; iz++)
                {
                    Vector3 tempGridPostition
                        = new Vector3(tempPosition.x + (ix * 5f), tempPosition.y, tempPosition.z + (iz * 5f));

                    if (GridDatas.ContainsKey(tempGridPostition))
                    {
                        if (GridDatas[tempGridPostition].IsContructed)
                        {
                            Debug.Log("There is already a building at that location.");
                            return;
                        }
                    }
                    else
                    {
                        Debug.Log("It's not allowed position.");
                        return;
                    }
                }
            }

            List<Vector3> outLinePositions = new();

            for (int ix = -tempXSize; ix < (tempXSize + 1); ix++)
            {
                outLinePositions.Add(new Vector3(tempPosition.x + (ix * 5f), tempPosition.y, tempPosition.z + (-(tempZSize + 1) * 5f)));
                outLinePositions.Add(new Vector3(tempPosition.x + (ix * 5f), tempPosition.y, tempPosition.z + ((tempZSize + 1) * 5f)));
            }

            for (int iz = -tempZSize; iz < (tempZSize + 1); iz++)
            {
                outLinePositions.Add(new Vector3(tempPosition.x + (-(tempXSize + 1) * 5f), tempPosition.y, tempPosition.z + (iz * 5f)));
                outLinePositions.Add(new Vector3(tempPosition.x + ((tempXSize + 1) * 5f), tempPosition.y, tempPosition.z + (iz * 5f)));
            }

            Debug.Log("Found Outlines : " + outLinePositions.Count);

            foreach (var aa in outLinePositions) //Todo: Remove
            {
                Debug.Log("OutLinePosition : " + aa);
            }

            bool foundRoad = false;

            foreach (var aa in outLinePositions)
            {
                if (GridDatas.ContainsKey(aa))
                {
                    if (GridDatas[aa].IsRoad)
                    {
                        Debug.Log("There is Road.");
                        Debug.Log("BREAK!");
                        foundRoad = true;
                        break;
                    }
                    else
                    {
                        Debug.Log("There is No Road.");
                        continue;
                    }
                }
                else
                {
                    Debug.Log("Outline is on Border.");
                    continue;
                }
            }

            if (!foundRoad) { Debug.Log("No Road Found."); }

            Debug.Log("Construction Success."); //Todo:

            for (int ix = -tempXSize; ix < tempXSize + 1; ix++)
            {
                for (int iz = -tempZSize; iz < tempZSize + 1; iz++)
                {
                    Vector3 tempGridPostition
                        = new Vector3(tempPosition.x + (ix * 5f), tempPosition.y, tempPosition.z + (iz * 5f));

                    GridDatas[tempGridPostition].IsContructed = true;
                }
            }

            foreach (var aa in outLinePositions) //Todo: Remove
            {
                GridDatas[aa].IsContructed = true;
            }

            this.RoomManager.AddRoom(attachedConstructureName);
            Destroy(ConstructurePlaceHolder);

            this.cursorManager.isAttachedOnGridCursor = false; //Todo:
            this.cursorManager.attachedNameOnGridCursor = ""; //Todo: 
        }
    }
}
