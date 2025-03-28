using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase<EState> where EState : Enum
{
    public StateBase(EState _key)
    {
        StateKey = _key;
    }

    private object mainClassRef;

    public void InitState<T>(T _classReference) where T : class
    {
        mainClassRef = _classReference;
    }

    public T GetMainClassReference<T>() where T : class
    {
        return mainClassRef as T;
    }

    public EState StateKey { get; private set; }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
