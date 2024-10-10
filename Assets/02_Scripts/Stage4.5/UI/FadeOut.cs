using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    Color colorInit = new Color(1f, 1f, 1f, 0f);
    Color colorDelta = new Color(1f, 1f, 1f, 0.2f);
    void Start()
    {
        GetComponent<Image>().color = colorInit;
    }
    void Update()
    {
        if(GetComponent<Image>().color.a < 1.0f)
            GetComponent<Image>().color += colorDelta * Time.deltaTime;
    }
}