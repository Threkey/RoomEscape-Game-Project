// Script by Marcelli Michele

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveRuller : MonoBehaviour
{
    PadLockPassword _lockPassword;
    PadLockEmissionColor _pLockColor;
    AudioSource au;

    [HideInInspector]
    public List <GameObject> _rullers = new List<GameObject>();
    private int _scroolRuller = 0;
    private int _changeRuller = 0;
    [HideInInspector]
    public int[] _numberArray = {0,0,0,0};

    private int _numberRuller = 0;

    private bool _isActveEmission = false;

    public Transform transSubCanvas;
    public Button btnLeft;
    public Button btnRight;
    public Button btnUp;
    public Button btnDown;

    Vector3 upArrowPos;
    Vector3 downArrowPos;

    void Awake()
    {
        _lockPassword = FindObjectOfType<PadLockPassword>();
        _pLockColor = FindObjectOfType<PadLockEmissionColor>();

        _rullers.Add(GameObject.Find("Ruller1"));
        _rullers.Add(GameObject.Find("Ruller2"));
        _rullers.Add(GameObject.Find("Ruller3"));
        _rullers.Add(GameObject.Find("Ruller4"));

        foreach (GameObject r in _rullers)
        {
            r.transform.Rotate(-144, 0, 0, Space.Self);
        }
    }

    private void Start()
    {
        au = GetComponent<AudioSource>();

        btnLeft = transSubCanvas.Find("Button_Left").GetComponent<Button>();
        btnRight = transSubCanvas.Find("Button_Right").GetComponent<Button>();
        btnUp = transSubCanvas.Find("Button_Up").GetComponent<Button>();
        btnDown = transSubCanvas.Find("Button_Down").GetComponent<Button>();

        btnLeft.onClick.AddListener(MoveRullesLeft);
        btnRight.onClick.AddListener(MoveRullesRight);
        btnUp.onClick.AddListener(RotateRullersUp);
        btnDown.onClick.AddListener(RotateRullersDown);

        upArrowPos = btnUp.GetComponent<RectTransform>().anchoredPosition;
        downArrowPos = btnDown.GetComponent<RectTransform>().anchoredPosition;
    }
    void Update()
    {
        MoveRulles();
        RotateRullers();
        _lockPassword.Password();

        btnUp.GetComponent<RectTransform>().anchoredPosition = upArrowPos;
        btnDown.GetComponent<RectTransform>().anchoredPosition = downArrowPos;
    }

    void MoveRulles()
    {
        /*
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            _isActveEmission = true;
            _changeRuller ++;
            _numberRuller += 1;

            if (_numberRuller > 3)
            {
                _numberRuller = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            _isActveEmission = true;
            _changeRuller --;
            _numberRuller -= 1;

            if (_numberRuller < 0)
            {
                _numberRuller = 3;
            }
        }
        */
        _changeRuller = (_changeRuller + _rullers.Count) % _rullers.Count;


        for (int i = 0; i < _rullers.Count; i++)
        {
            if (_isActveEmission)
            {
                if (_changeRuller == i)
                {

                    _rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = true;
                    _rullers[i].GetComponent<PadLockEmissionColor>().BlinkingMaterial();
                }
                else
                {
                    _rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = false;
                    _rullers[i].GetComponent<PadLockEmissionColor>().BlinkingMaterial();
                }
            }
        }

    }

    public void MoveRullesRight()
    {
        _isActveEmission = true;
        _changeRuller++;
        _numberRuller += 1;

        downArrowPos.x += 180;
        upArrowPos.x += 180;

        if (_numberRuller > 3)
        {
            _numberRuller = 0;
            downArrowPos.x = -275f;
            upArrowPos.x = -275f;
        }
    }

    public void MoveRullesLeft()
    {
        _isActveEmission = true;
        _changeRuller--;
        _numberRuller -= 1;

        downArrowPos.x -= 180;
        upArrowPos.x -= 180;

        if (_numberRuller < 0)
        {
            _numberRuller = 3;
            downArrowPos.x = 265f;
            upArrowPos.x = 265f;
        }
    }

    void RotateRullers()
    {
        /*
        if (Input.GetKeyDown(KeyCode.W))
        {
            _isActveEmission = true;
            _scroolRuller = 36;
            _rullers[_changeRuller].transform.Rotate(-_scroolRuller, 0, 0, Space.Self);

            _numberArray[_changeRuller] += 1;

            if (_numberArray[_changeRuller] > 9)
            {
                _numberArray[_changeRuller] = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _isActveEmission = true;
            _scroolRuller = 36;
            _rullers[_changeRuller].transform.Rotate(_scroolRuller, 0, 0, Space.Self);

            _numberArray[_changeRuller] -= 1;

            if (_numberArray[_changeRuller] < 0)
            {
                _numberArray[_changeRuller] = 9;
            }
        }
        */
    }

    public void RotateRullersUp()
    {
        au.Play();
        _isActveEmission = true;
        _scroolRuller = 36;
        _rullers[_changeRuller].transform.Rotate(-_scroolRuller, 0, 0, Space.Self);

        _numberArray[_changeRuller] += 1;

        if (_numberArray[_changeRuller] > 9)
        {
            _numberArray[_changeRuller] = 0;
        }
    }

    public void RotateRullersDown()
    {
        au.Play();
        _isActveEmission = true;
        _scroolRuller = 36;
        _rullers[_changeRuller].transform.Rotate(_scroolRuller, 0, 0, Space.Self);

        _numberArray[_changeRuller] -= 1;

        if (_numberArray[_changeRuller] < 0)
        {
            _numberArray[_changeRuller] = 9;
        }
    }
}
