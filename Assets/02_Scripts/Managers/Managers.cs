using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Managers : MonoBehaviour
{
    static Managers s_instance;  //유일성이 보장된다.
    // 유일한 매니저를 갖고온다. // 프로퍼티 // 읽기 전용
    public static Managers Instance { get { Init(); return s_instance; } }

    UIManager _ui = new UIManager();

    public static UIManager UI { get { return Instance._ui; } }

    public GameObject goMainCharacter;

    // 1Level
    public bool isGetHint { get; set; } = false;
    public bool isGetOfficeKnife { get; set; } = false;
    public bool isBookshelfMoved { get; set; } = false;
    public bool isUnlocked {  get; set; } = false;

    // 2Level


    // Web
    string currentUrl;              //현재 페이지의 URL
    [HideInInspector]
    public string lv2Url = "https://8nlx1uzb6j.execute-api.ap-northeast-2.amazonaws.com/escape/escape_lv2_post";
    [HideInInspector]
    public string diary1Url = "https://8nlx1uzb6j.execute-api.ap-northeast-2.amazonaws.com/escape/escape_diary1_post";

    [System.Serializable]
    public class User           // 사용자 데이터를 저장하는 클래스
    {
        public string name;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        currentUrl = Application.absoluteURL;
    }

    private void Update()
    {
        currentUrl = Application.absoluteURL;
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null) // go가 없으면
            {
                go = new GameObject { name = "@Managers" }; // 코드상으로 오브젝트를 만들어 줌
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }

    public IEnumerator coSendData(string name, string apiUrl)
    {
        // JSON 형식의 데이터 준비
        User user = new User { name = name };
        string jsonData = JsonUtility.ToJson(user);

        // UnityWebRequest를 사용하여 POST 요청 생성
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
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


    public string GetName()
    {
#if UNITY_EDITOR
        return "Character Name";
#endif
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

    public bool isCharacterNearby(GameObject go)
    {
        if(Vector3.Distance(goMainCharacter.transform.position, go.transform.position) <= 4.5f)
            return true;
        else
            return false;
    }
    public bool isCharacterNearby(GameObject go, float distance)
    {
        if (Vector3.Distance(goMainCharacter.transform.position, go.transform.position) <= distance)
            return true;
        else
            return false;
    }
}