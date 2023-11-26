using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, StateBase<EState>> States = new Dictionary<EState, StateBase<EState>>();
    protected StateBase<EState> stateActive;

    protected bool isTransitioningState = false;

    public void EnterInitialState(StateBase<EState> _state)
    {
        stateActive = _state;
        stateActive.EnterState();
    }

    public void AddState<T>(EState stateKey, StateBase<EState> state, T mainClasRef) where T : class
    {
        state.InitState(mainClasRef);
        States.Add(stateKey, state);
    }

    public T GetState<T>(EState _stateKey) where T : class
    {

        if (States.ContainsKey(_stateKey))
        {
            return States[_stateKey] as T;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (stateActive == null) { return; }

        if (!isTransitioningState)
        {
            stateActive.UpdateState();
        }
    }

    public void TransitionToState(EState _newState)
    {
        isTransitioningState = true;

        stateActive.ExitState();
        stateActive = States[_newState];
        stateActive.EnterState();

        isTransitioningState = false;
    }
}
