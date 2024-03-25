using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class PlayerSpotlight : MonoBehaviour
{
    public Transform target;
    public GameObject spotlightFork;
    public float widgeAngle;
    public float narrowAngle;
    public float narrowingSpeed;

    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
            float distance = Vector3.Distance(Vector3.ProjectOnPlane(transform.position, Vector3.up), Vector3.ProjectOnPlane(target.transform.position, Vector3.up));
            // light.intensity = Mathf.Clamp(distance / 5f, 1f, 1.25f);
            light.spotAngle = widgeAngle - Mathf.Clamp(distance * narrowingSpeed, 0f, narrowAngle);
            // Debug.Log(distance + " " + light.spotAngle);
        }
        if (spotlightFork != null)
        {
            Vector3 forkAngle = spotlightFork.transform.localEulerAngles;
            spotlightFork.transform.localEulerAngles = new Vector3(forkAngle.x, transform.localEulerAngles.y, forkAngle.z);
        }

    }

}
