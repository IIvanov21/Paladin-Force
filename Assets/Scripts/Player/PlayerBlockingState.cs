using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockingHash = Animator.StringToHash("Block");
    private const float CrossFixedTime = 0.1f;
    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    

    public override void Enter()
    {
        stateMachine.Health.SetInvulnerable(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockingHash, CrossFixedTime);
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if(!stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }

        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }
}
