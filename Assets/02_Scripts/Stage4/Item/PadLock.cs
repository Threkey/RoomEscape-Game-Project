using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PadLock : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject subCanvas;
    public Camera subCamera;
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            if (hit.collider.gameObject == gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                // 메인캔버스가 꺼지고 서브 캔버스가 켜지고 서브카메라 뎁스가 1이 되고 게임오브젝트 false
                mainCanvas.SetActive(false);
                subCanvas.SetActive(true);
                subCamera.depth = 1f;

                gameObject.SetActive(false);
            }
    }
}