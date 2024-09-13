using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    GameObject goCam;
    void Start()
    {
        if (GameObject.Find("SubCamera").GetComponent<Camera>().depth > 0.0f)
            goCam = GameObject.Find("SubCamera");
        else
            goCam = GameObject.Find("Main Camera");
        transform.LookAt(goCam.transform);
        Invoke("Destroy", 0.4f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
