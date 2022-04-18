using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public HealthBar healthBar;

        AnimationHandler animationHandler;

        private void Awake()
        {
            animationHandler = GetComponentInChildren<AnimationHandler>();
        }

        private void Start()
        {
            maxHealth = SetMaxLevelHalth();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private int SetMaxLevelHalth()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            healthBar.SetCurrentHealth(currentHealth);

            animationHandler.PlayTargetAnimation("Damage_01", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animationHandler.PlayTargetAnimation("Dead_01", true);
            }
        }
    }
}

