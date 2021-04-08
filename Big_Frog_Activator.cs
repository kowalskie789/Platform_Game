using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Frog_Activator : MonoBehaviour
{
    public GameObject Boss_Frog;
    public new Camera camera;
   
    public GameObject camBossplace;

    private bool CameraTrigger = false;

    private bool IsDead = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !IsDead)
        {
            
            Boss_Frog.GetComponent<Frog>().enabled = true;
          
            Boss_Frog.GetComponent<GroundShaker>().enabled = true;
            camera.GetComponent<CameraFollow>().enabled = false;
           // Camera.transform.position = Vector3.Lerp(Camera.transform.position, camBossplace.transform.position, 1);
            CameraTrigger = true;
        }

    }
    private void Update()
    {
        if (!IsDead)
        {
            if (CameraTrigger)
            {
                camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 25, 1 * Time.deltaTime); // zooming out to battle boss
                camera.transform.position = Vector3.Lerp(camera.transform.position, camBossplace.transform.position, 1 * Time.deltaTime);
            }
            if (camera.transform.position == camBossplace.transform.position && CameraTrigger)
            {
                CameraTrigger = false;

            }
        }
        if(Boss_Frog==null)
        {
            IsDead = true;
          
            
        }
        if(IsDead)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 12, 1 * Time.deltaTime); // chcanging camera back to original position
            camera.GetComponent<CameraFollow>().enabled = true; //camera follow script
            Destroy(gameObject, 10);
        }
    }

}
