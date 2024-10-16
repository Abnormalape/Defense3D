using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">Data Type to Use.</typeparam>
    public interface IManagerFactory<T1>
    {
        public Dictionary<int, IController> ID_ControllerDictionary { get; set; }
        public T1 BaseDataDictionary { get; set; }


        public void OnInitializeManager_Factory();

        public void InitializeBaseData();

        public void FindAllInScene();

        public void SummonGameObject(int Id, Transform summonPoint = null);

        public void AddSummoned(int summoned_ID, IController summonedAttachedController);

        public void DestroyGameObject(GameObject prefabInstance);

        public void RemoveSummoned(int summoned_ID);
    }
}
