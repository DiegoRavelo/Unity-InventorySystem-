
public class PlayerStateFactory 
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle(){

        return new PlayerIdleState(_context, this);

    }

    public PlayerBaseState Move(){

        return new PlayerMoveState(_context, this);

    }

    public PlayerBaseState Dash(){

        return new PlayerDashingState(_context, this);
        
    }

    public PlayerBaseState Shooting(){

        return new PlayerShootingState(_context, this);

    }

}
