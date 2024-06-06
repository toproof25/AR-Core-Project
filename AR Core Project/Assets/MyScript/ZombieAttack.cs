using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttack : MonoBehaviour
{
    // ����޽�, �ִϸ����� ������
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;
    
    // ����� Ŭ�� ����
    public AudioClip idle;
    public AudioClip attack;

    // ��ƼŬ ���� ����
    public GameObject attackParticlePrefab; // ���� ��ƼŬ ������
    private ParticleSystem attackParticle; // �ν��Ͻ�ȭ�� ��ƼŬ �ý���


    // �� ������Ʈ ����
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = idle;
        audioSource.loop = true; // idle ����� �ݺ� ���
        audioSource.Play();
    }

    // �̵��� ���߰� ����, ���� ���, ��ƼŬ ��� ����
    public void Attack(Collider other)
    {
        // �̵� ���߰� ����
        navMeshAgent.isStopped = true; // NavMeshAgent ����
        animator.SetTrigger("attack");

        // ����
        audioSource.clip = attack;
        audioSource.loop = false; // ���� ����� �ݺ����� ����
        audioSource.Play();

        // ��ƼŬ
        Vector3 attackPosition = other.ClosestPoint(transform.position);
        GameObject particleInstance = Instantiate(attackParticlePrefab, attackPosition, Quaternion.identity);
        attackParticle = particleInstance.GetComponent<ParticleSystem>();
        attackParticle.Play();

    }

    // �ش� �Լ��� �ִϸ��̼� Ŭ���� ���� �� �̺�Ʈ�� �����
    public void OnAttackAnimationEnd()
    {
        StartCoroutine(ResumeMovementAfterAttack(0.5f)); // 0.5�� �ڿ� �̵� �� ���� �ʱ�ȭ
    }
    private IEnumerator ResumeMovementAfterAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        navMeshAgent.isStopped = false; 

        // �ٽ� ���� ����
        audioSource.clip = idle;
        audioSource.loop = true; 
        audioSource.Play();
    }

}
