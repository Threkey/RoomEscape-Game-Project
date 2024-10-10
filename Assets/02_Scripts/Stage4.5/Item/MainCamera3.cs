using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera3 : MonoBehaviour
{
    public Button btnDown;

    public Transform[] transCameras = new Transform[3];
    public Transform transCamera0Detail;

    int currentTransform = 0;

    public GameObject goClosetCollider;

    // Start is called before the first frame update
    void Start()
    {
        btnDown.onClick.AddListener(MoveCameraDown);

        transform.position = transCameras[0].position;
        transform.rotation = transCameras[0].rotation;
    }

    void MoveCameraDown()
    {
        transform.position = transCameras[currentTransform].position;
        transform.rotation = transCameras[currentTransform].rotation;

        btnDown.gameObject.SetActive(false);

        goClosetCollider.SetActive(true);
    }

    public void MoveCameraClosetDetail()
    {
        transform.position = transCamera0Detail.position;
        transform.rotation = transCamera0Detail.rotation;

        btnDown.gameObject.SetActive(true);
    }
}