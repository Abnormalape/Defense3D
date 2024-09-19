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

        public void MakeGrid(Dictionary<Vector3, DungeonGridData> gridDatas)
        {
            foreach (var data in gridDatas)
            {
                data.Value.GridObject =
                    Instantiate(GridPrefab, data.Value.ConstructedPosition, Quaternion.identity, GridHolder.transform);
            }

            HideGrid();
        }

        public void HideGrid()
        {
            GridHolder.SetActive(false);
        }

        public void ShowGrid(Dictionary<Vector3,DungeonGridData> dungeonGridDatas)
        {
            GridHolder.SetActive(true);

            foreach(var data in dungeonGridDatas)
            {
                if(!data.Value.IsContructed)
                    data.Value.SetVisible();
                else
                    data.Value.SetInvisible();
            }
        }
    }
}
