using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    //The input reader provides the player input data.
    [field:SerializeField]
    public InputReader InputReader {  get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchState(new PlayerTestState(this));
    }

    
}
