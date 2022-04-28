using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SH
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;
        NavMeshAgent navMeshAgent;

        public Rigidbody enemyRigidbody;

        public CharacterStats currentTarget;
        public LayerMask detectionLayer;

        public float distanceFromTarget;
        public float stoppingDistance = 1f;
        public float rotationSpeed = 15f;
        public float moveSpeed = 1f;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            navMeshAgent.enabled = false;
            enemyRigidbody.isKinematic = false;
        }

        public void HandleDetection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    //Check for team id

                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        currentTarget = characterStats;
                    }
                }
            }
        }

        public void HandleMoveToTarget()
        {
            Vector3 targetDirection = currentTarget.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            //if we are performing in action, stop enemy movement
            if (enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                navMeshAgent.enabled = false;
            }
            else
            {
                if (distanceFromTarget > stoppingDistance)
                {
                    enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                }
                else if (distanceFromTarget <= stoppingDistance)
                {
                    enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }

            HandleRotateTowardsToTarget();
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;
        }

        private void HandleRotateTowardsToTarget()
        {
            if (enemyManager.isPerformingAction)
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
            }
            else
            {
                Vector3 relativeDIrection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyRigidbody.velocity;

                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(currentTarget.transform.position);
                enemyRigidbody.velocity = targetVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
            }
        }
    }
}

