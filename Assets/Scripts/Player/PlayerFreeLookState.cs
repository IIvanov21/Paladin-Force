using UnityEngine;

public class PlayerFreeLookState:PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
    {
        this.shouldFade = shouldFade;
    }

    /*
     * Animation Variables
     */
    private const float AnimatorDampTime = 0.1f;
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeStateBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private bool shouldFade;

    public override void Enter()
    {
        if(shouldFade) stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash,AnimatorDampTime);
        else 
        {
            stateMachine.Animator.Play(FreeLookBlendTreeHash);
            stateMachine.Animator.SetFloat(FreeLookSpeedHash,0f);
        }
            stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }
        stateMachine.MovementVector = CalculateMovement();

        Move(stateMachine.MovementVector * stateMachine.PlayerSpeed,deltaTime);

        FaceMovementDirection(deltaTime);
    }

    Vector3 CalculateMovement()
    {
        //Get the forward direction of the camera
        Vector3 forward=stateMachine.MainCameraTransform.forward;

        //Zero out the Y coordinate to keep movement on the horizontal plane.
        forward.y= 0f;

        //Normalise the forwad vector to ensure it has a magnitude of 1
        forward.Normalize();

        Vector3 right = stateMachine.MainCameraTransform.right;
        right.y = 0f;
        right.Normalize();

        //Calculate the movement vector based on camera orientation and input values

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(float deltaTime)
    {
        if(stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);//Sets it to idle when we are not moving
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash,1,AnimatorDampTime,deltaTime);//Sets it to running
        /*stateMachine.Animator.SetFloat("FreeLookSpeed", stateMachine.InputReader.MovementValue.y);
        stateMachine.Animator.SetInteger("FreeLookSpeed", 1);
        stateMachine.Animator.SetBool("FreeLookSpeed", false);
        stateMachine.Animator.SetTrigger(FreeLookSpeedHash);*/


        stateMachine.transform.rotation=Quaternion.Lerp(stateMachine.transform.rotation,
            Quaternion.LookRotation(stateMachine.MovementVector),deltaTime*stateMachine.RotationDamping);
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) return;

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }
}
