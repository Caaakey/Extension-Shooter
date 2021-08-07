using UnityEngine;

namespace YourName.SurvivalShooter.Enemys
{
    public class EnemyAttack : MonoBehaviour
    {
        public float AttackDelayTime = 0.5f;
        public int AttackDamage = 10;
        private Animator m_Animator;
        private EnemyHealth m_Health;
        private bool m_IsPlayerInRange;
        private float m_Timer;

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Health = GetComponent<EnemyHealth>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (other.gameObject == PlayerStatus.Get.PlayerTransform.gameObject)
            {
                m_IsPlayerInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (other.gameObject == PlayerStatus.Get.PlayerTransform.gameObject)
            {
                m_IsPlayerInRange = false;
            }
        }

        private void Update()
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= AttackDelayTime && m_IsPlayerInRange && m_Health.CurrentHP > 0)
            {
                m_Timer = 0;
                PlayerStatus.Get.Hit(AttackDamage);
            }
        }

    }
}
