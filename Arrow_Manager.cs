using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Manager : MonoBehaviour
{
    private GameObject Player;
    public float speed = 2000;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.transform.rotation.y > 0)
        {

            GetComponent<Rigidbody2D>().velocity = (Vector2.left * speed * Time.deltaTime);
          
        }
        else
        {

            GetComponent<Rigidbody2D>().velocity = (Vector2.right * speed * Time.deltaTime);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            // pierdolnij
            Destroy(gameObject);
        }
        else if(collision.tag == "Ground")
        {
           Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 10);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 7);
    }
}
