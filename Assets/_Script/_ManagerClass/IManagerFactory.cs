using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">Controller To Manage.</typeparam>
    /// <typeparam name="T2">Manager.</typeparam>
    /// <typeparam name="T3">Data Type to Use.</typeparam>
    public interface IManagerFactory<T1, T2, T3>
        where T1 : IController<T2>
        where T2 : IManagerClass
    {
        public Dictionary<int, T1> ID_ControllerDictionary { get; set; }
        public T3 BaseDataDictionary { get; set; }


        public void OnInitializeManager_Factory();

        public void InitializeBaseData();

        public void FindAllInScene();

        public void SummonGameObject(int Id, Transform summonPoint = null);

        public void AddSummoned(int summoned_ID, T1 summonedAttachedController);

        public void DestroyGameObject(GameObject prefabInstance);

        public void RemoveSummoned(int summoned_ID);
    }
}
