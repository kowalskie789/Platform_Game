using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frog : MonoBehaviour
{
    public float jumptime = 2; // time between jumps    
    public float timeleft;
    public float jumpforce = 100; 
    private bool frogrotator = true;
    public GameObject Groundcheck;
    [HideInInspector]
    public Rigidbody2D frogrb;
    [HideInInspector]
    public Animator froganim;
    public float Playerdetectionradius; 

    public GameObject MisslePrefab;
    public float Spitspeed;
    [HideInInspector]
    public GameObject target;

    public float life;
    private float currentlife;
    public Image lifebar;
    private bool died = false;
    public GameObject loot_prefab;
    public int lootcount;

    // Start is called before the first frame update
    void Start()
    {
        frogrb = GetComponent<Rigidbody2D>();
        froganim = GetComponent<Animator>();
        timeleft = jumptime;
        currentlife = life;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Playerdetectionradius); // player detection (i need to chceck if its neded or just leftovers fom writing, i leave it for now)

        if (colliders != null)
        {

            target = GameObject.FindGameObjectWithTag("Player");

            if (timeleft <= 0 && (target.transform.position - transform.position).magnitude < Playerdetectionradius &&!died)
            {
                //Here add stuff to rotate frog towards player
                if ((target.transform.position - transform.position).x > 0 && frogrotator == true)
                {
                    transform.Rotate(0, -180, 0, Space.Self);
                    frogrotator = false;
                }
                else if ((target.transform.position - transform.position).x < 0 && frogrotator == false)
                {
                    transform.Rotate(0, -180, 0, Space.Self);
                    frogrotator = true;
                }

                froganim.SetBool("Attack", true);

                timeleft = jumptime;
            }





        }
        // randomjumping stuff

        if (timeleft <= 0 && (target.transform.position - transform.position).magnitude > Playerdetectionradius&&!died)
        {
            RandomJump();
        }

        if (IsGrounded())
        {
            froganim.SetBool("Jump", false);
            
        }
        else
        {
            froganim.SetBool("Jump", true);
            
        }
        //end of random jumping stuff

        //Life stuuf

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
            lifebar.fillAmount = lifebar.fillAmount - ((currentlife / life) / 50);
        }

        if (currentlife <= 0 && !died)
        {
            died = true;
            lifebar.enabled = false;
            // tutja dam taki szajs do zmiany póżniej:P
            GetComponent<Frog>().enabled = false;

           
            froganim.SetBool("Die", true);
            CreateLoot();
            Destroy(gameObject, 5);
        }




    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            Destroy(collision.gameObject);
            currentlife = currentlife - target.GetComponent<Player_Stats>().Player_Arrow_Damage; // target is player
        }
        if (collision.tag == "Arrow2")
        {
            Destroy(collision.gameObject);
            currentlife = currentlife - target.GetComponent<Player_Stats>().Player_Arrow_LVL2_Damage;
        }
    }
    void CreateLoot()
    {
        for (int i = 0; i < lootcount; i++)
        {
            GameObject loot = Instantiate(loot_prefab, transform.position, transform.rotation);
            loot.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2f, 2f) * 450,Random.Range(0,2f)*1500));
            StartCoroutine(Kelner()); // just helps to spread loot from eachother
        }
    }

    IEnumerator Kelner()
    {

        yield return new WaitForEndOfFrame();
    }

    private void RandomJump()
    {

        Vector2 cel = new Vector2(Random.Range(-1, 2), 2);
        if (cel.x != 0)
        {

            frogrb.AddForce(cel * jumpforce);

            if (cel.x > 0 && frogrotator == true)
            {
                transform.Rotate(0, -180, 0, Space.Self);
                frogrotator = false;
            }
            else if (cel.x < 0 && frogrotator == false)
            {
                transform.Rotate(0, -180, 0, Space.Self);
                frogrotator = true;
            }
        }

        timeleft = jumptime;
    }
    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(Groundcheck.transform.position, Vector2.down, 0.2f);
        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.CompareTag("Ground"))
            {

                return true;

            }
            else

                return false;
        }
        else
            return false;
    }
    //do innego skryptu
    public void FrogAttack()
    {
        GameObject missle = Instantiate(MisslePrefab, transform.position, transform.rotation);
        missle.GetComponent<Rigidbody2D>().velocity = (-(transform.position - target.transform.position).normalized*Spitspeed*Time.deltaTime);
        Destroy(missle, 7);
    }
}
