using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RustyKey : MonoBehaviour
{
    Managers gm;
    UIManager2 ui;

    [SerializeField]
    [TextArea(3, 5)]
    string itemDescription;
    [SerializeField]
    Sprite itemImage;
    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        ui = UIManager2.Instance;
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                ui.CloseDialog();
                ui.ChangeItemImage(itemImage);
                ui.ChangeItemDescription(itemDescription);
                ui.ChangeHintCode();
                ui.OpenItemPopup();
                gm.isGetKey = true;
                gameObject.SetActive(false);
            }
        }
    }
}