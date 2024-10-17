using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPad : MonoBehaviour
{
    public GameObject goPad;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnPad);
    }

    void OnPad()
    {
        goPad.SetActive(true);
    }
}
