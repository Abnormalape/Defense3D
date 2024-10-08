using BHSSolo.DungeonDefense.Contruct;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class SampleGridBuild : MonoBehaviour
    {
        private void Start()
        {
            //Dictionary<Vector2, DungeonGridData> gridDictionary = new();
            //List<DungeonGridData> List = new(10000);

            //for (int i = 0; i < 100; i++)
            //{
            //    for (int j = 0; j < 100; j++)
            //    {
            //        DungeonGridData tempGridData = new();
            //        tempGridData.SetName($"Grid: ({i},{j})");
            //        gridDictionary.Add(new Vector2(i, j), tempGridData);

            //        List.Add(tempGridData);
            //    }
            //}

            //List<DungeonGridData> connections = new();
            //Vector2 gridposition = new Vector2();


            //gridposition = new Vector2(50, 0);
            //connections.Add(gridDictionary[new Vector2(50, 1)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(50, 1);
            //connections.Add(gridDictionary[new Vector2(50, 0)]);
            //connections.Add(gridDictionary[new Vector2(50, 2)]);
            //connections.Add(gridDictionary[new Vector2(51, 1)]);
            //connections.Add(gridDictionary[new Vector2(49, 1)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(51, 1);
            //connections.Add(gridDictionary[new Vector2(50, 1)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(49, 1);
            //connections.Add(gridDictionary[new Vector2(50, 1)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(50, 2);
            //connections.Add(gridDictionary[new Vector2(50, 1)]);
            //connections.Add(gridDictionary[new Vector2(50, 3)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(50, 3);
            //connections.Add(gridDictionary[new Vector2(50, 2)]);
            //connections.Add(gridDictionary[new Vector2(50, 4)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(50, 4);
            //connections.Add(gridDictionary[new Vector2(50, 3)]);
            //connections.Add(gridDictionary[new Vector2(50, 5)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(50, 5);
            //connections.Add(gridDictionary[new Vector2(50, 4)]);
            //connections.Add(gridDictionary[new Vector2(51, 5)]);
            //connections.Add(gridDictionary[new Vector2(49, 5)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(51, 5);
            //connections.Add(gridDictionary[new Vector2(50, 5)]);
            //connections.Add(gridDictionary[new Vector2(51, 6)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(49, 5);
            //connections.Add(gridDictionary[new Vector2(50, 5)]);
            //connections.Add(gridDictionary[new Vector2(49, 6)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(49, 6);
            //connections.Add(gridDictionary[new Vector2(49, 5)]);
            //connections.Add(gridDictionary[new Vector2(50, 6)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(50, 6);
            //connections.Add(gridDictionary[new Vector2(49, 6)]);
            //connections.Add(gridDictionary[new Vector2(51, 6)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(51, 6);
            //connections.Add(gridDictionary[new Vector2(50, 6)]);
            //connections.Add(gridDictionary[new Vector2(51, 5)]);
            //connections.Add(gridDictionary[new Vector2(51, 7)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //gridposition = new Vector2(51, 7);
            //connections.Add(gridDictionary[new Vector2(51, 6)]);
            //gridDictionary[gridposition].SetConnectedRooms(connections);
            //gridDictionary[gridposition].SetBuilt(true);
            //connections.Clear();

            //GridPathFinder.SetNodeGrids(List);
        }

        //List<DungeonGridData> SampleMap = new(13);
        //private void DepthSearch()
        //{
        //    DungeonGridData sample0 = new();
        //    DungeonGridData sample1 = new();
        //    DungeonGridData sample2 = new();
        //    DungeonGridData sample3 = new();
        //    DungeonGridData sample4 = new();
        //    DungeonGridData sample5 = new();
        //    DungeonGridData sample6 = new();
        //    DungeonGridData sample7 = new();
        //    DungeonGridData sample8 = new();
        //    DungeonGridData sample9 = new();
        //    DungeonGridData sample10 = new();
        //    DungeonGridData sample11 = new();
        //    DungeonGridData sample12 = new();

        //    List<DungeonGridData> connections = new();

        //    connections.Add(sample1);
        //    sample0.SetConnectedRooms(connections, "Sample0");
        //    connections.Clear();

        //    connections.Add(sample2);
        //    connections.Add(sample5);
        //    connections.Add(sample0);
        //    sample1.SetConnectedRooms(connections, "Sample1");
        //    connections.Clear();

        //    connections.Add(sample3);
        //    connections.Add(sample4);
        //    connections.Add(sample1);
        //    sample2.SetConnectedRooms(connections, "Sample2");
        //    connections.Clear();

        //    connections.Add(sample6);
        //    connections.Add(sample2);
        //    sample3.SetConnectedRooms(connections, "Sample3");
        //    connections.Clear();

        //    connections.Add(sample6);
        //    connections.Add(sample2);
        //    connections.Add(sample5);
        //    connections.Add(sample7);
        //    sample4.SetConnectedRooms(connections, "Sample4");
        //    connections.Clear();

        //    connections.Add(sample4);
        //    connections.Add(sample7);
        //    connections.Add(sample1);
        //    sample5.SetConnectedRooms(connections, "Sample5");
        //    connections.Clear();

        //    connections.Add(sample3);
        //    connections.Add(sample4);
        //    connections.Add(sample11);
        //    connections.Add(sample8);
        //    sample6.SetConnectedRooms(connections, "Sample6");
        //    connections.Clear();

        //    connections.Add(sample5);
        //    connections.Add(sample8);
        //    connections.Add(sample12);
        //    connections.Add(sample4);
        //    sample7.SetConnectedRooms(connections, "Sample7");
        //    connections.Clear();

        //    connections.Add(sample6);
        //    connections.Add(sample7);
        //    connections.Add(sample10);
        //    connections.Add(sample9);
        //    sample8.SetConnectedRooms(connections, "Sample8");
        //    connections.Clear();

        //    connections.Add(sample8);
        //    connections.Add(sample12);
        //    sample9.SetConnectedRooms(connections, "Sample9");
        //    connections.Clear();

        //    connections.Add(sample8);
        //    connections.Add(sample11);
        //    sample10.SetConnectedRooms(connections, "Sample10");
        //    connections.Clear();

        //    connections.Add(sample6);
        //    connections.Add(sample10);
        //    sample11.SetConnectedRooms(connections, "Sample11");
        //    connections.Clear();

        //    connections.Add(sample7);
        //    connections.Add(sample9);
        //    sample12.SetConnectedRooms(connections, "Sample12");
        //    connections.Clear();

        //    SampleMap.Add(sample0);
        //    SampleMap.Add(sample1);
        //    SampleMap.Add(sample2);
        //    SampleMap.Add(sample3);
        //    SampleMap.Add(sample4);
        //    SampleMap.Add(sample5);
        //    SampleMap.Add(sample6);
        //    SampleMap.Add(sample7);
        //    SampleMap.Add(sample8);
        //    SampleMap.Add(sample9);
        //    SampleMap.Add(sample10);
        //    SampleMap.Add(sample11);
        //    SampleMap.Add(sample12);

        //    foreach (var e in SampleMap)
        //    {
        //        Debug.Log(e.namename);
        //        Debug.Log(e.ConnectedRooms.Count);
        //    }

        //    List<DungeonGridData> path;
        //    List<DungeonGridData> exclude = new();

        //    Debug.Log("===========================");
        //    GridPathFinder.FindPathToGoal(sample0, sample9, out path);
        //    Debug.Log("FIND PATH1!!!!!");
        //    Debug.Log($"PATH1 LENGTH : {path.Count}");
        //    foreach (var e in path)
        //    {
        //        Debug.Log(e.namename);
        //    }

        //    GridPathFinder.FindShortcutToGoal(sample0, sample9, out exclude);
        //}
    }
}
