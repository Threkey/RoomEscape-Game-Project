using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapBook : MonoBehaviour
{
    Managers gm;
    UIManager2 ui;

    [TextArea(3, 5)]
    public string itemDescription;
    public Sprite itemImage;

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
            if (gm.isCharacterNearby(this.gameObject))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    ui.CloseDialog();
                    ui.ChangeHintCode();
                    ui.ChangeItemImage(itemImage);
                    ui.ChangeItemDescription(itemDescription);
                    ui.OpenItemPopup();
                    StartCoroutine(gm.coSendData(gm.GetName(), gm.mapUrl));
                    gameObject.SetActive(false);
                }
            }
        }
    }
}