using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SecondBranch
{
    public class IdleState : State
    {
        private bool canWalk;

        public IdleState(BotControllerV2 bot, StateMachine stateMachine) : base(bot, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            bot.Wait = false;
            canWalk = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            bot.Timer(5);
            if(bot.Zone == "DanceNav" && !bot.Danced)
            {
                bot.Dancing = true;
                canWalk = false;
                stateMachine.ChangeState(bot.dancingState);
            }
            else if (bot.Zone == "BarNav" && !bot.HaveABottle)
            {
                bot.TakeABottle();                    
            }
            else if(bot.Zone == "LaungeNav" && bot.HaveABottle)
            {                
                canWalk = false;              
                bot.HaveABottle = false;
                stateMachine.ChangeState(bot.drinkingState);
            }
            else if (bot.Wait)
            {                
                canWalk = true;
                stateMachine.ChangeState(bot.walkingState);
            }           
        }
        public override void Exit()
        {            
            base.Exit();
            bot.ResetMoveParams();

            if (canWalk && !bot.SecurityCheck)
            {
                bot.SetDestiny();
                bot.Danced = false;
            }
        }
        public override string OutputName()
        {
            base.OutputName();
            return "Idle";

        }
    }
}

