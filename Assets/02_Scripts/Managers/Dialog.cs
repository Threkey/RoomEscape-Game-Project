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
    public struct dialog                // ��� ����
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
    public dialog[] dialogArr;                  // ���� ��� �迭
    [SerializeField]
    public DiscreteDialog[] discreteDialogArr;  // ���� ��� �迭

    int currentDialogIndex = -1;                // ���� ��� ����

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
            if (go == null) // go�� ������
            {
                go = new GameObject { name = "@Managers" }; // �ڵ������ ������Ʈ�� ����� ��
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
        // ���� ���� �̸��� �ؽ�Ʈ�� ���ļ� ��ȯ, �̸��� �����̸� ������ �̸��� ������

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
        // ��ȭ ��縦 �ε����� ã��
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
        // �̸��� �ؽ�Ʈ�� ���ļ� ��ȯ, �̸��� �����̸� ������ �̸��� ������

        string tmp;

        // index�� �迭 ���� ���̸� ����ó��
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
