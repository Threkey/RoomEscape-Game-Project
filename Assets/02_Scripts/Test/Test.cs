using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Managers;
using UnityEngine.Networking;
using TMPro;
using static System.Net.WebRequestMethods;

public class Test : MonoBehaviour
{
    [System.Serializable]
    public class User           // 사용자 데이터를 저장하는 클래스
    {
        public string name;
    }
    public TextMeshProUGUI text;

    string currentUrl = "https://hyjung95.cafe24.com/escape/2560/Stage0/index.html?name=a%202%20%ED%98%B8&screenWidth=2560&token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJuYW1lIjoiYSAyIFx1ZDYzOCIsImV4cCI6MTcyNTcyNjg5MH0.if7EJmUqmkQyNTTlRqLTmMi5P_z6FcU7ZgLk4OWwP3M";
    string serverUrl = "https://8nlx1uzb6j.execute-api.ap-northeast-2.amazonaws.com/escape/escape_lv2_post";          // 서버 API의 URL
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coSendData("as"));
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_WEBGL
        //currentUrl = Application.absoluteURL;
#endif

        text.text = "currentURL=" + currentUrl + "\n\nname = " + GetName();
    }

    public string GetName()
    {
        string name = "";

        if (!currentUrl.Contains("name="))
            return name;

        if (!currentUrl.Contains("&token="))
            return name;

        int indexOfNameEnds = currentUrl.IndexOf("&token=");

        // 이름 찾기
        for (int i = currentUrl.IndexOf("name=") + 5; i < currentUrl.Length && i < indexOfNameEnds; i++)
        {
            // 한글이랑 띄어쓰기 변환
            if (currentUrl[i] == '%')
            {
                string tmp = "";
                for (; currentUrl[i] == '%';)
                {
                    tmp += currentUrl[i++];
                    tmp += currentUrl[i++];
                    tmp += currentUrl[i++];
                }
                i--;

                name += UnityWebRequest.UnEscapeURL(tmp);
                continue;
            }
            name += currentUrl[i];
        }
        return name;
    }

        public IEnumerator coSendData(string name)
    {
        // JSON 형식의 데이터 준비
        User user = new User { name = name };
        string jsonData = JsonUtility.ToJson(user);

        // UnityWebRequest를 사용하여 POST 요청 생성
        UnityWebRequest request = new UnityWebRequest(serverUrl, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 요청 보내기 및 응답 기다리기
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("데이터 전송 성공: " + request.downloadHandler.text);
        }
    }
}
