using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tools : MonoBehaviour
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
                if (gm.isUnlocked2)
                {
                    ui.ChangeItemDescription(itemDescription);
                    ui.ChangeItemImage(itemImage);
                    ui.ChangeHintCode();
                    ui.OpenItemPopup();
                    gm.isGetTools = true;
                    StartCoroutine(gm.coSendData(gm.GetName(), gm.hiddenUrl));
                    gameObject.SetActive(false);
                }
            }
    }
}
