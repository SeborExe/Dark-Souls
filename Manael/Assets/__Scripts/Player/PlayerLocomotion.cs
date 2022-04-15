using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class PlayerLocomotion : MonoBehaviour
    {
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector] public Transform myTransform;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField] float movementSpeed = 5f;
        [SerializeField] float rotationSpeed = 10f;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
        }

        #region movement

        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandlerRotation(float delta)
        {
            Vector3 targetDirection = Vector3.zero;
            float moveOveride = inputHandler.moveAmount;

            targetDirection = cameraObject.forward * inputHandler.vertical;
            targetDirection += cameraObject.right * inputHandler.horizontal;

            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
                targetDirection = myTransform.forward;

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDirection);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }

        #endregion
    }
}
