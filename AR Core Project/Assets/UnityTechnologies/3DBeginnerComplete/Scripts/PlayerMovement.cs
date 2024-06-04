using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
//using static UnityEditor.PlayerSettings;

public class PlayerMovement : MonoBehaviour
{
    public InputAction MoveAction;
    
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
        
        MoveAction.Enable();
    }

    public void move(Vector2 pos)
    {

        // 카메라의 전방 벡터와 우측 벡터를 가져옴
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // y 축의 영향력을 없애기 위해 y 값을 0으로 설정
        cameraForward.y = 0;
        cameraRight.y = 0;

        // 벡터를 정규화
        cameraForward.Normalize();
        cameraRight.Normalize();

        // 조이스틱 입력을 카메라 방향에 맞게 변환
        Vector3 desiredMoveDirection = (cameraForward * pos.y + cameraRight * pos.x).normalized;

        // 이동
        m_Movement = desiredMoveDirection;

        /*
        // 백터값 설정
        float horizontal = pos.x;
        float vertical = pos.y;

        // 백터3로 설정 후 정규화
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        */


        // x축, y축 이동 좌표가 0과 근사하면 False, 이동에 충분한 값이라면 True
        bool hasHorizontalInput = !Mathf.Approximately(pos.x, 0f);
        bool hasVerticalInput = !Mathf.Approximately(pos.y, 0f);

        // x든 y든 이동이 가능한 상태라면 True
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        // 키 입력이 활성화되면 애니메이션 재생
        m_Animator.SetBool("IsWalking", isWalking);

        // 위와 동일하게 소리 재생
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        // 위에서 정규화한 백터3를 기준으로 회전
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }


    /*
    void FixedUpdate ()
    {

        // 이동 시 백터2값을 읽어옴
        var pos = MoveAction.ReadValue<Vector2>();

        //Debug.Log("pos : " + pos);

        // 백터값 설정
        float horizontal = pos.x;
        float vertical = pos.y;
        
        // 백터3로 설정 후 정규화
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        // x축, y축 이동 좌표가 0과 근사하면 False, 이동에 충분한 값이라면 True
        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);

        // x든 y든 이동이 가능한 상태라면 True
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        
        // 키 입력이 활성화되면 애니메이션 재생
        m_Animator.SetBool ("IsWalking", isWalking);

        // 위와 동일하게 소리 재생
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        // 위에서 정규화한 백터3를 기준으로 회전
        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }
    */
    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
}