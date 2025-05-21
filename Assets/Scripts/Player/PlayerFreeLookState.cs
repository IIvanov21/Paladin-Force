using UnityEngine;

public class PlayerFreeLookState:PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    /*
     * Animation Variables
     */
    private const float AnimatorDampTime = 0.1f;
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeStateBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash,AnimatorDampTime);
        stateMachine.InputReader.TargetEvent += OnTarget;
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.MovementVector = CalculateMovement();

        stateMachine.characterController.Move(stateMachine.MovementVector * stateMachine.PlayerSpeed*deltaTime);

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
}
