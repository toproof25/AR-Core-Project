using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;

    // 추가 변수
    private ZombieAttack zombie;

    private void Start()
    {
        zombie = transform.parent.gameObject.GetComponent<ZombieAttack>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player)
        {
            // 좀비라면 플레이어와 닿을 시 공격을 수행
            if (zombie != null)
            {
                zombie.Attack(other);
            }
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update ()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast (ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer ();
                }
            }
        }
    }
}
