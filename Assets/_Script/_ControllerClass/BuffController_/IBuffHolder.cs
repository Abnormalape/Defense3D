using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.Controller
{
    public interface IBuffHolder
    {
        public Dictionary<int, BuffController> HoldingBuffs { get; set; } //버프를 가진 애니까 보유버프 목록이 당연히 있어야지?
        public BuffManager BuffManager { get; set; }
        public List<NpcStatModifier> StatModifiers { get; set; }
        public void InitializeBuffHolder();
        public void AddBuff(int buffID);
        public void RemoveBuff(int buffID);
    }
}
