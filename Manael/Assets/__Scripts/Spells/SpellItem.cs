using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class SpellItem : MonoBehaviour
    {
        public GameObject spallWarmUpFX;
        public GameObject spellCastFX;
        public string spellAnimation;

        [Header("Spell type")]
        public bool isFaithSpell;
        public bool isMagicSpell;
        public bool isPyroSpell;

        [Header("Spell Discription")]
        [TextArea] public string spellDiscription;

        public virtual void AttemptToCastSpell()
        {
            Debug.Log("You attempt to cast a spell");
        }

        public virtual void SuccessfullyCastSpell()
        {
            Debug.Log("You successfuly cast a spell");
        }
    }
}

