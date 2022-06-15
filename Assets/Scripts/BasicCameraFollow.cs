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
        var newPos = Vector3.Lerp(transform.position, target.transform.position + offset, 0.8f);

        transform.position = newPos;
        transform.LookAt(target);
    }
}
