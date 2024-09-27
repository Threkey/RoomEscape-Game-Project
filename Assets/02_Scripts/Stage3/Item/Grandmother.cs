using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grandmother : MonoBehaviour
{
    Managers gm;
    UIManager2 ui;
    Dialog dl;

    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        ui = UIManager2.Instance;
        dl = Dialog.Instance;
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                if (!gm.isGrandmotherTalked)
                {
                    ui.ShowDialog();
                    gm.isGrandmotherTalked = true;
                }
                else
                {
                    ui.ShowDialog(2);
                }
            }
        }
    }
}