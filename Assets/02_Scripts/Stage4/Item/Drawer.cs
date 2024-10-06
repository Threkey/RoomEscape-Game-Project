using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drawer : MonoBehaviour
{
    public MainCamera2 mainCamera;

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            if (hit.collider.gameObject == gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                mainCamera.MoveCameraDrawerDetail();
                gameObject.SetActive(false);
            }
    }
}
