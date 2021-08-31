using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecondBranch
{
    public class DrinkingState : State
    {
         private float time = 0;
         public DrinkingState(BotControllerV2 bot, StateMachine stateMachine) : base(bot, stateMachine)
         {

         }

        public override void Enter()
        {
            bot.Drinking(true);
            time = bot.AnimationLenght("Drinking");
        }
        public override void LogicUpdate()
        {
            bot.Timer(time);
            if (bot.Wait)
            {
                stateMachine.ChangeState(bot.idleState);
            }
        }
        public override void Exit()
        {           
            bot.Drinking(false);
            bot.DestroyBottle();
        }

        public override string OutputName()
        {
            return "Drinking";
        }
    }
}

