/// <summary>
/// Created by Adam Chandler, 2020
/// In order to implement this StateMachine, you need to do the following things:
/// 1. Create some sort of Controller that inherits from StateMachine
/// 2. Create a few classes that inherit from State
/// 3. Implement the required functions in the new State class
/// 4. Create a few instances of the class somewhere in your new Controller
/// 5. Use ChangeState(state) to Change to any of the states in your Controller!
/// 6. If the States need more information, you can pass them down in the Constructor when the Controller initializes them
/// </summary>

using System.Collections;
using UnityEngine;

public abstract class StateMachineMB : MonoBehaviour
{
    public IState currentState;
	public IState previousState;

	bool inTransition = false;

	public void ChangeState(IState newState)
	{
		// ensure we're ready for a new state
		if (currentState == newState || inTransition)
			return;

		ChangeStateRoutine(newState);
	}

	public void RevertState()
	{
		if (previousState != null)
			ChangeState(previousState);
	}

	void ChangeStateRoutine(IState newState)
	{
		inTransition = true;
		// begin our exit sequence, to prepare for new state
		if (currentState != null)
			currentState.Exit();
		// save our current state, in case we want to return to it
		if (previousState != null)
			previousState = currentState;

		currentState = newState;

		// begin our new Enter sequence
		if (currentState != null)
			currentState.Enter();

		inTransition = false;
	}

	// pass down Update ticks to States, since they won't have a MonoBehaviour
	public void Update()
	{
		// simulate update ticks in states
		if (currentState != null && !inTransition)
			currentState.Tick();
	}

    public void FixedUpdate()
    {
		// simulate fixedUpdate ticks in states
		if (currentState != null && !inTransition)
			currentState.FixedTick();
    }
}
