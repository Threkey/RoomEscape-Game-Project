using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubCanvas2 : MonoBehaviour
{
    Managers gm;

    public MainCamera2 mainCamera;
    public Camera subCamera;

    public Button btnBack;
    public Button btnOpenDrawer;

    public GameObject mainCanvas;
    public GameObject padLock;
    public GameObject padLockCollider;
    public GameObject drawer;

    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;

        btnBack.onClick.AddListener(Back);
        btnOpenDrawer.onClick.AddListener(OpenDrawer);
    }

    void Back()
    {
        mainCamera.MoveCameraDrawerDetail();
        mainCanvas.SetActive(true);
        gameObject.SetActive(false);
        padLockCollider.SetActive(true);
        subCamera.depth = -1f;
    }

    void OpenDrawer()
    {
        padLock.SetActive(false);
        drawer.transform.localPosition = new Vector3(-1.1f, 0f, 0f);
        gm.isUnlocked2 = true;
        Back();
    }
}