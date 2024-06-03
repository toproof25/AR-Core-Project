using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        //Debug.Log("pos : " + pos);

        // 백터값 설정
        float horizontal = pos.x;
        float vertical = pos.y;

        // 백터3로 설정 후 정규화
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        // x축, y축 이동 좌표가 0과 근사하면 False, 이동에 충분한 값이라면 True
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

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