using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerSway : MonoBehaviour
{
    public float swaySpeed;
    public float spinSpeed;
    private float progress = 0f;

    void Update()
    {
        progress += Time.deltaTime * swaySpeed;
        if (progress > Mathf.PI * 2f) 
        {
            progress = 0f;
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + Time.deltaTime * spinSpeed, Mathf.Sin(progress));
    }
}
