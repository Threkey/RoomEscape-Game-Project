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

    [SerializeField]
    public dialog[] dialogArr;          // ��� �迭

    int currentDialogIndex = -1;        // ���� ��� ����

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
            if (go == null) // go�� ������
            {
                go = new GameObject { name = "@Manager" }; // �ڵ������ ������Ʈ�� ����� ��
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
        // ���� ���� �̸��� �ؽ�Ʈ�� ���ļ� ��ȯ, �̸��� �����̸� ������ �̸��� ������

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
