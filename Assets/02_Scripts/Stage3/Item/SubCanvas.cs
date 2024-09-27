using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubCanvas : MonoBehaviour
{
    Managers gm;

    public string[] answers = new string[4];                        // 문제 정답
    public TMP_InputField[] inputFields = new TMP_InputField[4];    // 답 입력칸
    public Button btnBack;
    public Button btnUnlock;
    public Camera subCamera;
    public GameObject mainCanvas;
    public GameObject subCanvasScreenSpace;
    public GameObject goMagicCircleBoundary;                        // 마법진 테두리
    public GameObject goUnlockMessage;

    Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;

        btnBack.onClick.AddListener(SubCameraOff);
        btnUnlock.onClick.AddListener(CheckAnswer);

        initPos = btnUnlock.transform.GetComponent<RectTransform>().anchoredPosition3D;
    }

    void Update()
    {
        goMagicCircleBoundary.transform.Rotate(0f, 0f, 0.01f);
        if (inputFields[0].text != "" && inputFields[1].text != "")
            if (inputFields[2].text != "" && inputFields[3].text != "")
                btnUnlock.interactable = true;
            else
                btnUnlock.interactable = false;
    }

    void CheckAnswer()
    {
        // 4개의 답이 맞는지 체크
        int stack = 0;

        for(int i = 0;  i < answers.Length; i++)
        {
            if (inputFields[i].text == answers[i])
                stack++;
        }

        // 답이 맞으면 클리어, 아니면 버튼이 흔들림
        if (stack == 4)
        {
            StartCoroutine(gm.coSendData(gm.GetName(), gm.lv4Url));
            goUnlockMessage.SetActive(true);
        }
        else
        {
            btnUnlock.transform.GetComponent<RectTransform>().anchoredPosition3D = initPos;
            StartCoroutine(coButtonShaking());
        }
    }

    void SubCameraOff()
    {
        subCamera.depth = -1;
        mainCanvas.SetActive(true);
        subCanvasScreenSpace.SetActive(false);
    }

    IEnumerator coButtonShaking()
    {
        float direct = 1.0f;
        float speed = 0.0005f;
        int count = 0;

        while (count < 6)
        {
            btnUnlock.transform.GetComponent<RectTransform>().Translate(Vector3.right * direct * speed);
            if (Vector3.Distance(initPos, btnUnlock.transform.GetComponent<RectTransform>().anchoredPosition3D) > 0.01f)
            {
                direct *= -1;
                count++;
            }
            yield return null;
        }
        btnUnlock.transform.GetComponent<RectTransform>().anchoredPosition3D = initPos;
    }
}