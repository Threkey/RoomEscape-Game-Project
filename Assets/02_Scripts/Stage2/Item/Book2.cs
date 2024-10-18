using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Book2 : MonoBehaviour
{
    Managers gm;
    UIManager2 ui;
    AudioSource au;

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
        au = GetComponent<AudioSource>();
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
                au.Play();
                ui.ChangeItemImage(itemImage);
                ui.ChangeItemDescription(itemDescription);
                ui.ChangeHintCode();
                ui.OpenItemPopup();
            }
        }
    }
}