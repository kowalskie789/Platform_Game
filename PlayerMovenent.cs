using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovenent : MonoBehaviour
{

    private Rigidbody2D rb;

    private bool doublejump; //chcecking id double jump has been used
    public GameObject Groundcheck; //chcecking if player is on the ground
    public Animator anim;
    private bool rotatehelper = true; //bool to chceck wich direction player is faced(might to swich to simple rotarion.y>0 or smh)
    [HideInInspector]
    public Player_Stats Stats; //script with all stats 

    public Image lifebar;
    public float currentlife;

    public float HangTime = 0.1f;           // these  floats are for easing player life with jumping
    private float HangCounter;     

    public float jumpBufferLength = 0.1f;
    private float jumpBufferCounter;        // wery usefull :) 

    //camera stuff
    public Transform camTarget;
    public float aheadAmount;  // amount Camera can go ahead of player
    public float aheadSpeed; // speed of camera
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Stats = gameObject.GetComponent<Player_Stats>();
        currentlife = Stats.Player_life;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal") * Stats.Player_speed * Time.deltaTime, 0, 0);

        rb.transform.Translate(dir, Space.World);
        
        //animator running
        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        //jumping

        //manage Hangtime
        if(IsGrounded())
        {
            HangCounter = HangTime;
        }
        else
        {
            HangCounter -= Time.deltaTime;
        }
         //manage jump buffer
         if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferLength;
        }
         else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (HangCounter>0 && jumpBufferCounter>=0)
        {
            rb.velocity = (Vector2.up * Stats.Player_jump_force);
            anim.SetBool("IsJumping", true);
            jumpBufferCounter = 0;

            StartCoroutine("Waiter"); // this helps separate jump and doublejump
        }

        // this part makes little jumps when player lets go jump erlier
        if (Input.GetKeyUp(KeyCode.Space)&& rb.velocity.y>0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

        //doublejumping
        if (Input.GetKeyDown(KeyCode.Space) && doublejump == true && Stats.Player_doublejump == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity = (Vector2.up * Stats.Player_jump_force);
            doublejump = false;
        }

        //animation for jumping
        if (rb.velocity.y == 0)
        {
            anim.SetBool("IsJumping", false);
        }

        //Player rotation
        if (Input.GetAxis("Horizontal") < 0 && rotatehelper == true)
        {
            transform.Rotate(0, -180, 0, Space.Self);
            rotatehelper = false;
            camTarget.localPosition =new Vector3(-camTarget.localPosition.x,camTarget.localPosition.y,-10);
        }
        else if (Input.GetAxis("Horizontal") > 0 && rotatehelper == false)
        {

            transform.Rotate(0, -180, 0, Space.Self);
            rotatehelper = true;
            camTarget.localPosition = new Vector3(-camTarget.localPosition.x, camTarget.localPosition.y, -10);
        }
        //end player rotation

        //life stuff
        lifebar.fillAmount = (currentlife / Stats.Player_life);
        if (currentlife <= 0)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name); // this has to go later when i make chceckpoints
        }
        //end of life stuff

        // camera target stuff
            if (Input.GetAxisRaw("Horizontal") > 0 && rotatehelper == true)
            {
                camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), camTarget.localPosition.y, camTarget.localPosition.z);
            }

            if (Input.GetAxisRaw("Horizontal") < 0 && rotatehelper == false)
            {
                camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, -aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), camTarget.localPosition.y, camTarget.localPosition.z);
            }
        
        
    }
    IEnumerator Waiter()
    {

        yield return new WaitForSeconds(.1f);
        doublejump = true;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(Groundcheck.transform.position, Vector2.down, 0.1f);
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
    public void TakeDomage(float x)
    {
        currentlife -= x;
        StartCoroutine(Blink(2f));

    }
    
 
    IEnumerator Blink(float duration) // blinking when domaged
    {
        var endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
