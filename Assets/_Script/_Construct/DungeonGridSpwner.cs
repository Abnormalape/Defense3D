using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Contruct
{
    public class DungeonGridSpwner : MonoBehaviour
    {
        [SerializeField] private GameObject GridPrefab;
        [SerializeField] private GameObject GridHolder;
        private const string GRIDHOLDER = "GridHolder";
        private const string PREFABPATH = "Prefabs/RoomGrid/RoomGrid";


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
                    Instantiate(GridPrefab, data.Key, Quaternion.identity, GridHolder.transform);
                //Instantiate(GridPrefab, data.Value.ConstructedPosition, Quaternion.identity, GridHolder.transform);
            }

            //foreach (Vector3 gridPosition in gridPositions)
            //{
            //    Instantiate(GridPrefab, gridPosition, Quaternion.identity, GridHolder.transform);
            //}

            HideAllGrids(gridDatas);
        }

        public void HideAllGrids(Dictionary<Vector3, DungeonGridData> dungeonGridDatas)
        {
            foreach (var data in dungeonGridDatas)
            {
                data.Value.SetInvisible();
            }
        }

        public void HideGrid(DungeonGridData dungeonGridData)
        {
            dungeonGridData.SetInvisible();
        }

        public void ShowAllGrids(Dictionary<Vector3, DungeonGridData> dungeonGridDatas)
        {
            foreach (var data in dungeonGridDatas)
            {
                data.Value.SetVisible();
            }
        }

        public void ShowAllGrids(List<DungeonGridData> dungeonGridDatas)
        {
            foreach (var e in dungeonGridDatas)
            {
                e.SetVisible();
            }
        }

        public void ShowGrid(DungeonGridData dungeonGridData)
        {
            dungeonGridData.SetVisible();
        }
    }
}
