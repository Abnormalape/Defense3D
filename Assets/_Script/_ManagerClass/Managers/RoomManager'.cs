using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class RoomManager_ : MonoBehaviour, IManagerClass, IManagerFactory<RoomBaseStatus>
    {
        public GameManager_ OwnerManager { get; set; }
        public Dictionary<int, IController> ID_ControllerDictionary { get; set; } = new();
        public RoomBaseStatus BaseDataDictionary { get; set; }

        public const string ROOM_PATH = "RoomPrefab";
        public static int Room_ID { get; private set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            OnInitializeManager_Factory();
        }

        public void OnInitializeManager_Factory()
        {
            InitializeBaseData();
            FindAllInScene();
        }

        public void InitializeBaseData()
        {
        }

        public void FindAllInScene()
        {
            RoomController[] roomsInMap = FindObjectsByType<RoomController>(FindObjectsSortMode.None);

            foreach (RoomController e in roomsInMap)
            {
                //Must execute A and B separately.
                ((IController)e).InitializeController(this); //Controller Initialize
                e.RoomControllerInitializer(); //Room Controller Initialize

                //e.AllyControllerInitializer(BaseDataDictionary[e.AllyEnum_]); //=> AllyController Needs its Data.
                //But how on Room? Does Room Need Data?
                //==> Yes. 

                e.Room_ID = Room_ID;
                Room_ID++;

                AddSummoned(e.Room_ID, e as IController);
            }

            Debug.Log($"Found, Set, Register Complete.\n{roomsInMap.Length} Rooms in map.");

            //AddSummoned(Room_ID, null);//Todo: ? For what?
        }

        public void SummonGameObject(GameObject prefab, Transform summonPoint)
        {
            GameObject tempSummoned
                = Instantiate(prefab, summonPoint.position, summonPoint.rotation, null); //Todo: null Is for RoomHolder.

            AddSummoned(Room_ID, tempSummoned.GetComponent<IController>());
        }

        public void SummonGameObject(int Id, Transform summonPoint = null)
        {
            throw new NotImplementedException();
        }

        public void AddSummoned(int summoned_ID, IController summonedAttachedController)
        {
            ID_ControllerDictionary.Add(summoned_ID, summonedAttachedController);
            Room_ID++;
        }

        public void RemoveSummoned(int summoned_ID)
        {
            ID_ControllerDictionary.Remove(summoned_ID);
        }

        public void DestroyGameObject(GameObject prefabInstance)
        {
            Destroy(prefabInstance);
        }

        public GameObject MakeNewRoom(string RoomName) //Todo: Add Room.
        {
            //Debug.Log("Built Room Name : " + RoomName);

            GameObject roomPrefab = Resources.Load($"Prefabs/Room/{RoomName}") as GameObject;
            return Instantiate(roomPrefab);
        }

        public GameObject TempBuildRoomMethodUsingCsvData(string MapCodeOnCsv) //Todo:
        {
            GameObject t;
            if (MapCodeOnCsv.StartsWith("R"))
            {
                t = MakeNewRoom("StandardSmallRoom");
            }
            //else if(MapCodeOnCsv == "E") //For entrance
            else
            {
                t = MakeNewRoom("StandardSmallPassage");
            }


            IController tempController = t.GetComponent<IController>();
            tempController.InitializeController(this);

            AddSummoned(Room_ID, t.GetComponent<IController>());
            return t;
        }

    }

    public class RoomBaseStatus //Todo: Temp Class
    {

    }
}
