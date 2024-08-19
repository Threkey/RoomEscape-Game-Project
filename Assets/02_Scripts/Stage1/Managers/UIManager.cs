using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager s_instance;

    public static UIManager Instance { get { Init(); return s_instance; } }

    Transform transCanvas;
    Transform transSubCanvas;
    Button btnOpenDialog;
    Button btnCloseDialog;
    Button btnOpenItem;
    Button btnCloseItem;
    Button btnClosePopup;
    GameObject dialog;
    GameObject item;
    GameObject itemPopup;

    Button btnBack;
    public TextMeshProUGUI textMessage;

    Camera subCamera;

    // Start is called before the first frame update
    void Start()
    {
        subCamera = GameObject.Find("SubCamera").GetComponent<Camera>();

        transCanvas = GameObject.Find("Canvas").GetComponent<Transform>();
        transSubCanvas = GameObject.Find("SubCanvas").GetComponent<Transform>();

        dialog = transCanvas.Find("ImageDialog").gameObject;
        item = transCanvas.Find("ParentItem").gameObject;
        itemPopup = transCanvas.Find("PanelItemPopup").gameObject;

        btnOpenDialog = GameObject.Find("ButtonOpenDialog").GetComponent<Button>();
        btnOpenItem = GameObject.Find("ButtonOpenItem").GetComponent<Button>();
        btnCloseDialog = dialog.GetComponent<Transform>().Find("ButtonCloseDialog").GetComponent<Button>();
        btnCloseItem = item.GetComponent<Transform>().Find("ButtonCloseItem").GetComponent<Button>();
        btnClosePopup = itemPopup.GetComponent<Transform>().Find("ButtonClosePopup").GetComponent<Button>();

        btnBack = transSubCanvas.Find("Button_Back").GetComponent<Button>();
        textMessage = transSubCanvas.Find("Text_Message").GetComponent<TextMeshProUGUI>();
        

        btnOpenDialog.onClick.AddListener(OpenDialog);
        btnOpenItem.onClick.AddListener(OpenItem);
        btnCloseDialog.onClick.AddListener(CloseDialog);
        btnCloseItem.onClick.AddListener(CloseItem);
        btnClosePopup.onClick.AddListener(ClosePopup);
        btnBack.onClick.AddListener(SubCameraOff);

    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@UI");
            if (go == null) // go가 없으면
            {
                go = new GameObject { name = "@UI" }; // 코드상으로 오브젝트를 만들어 줌
                go.AddComponent<UIManager>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<UIManager>();
        }
    }

    void OpenDialog()
    {
        dialog.SetActive(true);
        btnOpenDialog.gameObject.SetActive(false);
    }
    void OpenItem()
    {
        item.SetActive(true);
        btnOpenItem.gameObject.SetActive(false);
    }

    void CloseDialog()
    {
        dialog.SetActive(false);
        btnOpenDialog.gameObject.SetActive(true);
    }
    void CloseItem()
    {
        item.SetActive(false);
        btnOpenItem.gameObject.SetActive(true);
    }

    public void OpenPopup()
    {
        itemPopup.SetActive(true);
    }

    void ClosePopup()
    {
        itemPopup.SetActive(false);
    }

    public void SubCameraOn()
    {
        subCamera.depth = 1.0f;
        transCanvas.gameObject.SetActive(false);
    }

    public void SubCameraOff()
    {
        subCamera.depth = -1.0f;
        transCanvas.gameObject.SetActive(true);
    }
}
