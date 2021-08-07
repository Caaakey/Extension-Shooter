using System.Collections;
using UnityEngine;

namespace YourName.SurvivalShooter.Enemys
{
    public class EnemyHealth : MonoBehaviour
    {
        public float MaxHP = 100f;
        public float SinkSpeed = 2.5f;
        public Vector2Int DropMoneyMinMax;
        [System.NonSerialized] public float CurrentHP = 0;
        [SerializeField] private AudioClip m_DeathClip;
        [SerializeField] private ParticleSystem m_HitParticle;
        [SerializeField] private ParticleSystem m_DeathParticle;
        private Collider[] m_Colliders;
        private Animator m_Animator;
        private AudioSource m_AudioSource;
        private EnemyAttack m_Attack;
        private EnemyMovement m_Movement;

        private void Awake()
        {
            CurrentHP = MaxHP;

            m_Colliders = GetComponents<Collider>();
            m_Animator = GetComponent<Animator>();
            m_AudioSource = GetComponent<AudioSource>();

            m_Attack = GetComponent<EnemyAttack>();
            m_Movement = GetComponent<EnemyMovement>();
        }

        public void Hit(float damage, Quaternion direciton)
        {
            CurrentHP -= damage;

            if (CurrentHP <= 0) Death();
            else
            {
                m_AudioSource.Play();

                var particle = Instantiate(m_HitParticle, transform.position, direciton, null);
                particle.Play();

                Destroy(particle, particle.main.startLifetime.constantMax);
            }
        }

        public void Death()
        {
            for (int i = 0; i < m_Colliders.Length; ++i)
                m_Colliders[i].enabled = false;

            m_Animator.SetBool("Dead", true);

            m_AudioSource.clip = m_DeathClip;
            m_AudioSource.Play();
            m_DeathParticle.Play();

            m_Attack.enabled = false;
            m_Movement.enabled = false;

            Destroy(gameObject, 1.5f);
        }
    }
}
