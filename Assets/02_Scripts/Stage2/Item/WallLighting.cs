using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLighting : MonoBehaviour
{
    Light li;

    [SerializeField]
    float speed;
    float b = 1.3f;

    void Start()
    {
        li = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        li.intensity = Mathf.Lerp(li.intensity, b, speed * Time.deltaTime);
        // li.intensity += speed * Time.deltaTime;

        if (li.intensity <= 0.75f)
            b = 1.3f;
        else if (li.intensity >= 1.25f)
            b = 0.7f;
    }
}
