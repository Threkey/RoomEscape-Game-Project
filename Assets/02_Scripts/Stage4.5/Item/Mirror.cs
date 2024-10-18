using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mirror : MonoBehaviour
{
    Managers gm;
    AudioSource au;

    public GameObject goItem;       // Active시킬 오브젝트
    public GameObject goItemSlot;

    private void Start()
    {
        gm = Managers.Instance;
        au = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
                if (gm.isAmuletSelected && gm.isSaltUsed)
                {
                    goItem.SetActive(true);
                    goItemSlot.SetActive(false);
                    gm.isAmuletUsed = true;
                    au.Play();
                }
        }
    }
}
