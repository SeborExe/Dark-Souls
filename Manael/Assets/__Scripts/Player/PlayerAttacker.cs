using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimationHandler animationHandler;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;

        private void Awake()
        {
            animationHandler = GetComponentInChildren<AnimationHandler>();
            inputHandler = GetComponent<InputHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animationHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OH_Light_attack_01)
                {
                    animationHandler.PlayTargetAnimation(weapon.OH_Light_attack_02, true);
                }
                else if (lastAttack == weapon.TH_Light_Attack_01)
                {
                    animationHandler.PlayTargetAnimation(weapon.TH_Light_Attack_02, true);
                }
            }
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;

            if (inputHandler.twoHandFlag)
            {
                animationHandler.PlayTargetAnimation(weapon.TH_Light_Attack_01, true);
                lastAttack = weapon.TH_Light_Attack_01;
            }
            else
            {
                animationHandler.PlayTargetAnimation(weapon.OH_Light_attack_01, true);
                lastAttack = weapon.OH_Light_attack_01;
            }
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            if (inputHandler.twoHandFlag)
            {

            }
            else
            {
                animationHandler.PlayTargetAnimation(weapon.OH_Heavy_attack_01, true);
                lastAttack = weapon.OH_Heavy_attack_01;
            }
        }
    }
}


/*combo with 3 attack will look like:
 * public void HandleLightWeaponCombo(WeaponItem weapon)
{
    if(inputHandler.comboFlag)
    {
        animatorHandler.anim.SetBool("canDoCombo", false);

        #region Light Attack Combos
        if (lastAttack == weapon.OH_Light_Attack_1)
        {
            animatorHandler.anim.SetBool("canDoCombo", false);
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
            lastAttack2 = weapon.OH_Light_Attack_2;

            StartCoroutine(HandleLightLastAttack());
        }

        if (lastAttack == weapon.OH_Light_Attack_2)
        {
            animatorHandler.anim.SetBool("canDoCombo", false);
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_3, true);
        }
}

private IEnumerator HandleLightLastAttack()
{
    yield return null;
    lastAttack = lastAttack2;

}

*/

