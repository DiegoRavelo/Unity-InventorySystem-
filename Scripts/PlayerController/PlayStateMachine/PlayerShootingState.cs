using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingState : PlayerBaseState
{
    // Start is called before the first frame update

     public PlayerShootingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base ( currentContext, playerStateFactory)
    {
        
    }

    float currentVelocity;
    public override void EnterState(){}

    public override void UpdateState()
    {
        CheckSwitchState();
        HandleShooting();
    }

    public override void ExitState(){}

    public override void CheckSwitchState(){}

    public override void InitilizeSubState(){}

    void HandleShooting()
    {
        if(_ctx.IsLooking)
        {
           Vector3 facingDirection = _ctx.Info.point - _ctx.transform.position;

           var targetAngle = Mathf.Atan2(facingDirection.x, facingDirection.z) * Mathf.Rad2Deg;
 
           var angle = Mathf.SmoothDampAngle(_ctx.transform.eulerAngles.y, targetAngle, ref currentVelocity , 0f);

           _ctx.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        }else
        {
            SwitchState(_factory.Idle());

        }
     
    }
}
