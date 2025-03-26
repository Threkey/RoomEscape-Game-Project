using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    public GameObject goMainCharacter;
    public GameObject goMirrorRoom;
    public GameObject goMainRoom;
    public GameObject goDoor;

    UIManager2 ui;
    Managers gm;

    public float  xSize = 1.0f, zSize = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        ui = UIManager2.Instance;
        gm = Managers.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // MainCharacter가 범위 안에 들어오면 MirroRoom을 끄고 MainRoom을 키고 문을 닫음
        if(goMainCharacter.transform.position.x > (transform.position.x - 0.5f) - xSize && goMainCharacter.transform.position.x < (transform.position.x - 0.5f) + xSize)
        {
            if(goMainCharacter.transform.position.z > transform.position.z - zSize && goMainCharacter.transform.position.z < transform.position.z + zSize)
            {
                ui.ShowDialog();
                goDoor.transform.Rotate(0f, 90.0f, 0f);
                goMainRoom.SetActive(true);
                goMirrorRoom.SetActive(false);
                goDoor.GetComponent<AudioSource>().Play();
                gm.isDoorClosed = true;
            }
        }
    }
}
