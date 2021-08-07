using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  namespace ?
//  �ش� �ڵ带 �׷�ȭ ��Ų��
//  1. �׷�ȭ�� �ڵ�� �׷쿡 �������� �ʴ� Ŭ���������� ����� �� ����
//  2. �׷쵵 �ڽ��� �ִ�
//  ����� ���� ��з�.�ߺз�.�Һз� .. 
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
            //  Input.GetAxis -> Smoothing filtering value (������ �ʴ����� ����� ��)
            //  GetAxisRaw �� ������ �ʴ����� ������� �ʴ� ���� �ش�(-1, 0, 1)
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            //  Movement
            //  normalized : �ش� ������ ũ�⸦ 1�� �����
            m_Movement.Set(h, 0, v);
            m_Movement = Speed * Time.deltaTime * m_Movement.normalized;

            m_Rigidbody.MovePosition(transform.position + m_Movement);

            //  Turn
            // Ray ? ������ ��!
            //  �������ε� �������� �߻��Ѵ�! (�������� ����)
            //  Camera.main.ScreenPointToRay(Vector3)
            //  �츮�� �����ִ� ȭ�� ������ ��ǥ�� ������ �������� ������ ���Ѵ�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //  Raycast : ������ ������ ������� �������� ���!
            //  �������� ù ��°�� ���� �����͸� �����´�
            //  1. Z �� ����(ī�޶� Z�� �� ���� ����)���� �� ó�� �������� ���� �༮ �����͸� ����´�
            //  2. Collider �� �ִ� �༮�鸸 �´´�
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, GameManager.FloorLayerMask))
            {
                //  RaycastHit ���� �������� ��ġ�� point ���� ������ �� �� �ִ�
                Vector3 point = hit.point - transform.position;
                point.y = 0;

                m_Rigidbody.MoveRotation(Quaternion.LookRotation(point));
            }

            //  Animating
            bool isWalking = h != 0f || v != 0f;
            m_Animator.SetBool("IsWalking", isWalking);
        }

    }
}
