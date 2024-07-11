using JetBrains.Annotations;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.NPCs
{
    /// <summary>
    /// NPC가 가지는 공통적인 특성.
    /// nPCNature: npc의 기질. ex) 슬라임의 경우, 부정형 몸체.
    /// nPCState : npc의 상태. ex) 부정형 몸체의 경우, 물리데미지 경감, 산성데미지 부여 등.
    /// nPCStatus : npc의 능력치. 해당 클래스에서 설명.
    /// nPCEquipment : npc의 장비.
    /// </summary>
    class NPCs
    {
        protected string NPCName;
        protected NPCType NPCType;
        protected int NPCLevel = 1;
        protected Dictionary<string, string> NPCData;
        //protected NPCNature[] nPCNature; //Todo:
        //protected NPCState[] nPCState;
        //protected NPCStatus nPCStatus;
        protected NPCEquipment[] nPCEquipment;
    }
    public enum NPCType
    {
        None,
        Monster,
        Hero,
    }
}
