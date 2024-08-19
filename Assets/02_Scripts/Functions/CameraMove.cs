using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;       // ī�޶� ���� ��ü
    Vector3 initPos;                // �ʱ� ī�޶� ��ġ

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
