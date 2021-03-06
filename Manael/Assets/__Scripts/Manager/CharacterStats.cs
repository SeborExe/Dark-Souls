using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class CharacterStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public int staminaLevel = 10;
        public float maxStamina;
        public float currentStamina;

        public int manaLevel = 10;
        public float maxMana;
        public float currentMana;

        public int soulCount = 0;

        public bool isDead;
    }
}

