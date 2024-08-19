using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;       // 카메라가 따라갈 물체
    Vector3 initPos;                // 초기 카메라 위치

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + initPos;
    }
}
