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

    public class GameObjectOperations : Singleton<GameObjectOperations>
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
        }

        public static void Clear(Transform g)
        {
            foreach (Transform item in g)
            {
                if (item != g)
                {
                    Destroy(g.gameObject);
                }
            }
        }
    }
}