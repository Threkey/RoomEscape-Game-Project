using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotelSafe : MonoBehaviour
{
    public MainCamera mainCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                mainCamera.MoveCameraHotelSafeDetail();
                gameObject.SetActive(false);
            }
        }
    }
}