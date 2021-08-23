using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecondBranch
{/// <summary>
/// Обеспечение абстракции
/// </summary>
    public class StateMachine
    {
        public State CurrentState { get; private set; }     //текущее состояние

        /// <summary>
        /// Инициализация СтейтМашины
        /// </summary>
        /// <param name="staringState"></param>
        public void Initialize(State staringState)
        {
            CurrentState = staringState;
            staringState.Enter();
        }
        /// <summary>
        /// Изменение состояния
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(State newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}

