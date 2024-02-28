using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{
    public PlayerDashingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base ( currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {

        HandleDash();
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState(){}

    public override void CheckSwitchState()
    {

    }

    public override void InitilizeSubState(){}

    void HandleDash()
    {
        if( _ctx.MiCorourtine == null)
        {
             _ctx.MiCorourtine = _ctx.StartCoroutine(Dash());

             _ctx.Anim.Play("Roll");

        }
     
    }

     private IEnumerator Dash()
    {
 

        float startTime = Time.time;

        while(Time.time < startTime + _ctx._Movement.dashTime)
        {

            _ctx.CharacterController.Move(_ctx.transform.forward * _ctx._Movement.dashSpeed *  Time.deltaTime);

            yield return null;
        }

       _ctx.MiCorourtine = null;

       SwitchState(_factory.Idle());

        
        

    }


   
}
