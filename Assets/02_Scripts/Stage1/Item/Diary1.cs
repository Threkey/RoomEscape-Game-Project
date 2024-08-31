using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary1 : MonoBehaviour
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
        // 일기장을 누르면 사라지고 웹으로 얻었다는 정보를 보냄
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (gm.isCharacterNearby(this.gameObject))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    StartCoroutine(gm.coSendData(gm.GetName(), gm.diary1Url));
                    gm.isGetDiary1 = true;
                    ui.ChangeItemImage(ui.spriteDiary);
                    ui.ChangeItemDescription(itemDescription);
                    ui.OpenPopup(false);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}