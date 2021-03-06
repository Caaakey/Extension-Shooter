using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  namespace ?
//  해당 코드를 그룹화 시킨다
//  1. 그룹화된 코드는 그룹에 속해있지 않는 클래스에서는 사용할 수 없다
//  2. 그룹도 자식이 있다
//  사용할 때는 대분류.중분류.소분류 .. 
namespace YourName.SurvivalShooter.Characters
{
    public class PlayerMovement : MonoBehaviour
    {
        public float Speed = 6f;
        private Vector3 m_Movement;
        private Animator m_Animator;
        private Rigidbody m_Rigidbody;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            //  Input.GetAxis -> Smoothing filtering value (스무딩 필더링이 적용된 값)
            //  GetAxisRaw 는 스무딩 필더링을 적용받지 않는 값을 준다(-1, 0, 1)
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            //  Movement
            //  normalized : 해당 벡터의 크기를 1로 만든다
            m_Movement.Set(h, 0, v);
            m_Movement = Speed * Time.deltaTime * m_Movement.normalized;

            m_Rigidbody.MovePosition(transform.position + m_Movement);

            //  Turn
            // Ray ? 레이저 뿅!
            //  직선으로된 레이저를 발사한다! (레이저의 정보)
            //  Camera.main.ScreenPointToRay(Vector3)
            //  우리가 보고있는 화면 기준의 좌표를 가지고 레이저의 정보를 구한다
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //  Raycast : 레이저 정보를 기반으로 레이저를 쏜다!
            //  레이저를 첫 번째로 맞은 데이터를 가져온다
            //  1. Z 축 기준(카메라 Z축 앞 방향 기준)으로 맨 처음 레이저를 맞은 녀석 데이터를 갖고온다
            //  2. Collider 가 있는 녀석들만 맞는다
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, GameManager.Get.FloorLayerMask))
            {
                //  RaycastHit 에서 레이저의 위치는 point 에서 가지고 올 수 있다
                Vector3 point = hit.point - transform.position;
                point.y = 0;

                m_Rigidbody.MoveRotation(Quaternion.LookRotation(point));
            }

            //  Animating
            bool isWalking = h != 0f || v != 0f;
            m_Animator.SetBool("IsWalking", isWalking);
        }

        public void Death()
        {
            m_Animator.SetBool("IsDead", true);
        }

    }
}
