using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagicCircle : MonoBehaviour
{
    Managers gm;
    public GameObject goMagicCircle;

    Color colorBright = new Color(1f, 1f, 1f);
    Color colorDark = new Color(0.6f, 0.6f, 0.6f);

    private void Start()
    {
        gm = Managers.Instance;
    }
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            if (hit.collider.gameObject == gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                goMagicCircle.GetComponent<SpriteRenderer>().color = colorDark;
                StartCoroutine(gm.coSendData(gm.GetName(), gm.dragUrl));
            }
    }

    private void OnMouseEnter()
    {
        goMagicCircle.GetComponent<SpriteRenderer>().color = colorBright;
    }

    private void OnMouseExit()
    {
        goMagicCircle.GetComponent<SpriteRenderer>().color = colorDark;
    }

    private void OnMouseUpAsButton()
    {
        goMagicCircle.GetComponent<SpriteRenderer>().color = colorBright;
    }
}
