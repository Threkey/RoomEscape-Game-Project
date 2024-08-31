using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour
{
    Animator anim;

    Vector3 destPos;                    // ������
    Vector3 dir;                        // �������� ���ϴ� ����

    NavMeshAgent agent;

    bool isMove = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();

        agent.updateRotation = false;           // ������̼� ������Ʈ ������Ʈ�� �ڵ����� ȸ������ ������ ���ϰ�
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ����Ŭ���� �ϰ� ���콺�� UI���� ������
        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            SetDestination();
        }

        LookMoveDirection();
        
        anim.SetBool("walk", isMove);
    }

    // destPos����
    void SetDestination()
    {
        // ���콺 Ŭ�� ��ġ ����ĳ��Ʈ
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ����ĳ��Ʈ ��ġ destPos����
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "Floor")
            {
                destPos = hit.point;
                isMove = true;

                agent.SetDestination(destPos);
            }
        }
    }

    // ȸ��
    void LookMoveDirection()
    {
        if (isMove)
        {
            // ĳ������ �ӵ��� 0�̸� (�������� �����ϸ�) �̵� ����
            if (agent.velocity.magnitude == 0.0f)
            {
                isMove = false;
                return;
            }

            // dir�� �����ϰ� ĳ���Ͱ� dir�������� �ε巴�� ȸ��
            dir = agent.steeringTarget - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);

        }
    }
}