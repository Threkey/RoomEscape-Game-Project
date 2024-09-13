using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeKnife : MonoBehaviour
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
        // 커터칼을 누르면 커터칼이 사라지고 아이템 크게보기
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (gm.isCharacterNearby(this.gameObject))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    ui.CloseDialog();
                    gm.isGetOfficeKnife = true;
                    ui.ChangeItemImage(ui.spriteOfficeKnife);
                    ui.ChangeItemDescription(itemDescription);
                    ui.ChangeHintCode("");
                    ui.OpenPopup();
                    this.gameObject.SetActive(false);
                    ui.btnHintReveal.gameObject.SetActive(true);
                }
            }
        }
    }
}