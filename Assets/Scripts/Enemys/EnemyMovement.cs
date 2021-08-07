using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace YourName.SurvivalShooter.Enemys
{
    public class EnemyMovement : MonoBehaviour
    {
        private NavMeshAgent m_NavMesh;
        private bool m_IsStop = false;
        [SerializeField] private float m_UpdateTrackingTime = 2f;

        private void Awake()
        {
            m_NavMesh = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            m_NavMesh.SetDestination(PlayerStatus.Get.PlayerTransform.position);

            StartCoroutine(UpdateFindPlayer());
        }

        private void OnDisable()
        {
            m_NavMesh.enabled = false;
            m_IsStop = true;
        }

        private IEnumerator UpdateFindPlayer()
        {
            while (!m_IsStop)
            {
                yield return new WaitForSeconds(m_UpdateTrackingTime);

                if (m_IsStop) break;
                else m_NavMesh.SetDestination(PlayerStatus.Get.PlayerTransform.position);
            }

            yield break;
        }

    }
}
