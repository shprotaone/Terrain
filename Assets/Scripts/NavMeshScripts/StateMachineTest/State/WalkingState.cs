using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SecondBranch
{
    public class WalkingState : State
    {
        public WalkingState (BotControllerV2 bot, StateMachine stateMachine): base(bot, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            bot.IsStopped = false;
        }
        public override void LogicUpdate()
        {                
            bot.Move();
            if (bot.Finished)
            {
                stateMachine.ChangeState(bot.idleState);
            }            
        }
        public override void Exit()
        {
            bot.IsStopped = true;
            bot.Finished = false;
            bot.ResetMoveParams();
        }
        public override string OutputName()
        {
            return "Walking";
        }
    }
}

