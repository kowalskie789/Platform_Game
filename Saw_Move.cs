using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw_Move : MonoBehaviour
{

    public Vector2[] pos;
    int targetIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        transform.position = Vector3.MoveTowards(transform.position, pos[targetIndex], 5*Time.deltaTime);
        if (new Vector2(transform.position.x,transform.position.y) == pos[targetIndex])
            targetIndex = (targetIndex + 1) % pos.Length;

    }

}
