using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    // this is for deletion 
    // i copied importatnt stuff to Frog script :P

    public GameObject Player;
    public float life;
    private float currentlife;
    public Image lifebar;
    private Animator anim;
    private bool died = false;
    public GameObject loot_prefab;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        currentlife = life;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlife != life && !died)
        {
            lifebar.enabled = true;
        }
        else
        {
            lifebar.enabled = false;
        }
        if (lifebar.fillAmount > (currentlife / life))
        {
            lifebar.fillAmount = lifebar.fillAmount - ((currentlife / life) / 100);
        }

        if (currentlife <= 0 && !died)
        {
            died = true;
            lifebar.enabled = false;
            // tutja dam taki szajs do zmiany póżniej:P
            GetComponent<Frog>().enabled = false;

            anim.SetBool("Jump", false);
            anim.SetBool("Attack", false);
            anim.SetBool("Die", true);
            CreateLoot();
            Destroy(gameObject,5);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            Destroy(collision.gameObject);
            currentlife = currentlife - Player.GetComponent<Player_Stats>().Player_Arrow_Damage;
        }
        if(collision.tag == "Arrow2")
        {
            Destroy(collision.gameObject);
            currentlife = currentlife - Player.GetComponent<Player_Stats>().Player_Arrow_LVL2_Damage;
        }
    }
    void CreateLoot()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject loot = Instantiate(loot_prefab, transform.position, transform.rotation);
            loot.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2f, 2f)*450, 2000));
            StartCoroutine(Kelner());
        }
    }
    IEnumerator Kelner()
    {
        
        yield return new WaitForEndOfFrame();
    }
}
