using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager2 : MonoBehaviour
{
    Managers gm;
    Dialog dl;

    static UIManager2 s_instance;
    public static UIManager2 Instance { get { Init(); return s_instance;} }

    public Transform transCanvas;

    GameObject goDialog;
    TextMeshProUGUI textDialog;
    Button btnDialog;
    Button btnOpenDialogRecord;
    GameObject goDialogRecord;
    GameObject goContent;
    GameObject goTextDialogRecord;
    Button btnCloseDialogRecord;
    GameObject goItemPopup;
    Button btnCloseItemPopup;
    TextMeshProUGUI textItemDescription;
    Image imgItem;
    TextMeshProUGUI textHintCode;

    AudioSource au;
    public AudioClip[] audioClips;      // 0: 버튼클릭, 1: 화면클릭

    void Awake()
    {
        Init();
        gm = Managers.Instance;
        dl = Dialog.Instance;
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null) // go가 없으면
            {
                go = new GameObject { name = "@Managers" }; // 코드상으로 오브젝트를 만들어 줌
                go.AddComponent<UIManager2>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<UIManager2>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        au = GetComponent<AudioSource>();

        goDialog = transCanvas.Find("Image_Dialog").gameObject;
        textDialog = goDialog.transform.Find("Text_Dialog").GetComponent<TextMeshProUGUI>();
        btnDialog = transCanvas.Find("Button_Dialog").GetComponent<Button>();
        btnOpenDialogRecord = transCanvas.Find("Button_OpenDialogRecord").GetComponent<Button>();
        goDialogRecord = transCanvas.Find("Image_DialogRecord").gameObject;
        goContent = goDialogRecord.transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
        goTextDialogRecord = goContent.transform.Find("Text_DialogRecord (0)").gameObject;
        btnCloseDialogRecord = goDialogRecord.transform.Find("Button_CloseDialogRecord").GetComponent<Button>();
        goItemPopup = transCanvas.Find("Panel_ItemPopup").gameObject;
        btnCloseItemPopup = goItemPopup.transform.Find("Button_CloseItemPopup").GetComponent<Button>();
        textItemDescription = goItemPopup.transform.Find("Text_ItemDescription").GetComponent<TextMeshProUGUI>();
        imgItem = goItemPopup.transform.Find("Image_Item").GetComponent<Image>();
        textHintCode = goItemPopup.transform.Find("Text_HintCode").GetComponent<TextMeshProUGUI>();

        btnDialog.onClick.AddListener(CloseDialog);
        btnOpenDialogRecord.onClick.AddListener(OpenDialogRecord);
        btnCloseDialogRecord.onClick.AddListener(CloseDialogRecord);
        btnCloseItemPopup.onClick.AddListener(CloseItemPopup);

        ShowDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
        }
    }

    void PlayClickSound()
    {
        au.clip = audioClips[1];
        au.volume = 0.3f;
        au.pitch = 2.0f;
        au.Play();
    }

    void PlayButtonSound()
    {
        au.clip = audioClips[0];
        au.volume = 1.0f;
        au.pitch = 1.0f;
        au.Play();
    }

    public void ShowDialog()
    {
        // 일반 Dialog
        textDialog.text = dl.SetNextDialog();

        goDialog.SetActive(true);
        btnDialog.gameObject.SetActive(true);

        // 다이얼로그 레코드에 추가
        if(goContent.transform.Find("Text_DialogRecord"+dl.GetCurrentDialogIndex()) == null)
        {
            GameObject goTemp = new GameObject("Text_DialogRecord");
            goTemp.transform.SetParent(goContent.transform);
            goTemp.AddComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 230.0f);
            goTemp.AddComponent<TextMeshProUGUI>().fontSize = 40.0f;
            goTemp.GetComponent<TextMeshProUGUI>().text = textDialog.text;
            goTemp.name += dl.GetCurrentDialogIndex();
        }
    }

    public void ShowDialog(int index)
    {
        // 일반 Dialog
        textDialog.text = dl.SetIndexedDialog(index);

        goDialog.SetActive(true);
        btnDialog.gameObject.SetActive(true);
    }

    public void ShowDiscreteDialog(int index)
    {
        // DicreteDialog
        textDialog.text = dl.SetDiscreteDialog(index);

        goDialog.SetActive(true);
        btnDialog.gameObject.SetActive(true);

        // 처음 나왔으면 다이얼로그 레코드에 추가
        if (dl.discreteDialogArr[index].isShowed == false)
        {
            dl.discreteDialogArr[index].isShowed = true;
            GameObject goTemp = new GameObject("Text_DiscreteDialogRecord");
            goTemp.transform.SetParent(goContent.transform);
            goTemp.AddComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 230.0f);
            goTemp.AddComponent<TextMeshProUGUI>().fontSize = 40.0f;
            goTemp.GetComponent<TextMeshProUGUI>().text = textDialog.text;
            goTemp.name += index;
        }
    }

    public void CloseDialog()
    {
        // 하단 다이얼로그 닫기
        goDialog.SetActive(false);
        btnDialog.gameObject.SetActive(false);

        // 연속 대화
        if (SceneManager.GetActiveScene().name == "Stage3")
            if (dl.GetCurrentDialogIndex() >= 2 && dl.GetCurrentDialogIndex() < 8)
                ShowDialog();
    }

    public void OpenDialogRecord()
    {
        goDialogRecord.SetActive(true);
        btnOpenDialogRecord.gameObject.SetActive(false);
    }

    public void CloseDialogRecord()
    {
        goDialogRecord.SetActive(false);
        btnOpenDialogRecord.gameObject.SetActive(true);
    }

    public void CloseItemPopup()
    {
        goItemPopup.SetActive(false);
    }

    public void OpenItemPopup()
    {
        goItemPopup.SetActive(true);
    }

    public void ChangeItemDescription(string description)
    {
        textItemDescription.text = description;
    }

    public void ChangeHintCode(string code)
    {
            textHintCode.text = "HintCode: " + code;
    }

    public void ChangeHintCode()
    {
        textHintCode.text = "";
    }

    public void ChangeItemImage(Sprite sprite)
    {
        goItemPopup.transform.Find("Image_Item").GetComponent<Image>().sprite = sprite;
        goItemPopup.transform.Find("Image_Item").GetComponent<Image>().preserveAspect = true;

    }

    public Sprite GetCurrentItemImage()
    {

        return goItemPopup.transform.Find("Image_Item").GetComponent<Image>().sprite;

    }

    void StageClear()
    {
        // 스테이지 클리어시 해야할 것들 넣기
        Debug.Log("스테이지 클리어");
        //textUnlockMessage.gameObject.SetActive(true);
        StartCoroutine(gm.coSendData(gm.GetName(), gm.lv2Url));
    }
}