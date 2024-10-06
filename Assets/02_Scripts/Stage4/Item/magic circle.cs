using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagicCircle : MonoBehaviour
{
    Managers gm;
    UIManager2 ui;

    [TextArea(3, 5)]
    public string itemDescription;
    public Sprite itemImage;

    private void Start()
    {
        gm = Managers.Instance;
        ui = UIManager2.Instance;
    }
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            if (hit.collider.gameObject == gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                StartCoroutine(gm.coSendData(gm.GetName(), gm.dragUrl));
            }
    }
}
