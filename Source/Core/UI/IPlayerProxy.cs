﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sanguosha.Core.Cards;
using Sanguosha.Core.Players;
using Sanguosha.Core.Skills;
using Sanguosha.Core.Games;

namespace Sanguosha.Core.UI
{
    public enum VerifierResult
    {
        Success,
        Partial,
        Fail,
    }

    public class ResultDeckOptions
    {
        public string Name { get; set; }
        public int Maximum { get; set; }
        public bool Rearrangeable { get; set; }
        ResultDeckOptions()
        {
            Name = null;
            Maximum = 1;
            Rearrangeable = false;
        }
    }

    public class AdditionalCardChoiceOptions
    {
        /// <summary>
        /// Gets/sets if the choice is for WuGuFengDeng.
        /// </summary>
        public bool IsWuGu { get; set; }
        /// <summary>
        /// Gets/sets if the choice is for initial hero pick for 1v1 or 3v3 game.
        /// </summary>
        public bool IsTwoSidesCardChoice { get; set; }        
        public List<bool> Rearrangeable { get; set; }
        public List<OptionPrompt> Options { get; set; }
        public List<List<Card>> DefaultResult { get; set; }
        public int OptionResult { get; set; }
        /// <summary>
        /// 细微控制哪个result可以被reveal
        /// </summary>
        /// <seealso cref="Sanguosha.Expansions.OverKnightFame11.Skills.XinZhan"/>
        public List<bool> AdditionalFineGrainedCardRevealPolicy { get; set; }
        /// <summary>
        /// Gets/sets whether a player can abstain from making the choice.
        /// </summary>
        public bool IsCancellable { get; set; }
    }
    
    public interface ICardChoiceVerifier
    {
        VerifierResult Verify(List<List<Card>> answer);
        UiHelper Helper { get; }
    }

    public delegate void CardChoiceRearrangeCallback(CardRearrangement RearrangeHint);

    public interface IPlayerProxy
    {
        Player HostPlayer { get; set; }
        int TimeOutSeconds { get; set; }
        
        /// <summary>
        /// Gets/sets whether this UiProxy can make input to the game.
        /// A UiProxy is not playable if it is not associated with 
        /// the main player or if the player is spectating.
        /// </summary>
        bool IsPlayable { get; }

        /// <summary>
        /// 询问使用或打出卡牌，可以发动技能。
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="verifier"></param>
        /// <param name="skill"></param>
        /// <param name="cards"></param>
        /// <param name="players"></param>
        /// <returns>False if user cannot provide an answer.</returns>
        bool AskForCardUsage(Prompt prompt, ICardUsageVerifier verifier,
                             out ISkill skill, out List<Card> cards, out List<Player> players);
        /// <summary>
        /// 询问用户从若干牌堆中选择卡牌，例如顺手牵羊，观星等等。
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="sourceDecks"></param>
        /// <param name="resultDeckNames"></param>
        /// <param name="resultDeckMaximums"></param>
        /// <param name="verifier"></param>
        /// <param name="answer">用户选择结果。对应resultDeckNames，每个选出的牌堆占用一个list。</param>
        /// <returns>False if user cannot provide an answer.</returns>
        bool AskForCardChoice(Prompt prompt, 
                              List<DeckPlace> sourceDecks,
                              List<string> resultDeckNames,
                              List<int> resultDeckMaximums,
                              ICardChoiceVerifier verifier,
                              out List<List<Card>> answer,
                              AdditionalCardChoiceOptions helper = null,                  
                              CardChoiceRearrangeCallback callback = null);

        /// <summary>
        /// 询问多选题目，例如是否发动洛神
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="questions">问题列表</param>
        /// <param name="answer">回答</param>
        /// <returns></returns>
        bool AskForMultipleChoice(Prompt prompt, List<OptionPrompt> options, out int answer);

        void Freeze();
    }
}
