using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class AmbushState : State
    {
        public bool isSleeping;
        public float detectRadius = 2f;
        public LayerMask detectionLayer;
        public string sleepAnimation;
        public string wakeAnimation;

        public PursueTargetState pursueTargetState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (isSleeping && enemyManager.isInteracting == false)
            {
                enemyAnimatorManager.PlayTargetAnimation(sleepAnimation, true);
            }

            #region Handle target detection

            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position - enemyManager.transform.position;
                    enemyManager.viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                    if (enemyManager.viewableAngle > enemyManager.minimumDetectionAngle &&
                        enemyManager.viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterStats;
                        isSleeping = false;
                        enemyAnimatorManager.PlayTargetAnimation(wakeAnimation, true);
                    }
                }
            }



            #endregion

            #region Hande State Change

            if (enemyManager.currentTarget != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }

            #endregion
        }
    }
}
