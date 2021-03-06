using UnityEngine;

namespace YourName.SurvivalShooter.Weapons
{
    public class HighSpeedAmmoChecker : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_Rigidbody;
        [SerializeField] private Collider m_Collider;
        [SerializeField] private BaseAmmo m_Ammo;
        [SerializeField] private LayerMask m_Layermask = -1;
        [SerializeField] private float m_SkinWidth = 0.1f;
        private float m_MinimumExtent;
        private float m_PartialExtent;
        private float m_SqrtMinimumExtent;
        private Vector3 m_PreviousPosition;
        private RaycastHit[] m_RaycastHits = new RaycastHit[2];

        public Vector3 GetPreviousPosition { get => m_PreviousPosition; }

        private void Start()
        {
            m_PreviousPosition = m_Rigidbody.position;
            m_MinimumExtent = Mathf.Min(
                Mathf.Min(
                    m_Collider.bounds.extents.x,
                    m_Collider.bounds.extents.y,
                    m_Collider.bounds.extents.z)
                );
            m_PartialExtent = m_MinimumExtent * (1.0f - m_SkinWidth);
            m_SqrtMinimumExtent = m_MinimumExtent * m_MinimumExtent;
        }

        private void FixedUpdate()
        {
            Vector3 movementThisStep = m_Rigidbody.position - m_PreviousPosition;
            float movementSqrMagnitude = movementThisStep.sqrMagnitude;

            if (movementSqrMagnitude > m_SqrtMinimumExtent)
            {
                float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);

                int hitCount = Physics.RaycastNonAlloc(
                    m_PreviousPosition,
                    movementThisStep,
                    m_RaycastHits,
                    movementMagnitude,
                    m_Layermask);

                if (hitCount != 0)
                {
                    for (int i = 0; i < hitCount; ++i)
                    {
                        ref var hitInfo = ref m_RaycastHits[i];
                        if (hitInfo.collider != m_Collider)
                        {
                            m_Ammo.OnCheckCollision(hitInfo.collider);
                            break;
                        }
                        else
                            m_Rigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * m_PartialExtent;
                    }
                }
            }

            m_PreviousPosition = m_Rigidbody.position;
        }

    }
}
