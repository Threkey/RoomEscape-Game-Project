using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Closet3 : MonoBehaviour
{
    public MainCamera3 mainCamera3;


    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                mainCamera3.MoveCameraClosetDetail();
                gameObject.SetActive(false);
            }
        }
    }
}
