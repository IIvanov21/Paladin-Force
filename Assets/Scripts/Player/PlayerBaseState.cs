using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    /*
    * Simple function which moves the player during an attack only using the given force of that attack.
    * It can also be applied in other physics, such as when the player is hit.
    */
    protected void Move(float deltaTime)
    {
        stateMachine.characterController.Move((Vector3.zero + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    /*
     * This Move function, takes in account input movement and can be used for normal scenarios, in all states.
     */
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.characterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void FaceTarget()
    {
        //If there is no target, exit early.
        if (stateMachine.Targeter.CurrentTarget == null) return;

        //If there is a target make the player always face that target.
        //A simple version of LookAt function.
        Vector3 facingVector = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;

        facingVector.y = 0.0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(facingVector);
    }

    protected void ReturnToLocomotion()
    {
        if (stateMachine.Targeter.CurrentTarget != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
