using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class BuffManager : MonoBehaviour, IManagerClass//Factory interface를 사용할까?
    {
        public GameManager_ GameManager { get; set; }
        private DataManager_ DataManager { get; set; }
        private List<BuffBaseData> BuffBaseDatas { get; set; } //Buff 초기화에 사용할 BaseData. Index와 ID가 일치한다(중요).
        public Dictionary<int, Type> BuffTypes { get; set; } = new();

        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
            DataManager = GameManager.DataManager_;
            this.BuffBaseDatas = DataManager.BuffBaseDatas;

            var tempBuffs = Assembly.GetExecutingAssembly().GetTypes()
                                    .Where(t => typeof(BuffController).IsAssignableFrom(t)
                                             && !t.IsInterface
                                             && !t.IsAbstract).ToList();

            foreach (var item in tempBuffs)
            {
                int tempBuffID = (Activator.CreateInstance(item) as BuffController).BuffID;
                BuffTypes.Add(tempBuffID, item);
            }
        }

        /// <summary>
        /// Return buff with buff Data.
        /// Must Initialize Result.
        /// </summary>
        /// <param name="buffID">Buff Id To Instantiate</param>
        /// <param name="buffData">TempBuffBaseData To Initialize</param>
        /// <returns></returns>
        public BuffController InstantiateBuff(int buffID, out BuffBaseData buffData)
        {
            if (buffID < 0 || buffID >= BuffBaseDatas.Count)
            {
                Debug.LogWarning($"Invalid buffID: {buffID}");
                buffData = null;
                return null;
            }

            BuffTypes.TryGetValue(buffID, out var buffType); //For Instance.
            BuffBaseData tempBaseData = BuffBaseDatas[buffID]; //For Initialize.

            if (buffType == null || tempBaseData == null) { buffData = null; return null; }

            var instance = Activator.CreateInstance(buffType) as BuffController;
            instance.InitializeController(this);

            buffData = tempBaseData;
            return instance;
        }
    }
}
