using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class PlayerStats : CharacterStats
    {
        HealthBar healthBar;
        StaminaBar staminaBar;
        ManaBar manaBar;
        PlayerManager playerManager;

        PlayerAnimatorManager animationHandler;

        float staminaRegenerationAmount = 1;
        float staminaRegenerationTimer = 0;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            animationHandler = GetComponentInChildren<PlayerAnimatorManager>();

            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            manaBar = FindObjectOfType<ManaBar>();
        }

        private void Start()
        {
            maxHealth = SetMaxLevelHalth();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            maxStamina = SetMaxStamina();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);

            maxMana = SetMaxMana();
            currentMana = maxMana;
            manaBar.SetMaxMana(maxMana);
        }

        private int SetMaxLevelHalth()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private float SetMaxStamina()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        private float SetMaxMana()
        {
            maxMana = manaLevel * 10;
            return maxMana;
        }

        public void TakeDamage(int damage)
        {
            if (playerManager.isInvulnerable) return;
            if (isDead) return;

            currentHealth -= damage;

            healthBar.SetCurrentHealth(currentHealth);
            animationHandler.PlayTargetAnimation("Damage_01", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animationHandler.PlayTargetAnimation("Dead_01", true);
                isDead = true;
            }
        }

        public void TakeDamageNoAnimation(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public void TakeStaminaDamage(int damage)
        {
            currentStamina -= damage;

            staminaBar.SetCurrentStamina(currentStamina);
        }

        public void RegenerateStamina()
        {
            if (playerManager.isInteracting)
                staminaRegenerationTimer = 0;

            else
            {
                staminaRegenerationTimer += Time.deltaTime;

                if (currentStamina < maxStamina && staminaRegenerationTimer > 1f)
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }

        }

        public void HealPlayer(int amount)
        {
            currentHealth += amount;

            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            healthBar.SetCurrentHealth(currentHealth);
        }

        public void DeductManaPoints(int mana)
        {
            currentMana -= mana;

            if (currentMana < 0)
            {
                currentMana = 0;
            }

            manaBar.SetCurrentMana(currentMana);
        }
    }
}

