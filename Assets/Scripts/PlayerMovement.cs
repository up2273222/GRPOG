using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   private Rigidbody _rb;
   public Camera playerCamera;
   
   public float moveSpeed = 1f;

   private Vector3 _movementDirection;
   
   public InputActionReference moveAction;

   private void Awake()
   {
      _rb = GetComponent<Rigidbody>();
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }

   private void Update()
   {
      _movementDirection = playerCamera.transform.forward.normalized * moveAction.action.ReadValue<Vector2>().y + playerCamera.transform.right.normalized * moveAction.action.ReadValue<Vector2>().x;
      
   }

   private void FixedUpdate()
   {
      _rb.AddForce((new Vector3(_movementDirection.x, 0 , _movementDirection.z) * moveSpeed), ForceMode.VelocityChange);
   }
   
   
   
   
}
