using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraFollow : Singleton<BasicCameraFollow>
{
    public Vector3 offset;
    [SerializeField] Transform target;

    public void Set(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target == null) return;
        var newPos = target.transform.position + offset;
        transform.position = newPos;
        transform.LookAt(target);
    }
}
