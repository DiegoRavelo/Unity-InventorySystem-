using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{

     public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base ( currentContext, playerStateFactory){

    }

    private float currentVelocity;

    public override void EnterState()
    {
        _ctx.Anim.Play("Run");

    }

    public override void UpdateState()
    {
        HandleMovement();
    
        CheckSwitchState();

    }

    public override void ExitState(){}

    public override void CheckSwitchState()
    {
        if(_ctx.IsDashing)
        {
            SwitchState(_factory.Dash());
        }
        else if(_ctx.IsLooking)
        {
            SwitchState(_factory.Shooting());

        }else if(!_ctx.IsMoving)
        {
            SwitchState(_factory.Idle());

        }
    }

    public override void InitilizeSubState(){}

    public void HandleMovement()
    {
        if(_ctx.IsMoving)
        {   

         _ctx.movement.currentSpeed = Mathf.MoveTowards( _ctx._Movement.currentSpeed,  _ctx._Movement.targetSpeed,  _ctx._Movement.acceleration * Time.deltaTime);

         _ctx.CharacterController.Move(_ctx.Direction *  _ctx._Movement.currentSpeed * Time.deltaTime);

          if (_ctx.Input.sqrMagnitude == 0) return;


             var targetAngle = Mathf.Atan2(_ctx.Direction.x,_ctx.Direction.z) * Mathf.Rad2Deg;

             var angle = Mathf.SmoothDampAngle(_ctx.transform.eulerAngles.y, targetAngle, ref currentVelocity , 0.05f);

             _ctx.transform.rotation = Quaternion.Euler(0f, angle, 0f);
           

        }
       

    }
}
