using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class UIController_ : MonoBehaviour
    {
        public abstract UIManager_ UIManager_ { get; set; }

        public abstract UI_enum UIType { get; set; }

        public abstract Canvas myCanvas { get; set; }


        public abstract void Open();

        public abstract void Close();

        public abstract void OnUIUpdate();

        public abstract void FixUI();
    }
}