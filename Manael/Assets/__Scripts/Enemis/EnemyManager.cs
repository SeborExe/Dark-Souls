using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;

        bool isPerformingAction;

        [Header("A.I. Settings")]
        public float detectionRadius = 20f;
        public float minimumDetectionAngle = -50f;
        public float maximumDetectionAngle = 50f;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        }

        private void Update()
        {
            HandleCurrentAction();
        }

        void HandleCurrentAction()
        {
            if (enemyLocomotionManager.currentTarget == null)
            {
                enemyLocomotionManager.HandleDetection();
            }
        }
    }
}

