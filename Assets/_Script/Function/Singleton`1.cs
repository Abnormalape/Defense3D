using System;

namespace BHSSolo.DungeonDefense.Singleton
{
    public abstract class Singleton<T>
        where T : Singleton<T>
    {
        public static T instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = (T)Activator.CreateInstance(typeof(T));

                    // : if have to get something private or public or like these, use below
                    //ConstructorInfo constructorInfo = typeof(T).GetConstructor(new TypeAttributes[0]);
                    //if (constructorInfo != null)
                    //    s_instance = (T)constructorInfo.Invoke(null);
                }
                return s_instance;
            }
        }
        private static T s_instance;
    }
}
