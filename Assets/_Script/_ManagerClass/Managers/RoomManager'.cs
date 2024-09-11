using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    /// <summary>
    /// Same as Ally
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
            //AllyController_[] allysInMap = FindObjectsByType<AllyController_>(FindObjectsSortMode.None);

            //foreach (AllyController_ e in allysInMap)
            //{
            //    ((IController)e).ControllerInitializer(this); //Controller Initialize
            //    e.AllyControllerInitializer(BaseDataDictionary[e.AllyEnum_]); //Ally Controller Initialize

            //    e.Ally_ID = Ally_ID;
            //    Ally_ID++;

            //    AddSummoned(e.Ally_ID, e as IController);
            //}

            //Debug.Log($"Found, Set, Registet Complete.\n{allysInMap.Length} Allys in map.");

            AddSummoned(Room_ID, null);//Todo:
        }

        public void SummonGameObject(GameObject prefab, Transform summonPoint)
        {
        }

        public void AddSummoned(int summoned_ID, IController summonedAttachedController)
        {
            ID_ControllerDictionary.Add(summoned_ID, summonedAttachedController);
            Room_ID++; ;
        }

        public void DestroyGameObject(GameObject prefabInstance)
        {
            Destroy(prefabInstance);
        }

        public void RemoveSummoned(int summoned_ID)
        {
            ID_ControllerDictionary.Remove(summoned_ID);
        }
    }

    public struct RoomBaseData
    {

    }
}
