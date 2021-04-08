using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public GameObject arrow;
    public GameObject arrow_lvl_2;
    public Vector3 arrowoffset; //position for instatiate arrow
    public float arrowspeed;
    
    [HideInInspector]
    public Player_Stats Stats; //stats for all
    public Animator anim;

    public float timecounter;

    

    
    
   
    //Attack is called by animation event!!

    void Start()
    {
        Stats = gameObject.GetComponent<Player_Stats>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //animation for attack
       
        if(Input.GetKey(KeyCode.LeftControl))
        { 
            timecounter += Time.deltaTime;
            anim.SetBool("Attack", true);

        }
       
        
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetFloat("Speed", 1f);
            Fire();
          
        }
       
    }

    
    private void Fire()
    {
        if (timecounter < 1.5)
        {
            Instantiate(arrow, transform.position + arrowoffset, transform.rotation);
           
        }
        if (timecounter > 1.5)
        {
            Instantiate(arrow_lvl_2, transform.position + arrowoffset, transform.rotation);
            
        }
        anim.SetBool("Attack", false);
        timecounter = 0;

    }
    public void Attack()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            anim.SetFloat("Speed", 0f);
        
    }
    
    
}
