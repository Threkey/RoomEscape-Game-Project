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
    [Serializable]
    public struct DiscreteDialog
    {
        public string name;
        [TextArea(3, 5)]
        public string text;
        [HideInInspector]
        public bool isShowed;
    }
    
    [SerializeField]
    public dialog[] dialogArr;                  // 연속 대사 배열
    [SerializeField]
    public DiscreteDialog[] discreteDialogArr;  // 개별 대사 배열

    int currentDialogIndex = -1;                // 현재 대사 순서

    void Awake()
    {
        Init();
        InitSetup();
        gm = Managers.Instance;
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null) // go가 없으면
            {
                go = new GameObject { name = "@Managers" }; // 코드상으로 오브젝트를 만들어 줌
                go.AddComponent<Dialog>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Dialog>();
        }

    }

    void InitSetup()
    {
        currentDialogIndex = -1;
        for (int i = 0; i < discreteDialogArr.Length; i++)
            discreteDialogArr[i].isShowed = false;

    }

    public string SetNextDialog()
    {
        // 다음 대사로 이름과 텍스트를 합쳐서 반환, 이름이 공백이면 웹에서 이름을 가져옴

        string tmp;

        currentDialogIndex++;

        if (currentDialogIndex >= dialogArr.Length)
        {
            Debug.Log("Error : Array Index Out of Bounds Exception");
            return "<color=red>Error : Array Index Out of Bounds Exception</color>";
        }


        if (dialogArr[currentDialogIndex].name == "")
        {
            dialogArr[currentDialogIndex].name = gm.GetName();
        }

        tmp = "<color=orange>" + dialogArr[currentDialogIndex].name + "</color>\n" + dialogArr[currentDialogIndex].text;
        return tmp;
    }

    public string SetIndexedDialog(int index)
    {
        // 대화 대사를 인덱스로 찾음
        string tmp;

        currentDialogIndex = index;

        if (currentDialogIndex >= dialogArr.Length)
        {
            Debug.Log("Error : Array Index Out of Bounds Exception");
            return "<color=red>Error : Array Index Out of Bounds Exception</color>";
        }


        if (dialogArr[currentDialogIndex].name == "")
        {
            dialogArr[currentDialogIndex].name = gm.GetName();
        }

        tmp = "<color=orange>" + dialogArr[currentDialogIndex].name + "</color>\n" + dialogArr[currentDialogIndex].text;
        return tmp;
    }
    public string SetDiscreteDialog(int index)
    {
        // 이름과 텍스트를 합쳐서 반환, 이름이 공백이면 웹에서 이름을 가져옴

        string tmp;

        // index가 배열 범위 밖이면 예외처리
        if (discreteDialogArr.Length <= index)
        {
            Debug.Log("Error : Array Index Out of Bounds Exception");
            return "<color=red>Error : Array Index Out of Bounds Exception</color>";
        }


        if (discreteDialogArr[index].name == "")
        {
            discreteDialogArr[currentDialogIndex].name = gm.GetName();
        }

        tmp = "<color=orange>" + discreteDialogArr[index].name + "</color>\n" + discreteDialogArr[index].text;
        return tmp;
    }

    public int GetCurrentDialogIndex()
    {
        return currentDialogIndex;
    }
}
