using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SecondBranch
{
    public abstract class State
    {
        protected BotControllerV2 bot;
        protected StateMachine stateMachine;

        protected State(BotControllerV2 bot, StateMachine stateMachine)
        {
            this.bot = bot;
            this.stateMachine = stateMachine;
        }
        /// <summary>
        /// Вход в состояние
        /// </summary>
        public virtual void Enter()
        {

        }
        #region UpdateLoop
        /// <summary>
        /// Ввод
        /// </summary>
        public virtual void Input()
        {

        }
        public virtual void LogicUpdate()
        {

        }
        #endregion
        /// <summary>
        /// Выход из состояния
        /// </summary>
        public virtual void Exit()
        {

        }

        public virtual string OutputName()
        {
            return " ";
        }
    }
}

