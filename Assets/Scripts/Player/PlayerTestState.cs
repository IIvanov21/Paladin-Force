using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("We have entered the test state!");
    }

    public override void Exit()
    {
        Debug.Log("We have exited the test state!");

    }

    public override void Tick(float deltaTime)
    {
        
    }
}
