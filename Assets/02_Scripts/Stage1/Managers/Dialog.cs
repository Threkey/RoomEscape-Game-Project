using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    Managers gm;

    static Dialog s_instance;
    public static Dialog Instance { get { Init(); return s_instance; } }

    [Serializable]
    public struct dialog                // 대사 정보
    {
        public string name;
        [TextArea(3, 5)]
        public string text;
    }

    [SerializeField]
    public dialog[] dialogArr;          // 대사 배열

    int currentDialogIndex = -1;        // 현재 대사 순서

    void Awake()
    {
        Init();
        InitSetup();
    }

    void Start()
    {
        gm = Managers.Instance;
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null) // go가 없으면
            {
                go = new GameObject { name = "@Manager" }; // 코드상으로 오브젝트를 만들어 줌
                go.AddComponent<Dialog>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Dialog>();
        }
    }

    void InitSetup()
    {
        currentDialogIndex = -1;
    }

    public string SetNextDialog()
    {
        // 다음 대사로 이름과 텍스트를 합쳐서 반환, 이름이 공백이면 웹에서 이름을 가져옴

        string tmp;

        currentDialogIndex++;

        if (currentDialogIndex >= dialogArr.Length)
        {
            Debug.Log("Error : Array Index Out of Bounds Exception");
            return "<color = red>Error : Array Index Out of Bounds Exception</color>";
        }


        if (dialogArr[currentDialogIndex].name == "")
        {
            dialogArr[currentDialogIndex].name = gm.GetName();
        }

        tmp = "<color=orange>" + dialogArr[currentDialogIndex].name + "</color>\n" + dialogArr[currentDialogIndex].text;
        return tmp;
    }
}
