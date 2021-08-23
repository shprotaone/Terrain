using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecondBranch
{
    public class DrinkingState : State
    {
         public DrinkingState(BotControllerV2 bot, StateMachine stateMachine) : base(bot, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
        public override void Exit()
        {
            base.Exit();
        }

        public override string OutputName()
        {
            return "Drinking";
        }
    }
}

