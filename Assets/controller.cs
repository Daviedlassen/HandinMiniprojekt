using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SimpleController : MonoBehaviour
{
    public float wSpeed = 2f;
    public float rSpeed = 6f;
    //public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    private void Start()
    {
        InitializeCursorLock();
        InitializeCharacterController();
    }

    private void InitializeCursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void InitializeCharacterController()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateMovement();
        UpdateRotation();
    }

    //Camera movement
    private void UpdateRotation()
    {
        if (!canMove)
            return;

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void UpdateMovement()
    {
        if (!canMove)
            return;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        //"?" checks if the condition is true, if its true it just assigns the runningSpeed value to the curSpeed 
        // the ":" is just an else if kinda operator, both are just conditionals basically
        float moveSpeedX = (isRunning ? rSpeed : wSpeed) * Input.GetAxis("Vertical");
        float moveSpeedY = (isRunning ? rSpeed : wSpeed) * Input.GetAxis("Horizontal");

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * moveSpeedX) + (right * moveSpeedY);
        
        moveDirection.y = movementDirectionY;

        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
