using UnityEngine;

public class StateMachine : MonoBehaviour
{
    State currentState;

    // Update is called once per frame
    void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
       
        currentState?.Exit();

        currentState = newState;

        currentState?.Enter();
    }
}
