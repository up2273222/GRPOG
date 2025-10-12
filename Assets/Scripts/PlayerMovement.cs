using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   private Rigidbody _rb;
   public Camera playerCamera;

   public float moveSpeed;
   public float jumpForce;

   private Vector3 _movementDirection;
   private Vector3 _cameraForward;
   private Vector3 _cameraRight;
   
   [SerializeField] private float airControlModifier = 0.2f;
   
   
   
   public InputActionReference moveAction;
   public InputActionReference jumpAction;

   private void Awake()
   {
      _rb = GetComponent<Rigidbody>();
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }

   private void OnEnable()
   {
      jumpAction.action.started += Jump;
   }

   private void OnDisable()
   {
      jumpAction.action.started -= Jump;
   }

  

   private void Update()
   {
      CalculateMovementDirection();

      _rb.linearDamping = IsGrounded() ? 4.0f : 0.5f;
   }

   private void FixedUpdate()
   {
      if (IsGrounded())
      {
         _rb.AddForce((new Vector3(_movementDirection.x, 0 , _movementDirection.z) * moveSpeed), ForceMode.VelocityChange);
      }
      else
      {
         _rb.AddForce(((new Vector3(_movementDirection.x, 0 , _movementDirection.z) * moveSpeed) * airControlModifier), ForceMode.VelocityChange);
      }
      
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
      
     // Debug.Log(_rb.linearVelocity.magnitude);
      
   }
   
   private void Jump(InputAction.CallbackContext obj)
   {
      if (IsGrounded())
      {
         _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      }
      
   }

   private bool IsGrounded()
   {
      Ray groundRay = new Ray(transform.position, Vector3.down);
      return Physics.SphereCast(groundRay, 0.1f, 0.95f);
   }
   
   
   
   
   
}
