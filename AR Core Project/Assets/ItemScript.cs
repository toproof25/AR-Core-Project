using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    public GameObject player;
    public GameObject keySpawn;

    public GameEnding gameEnding;


    private void Start()
    {
        Respawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            SendMessge.Instance.ShowMessage("Get Item!!!! ");
            gameEnding.GetItem();
            gameObject.SetActive(false);
        }
    }

    public void Respawn()
    {
        int random_spawn = Random.Range(1, 5);
        Transform random_transform = keySpawn.transform.Find("spawn" + random_spawn);
        transform.position = random_transform.position;
        gameObject.SetActive(true);
    }
}
