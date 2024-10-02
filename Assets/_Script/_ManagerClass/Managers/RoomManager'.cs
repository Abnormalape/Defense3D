using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class RoomManager_ : MonoBehaviour, IManagerClass, IManagerFactory<RoomBaseData> //Like Ally
    /// <summary>
    /// 
    /// </summary>
    public class RoomManager_ : MonoBehaviour, IManagerClass, IManagerFactory<RoomBaseData>
    {
        public GameManager_ OwnerManager { get; set; }
        public Dictionary<int, IController> ID_ControllerDictionary { get; set; } = new();
        public Dictionary<Enum, RoomBaseData> BaseDataDictionary { get; set; } = new();

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
                ((IController)e).ControllerInitializer(this); //Controller Initialize
                e.RoomControllerInitializer(); //Ally Controller Initialize

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

        public void AddSummoned(int summoned_ID, IController summonedAttachedController)
        {
            ID_ControllerDictionary.Add(summoned_ID, summonedAttachedController);
            Room_ID++;
        }

        public void DestroyGameObject(GameObject prefabInstance)
        {
            Destroy(prefabInstance);
        }

        public void RemoveSummoned(int summoned_ID)
        {
            ID_ControllerDictionary.Remove(summoned_ID);
        }

        public void AddRoom(string RoomName) //Todo: Remove
        {
            Debug.Log("Built Room Name : " + RoomName);
        }
    }

    public struct RoomBaseData
    {

    }
}
