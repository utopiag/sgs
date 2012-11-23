﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sanguosha.Core.Triggers;
using Sanguosha.Core.Cards;
using Sanguosha.Core.UI;
using Sanguosha.Core.Skills;
using Sanguosha.Core.Games;
using Sanguosha.Core.Heroes;
using Sanguosha.Expansions.Fire.Skills;
using Sanguosha.Expansions.Basic.Skills;

namespace Sanguosha.Expansions.Fire
{
    public class FireExpansion : Expansion
    {
        public FireExpansion()
        {
            CardSet = new List<Card>();
            
            CardSet.Add(new Card(SuitType.None, -1, new HeroCardHandler(new Hero("WoLong", true, Allegiance.Shu, 3, /*new BaZhen(),*/ new HuoJi(), new KanPo()))));
            CardSet.Add(new Card(SuitType.None, -1, new HeroCardHandler(new Hero("TaiShiCi", true, Allegiance.Wu, 4, new TianYi()))));
            CardSet.Add(new Card(SuitType.None, -1, new HeroCardHandler(new Hero("XunYu", true, Allegiance.Wei, 3, new QuHu()))));
            CardSet.Add(new Card(SuitType.None, -1, new HeroCardHandler(new Hero("PangTong", true, Allegiance.Shu, 3, new LianHuan()))));
            CardSet.Add(new Card(SuitType.None, -1, new HeroCardHandler(new Hero("ShenZhouYu", true, Allegiance.God, 4, new QinYin()))));

        }
    }
}
