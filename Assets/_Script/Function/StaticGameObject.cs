using UnityEngine;

namespace BHSSolo.DungeonDefense.Function
{
    public class StaticGameObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}