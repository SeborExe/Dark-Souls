using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SH
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        Animator anim;
        InteractableUI interactableUI;

        [SerializeField] GameObject interactableUIGameObject;
        public GameObject itemInteractableObject;

        public bool isInteracting;

        [Header("Player flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;

        [SerializeField] float fadeSpeed = 0.2f;
        bool hide = false;
        float originalTransparency;

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
        }


        private void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            interactableUI = FindObjectOfType<InteractableUI>();

            originalTransparency = itemInteractableObject.GetComponent<Image>().color.a;
        }

        private void Update()
        {
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            anim.SetBool("isInAir", isInAir);

            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleFall(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleJumping();

            CheckForInteractable();
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

            if (hide)
            {
                SlowlyHideText();
            }
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.d_pad_up = false;
            inputHandler.d_pad_down = false;
            inputHandler.d_pad_left = false;
            inputHandler.d_pad_right = false;
            inputHandler.a_Input = false;
            inputHandler.jump_Input = false;

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }

        public void CheckForInteractable()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, -0.3f, transform.forward, out hit, 1f))
            {
                if (hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);

                        if (inputHandler.a_Input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }

            else
            {
                if (interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }
            }
        }

        #region Show text after pick up item
        public void PickUpItem()
        {
            itemInteractableObject.SetActive(true);
            StartCoroutine(HideTextObjectCoroutine());         
        }

        IEnumerator HideTextObjectCoroutine()
        {
            yield return new WaitForSeconds(2f);
            hide = true;
        }

        private void SlowlyHideText()
        {
            Color color = itemInteractableObject.GetComponent<Image>().color;

            color.a -= Time.deltaTime * fadeSpeed;
            Color newColor = new Color(0, 0, 0, color.a);
            itemInteractableObject.GetComponent<Image>().color = newColor;

            if (itemInteractableObject != null && (itemInteractableObject.GetComponent<Image>().color.a <= 0.01 || inputHandler.a_Input))
            {
                itemInteractableObject.GetComponent<Image>().color = new Color(0, 0, 0, originalTransparency);
                itemInteractableObject.SetActive(false);
                hide = false;
            }
        }
        #endregion
    }
}
