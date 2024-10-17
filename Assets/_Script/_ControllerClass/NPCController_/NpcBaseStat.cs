using Unity.VisualScripting;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class NpcBaseStat
    {
        public static NpcBaseStat operator +(NpcBaseStat statA, NpcBaseStat statB)
        {
            NpcBaseStat result = new NpcBaseStat();
            for (int i = 0; i < result.NpcBaseStats.Length; i++)
            {
                result[i] = statA[i] + statB[i];
            }
            return result;
        }

        public float this[NpcStat npcStat]
        {
            get
            {
                int statID = (int)npcStat;
                return NpcBaseStats[statID];
            }
            set
            {
                int statID = (int)npcStat;
                NpcBaseStats[statID] = value;
            }
        }

        public float this[int npcID]
        {
            get
            {
                int statID = (int)npcID;
                return NpcBaseStats[statID];
            }
            set
            {
                int statID = (int)npcID;
                NpcBaseStats[statID] = value;
            }
        }

        public NpcBaseStat(NpcBaseStatus npcBaseStatus, int currentLevel = 1)
        {
            int tempCurrentLevel = currentLevel;

            for (int i = 0; i < 20; i += 2)
            {
                NpcBaseStats[i] = npcBaseStatus[i];
            }

            for (int i = 0; i < 20; i += 2)
            {
                float personal = Random.Range(0.8f, 1.2f);
                NpcBaseStats[i] = npcBaseStatus[i] * personal;
            }

            for (int i = 0; i < 20; i += 2)
            {
                NpcBaseStats[i] = NpcBaseStats[i] + (NpcBaseStats[i + 1] * (tempCurrentLevel - 1));
            }
        }

        public NpcBaseStat()
        {
            for (int i = 0; i < NpcBaseStats.Length; i++)
            {
                NpcBaseStats[i] = 0;
            }
        }


        public float[] NpcBaseStats { get; private set; } = new float[20];
    }
}