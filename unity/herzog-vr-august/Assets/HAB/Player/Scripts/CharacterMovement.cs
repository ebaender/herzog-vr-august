using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public float acceleration = 1f;
    public float maxVelocity = 2f;
    public SteamVR_Action_Vector2 movement;

    private Camera playerCamera;
    private CharacterController characterController;
    private Vector2 playerRotation;
    private Vector3 initalRotation;
    private Vector2 velocity;
    private bool freezeMovement = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        velocity = Vector2.zero;
        playerRotation = transform.localEulerAngles;
        initalRotation = playerRotation;
    }

    public void Update()
    {
        Vector2 moveDirection = new Vector2(movement.axis.x, movement.axis.y);
        if (!freezeMovement)
        {
            Move(moveDirection);
        }
    }

    private void Move(Vector2 direction)
    {
        for (int i = 0; i < 2; i++)
        {
            if (direction[i] != 0f)
            {
                velocity[i] += Mathf.Sign(direction[i]) * acceleration * Time.deltaTime;
            }
            else
            {
                float sign = Mathf.Sign(velocity[i]);
                velocity[i] -= sign * acceleration * Time.deltaTime;
                if (sign != Mathf.Sign(velocity[i]))
                {
                    velocity[i] = 0f;
                }
            }
            velocity[i] = Mathf.Clamp(velocity[i], -Mathf.Pow(direction[i], 2f) * maxVelocity, Mathf.Pow(direction[i], 2f) * maxVelocity);
        }
        Vector3 forward = Player.instance.headCollider.transform.TransformDirection(new Vector3(0f, 0f, 1f));
        Vector3 right = Player.instance.headCollider.transform.TransformDirection(new Vector3(1f, 0f, 0f));
        Vector3 characterDirection = Vector3.ProjectOnPlane(right, Vector3.up).normalized * velocity.x + Vector3.ProjectOnPlane(forward, Vector3.up).normalized * velocity.y;
        characterController.SimpleMove(characterDirection);
    }

    public void SetEulerAngles(Vector3 eulerAngles)
    {
        playerRotation = eulerAngles;
        transform.localEulerAngles = eulerAngles;
        playerCamera.transform.localEulerAngles = new Vector3(playerRotation.x, 0f, 0f);
    }

    public Vector2 GetPlayerRotation()
    {
        return playerRotation;
    }

}
