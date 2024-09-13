using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class painting : MonoBehaviour
{
    Managers gm;
    UIManager ui;

    [SerializeField]
    [TextArea(3, 5)]
    public string itemDescription;
    [SerializeField]
    string hintCode;
    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        ui = UIManager.Instance;
    }

    private void OnMouseDown()
    {
        // 그림을 누르면 자세히보기
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (gm.isCharacterNearby(this.gameObject))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    ui.CloseDialog();
                    ui.ChangeItemDescription(itemDescription);
                    ui.ChangeItemImage(ui.spritePaintingA);
                    ui.ChangeHintCode(hintCode);
                    ui.OpenPopup();
                }
            }
        }
    }
}