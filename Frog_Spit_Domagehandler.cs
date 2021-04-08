using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Spit_Domagehandler : MonoBehaviour
{
    public float damage = 100;
    // Start is called before the first frame update
    void Update()
    {
        Destroy(gameObject, 10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
          collision.gameObject.GetComponent<PlayerMovenent>().TakeDomage(damage);
            Destroy(gameObject);
        }
       if(collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
