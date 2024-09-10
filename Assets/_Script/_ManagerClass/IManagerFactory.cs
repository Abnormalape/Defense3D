using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    /// <summary>
    /// Interface for Manager who is responsible for summon, and destroy GameObjcet.
    /// T is for Data Structure Like AllyBaseStatus.
    /// </summary>
    public interface IManagerFactory<T> where T : struct
    {
        public Dictionary<int, IController> ID_ControllerDictionary { get; set; }
        public Dictionary<Enum, T> BaseDataDictionary { get; set; }


        public void OnInitializeManager_Factory();

        public void InitializeBaseData();

        public void FindAllInScene();

        public void SummonGameObject(GameObject prefab, Transform summonPoint);

        public void AddSummoned(int summoned_ID, IController summonedAttachedController);

        public void DestroyGameObject(GameObject prefabInstance);

        public void RemoveSummoned(int summoned_ID);
    }
}
