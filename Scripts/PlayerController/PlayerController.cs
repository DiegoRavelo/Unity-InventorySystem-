using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private CharacterController _characterController;
    private Vector3 direction;


    [SerializeField] private Movement movement;

    [SerializeField] private WeaponController weaponController;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
     private float _velocity;
    private static Vector2 input;
    private float currentVelocity;
    private bool performingAction;
    private Coroutine miCoroutine;

    private bool IsGrounded() => _characterController.isGrounded;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        
    }


    void Update()
    {
        if(!performingAction)
        {
             ApplyMovement();

             ApplyRotation();  

        }
       

        ApplyGravity();

              
    }

    public void ApplyMovement()
    {
        
         movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, movement.targetSpeed, movement.acceleration * Time.deltaTime);
         
        _characterController.Move(direction * movement.currentSpeed * Time.deltaTime);

    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        direction = matrix.MultiplyPoint3x4(new Vector3(input.x , 0 , input.y));


        
    
    }

    public void LookAt(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            print("lookin");
           

            Ray ray = _camera.ScreenPointToRay(context.ReadValue<Vector2>());

            if (Physics.Raycast(ray, out RaycastHit info))
            {
                

                Vector3 facingDirection = info.point - transform.position;

                var targetAngle = Mathf.Atan2(facingDirection.x, facingDirection.z) * Mathf.Rad2Deg;

                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity , 0f);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);



            }
        }
        else if(context.canceled)
        {
             performingAction = false;

        }
        


    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(context.started && miCoroutine == null && !performingAction)
        {
            print("dash");

            performingAction = true;

            miCoroutine = StartCoroutine(Dash());
            
        }
    }

    private IEnumerator Dash()
    {
        
        float startTime = Time.time;

        while(Time.time < startTime + movement.dashTime)
        {
            _characterController.Move(transform.forward * movement.dashSpeed *  Time.deltaTime);

            yield return null;
        }

        miCoroutine = null;

        performingAction = false;

        
    }

    
    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;

        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;

        }
        direction.y = _velocity;   

    }

     private void ApplyRotation()
    {
        if (input.sqrMagnitude == 0) return;

             var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

             var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity , 0.05f);

             transform.rotation = Quaternion.Euler(0f, angle, 0f);


    }



  


    


}
