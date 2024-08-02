using UnityEngine;

namespace BHSSolo.DungeonDefense.Singleton
{
    public class SingletonMono<T> : MonoBehaviour
        where T : SingletonMono<T>
    {
        private static T s_instanceMono;

        public static T InstanceMono
        {
            get
            {
                if (s_instanceMono == null)
                {
                    s_instanceMono = GameObject.FindFirstObjectByType<T>();

                    if (s_instanceMono == null)
                    {
                        s_instanceMono = new GameObject(nameof(T)).AddComponent<T>();
                    }
                }
                return s_instanceMono;
            }
        }
    }
}
