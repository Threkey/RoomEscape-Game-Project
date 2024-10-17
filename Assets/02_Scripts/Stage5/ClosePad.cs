using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePad : MonoBehaviour
{
    public GameObject goPad;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OffPad);
    }

    void OffPad()
    {
        goPad.SetActive(false);
    }
}
