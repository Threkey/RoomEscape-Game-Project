using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera : MonoBehaviour
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnDown;

    public Transform[] transCameras = new Transform[3];

    Transform transCamera1Detail;
    Transform transCamera2Detail;

    int currentTransform = 1;
    bool isDetail = false;

    // Start is called before the first frame update
    void Start()
    {
        btnLeft.onClick.AddListener(MoveCameraLeft);
        btnRight.onClick.AddListener(MoveCameraRight);
    }

    void MoveCameraLeft()
    {
        btnRight.gameObject.SetActive(true);

        if(currentTransform == 1)
            btnLeft.gameObject.SetActive(false);

        if (currentTransform > 0)
            transform.rotation = transCameras[--currentTransform].rotation;
    }

    void MoveCameraRight()
    {
        btnLeft.gameObject.SetActive(true);

        if (currentTransform == 1)
            btnRight.gameObject.SetActive(false);

        if (currentTransform < 2)
            transform.rotation = transCameras[++currentTransform].rotation;
    }
}