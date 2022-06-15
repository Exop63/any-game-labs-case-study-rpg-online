using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDot : MonoBehaviour
{
    public string text = "Connecting";
    public float time = .1f;

    private Text textCompnent;
    private int dotCount = 0;
    private int direction = 1;

    void OnEnable()
    {
        textCompnent = GetComponent<Text>();
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        dotCount += direction; ;
        if (dotCount % 3 == 0) direction *= -1;
        var value = text;
        for (int i = 0; i < dotCount; i++)
        {
            value += ".";
        }
        textCompnent.text = value;

        yield return new WaitForSeconds(time);
        if (enabled) StartCoroutine(Animate());
    }
}
