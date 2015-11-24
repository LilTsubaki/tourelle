using UnityEngine;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class Pool<T> where T : ObjetPoolable<T>, new()
    {
        private List<T> objects =  new List<T>();
        private T obj;

        public Pool (T t)
        {
            obj = t;
        }


        public T getFirst()
        {
            for(int i = 0; i < objects.Count; i++)
            {
                if (objects[i].isAvailable())
                {
                    objects[i].putUnavailable();
                    return objects[i];
                }    
            }
            T objectReturn = new T();
            objectReturn.Copy(obj);
            objectReturn.putUnavailable();
            objects.Add(objectReturn);
            return objectReturn;
        }
    }
}
