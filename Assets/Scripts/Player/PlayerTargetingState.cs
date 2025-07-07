using UnityEngine;

public class PlayerTargetingState:PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){ }

    private readonly int TargetingBlendTree = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForward = Animator.StringToHash("TargetingForwad");
    private readonly int TargetingRight = Animator.StringToHash("TargetingRight");
    private const float AnimatorDampTime = 0.1f;



    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTree,AnimatorDampTime);

        stateMachine.InputReader.CancelTargetEvent += OnCancel;
        stateMachine.InputReader.DodgeEvent += OnDodge;

    }

    public override void Exit()
    {
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.CancelTargetEvent -= OnCancel;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        if(stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        stateMachine.MovementVector = CalculateMovement();
        Move(stateMachine.MovementVector * stateMachine.TargetingMovementSpeed,deltaTime);

        FaceTarget();

        UpdateAnimator(deltaTime);
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();
        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;

    }

    private void UpdateAnimator(float deltaTime)
    {
        if(stateMachine.InputReader.MovementValue==Vector2.zero)
        {
            stateMachine.Animator.SetFloat(TargetingRight, 0f,AnimatorDampTime,deltaTime);
            stateMachine.Animator.SetFloat(TargetingForward, 0f, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(TargetingRight, stateMachine.InputReader.MovementValue.x, 
            AnimatorDampTime, deltaTime);
        stateMachine.Animator.SetFloat(TargetingForward, stateMachine.InputReader.MovementValue.y, 
            AnimatorDampTime, deltaTime);

    }

    /*private void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null) return;

        Vector3 facingVector=stateMachine.Targeter.CurrentTarget.transform.position-
            stateMachine.transform.position;

        facingVector.y = 0.0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(facingVector);
    }*/

    private void OnDodge()
    {
        // Switch to dodging state
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.MovementValue));
    }
}
