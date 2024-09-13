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
    GameObject dialogRecord;
    GameObject item;
    GameObject itemPopup;
    TextMeshProUGUI textItemDescription;
    TextMeshProUGUI textHintCode;
    public Button btnHintReveal;

    TextMeshProUGUI[] textDialogRecords;
    GameObject goDialog;
    Button btnDialog;
    TextMeshProUGUI textDialog;

    public Sprite spriteDiary;
    public Sprite spriteDiaryIcon;
    public Sprite spritePaintingA;
    public Sprite spritePaintingB;
    public Sprite spriteOfficeKnife;

    Button btnBack;
    [HideInInspector]
    public TextMeshProUGUI textMessage;
    [HideInInspector]
    public Button btnDoorOpen;
    TextMeshProUGUI textUnlockMessage;

    Camera subCamera;

    // 사운드
    AudioSource au;
    public AudioClip[] audioClips;      // 0: 버튼클릭, 1: 사진 들추기, 2: 숨겨진 문 작동

    void Awake()
    {
        gm = Managers.Instance;
        dl = Dialog.Instance;
        au = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Dialog
        textDialogRecords = new TextMeshProUGUI[dl.dialogArr.Length];

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
        goDialog = transCanvas.Find("Image_Dialog").gameObject;
        btnDialog = transCanvas.Find("Button_Dialog").GetComponent<Button>();
        textDialog = goDialog.transform.Find("Text_Dialog").GetComponent<TextMeshProUGUI>();
        textItemDescription = itemPopup.transform.Find("TextItemDescription").GetComponent<TextMeshProUGUI>();
        btnHintReveal = itemPopup.transform.Find("Image").Find("Button_RevealHint").GetComponent<Button>();
        textHintCode = itemPopup.transform.Find("Text_HintCode").GetComponent<TextMeshProUGUI>();


        for (int i = 0; i < textDialogRecords.Length; i++)
            textDialogRecords[i] = dialogRecord.transform.Find("Scroll View").Find("Viewport").Find("Content").Find("Text_DialogRecord (" + i + ")").GetComponent<TextMeshProUGUI>();

        btnBack = transSubCanvas.Find("Button_Back").GetComponent<Button>();
        textMessage = transSubCanvas.Find("Text_Message").GetComponent<TextMeshProUGUI>();
        btnDoorOpen = transSubCanvas.Find("Button_DoorOpen").GetComponent<Button>();
        textUnlockMessage = transSubCanvas.Find("Text_UnlockMessage").GetComponent<TextMeshProUGUI>();

        // Button Events
        btnOpenDialog.onClick.AddListener(OpenDialogRecord);
        btnCloseDialog.onClick.AddListener(CloseDialogRecord);
        //btnOpenItem.onClick.AddListener(OpenItem);
        //btnCloseItem.onClick.AddListener(CloseItem);
        btnClosePopup.onClick.AddListener(ClosePopup);
        btnBack.onClick.AddListener(SubCameraOff);
        btnDoorOpen.onClick.AddListener(StageClear);
        btnDialog.onClick.AddListener(CloseDialog);
        btnHintReveal.onClick.AddListener(ChangePaintingImage);

        // 게임 시작하자마자 첫 대사 출력
        ShowDialog();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            au.clip = audioClips[3];
            au.volume = 0.3f;
            au.pitch = 2.0f;
            au.Play();
        }
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
        CloseDialog();
        au.clip = audioClips[0];
        au.volume = 1.0f;
        au.pitch = 1.0f;
        au.Play();
        dialogRecord.SetActive(true);
        btnOpenDialog.gameObject.SetActive(false);
    }

    void CloseDialogRecord()
    {
        au.clip = audioClips[0];
        au.volume = 1.0f;
        au.pitch = 1.0f;
        au.Play();
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


    public void OpenPopup()
    {
        itemPopup.SetActive(true);
    }

    void ClosePopup()
    {
        au.clip = audioClips[0];
        au.volume = 1.0f;
        au.pitch = 1.0f;
        au.Play();
        itemPopup.SetActive(false);

        if(gm.isGetHint && dl.GetCurrentDialogIndex() == 1)
        {
            ShowDialog();
            au.clip = audioClips[2];
            au.volume = 1.0f;
            au.pitch = 1.0f;
            au.Play();
        }
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
            
    }

    public Sprite GetCurrentItemImage()
    {

    return itemPopup.transform.Find("Image").GetComponent<Image>().sprite;

    }

    public void ChangePaintingImage()
    {
        if(GetCurrentItemImage() == spritePaintingA)
        {
            ChangeItemImage(spritePaintingB);
            au.clip = audioClips[1];
            au.volume = 1.0f;
            au.pitch = 1.0f;
            au.Play();
            gm.isGetHint = true;
        }

        else if (GetCurrentItemImage() == spritePaintingB)
            ChangeItemImage(spritePaintingA);
    }

    void StageClear()
    {
        // 스테이지 클리어시 해야할 것들 넣기
        Debug.Log("스테이지 클리어");
        textUnlockMessage.gameObject.SetActive(true);
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

    public void CloseDialog()
    {
        if(dl.GetCurrentDialogIndex() == 0)
        {
            ShowDialog();
            return;
        }
        goDialog.SetActive(false);
        btnDialog.gameObject.SetActive(false);
    }

    public void ChangeItemDescription(string description)
    {
        textItemDescription.text = description;
    }

    public void ChangeHintCode(string code)
    {
        if (code == "")
            textHintCode.text = "";
        else
            textHintCode.text = "HintCode: " + code;
    }
}