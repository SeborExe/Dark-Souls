using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            AnimationHandler animationHandler;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animationHandler = playerManager.GetComponentInChildren<AnimationHandler>();

            playerLocomotion.rigidbody.velocity = Vector3.zero; //Stop when picking item
            animationHandler.PlayTargetAnimation("Pick Up Item", true);
            playerInventory.weaponsInventory.Add(weapon);
            Destroy(gameObject);

        }
    }

}
