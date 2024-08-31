using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    Managers gm;
    UIManager ui;

    [SerializeField]
    [TextArea(3, 5)]
    string itemDescription;
    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        ui = UIManager.Instance;
    }

    private void OnMouseDown()
    {
        // ´Þ·ÂÀ» ´©¸£¸é ÆË¾÷ÀÌ ¶ä
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (gm.isCharacterNearby(this.gameObject))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    if (ui.GetCurrentItemImage() == null || ui.GetCurrentItemImage() == ui.spriteDiary)
                        ui.ChangeItemImage(ui.spritesCalendar[0]);
                    ui.ChangeItemDescription(itemDescription);
                    ui.OpenPopup(true);
                }
            }
        }
    }
}