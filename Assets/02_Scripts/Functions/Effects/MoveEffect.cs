using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveEffect : MonoBehaviour
{
    public static bool on = false;
    public static float timer = 0.0f;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        if(timer >= 5000.0f)
        {
            timer = 10.0f;
        }
    }
}
