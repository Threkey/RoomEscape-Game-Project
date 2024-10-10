using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Amulet : MonoBehaviour
{
    Managers gm;
    public GameObject goItem;       // Active��ų ������Ʈ
    public GameObject goItemSlot;
    public GameObject goFadeOut;

    private void Start()
    {
        gm = Managers.Instance;
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
                if (gm.isLighterSelected && gm.isAmuletUsed)
                {
                    goItem.SetActive(true);
                    goItemSlot.SetActive(false);
                    gm.isLighterUsed = true;
                    goFadeOut.SetActive(true);
                }
        }
    }
}
