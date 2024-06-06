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


    // 변경한 이동하는 코드
    // 이동하는데 카메라가 앞을 보는 기준으로 플레이어 캐릭터의 상하좌우를 결정하도록 수정함
    public void move(Vector2 pos)
    {
        // 카메라의 앞, 옆 벡터를 가져옴
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // y축은 제외
        cameraForward.y = 0;
        cameraRight.y = 0;

        // 카메라의 앞, 오른쪽 방향으로 정규화
        cameraForward.Normalize();
        cameraRight.Normalize();

        // 조이스틱 입력, 카메라 방향에 맞게 변환
        Vector3 desiredMoveDirection = (cameraForward * pos.y + cameraRight * pos.x).normalized;

        // 이동
        m_Movement = desiredMoveDirection;

        // 아래는 기존 코드 ( 이동중이면 -> 애니메이션 재생 -> 사운드 재생 )

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
    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}