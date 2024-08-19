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
        // 웹페이지에서의 키보드 입력을 가져오지 못하게
        WebGLInput.captureAllKeyboardInput = false;

#endif

        // 테스트 //

        string url = Application.absoluteURL;

        test.text = url;
    }
}

