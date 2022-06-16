using System;
using System.Collections;
using UnityEngine;

namespace Extensions
{
    public class EventSystem : Singleton<EventSystem>
    {
        public void WaitAndDo(float time, Action onComplete)
        {
            if (onComplete == null) return;
            StartCoroutine(IEWaitAndDo(time, onComplete));
        }

        public IEnumerator IEWaitAndDo(float time, Action onComplete)
        {
            if (onComplete != null)
            {
                yield return new WaitForSeconds(time);
                onComplete();
            }
        }

    }

    public static class GameObjectOperation
    {
        public static Transform GetChild(Transform g, string name)
        {
            foreach (Transform item in g)
            {

                Debug.Log("item: " + item.name);
                if (item.name == name)
                {
                    return item;
                }
                else
                {
                    var found = GetChild(item, name);
                    if (found != null) return found;
                }
            }
            return null;
            // var childs = g.GetComponentsInChildren<T>();
            // if (childs.Length <= 1) return null;

            // for (int i = 0; i < childs.Length; i++)
            // {
            //     var child = childs[i];
            //     if (child.name == name)
            //     {
            //         return child;
            //     }
            //     else return GameObjectOperation.GetChild<T>(child, name);
            // }
            // return null;
        }
    }
}