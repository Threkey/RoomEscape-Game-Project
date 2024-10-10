using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    Managers gm;
    public Button[] btnItems = new Button[3];       // 0: Salt, 1: Amulet, 2: Lighter
    public Sprite[] spriteItems = new Sprite[3];
    public Sprite[] spriteItemsSelected = new Sprite[3];

    void Start()
    {
        gm = Managers.Instance;
        btnItems[0].onClick.AddListener(SelectSalt);
        btnItems[1].onClick.AddListener(SelectAmulet);
        btnItems[2].onClick.AddListener(SelectLighter);
    }

    void SelectSalt()
    {
        gm.isSaltSelected = true;
        gm.isAmuletSelected = false;
        gm.isLighterSelected = false;

        btnItems[0].transform.Find("Image").GetComponent<Image>().sprite = spriteItemsSelected[0];
        btnItems[1].transform.Find("Image").GetComponent<Image>().sprite = spriteItems[1];
        btnItems[2].transform.Find("Image").GetComponent<Image>().sprite = spriteItems[2];
    }
    void SelectAmulet()
    {
        gm.isSaltSelected = false;
        gm.isAmuletSelected = true;
        gm.isLighterSelected = false;

        btnItems[0].transform.Find("Image").GetComponent<Image>().sprite = spriteItems[0];
        btnItems[1].transform.Find("Image").GetComponent<Image>().sprite = spriteItemsSelected[1];
        btnItems[2].transform.Find("Image").GetComponent<Image>().sprite = spriteItems[2];
    }
    void SelectLighter()
    {
        gm.isSaltSelected = false;
        gm.isAmuletSelected = false;
        gm.isLighterSelected = true;

        btnItems[0].transform.Find("Image").GetComponent<Image>().sprite = spriteItems[0];
        btnItems[1].transform.Find("Image").GetComponent<Image>().sprite = spriteItems[1];
        btnItems[2].transform.Find("Image").GetComponent<Image>().sprite = spriteItemsSelected[2];
    }
}
