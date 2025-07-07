using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack attack;
    private float previousFrameTime;
    private bool alreadyAppliedForce = false;

    public PlayerAttackingState(PlayerStateMachine stateMachine,int attackIndex) : base(stateMachine)
    {
        attack=stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);

        stateMachine.WeaponDamage.SetAttack(attack.Damage, attack.Knockback);
    }

    public override void Exit()
    {

    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalisedTime=GetNormalisedTime();

        if(normalisedTime<1f)
        {
            if(normalisedTime >=attack.ForceTime) TryApplyForce();

            if(stateMachine.InputReader.IsAttacking) TryComboAttack(normalisedTime);
        }
        else 
        {
            if (stateMachine.Targeter.CurrentTarget != null) 
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            else stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        //Store the current normalised time for use in the next frame
        previousFrameTime=normalisedTime;
    }

    private float GetNormalisedTime()
    {
        //Get the current and next animaotr state info
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if(stateMachine.Animator.IsInTransition(0)&&nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if( !stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0.0f;
        }

    }

    private void TryComboAttack(float normalisedTime)
    {
        if (attack.ComboStateIndex == -1) return;

        if (normalisedTime < attack.ComboAttackTime) return;

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;

        //Apply force in the direction the player is facing
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }
}
