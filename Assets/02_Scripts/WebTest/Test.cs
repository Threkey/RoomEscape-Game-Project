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
    public class User           // ����� �����͸� �����ϴ� Ŭ����
    {
        public string name;
    }
    public TextMeshProUGUI text;

    string currentUrl;
    string serverUrl = "https://8nlx1uzb6j.execute-api.ap-northeast-2.amazonaws.com/escape/escape_lv2_post";          // ���� API�� URL
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coSendData("as"));
    }

    // Update is called once per frame
    void Update()
    {
        currentUrl = Application.absoluteURL;

        text.text = "currentURL=" + currentUrl + "\n\nname = " + GetName();
    }

    public string GetName()
    {
        string name = "";

        if (!currentUrl.Contains("name="))
            return "";

        // �̸� ã��
        for (int i = currentUrl.IndexOf("name=") + 5; i < currentUrl.Length; i++)
        {
            name += currentUrl[i];
        }

        return name;
    }

    public IEnumerator coSendData(string name)
    {
        // JSON ������ ������ �غ�
        User user = new User { name = name };
        string jsonData = JsonUtility.ToJson(user);

        // UnityWebRequest�� ����Ͽ� POST ��û ����
        UnityWebRequest request = new UnityWebRequest(serverUrl, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // ��û ������ �� ���� ��ٸ���
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("������ ���� ����: " + request.downloadHandler.text);
        }
    }
}
