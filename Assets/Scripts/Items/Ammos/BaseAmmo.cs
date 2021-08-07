using UnityEngine;

namespace YourName.SurvivalShooter.Weapons
{
    public abstract class BaseAmmo : BaseItem
    {
        [SerializeField] private Rigidbody m_Rigidbody;
        [SerializeField] private Collider m_Collider;
        [SerializeField] private float m_Damage;
        [SerializeField] private float m_FireSpeed;
        private HighSpeedAmmoChecker m_Checker;

        private float TotalDamage { get; set; } = 0;

        private void Awake()
        {
            m_Checker = GetComponent<HighSpeedAmmoChecker>();
        }

        public void Create(float damage)
        {
            TotalDamage = m_Damage + damage;
            m_Rigidbody.AddRelativeForce(Vector3.forward * m_FireSpeed, ForceMode.Impulse);
        }

        public void OnCheckCollision(Collider other)
        {
            m_Collider.isTrigger = false;
            m_Checker.enabled = false;

            Destroy(gameObject, 10f);
        }

    }
}
