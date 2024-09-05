using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class UIController_ : MonoBehaviour
    {
        public abstract UIManager_ UIManager_ { get; set; }

        //public abstract UI_enum UIType { get; set; }
    }
}