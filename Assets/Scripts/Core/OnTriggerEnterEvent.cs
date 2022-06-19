using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{
    public UnityActionCollider onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(other);
    }
}


[System.Serializable]
public class UnityActionCollider : UnityEvent<Collider> { }
