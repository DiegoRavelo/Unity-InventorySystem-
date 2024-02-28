using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    
    private CharacterController _characterController;

    public CharacterController CharacterController { get { return _characterController; } set { _characterController = value; }}
    private Vector3 direction;

    public Vector3 Direction { get { return direction; } set { direction = value; }}
    [SerializeField] public Movement movement;
    public Movement _Movement { get { return movement; } set { movement = value; }}
    [SerializeField] private Camera _camera;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;
    private static Vector2 input;

    public Vector2 Input  { get { return input; } set { input = value; }}
    private float currentVelocity;

    public float CurrentVelocity  { get { return currentVelocity; } set { currentVelocity = value; }}

    private bool performingAction;
    private Coroutine miCoroutine;
    public Coroutine MiCorourtine { get { return miCoroutine; } set { miCoroutine = value; }}

    private IEnumerator miEnumerator;
    public IEnumerator MiEnumerator  { get { return miEnumerator; } set { miEnumerator = value; }}

    [SerializeField] private Animator animator;

    public Animator Anim { get { return animator; } set { animator = value; }}
    // State Machine Varaible

    PlayerBaseState _currentState;

    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; }}

    PlayerStateFactory _states;

    // Input System varaibles 

    [SerializeField] private bool isDashing;

    [SerializeField] private bool isMoving;

     [SerializeField] private bool isLooking;

    private Ray ray;

    private RaycastHit info;

    public RaycastHit Info  { get { return info; } set { info = value; }}

    public Ray Ray { get { return ray; } set { ray = value; }}

    public bool IsMoving { get { return isMoving; } set { isMoving = value; }}

    public bool IsDashing { get { return isDashing; } set { isDashing = value; }}

    public bool IsLooking { get { return isLooking; } set { isLooking = value; }}






    private bool IsGrounded() => _characterController.isGrounded;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

         _characterController = GetComponent<CharacterController>();

         _states = new PlayerStateFactory(this);

         _currentState = _states.Idle();

         _currentState.EnterState();
    }

    void Update()
    {
        _currentState.UpdateState();

        Debug.Log(_currentState);
    }

    public void LookAt(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
          
           
            ray = _camera.ScreenPointToRay(context.ReadValue<Vector2>());

            if (Physics.Raycast(ray, out info))
            {
                

                isLooking = true;
            
            }
        }
        else if(context.canceled)
        {
            isLooking = false;

        }


    }

 

      public void Move(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isMoving = true;

            input = context.ReadValue<Vector2>();

            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            direction = matrix.MultiplyPoint3x4(new Vector3(input.x , 0 , input.y));

            
        }
        else
        {
            isMoving = false;
        }

       
    
    }

    

      public void Dash(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            
            isDashing = true;
            print("weee");
        }else if(context.canceled)
        {
             isDashing = false;
            
        }
        // else
        // {
        //     isDashing = false;
        // }

    }


    
}
[System.Serializable]
public struct Movement
{
    public float targetSpeed;
    public float multiplier;
    public float acceleration;
    public float currentSpeed;
    public float jumpPower;
    public float dashSpeed;
    public float dashTime;

}

