using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class AllyStatusModifier
    {
        public AllyStatusModifier(AllyController_ ownerCcontroller) 
        { 
            this.ownerCcontroller = ownerCcontroller;
        }


        private AllyController_ ownerCcontroller;


        public void ModifyStatus()
        {
            Debug.Log("Modify Status");
        }
    }
}
