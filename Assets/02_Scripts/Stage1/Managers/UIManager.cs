using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Managers gm;
    Dialog dl;

    static UIManager s_instance;

    public static UIManager Instance { get { Init(); return s_instance; } }

    Transform transCanvas;
    Transform transSubCanvas;
    Button btnOpenDialog;
    Button btnCloseDialog;
    //Button btnOpenItem;
    Button btnCloseItem;
    Button btnClosePopup;
    Button btnLeft;
    Button btnRight;
    GameObject dialogRecord;
    GameObject item;
    GameObject itemPopup;
    TextMeshProUGUI textItemDescription;
    TextMeshProUGUI[] textDialogRecords = new TextMeshProUGUI[2];

    GameObject goDialog;
    Button btnDialog;
    TextMeshProUGUI textDialog;

    public Sprite spriteDiary;
    public Sprite spriteDiaryIcon;
    public Sprite[] spritesCalendar = new Sprite[12];

    Button btnBack;
    [HideInInspector]
    public TextMeshProUGUI textMessage;
    [HideInInspector]
    public Button btnDoorOpen;

    Camera subCamera;

    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        dl = Dialog.Instance;

        // Camera
        subCamera = GameObject.Find("SubCamera").GetComponent<Camera>();

        // Canvas
        transCanvas = GameObject.Find("Canvas").GetComponent<Transform>();
        transSubCanvas = GameObject.Find("SubCanvas").GetComponent<Transform>();
        transSubCanvas.gameObject.SetActive(false);

        // Objects
        dialogRecord = transCanvas.Find("ImageDialog").gameObject;
        item = transCanvas.Find("ParentItem").gameObject;
        itemPopup = transCanvas.Find("PanelItemPopup").gameObject;

        btnOpenDialog = GameObject.Find("ButtonOpenDialog").GetComponent<Button>();
        //btnOpenItem = GameObject.Find("ButtonOpenItem").GetComponent<Button>();
        btnCloseDialog = dialogRecord.GetComponent<Transform>().Find("ButtonCloseDialog").GetComponent<Button>();
        btnCloseItem = item.GetComponent<Transform>().Find("ButtonCloseItem").GetComponent<Button>();
        btnClosePopup = itemPopup.GetComponent<Transform>().Find("ButtonClosePopup").GetComponent<Button>();
        btnLeft = itemPopup.transform.Find("Button_Left").GetComponent<Button>();
        btnRight = itemPopup.transform.Find("Button_Right").GetComponent<Button>();
        goDialog = transCanvas.Find("Image_Dialog").gameObject;
        btnDialog = transCanvas.Find("Button_Dialog").GetComponent<Button>();
        textDialog = goDialog.transform.Find("Text_Dialog").GetComponent<TextMeshProUGUI>();
        textItemDescription = itemPopup.transform.Find("TextItemDescription").GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < textDialogRecords.Length; i++)
            textDialogRecords[i] = dialogRecord.transform.Find("Scroll View").Find("Viewport").Find("Content").Find("Text_DialogRecord (" + i + ")").GetComponent<TextMeshProUGUI>();

        btnBack = transSubCanvas.Find("Button_Back").GetComponent<Button>();
        textMessage = transSubCanvas.Find("Text_Message").GetComponent<TextMeshProUGUI>();
        btnDoorOpen = transSubCanvas.Find("Button_DoorOpen").GetComponent<Button>();
        
        // Button Events
        btnOpenDialog.onClick.AddListener(OpenDialogRecord);
        btnCloseDialog.onClick.AddListener(CloseDialogRecord);
        //btnOpenItem.onClick.AddListener(OpenItem);
        //btnCloseItem.onClick.AddListener(CloseItem);
        btnClosePopup.onClick.AddListener(ClosePopup);
        btnBack.onClick.AddListener(SubCameraOff);
        btnLeft.onClick.AddListener(CalendarLeft);
        btnRight.onClick.AddListener(CalendarRight);
        btnDoorOpen.onClick.AddListener(StageClear);
        btnDialog.onClick.AddListener(CloseDialog);

        // 게임 시작하자마자 첫 대사 출력
        ShowDialog();
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

    void OpenDialogRecord()
    {
        dialogRecord.SetActive(true);
        btnOpenDialog.gameObject.SetActive(false);
    }

    void CloseDialogRecord()
    {
        dialogRecord.SetActive(false);
        btnOpenDialog.gameObject.SetActive(true);
    }

    /*
    void OpenItem()
    {
        item.SetActive(true);
        btnOpenItem.gameObject.SetActive(false);
    }

    void CloseItem()
    {
        item.SetActive(false);
        btnOpenItem.gameObject.SetActive(true);
    }
    */


    public void OpenPopup(bool isCalendar)
    {
        if (isCalendar)
        {
            btnLeft.gameObject.SetActive(true);
            btnRight.gameObject.SetActive(true);
        }
        else
        {
            btnRight.gameObject.SetActive(false);
            btnLeft.gameObject.SetActive(false);
        }

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
        transSubCanvas.gameObject.SetActive(true);
    }

    public void SubCameraOff()
    {
        subCamera.depth = -1.0f;
        transCanvas.gameObject.SetActive(true);
        transSubCanvas.gameObject.SetActive(false);
    }

    public void ChangeItemImage(Sprite sprite)
    {
        itemPopup.transform.Find("Image").GetComponent<Image>().sprite = sprite;
        itemPopup.transform.Find("Image").GetComponent<Image>().preserveAspect = true;

        if (sprite == spritesCalendar[0])
        {
            btnLeft.interactable = false;
            btnRight.interactable = true;
        }
            
    }

    public Sprite GetCurrentItemImage()
    {

    return itemPopup.transform.Find("Image").GetComponent<Image>().sprite;

    }

    void CalendarLeft()
    {
        for(int i = 1; i < spritesCalendar.Length; i++)
        {
            if(GetCurrentItemImage() == spritesCalendar[i])
            {
                ChangeItemImage(spritesCalendar[i - 1]);
                break;
            }
                
        }

        if(GetCurrentItemImage() == spritesCalendar[0])
            btnLeft.interactable = false;
        if(!btnRight.IsInteractable())
            btnRight.interactable = true;
    }

    void CalendarRight()
    {
        for (int i = 0; i < spritesCalendar.Length - 1; i++)
        {
            if (GetCurrentItemImage() == spritesCalendar[i])
            {
                ChangeItemImage(spritesCalendar[i + 1]);
                break;
            }
                
        }

        if (GetCurrentItemImage() == spritesCalendar[spritesCalendar.Length - 1])
            btnRight.interactable = false;
        if (!btnLeft.IsInteractable())
            btnLeft.interactable = true;
    }

    void StageClear()
    {
        // 스테이지 클리어시 해야할 것들 넣기
        Debug.Log("스테이지 클리어");
        StartCoroutine(gm.coSendData(gm.GetName(), gm.lv2Url));
    }

    public void ShowDialog()
    {
        textDialog.text = dl.SetNextDialog();

        goDialog.SetActive(true);
        btnDialog.gameObject.SetActive(true);

        // 다이얼로그 기록에 추가
        for(int i = 0; i < textDialogRecords.Length; i++)
        {
            if (!textDialogRecords[i].gameObject.activeSelf)
            {
                textDialogRecords[i].gameObject.SetActive(true);
                textDialogRecords[i].text = textDialog.text;
                break;
            }
        }
    }

    void CloseDialog()
    {
        goDialog.SetActive(false);
        btnDialog.gameObject.SetActive(false);
    }

    public void ChangeItemDescription(string description)
    {
        textItemDescription.text = description;
    }
}
