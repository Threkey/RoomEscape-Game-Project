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
            if (gm.isCharacterNearby(this.gameObject))
            {
                if (gm.isBookshelfMoved && hit.collider.gameObject == this.gameObject)
                {
                    ui.SubCameraOn();
                    // 자물쇠 
                }
            }
        }
    }
}
