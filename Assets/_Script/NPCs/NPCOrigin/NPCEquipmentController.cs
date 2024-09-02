using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.NPCs
{
    public class NPCEquipmentController
    {
        private readonly NPC _ownerNPC;

        public List<NPCEquipment> NPCEquipments { get; private set; } = new(4);


        public NPCEquipmentController(NPC owner)
        {
            _ownerNPC = owner;
        }
    }
}
