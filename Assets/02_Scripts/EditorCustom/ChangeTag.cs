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
    string stringValue;         // 입력된 태그

    // 선택된 오브젝트의 자식오브젝트까지 한번에 태그를 바꿈
    [MenuItem("CustomMenu/ChangeTagIncludeChild")]
    static void WindowOpen()
    {
        // 창 띄우기
        ChangeTag win = GetWindow<ChangeTag>();
        win.titleContent = new GUIContent("TagChanger");
        win.Show();
    }

    private void OnGUI()
    {
        // 빈공간
        GUILayout.Space(10);

        // stringValue에 넣을 tag 입력 칸
        stringValue = EditorGUILayout.TextField("Tag", stringValue);

        EditorGUILayout.Space(10);

        // 버튼생성하고 버튼이 눌리면
        if (GUILayout.Button("적용"))
        {
            //기능//

            var selection = Selection.objects;          // 현재 선택된 오브젝트

            foreach (var item in selection)
            {
                GameObject go = item as GameObject;     // item을 GameObject로 캐스팅
                try
                {
                    go.tag = stringValue;
                    // 자식 오브젝트까지 태그 변경
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

        // 텍스트
        EditorGUILayout.LabelField("현재 선택된 오브젝트의 자식 오브젝트까지 입력된 \n태그로 변경.\n현재 생성되어 있는 태그만 가능합니다.",GUILayout.Height(50));

        EditorGUILayout.Space(20);

        EditorGUILayout.LabelField("You can change the tags of currently selected\nobjects, including the child objects.\nOnly tags that currently exist are allowed.", GUILayout.Height(50));
    }
}
#endif