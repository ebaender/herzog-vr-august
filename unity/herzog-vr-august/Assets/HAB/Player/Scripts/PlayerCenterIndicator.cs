using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCenterIndicator : MonoBehaviour
{
    public GameObject head;
    public float sensitivity;
    public float deadzone;
    public float brightness;

    private Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    void Update()
    {
        float distance = Vector3.Distance(Vector3.ProjectOnPlane(transform.position, Vector3.up), Vector3.ProjectOnPlane(head.transform.position, Vector3.up));
        float alpha =  Mathf.Clamp(distance * sensitivity - deadzone, 0f, 1f);
        material.SetVector("_TintColor", new Vector4(35f / 255f, 166f / 255f, 201f / 255f, alpha * brightness));
    }
}
