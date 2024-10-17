using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject goDiaryImage;
    public Button btnDiaryCover;

    public float speed = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        btnDiaryCover.onClick.AddListener(OpenBook);
    }

    void OpenBook()
    {
        // å Ŀ�� ������� å �̹��� ����� å �̹��� �߾����� �̵���Ŵ
        btnDiaryCover.gameObject.SetActive(false);
        goDiaryImage.SetActive(true);
        StartCoroutine(coMoveDiaryImage());
    }

    IEnumerator coMoveDiaryImage()
    {
        while (true)
        {
            goDiaryImage.GetComponent<RectTransform>().localPosition = new Vector3(Mathf.Lerp(goDiaryImage.GetComponent<RectTransform>().localPosition.x, 0f, speed * 0.01f), 0f, 0f);
            yield return null;
            if (goDiaryImage.GetComponent<RectTransform>().localPosition.x == 0f)
                break;
        }
    }
}