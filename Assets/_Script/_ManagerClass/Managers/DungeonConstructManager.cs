using BHSSolo.DungeonDefense.Contruct;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DungeonConstructManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ OwnerManager { get; set; }


        private GameStateManager_ gameStateManager; //Todo:
        private DungeonGridSpwner dungeonGridSpwner;
        public Dictionary<Vector3,DungeonGridData> GridDatas = new(10000);

        private bool showingGrid = false;


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;

            gameStateManager = OwnerManager.GameStateManager_;
            gameStateManager.OnGameStateChanged += GameStateReaction;

            dungeonGridSpwner = FindFirstObjectByType<DungeonGridSpwner>();

            for (int ix = -50; ix < 51; ix++)
            {
                for (int iz = 0; iz < 101; iz++)
                {
                    int rand0to1 = UnityEngine.Random.Range(0, 2);

                    GridDatas.Add(
                        new Vector3(ix, 0.01f, iz) * 5f
                        ,new DungeonGridData(
                            false
                            ,new Vector3(ix, 0.01f, iz) * 5f));
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

        public void ShowGrid(Dictionary<Vector3,DungeonGridData> gridDatas)
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
    }
}
