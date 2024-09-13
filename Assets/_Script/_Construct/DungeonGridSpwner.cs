using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Contruct
{
    public class DungeonGridSpwner : MonoBehaviour
    {
        [SerializeField] private GameObject GridPrefab;
        [SerializeField] private GameObject GridHolder;
        private readonly string GRIDHOLDER = "GridHolder";
        private readonly string PREFABPATH = "Prefabs/RoomGrid/RoomGrid";


        private void Awake()
        {
            if (GridPrefab == null)
                GridPrefab = Resources.Load(PREFABPATH) as GameObject;

            if (GridHolder == null)
                GridHolder = transform.Find(GRIDHOLDER).gameObject;
        }

        private void Start()
        {
            if (GridPrefab == null)
                Debug.LogWarning("No Prefab Found On Grid Spawner.");
            if (GridHolder == null)
                Debug.LogWarning("No Gird Holder Found On Grid Spawner.");
        }

        public void MakeGrid(List<DungeonGridData> gridDatas)
        {
            foreach (DungeonGridData data in gridDatas)
            {
                data.GridObject =
                    Instantiate(GridPrefab, data.ConstructedPosition, Quaternion.identity, GridHolder.transform);
            }

            HideGrid();
        }

        public void HideGrid()
        {
            GridHolder.SetActive(false);
        }

        public void ShowGrid(List<DungeonGridData> dungeonGridDatas)
        {
            GridHolder.SetActive(true);

            foreach(DungeonGridData data in dungeonGridDatas)
            {
                if(data.IsContructed)
                    data.SetVisible();
                else
                    data.SetInvisible();
            }
        }
    }
}
