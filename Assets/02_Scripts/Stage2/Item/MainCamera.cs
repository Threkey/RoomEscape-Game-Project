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
    public Transform transCamera0Detail;
    public Transform transCamera1Detail;

    int currentTransform = 1;

    public GameObject goClosetCollider;
    public GameObject goHotelSafeCollider;

    // Start is called before the first frame update
    void Start()
    {
        btnLeft.onClick.AddListener(MoveCameraLeft);
        btnRight.onClick.AddListener(MoveCameraRight);
        btnDown.onClick.AddListener(MoveCameraDown);

        transform.position = transCameras[1].position;
        transform.rotation = transCameras[1].rotation;
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

    void MoveCameraDown()
    {
        transform.position = transCameras[currentTransform].position;
        transform.rotation = transCameras[currentTransform].rotation;

        btnDown.gameObject.SetActive(false);
        btnRight.gameObject.SetActive(true);
        if(currentTransform == 1)
            btnLeft.gameObject.SetActive(true);

        goClosetCollider.SetActive(true);
        goHotelSafeCollider.SetActive(true);
    }

    public void MoveCameraClosetDetail()
    {
        transform.position = transCamera0Detail.position;
        transform.rotation = transCamera0Detail.rotation;

        btnDown.gameObject.SetActive(true);
        btnLeft.gameObject.SetActive(false);
        btnRight.gameObject.SetActive(false);
    }

    public void MoveCameraHotelSafeDetail()
    {
        transform.position = transCamera1Detail.position;
        transform.rotation = transCamera1Detail.rotation;

        btnDown.gameObject.SetActive(true);
        btnLeft.gameObject.SetActive(false);
        btnRight.gameObject.SetActive(false);
    }
}