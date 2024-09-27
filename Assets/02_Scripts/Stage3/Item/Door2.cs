using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    Managers gm;
    UIManager2 ui;
    AudioSource au;

    public Camera subCamera;
    public GameObject mainCanvas;
    public GameObject subCanvasScreenSpace;

    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        ui = UIManager2.Instance;
        au = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (gm.isCharacterNearby(this.gameObject))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    // ���� ĵ���� ���� ���� ĵ���� Ű�� ����ī�޶� ���� 1��
                    mainCanvas.SetActive(false);
                    subCanvasScreenSpace.SetActive(true);
                    subCamera.depth = 1;
                }
            }
        }
    }
}