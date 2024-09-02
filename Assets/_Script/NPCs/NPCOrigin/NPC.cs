using BHSSolo.DungeonDefense.DungeonRoom;
using System.Collections.Generic;
using UnityEngine;


namespace BHSSolo.DungeonDefense.NPCs
{
    /// <summary>
    /// NPC가 가지는 공통적인 특성.
    /// nPCNature: npc의 기질. ex) 슬라임의 경우, 부정형 몸체.
    /// nPCState : npc의 상태. ex) 부정형 몸체의 경우, 물리데미지 경감, 산성데미지 부여 등.
    /// nPCStatus : npc의 능력치. 해당 클래스에서 설명.
    /// nPCEquipment : npc의 장비.
    /// </summary>
    public abstract class NPC : MonoBehaviour
    {
        //private Data //Todo: Data Of Single NPC;

        protected Function.StateMachineBehaviour StateMachineBehaviour;
        public NPCStatusController      NPCStatusController { get; private set; }

        public NPCSubStatusController   NPCSubStateController { get; private set; }

        public NPCTraitController       NPCTraitController { get; private set; }

        public NPCBuffController        NPCBuffController { get; private set; }

        public NPCEquipmentController   NPCEquipmentController { get; private set; }

        public DungeonRoomController    CurrentRoom { get; protected set; }

        public Dictionary<string, string> NPCData { get; private set; }

        private void Awake()
        {
            NPCStatusController = new(this);
            NPCSubStateController = new(this);
            NPCTraitController = new(this);
            NPCBuffController = new(this);
            NPCEquipmentController = new(this);
        }

        //==============================================================//
        protected string NPCName;
        protected NPCType NPCType;
        protected int NPCLevel = 1;
        

        //protected List<NPCTrait> nPCTrait = new List<NPCTrait>(3);
        //protected List<NPCBuff> nPCBuff = new List<NPCBuff>(10);
        //protected NPCStatus nPCStatus;
        //protected List<NPCEquipment> nPCEquipment = new List<NPCEquipment>(3);

        //public NPCEventHandler NPCEventHandler { get; set; } = new NPCEventHandler();



        //protected static int NPCCount = 1; // Todo:
        //public int NPCID { get; protected set; } // Todo:
        //protected void InitNPCID() // Todo:
        //{
        //    NPCID = NPCCount;
        //    NPCCount++;
        //}
    }
    public enum NPCType
    {
        None,
        Monster,
        Hero,
    }
}
