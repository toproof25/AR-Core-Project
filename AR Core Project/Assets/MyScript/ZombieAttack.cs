using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttack : MonoBehaviour
{
    // 내비메쉬, 애니메이터 가져옴
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;
    
    // 오디오 클립 설정
    public AudioClip idle;
    public AudioClip attack;

    // 파티클 변수 설정
    public GameObject attackParticlePrefab; // 공격 파티클 프리팹
    private ParticleSystem attackParticle; // 인스턴스화된 파티클 시스템


    // 각 컴포넌트 설정
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = idle;
        audioSource.loop = true; // idle 사운드는 반복 재생
        audioSource.Play();
    }

    // 이동을 멈추고 공격, 사운드 재생, 파티클 재생 실행
    public void Attack(Collider other)
    {
        // 이동 멈추고 공격
        navMeshAgent.isStopped = true; // NavMeshAgent 멈춤
        animator.SetTrigger("attack");

        // 사운드
        audioSource.clip = attack;
        audioSource.loop = false; // 공격 사운드는 반복하지 않음
        audioSource.Play();

        // 파티클
        Vector3 attackPosition = other.ClosestPoint(transform.position);
        GameObject particleInstance = Instantiate(attackParticlePrefab, attackPosition, Quaternion.identity);
        attackParticle = particleInstance.GetComponent<ParticleSystem>();
        attackParticle.Play();

    }

    // 해당 함수는 애니메이션 클립이 끝날 때 이벤트로 실행됨
    public void OnAttackAnimationEnd()
    {
        StartCoroutine(ResumeMovementAfterAttack(0.5f)); // 0.5초 뒤에 이동 및 사운드 초기화
    }
    private IEnumerator ResumeMovementAfterAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        navMeshAgent.isStopped = false; 

        // 다시 기존 사운드
        audioSource.clip = idle;
        audioSource.loop = true; 
        audioSource.Play();
    }

}
