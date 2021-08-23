using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SecondBranch
{
    public class IdleState : State
    {
        private bool danced;
        private bool haveABottle;

        public IdleState(BotControllerV2 bot, StateMachine stateMachine) : base(bot, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            bot.Wait = false;                       
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            bot.Timer();
            if(bot.Zone == "DanceNav" && !danced)
            {
                bot.Dancing = true;
                danced = true;
                stateMachine.ChangeState(bot.dancingState);
            }
            else if (bot.Zone == "BarNav" && !haveABottle)
            {
                bot.TakeABottle();
                haveABottle = true;
            }
            else if (bot.Wait)
            {
                stateMachine.ChangeState(bot.walkingState);
                danced = false;
            }
            
        }

        public override void Exit()
        {            
            base.Exit();
            bot.ResetMoveParams();
            if (!bot.Dancing)
            {
                bot.SetDestiny();
            }                     
        }

        public override string OutputName()
        {
            base.OutputName();
            return "Idle";
        }
    }
}

