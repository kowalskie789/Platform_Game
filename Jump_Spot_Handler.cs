using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Spot_Handler : MonoBehaviour
{
    public float force;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity =new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, (force*Time.deltaTime));

        }
    }
}
