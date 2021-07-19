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

        public virtual void Enter()
        {

        }
        public virtual void Input()
        {

        }
        public virtual void LogicUpdate()
        {

        }
        public virtual void Exit()
        {

        }
    }
}

