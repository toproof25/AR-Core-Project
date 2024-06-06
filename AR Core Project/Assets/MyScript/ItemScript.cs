using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    public GameObject player;
    public GameObject keySpawn;
    public GameEnding gameEnding;

    // 아이템이 회전하는 속도
    private float rotationSpeed = 100f;

    private void Start()
    {
        Respawn();
    }

    // 아이템 지속적으로 회전
    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    // 아이템에 플레이어가 트리거되면 탈출 조건 활성화
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            SendMessge.Instance.ShowMessage("Get Item!!!! ");
            gameEnding.GetItem();
            gameObject.SetActive(false);
        }
    }

    // 재시작시 리스폰됨
    public void Respawn()
    {
        int random_spawn = Random.Range(1, 5);
        Transform random_transform = keySpawn.transform.Find("spawn" + random_spawn);
        transform.position = random_transform.position;
        gameObject.SetActive(true);
    }
}
