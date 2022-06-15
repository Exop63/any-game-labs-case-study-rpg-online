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
}