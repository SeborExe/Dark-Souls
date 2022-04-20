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

        public int staminaLevel = 10;
        public int maxStamina;
        public int currentStamina;

        HealthBar healthBar;
        StaminaBar staminaBar;

        AnimationHandler animationHandler;

        private void Awake()
        {
            animationHandler = GetComponentInChildren<AnimationHandler>();

            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
        }

        private void Start()
        {
            maxHealth = SetMaxLevelHalth();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            maxStamina = SetMaxStamina();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
        }

        private int SetMaxLevelHalth()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private int SetMaxStamina()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
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

        public void TakeStaminaDamage(int damage)
        {
            currentStamina -= damage;

            staminaBar.SetCurrentStamina(currentStamina);
        }
    }
}

