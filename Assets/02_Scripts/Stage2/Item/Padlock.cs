using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Padlock : MonoBehaviour
{
    Managers gm;
    UIManager2 ui;

    AudioSource au;

    public GameObject goChains;
    public GameObject goLeftDoor;
    public GameObject goRightDoor;
    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        ui = UIManager2.Instance;
        au = GameObject.Find("closet").transform.Find("Chains").GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                if(gm.isGetKey == true)
                {
                    // 사슬 풀고 옷장 문 열고 다이얼로그 띄움
                    goChains.transform.Find("ChainedChain").gameObject.SetActive(false);
                    goChains.transform.Find("UnchainedChain").gameObject.SetActive(true);
                    au.Play();

                    goLeftDoor.transform.Rotate(0f, 150f, 0f);
                    goLeftDoor.transform.localPosition = new Vector3(-3f, 0f, 4f);
                    goRightDoor.transform.Rotate(0f, -150f, 0f);
                    goRightDoor.transform.localPosition = new Vector3(-3f, 0f, -4f);

                    ui.ShowDialog();
                }
                else if(gm.isGetKey == false)
                {
                    ui.ShowDialog(0);
                }
            }
        }
    }
}