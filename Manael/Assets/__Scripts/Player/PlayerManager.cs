using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        PlayerManager playerManager;
        Animator anim;

        public bool isInteracting;

        [Header("Player flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
        }


        private void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            playerManager = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");

            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleFall(delta, playerLocomotion.moveDirection);
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.rb_input = false;
            inputHandler.rt_input = false;
            inputHandler.d_pad_up = false;
            inputHandler.d_pad_down = false;
            inputHandler.d_pad_left = false;
            inputHandler.d_pad_right = false;

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }
    }
}
