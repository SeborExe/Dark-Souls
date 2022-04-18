using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimationHandler animationHandler;

        private void Awake()
        {
            animationHandler = GetComponentInChildren<AnimationHandler>();
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            animationHandler.PlayTargetAnimation(weapon.OH_Light_attack_01, true);
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animationHandler.PlayTargetAnimation(weapon.OH_Heavy_attack_01, true);
        }
    }
}

