using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject CameraTarget;
    public float CamSpeed = 2f;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,new Vector3(CameraTarget.transform.position.x,CameraTarget.transform.position.y,-10)  ,CamSpeed*Time.deltaTime); 
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(transform.localPosition.x+x,transform.localPosition.y+y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
