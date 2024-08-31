using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour
{
    Animator anim;

    Vector3 destPos;                    // 목적지
    Vector3 dir;                        // 목적지를 향하는 방향

    NavMeshAgent agent;

    bool isMove = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();

        agent.updateRotation = false;           // 내비게이션 에이전트 컴포넌트가 자동으로 회전값을 구하지 못하게
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 오른클릭을 하고 마우스가 UI위에 없으면
        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            SetDestination();
        }

        LookMoveDirection();
        
        anim.SetBool("walk", isMove);
    }

    // destPos설정
    void SetDestination()
    {
        // 마우스 클릭 위치 레이캐스트
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 레이캐스트 위치 destPos설정
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

    // 회전
    void LookMoveDirection()
    {
        if (isMove)
        {
            // 캐릭터의 속도가 0이면 (목적지에 도착하면) 이동 종료
            if (agent.velocity.magnitude == 0.0f)
            {
                isMove = false;
                return;
            }

            // dir을 설정하고 캐릭터가 dir방향으로 부드럽게 회전
            dir = agent.steeringTarget - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);

        }
    }
}