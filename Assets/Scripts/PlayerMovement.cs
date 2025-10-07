using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   private Rigidbody _rb;
   public Camera playerCamera;
   
   public float moveSpeed = 1f;

   private Vector3 _movementDirection;
   private Vector3 _cameraForward;
   private Vector3 _cameraRight;
   
   
   
   public InputActionReference moveAction;

   private void Awake()
   {
      _rb = GetComponent<Rigidbody>();
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }

   private void Update()
   {
      CalculateMovementDirection();
      
   }

   private void FixedUpdate()
   {
      _rb.AddForce((new Vector3(_movementDirection.x, 0 , _movementDirection.z) * moveSpeed), ForceMode.VelocityChange);
   }


   private void CalculateMovementDirection()
   {
      _cameraForward = playerCamera.transform.forward;
      _cameraForward.y = 0;
      _cameraForward.Normalize();
      _cameraRight = playerCamera.transform.right;
      _cameraRight.y = 0;
      _cameraRight.Normalize();
      _movementDirection = _cameraForward * moveAction.action.ReadValue<Vector2>().y + _cameraRight * moveAction.action.ReadValue<Vector2>().x;
      _movementDirection.Normalize();
      
      Debug.Log(_rb.linearVelocity.magnitude);
   }
   
   
   
}
