// Script by Marcelli Michele

using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PadLockPassword : MonoBehaviour
{
    MoveRuller _moveRull;
    Managers gm;
    UIManager ui;
    public GameObject btnOpenDrawer;
    

    public int[] _numberPassword = {0,0,0,0};
    public int[] _numberPassword2 = {0,0,0,0};

    private void Awake()
    {
        _moveRull = FindObjectOfType<MoveRuller>();
        gm = Managers.Instance;
        if(SceneManager.GetActiveScene().name == "Stage1")
            ui = UIManager.Instance;
    }

    public void Password()
    {
        // Stage1
        if (_moveRull._numberArray.SequenceEqual(_numberPassword) && SceneManager.GetActiveScene().name == "Stage1")
        {
            //비밀번호가 맞으면 잠금이 풀림
            gm.isUnlocked = true;

            ui.textMessage.gameObject.SetActive(true);
            ui.btnDoorOpen.gameObject.SetActive(true);

            _moveRull.btnDown.gameObject.SetActive(false);
            _moveRull.btnUp.gameObject.SetActive(false);
            _moveRull.btnLeft.gameObject.SetActive(false);
            _moveRull.btnRight.gameObject.SetActive(false);

            // Es. Below the for loop to disable Blinking Material after the correct password
            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = false;
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>().BlinkingMaterial();
            }

        }

        // Stage4
        if (_moveRull._numberArray.SequenceEqual(_numberPassword2) && SceneManager.GetActiveScene().name == "Stage4")
        {
            //비밀번호가 맞으면 잠금이 풀림
            btnOpenDrawer.gameObject.SetActive(true);

            _moveRull.btnDown.gameObject.SetActive(false);
            _moveRull.btnUp.gameObject.SetActive(false);
            _moveRull.btnLeft.gameObject.SetActive(false);
            _moveRull.btnRight.gameObject.SetActive(false);

            // Es. Below the for loop to disable Blinking Material after the correct password
            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = false;
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>().BlinkingMaterial();
            }

        }
    }
}
