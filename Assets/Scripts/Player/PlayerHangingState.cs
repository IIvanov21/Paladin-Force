using JetBrains.Annotations;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private readonly int HangingIdleHash = Animator.StringToHash("HangingIdle");
    private const float CrossFixedTime = 0.1f;

    Vector3 ledgeForward, closestPoint;
    public PlayerHangingState(PlayerStateMachine stateMachine,Vector3 ledgeForward, Vector3 closestPoint) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
        this.closestPoint = closestPoint;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation=Quaternion.LookRotation(ledgeForward,Vector3.up);
        stateMachine.characterController.enabled = false;
        stateMachine.transform.position = closestPoint - 
            (stateMachine.LedgeDetector.transform.position-stateMachine.transform.position);
        stateMachine.characterController.enabled = true;

        stateMachine.Animator.CrossFadeInFixedTime(HangingIdleHash,CrossFixedTime);

    }

    public override void Exit()
    {
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.MovementValue.y <0f)//Drop from the ledge
        {
            stateMachine.characterController.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }

        if (stateMachine.InputReader.MovementValue.y > 0f)//Pull up
        {
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }
    }
}
