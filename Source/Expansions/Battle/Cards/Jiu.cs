﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sanguosha.Core.UI;
using Sanguosha.Core.Skills;
using Sanguosha.Core.Players;
using Sanguosha.Core.Games;
using Sanguosha.Core.Triggers;
using Sanguosha.Core.Exceptions;
using Sanguosha.Core.Cards;
using Sanguosha.Expansions.Basic.Cards;
using System.Diagnostics;

namespace Sanguosha.Expansions.Battle.Cards
{
    public class Jiu : LifeSaver
    {
        protected override void Process(Player source, Player dest, ICard card)
        {
            throw new NotImplementedException();
        }

        public override void Process(Player source, List<Player> dests, ICard card)
        {
            Trace.Assert(dests == null || dests.Count == 0);
            if (!PlayerIsCardTargetCheck(ref source, ref source, card))
            {
                return;
            }
            source[JiuUsed] = 1;
        }

        protected override VerifierResult Verify(Player source, ICard card, List<Player> targets)
        {
            if (Game.CurrentGame.IsDying.Count == 0 && targets != null && targets.Count >= 1)
            {
                return VerifierResult.Fail;
            }
            if (Game.CurrentGame.IsDying.Count > 0 && (targets == null || targets.Count != 1))
            {
                return VerifierResult.Fail;
            }
            if (Game.CurrentGame.IsDying.Count == 0)
            {
                if (source[JiuUsed] == 1)
                {
                    return VerifierResult.Fail;
                }
            }
            else
            {
                if (targets[0] != source)
                {
                    return VerifierResult.Fail;
                }
            }
            return VerifierResult.Success;
        }

        public override CardCategory Category
        {
            get { return CardCategory.Basic; }
        }

        public static PlayerAttribute JiuUsed = PlayerAttribute.Register("JiuUsed", true);
    }

    public class JiuDamage : Trigger
    {
        public override void Run(GameEvent gameEvent, GameEventArgs eventArgs)
        {
            if (eventArgs.Source[Jiu.JiuUsed] == 1)
            {
                eventArgs.Source[Jiu.JiuUsed] = 0;
                eventArgs.IntArg3--;
            }
        }
    }
}