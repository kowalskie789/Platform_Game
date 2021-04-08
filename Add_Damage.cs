using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_Damage : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovenent>().TakeDomage(damage);
        }
    }
}