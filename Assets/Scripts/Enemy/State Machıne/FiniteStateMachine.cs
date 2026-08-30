using UnityEngine;

public class FiniteStateMachine 
{
    public State currentState {  get; private set; } 

    public void Initialize(State StartingState)
    {
        currentState = StartingState;
        currentState.Enter();
    }


    public void ChangeState(State newState) {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
