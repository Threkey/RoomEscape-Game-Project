using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Web : MonoBehaviour
{
    public TextMeshProUGUI test;

    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        // �������������� Ű���� �Է��� �������� ���ϰ�
        WebGLInput.captureAllKeyboardInput = false;

#endif

        // �׽�Ʈ //

        string url = Application.absoluteURL;

        test.text = url;
    }
}

