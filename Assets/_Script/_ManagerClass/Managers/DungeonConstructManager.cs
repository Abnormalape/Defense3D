using BHSSolo.DungeonDefense.Contruct;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DungeonConstructManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ OwnerManager { get; set; }


        private DungeonGridSpwner dungeonGridSpwner;
        private List<DungeonGridData> GridDatas = new(10000);


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;

            dungeonGridSpwner = FindFirstObjectByType<DungeonGridSpwner>();

            for (int ix = -50; ix < 51; ix++)
            {
                for (int iz = 0; iz < 101; iz++)
                {
                    int rand0to1 = UnityEngine.Random.Range(0,2);

                    GridDatas.Add(new DungeonGridData(
                    rand0to1 == 0 ? true : false ,
                    new Vector3(ix, 0f, iz) * 5f));
                }
            }

            MakeGrid(GridDatas);
        }

        public void MakeGrid(List<DungeonGridData> gridDatas)
        {
            dungeonGridSpwner.MakeGrid(gridDatas);
        }

        public void ShowGrid(List<DungeonGridData> gridDatas)
        {
            dungeonGridSpwner.ShowGrid(gridDatas);
        }

        public void HideGrid()
        {
            dungeonGridSpwner.HideGrid();
        }
    }
}
