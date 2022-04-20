using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        Animator animator;

        QuickSlotsUI quickSlotsUI;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponCollider();
                quickSlotsUI.UpdateWeaponSlotsUI(true, weaponItem);

                #region Handle weapon idle animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.Left_Hand_Idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Left Arm Empty", 0.2f);
                }
                #endregion
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponCollider();
                quickSlotsUI.UpdateWeaponSlotsUI(false, weaponItem);

                #region Handle weapon idle animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.Right_Hand_Idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Right Arm Empty", 0.2f);
                }
                #endregion
            }
        }

        #region Open and Close weapon collider

        private void LoadLeftWeaponCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        private void LoadRightWeaponCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenRightDamageCollider()
        {
            rightHandDamageCollider.EnabelDamageCollider();
        }

        public void OpenLeftHandDamageCollider()
        {
            leftHandDamageCollider.EnabelDamageCollider();
        }

        public void CloseRightHandDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void CloseLeftHandDamageCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }

        #endregion
    }
}
