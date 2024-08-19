using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary1 : MonoBehaviour
{
    Managers gm;
    UIManager ui;
    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        ui = UIManager.Instance;
    }

    private void OnMouseDown()
    {
        // �ϱ����� ������ ������� ������ ����ٴ� ������ ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                StartCoroutine(gm.coSendData(gm.GetName(), gm.diary1Url));
                gm.isGetDiary1 = true;
                ui.OpenPopup();
                this.gameObject.SetActive(false);
            }
        }
    }
}