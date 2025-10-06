using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public float cameraSensitivity;

  public Transform playerOrientation;

  private float _rotX;
  private float _rotY;
  
  private void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  private void Update()
  {
    float mouseX = Input.GetAxisRaw("Mouse X") * cameraSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxisRaw("Mouse Y") * cameraSensitivity * Time.deltaTime;
    
    _rotX -= mouseY;
    _rotY += mouseX;
    _rotX = Mathf.Clamp(_rotX, -90f, 90f);
    
    transform.rotation = Quaternion.Euler(_rotX, _rotY, 0f);
    playerOrientation.rotation = Quaternion.Euler(_rotX, _rotY, 0f);
    


  }
  
}
