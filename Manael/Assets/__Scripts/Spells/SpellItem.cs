using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class SpellItem : Item
    {
        public GameObject spallWarmUpFX;
        public GameObject spellCastFX;
        public string spellAnimation;

        [Header("Spell cost")]
        public int manaCost;

        [Header("Spell type")]
        public bool isFaithSpell;
        public bool isMagicSpell;
        public bool isPyroSpell;

        [Header("Spell Discription")]
        [TextArea] public string spellDiscription;

        public virtual void AttemptToCastSpell(PlayerAnimatorManager animationHandler, PlayerStats playerStats)
        {
            Debug.Log("You attempt to cast a spell");
        }

        public virtual void SuccessfullyCastSpell(PlayerAnimatorManager animationHandler, PlayerStats playerStats)
        {
            Debug.Log("You successfuly cast a spell");
            playerStats.DeductManaPoints(manaCost);
        }
    }
}

