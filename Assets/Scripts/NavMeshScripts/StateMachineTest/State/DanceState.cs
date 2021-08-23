using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecondBranch
{
    public class DanceState : State
    {
        public DanceState(BotControllerV2 bot, StateMachine stateMachine): base(bot, stateMachine)
        {

        }

        public override void Enter()
        {
            bot.StartDance(bot.Dancing);
            bot.IsStopped = true;
            bot.Wait = false;
        }

        public override void LogicUpdate()
        {
            bot.Timer();
            if (bot.Wait)
            {
                stateMachine.ChangeState(bot.idleState);
            }
        }

        public override void Exit()
        {
            bot.Dancing = false;
            bot.StartDance(bot.Dancing);           
        }

        public override string OutputName()
        {
            return "JustDance";
        }
    }
    
}

