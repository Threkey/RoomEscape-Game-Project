using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera2 : MonoBehaviour
{
    Managers gm;

    public Button btnLeft;
    public Button btnRight;
    public Button btnDown;

    public Transform[] transCameras = new Transform[3];
    public Transform transCamera2Detail;
    public Transform transCameraDrawer;

    int currentTransform = 1;
    bool isDrawerCamera = false;

    public GameObject goClosetCollider;
    public GameObject goDrawerCollider;

    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;

        btnLeft.onClick.AddListener(MoveCameraLeft);
        btnRight.onClick.AddListener(MoveCameraRight);
        btnDown.onClick.AddListener(MoveCameraDown);

        transform.position = transCameras[1].position;
        transform.rotation = transCameras[1].rotation;
    }

    void MoveCameraLeft()
    {
        btnRight.gameObject.SetActive(true);

        if (currentTransform == 1)
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
        if(!isDrawerCamera)
        {
            transform.position = transCameras[currentTransform].position;
            transform.rotation = transCameras[currentTransform].rotation;

            btnDown.gameObject.SetActive(false);
            btnLeft.gameObject.SetActive(true);

            goClosetCollider.SetActive(true);
            goDrawerCollider.SetActive(true);
        }
        else
        {
            transform.position = transCamera2Detail.position;
            transform.rotation = transCamera2Detail.rotation;

            btnDown.gameObject.SetActive(true);

            goDrawerCollider.SetActive(true);

            isDrawerCamera = false;
        }
    }

    public void MoveCameraClosetDetail()
    {
        transform.position = transCamera2Detail.position;
        transform.rotation = transCamera2Detail.rotation;

        btnDown.gameObject.SetActive(true);
        btnLeft.gameObject.SetActive(false);
        btnRight.gameObject.SetActive(false);
    }

    public void MoveCameraDrawerDetail()
    {
        isDrawerCamera = true;

        transform.position = transCameraDrawer.position;
        transform.rotation = transCameraDrawer.rotation;

        btnDown.gameObject.SetActive(true);
        btnLeft.gameObject.SetActive(false);
        btnRight.gameObject.SetActive(false);
    }
}