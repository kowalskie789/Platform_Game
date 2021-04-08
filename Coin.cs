using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject Player; // player game obj contains player stats script
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            Player.GetComponent<Player_Stats>().Coins_Gathered+=1;
            Destroy(gameObject);
        }
    }
}
