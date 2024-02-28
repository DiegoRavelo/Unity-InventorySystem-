using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    // Start is called before the first frame update

     public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base ( currentContext, playerStateFactory)
    {
        
    }
    public override void EnterState(){

        //_ctx.Anim.Play("Idle");
    }

    public override void UpdateState()
    {
        CheckSwitchState();

        Debug.Log("Checking states");
    }

    public override void ExitState(){}

    public override void CheckSwitchState()
    {
        if(_ctx.IsMoving)
        {
            SwitchState(_factory.Move());
        }
        else if(_ctx.IsDashing)
        {
            SwitchState(_factory.Dash());
        }
        else if(_ctx.IsLooking)
        {
            SwitchState(_factory.Shooting());
        }
    }

    public override void InitilizeSubState(){}
}
