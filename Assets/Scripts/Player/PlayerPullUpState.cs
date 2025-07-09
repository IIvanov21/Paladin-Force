using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int PullUpHash = Animator.StringToHash("Pullup");
    private const float CrossFixedTime = 0.1f;

    private readonly Vector3 offset = new Vector3(0f, 2.325f, 0.65f);

    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PullUpHash, CrossFixedTime);
    }

    public override void Exit()
    {
        stateMachine.characterController.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }

    public override void Tick(float deltaTime)
    {
        if(GetNormalisedTime(stateMachine.Animator,"Climbing")<1f)
        {
            return;
        }

        stateMachine.characterController.enabled = false;

        stateMachine.transform.Translate(offset, Space.Self);

        stateMachine.characterController.enabled = true;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine,false));
    }
}
