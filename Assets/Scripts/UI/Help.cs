using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    private void Start()
    {
        HUD.Instance.SetHelp(this);
    }
}
