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
                // ����ĵ������ ������ ���� ĵ������ ������ ����ī�޶� ������ 1�� �ǰ� ���ӿ�����Ʈ false
                mainCanvas.SetActive(false);
                subCanvas.SetActive(true);
                subCamera.depth = 1f;

                gameObject.SetActive(false);
            }
    }
}