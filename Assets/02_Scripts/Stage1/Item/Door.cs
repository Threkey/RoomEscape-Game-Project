using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    Managers gm;
    UIManager ui;
    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        ui = UIManager.Instance;
    }

    // 테스트
    public TextMeshProUGUI test;


    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (gm.isBookshelfMoved && hit.collider.gameObject == this.gameObject)
            {
                if (!gm.isUnlocked)
                {
                    ui.SubCameraOn();
                    // 자물쇠 
                }
                else if (gm.isUnlocked)
                {
                    Debug.Log("스테이지 클리어");
                    StartCoroutine(gm.coSendData(gm.GetName(), gm.lv2Url));
                }
            }
        }
    }
}
