using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary3 : MonoBehaviour
{
    Managers gm;
    UIManager2 ui;
    AudioSource au;

    [TextArea(3, 5)]
    public string itemDescription;
    public Sprite itemImage;

    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        ui = UIManager2.Instance;
        au = GameObject.Find("Structure").transform.Find("MainRoom").Find("goDiary3").GetComponent<AudioSource>();
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
                    au.Play();
                    ui.CloseDialog();
                    ui.ChangeHintCode();
                    ui.ChangeItemImage(itemImage);
                    ui.ChangeItemDescription(itemDescription);
                    ui.OpenItemPopup();
                    StartCoroutine(gm.coSendData(gm.GetName(), gm.diary3Url));
                    gameObject.SetActive(false);
                }
            }
        }
    }
}