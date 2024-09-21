#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ChangeTag : EditorWindow
{
    string stringValue;         // �Էµ� �±�

    // ���õ� ������Ʈ�� �ڽĿ�����Ʈ���� �ѹ��� �±׸� �ٲ�
    [MenuItem("CustomMenu/ChangeTagIncludeChild")]
    static void WindowOpen()
    {
        // â ����
        ChangeTag win = GetWindow<ChangeTag>();
        win.titleContent = new GUIContent("TagChanger");
        win.Show();
    }

    private void OnGUI()
    {
        // �����
        GUILayout.Space(10);

        // stringValue�� ���� tag �Է� ĭ
        stringValue = EditorGUILayout.TextField("Tag", stringValue);

        EditorGUILayout.Space(10);

        // ��ư�����ϰ� ��ư�� ������
        if (GUILayout.Button("����"))
        {
            //���//

            var selection = Selection.objects;          // ���� ���õ� ������Ʈ

            foreach (var item in selection)
            {
                GameObject go = item as GameObject;     // item�� GameObject�� ĳ����
                try
                {
                    go.tag = stringValue;
                    // �ڽ� ������Ʈ���� �±� ����
                    if(go.transform.childCount > 0)
                    {
                        for (int i = 0; go.transform.GetChild(i) != null; i++)
                        {
                            go.transform.GetChild(i).gameObject.tag = stringValue;
                        }
                    }

                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }

        EditorGUILayout.Space(20);

        // �ؽ�Ʈ
        EditorGUILayout.LabelField("���� ���õ� ������Ʈ�� �ڽ� ������Ʈ���� �Էµ� \n�±׷� ����.\n���� �����Ǿ� �ִ� �±׸� �����մϴ�.",GUILayout.Height(50));

        EditorGUILayout.Space(20);

        EditorGUILayout.LabelField("You can change the tags of currently selected\nobjects, including the child objects.\nOnly tags that currently exist are allowed.", GUILayout.Height(50));
    }
}
#endif