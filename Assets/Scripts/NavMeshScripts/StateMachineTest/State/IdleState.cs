using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SecondBranch
{
    public class IdleState : State
    {
        public IdleState(BotControllerV2 bot, StateMachine stateMachine) : base(bot, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            bot.SetAnimationBool(bot.idleParam, true);
            bot.Stop();
        }

        public override void Exit()
        {
            base.Exit();
            bot.SetAnimationBool(bot.idleParam, false);
        }
    }
}

