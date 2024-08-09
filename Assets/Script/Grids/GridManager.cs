using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Grid
{


    class GridManager : MonoBehaviour
    {
        [SerializeField] private GameObject _gridPrefab;

        [SerializeField] private GameObject _gridContainer;

        [SerializeField] private List<FloorGridController> _generatedGridController = new(1000);

        private void Awake()
        {
            if (_gridPrefab == null) { Debug.LogAssertion("No Grid Obejct"); }
            if (_gridContainer == null) { transform.GetChild(0); }

            for (int i = 0; i < 18; i++)
            {
                for (int j = -4; j <= 4; j++)
                {
                    _generatedGridController.Add(
                        Instantiate(
                            _gridPrefab,
                            new Vector3(j, 0, i) * 5f,
                            Quaternion.identity,
                            _gridContainer.transform)
                        .GetComponent<FloorGridController>());
                }
            }

            foreach (FloorGridController gridcontroller in _generatedGridController)
            {
                gridcontroller.OnHasBuildingChanged += ActivateOrDeactiveGrid;
            }

            //HideGrids();
        }



        private void ActivateOrDeactiveGrid(GameObject grid, bool hasBuilding)
        {
            if (hasBuilding)
                grid.GetComponent<MeshRenderer>().enabled = false;
            else
                grid.GetComponent<MeshRenderer>().enabled = true;
        }

        private void ShowGrids()
        {
            _gridContainer.SetActive(true);
        }

        private void HideGrids()
        {
            _gridContainer.SetActive(false);
        }
    }
}
