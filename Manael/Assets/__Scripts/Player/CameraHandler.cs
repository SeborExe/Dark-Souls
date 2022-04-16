using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class CameraHandler : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTranfroms;
        Vector3 cameraTransformPosition;
        private LayerMask ignoreLayers;

        public static CameraHandler sigleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35f;
        public float maxPivot = 35f;

        private void Awake()
        {
            sigleton = this;
            myTranfroms = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }

        public void FollowTarget(float delta)
        {
            Vector3 targetPosition = Vector3.Lerp(myTranfroms.position, targetTransform.position, delta / followSpeed);
            myTranfroms.position = targetPosition;
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYinput)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYinput * lookSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maxPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTranfroms.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }
    }
}
