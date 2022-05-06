using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    [CreateAssetMenu(menuName = "Spells/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(AnimationHandler animationHandler, PlayerStats playerStats)
        {
            base.AttemptToCastSpell(animationHandler, playerStats);
            GameObject instantiatedWarpUpSpellFX = Instantiate(spallWarmUpFX, animationHandler.transform);
            animationHandler.PlayTargetAnimation(spellAnimation, true);
            Debug.Log("Attepting cast spell...");
        }

        public override void SuccessfullyCastSpell(AnimationHandler animationHandler, PlayerStats playerStats)
        {
            base.SuccessfullyCastSpell(animationHandler, playerStats);
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, animationHandler.transform);
            playerStats.HealPlayer(healAmount);
            Debug.Log("Spell cast succesfull...");
        }
    }
}

