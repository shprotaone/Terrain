using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SecondBranch
{
    public class WalkingState : IdleState
    {
        private bool walking;

        public WalkingState (BotControllerV2 bot, StateMachine stateMachine): base(bot, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            bot.Move();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            bot.CheckDistance();
        }

        public override void Exit()
        {
            base.Exit();
            bot.Stop();
        }
    }
}

