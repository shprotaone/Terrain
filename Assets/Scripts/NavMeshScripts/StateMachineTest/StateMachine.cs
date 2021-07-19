using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecondBranch
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialize(State staringState)
        {
            CurrentState = staringState;
            staringState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}

