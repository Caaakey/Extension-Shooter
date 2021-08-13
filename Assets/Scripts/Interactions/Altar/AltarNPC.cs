using System.Collections;
using UnityEngine;

namespace YourName.SurvivalShooter.Interactions.NPCs
{
    public class AltarNPC : BaseInteraction
    {
        [SerializeField] private GameObject m_StageUI;
        [SerializeField] private Transform m_RotateCube;

        protected override void OnStartInteraction()
        {
            m_StageUI.SetActive(true);
        }

        protected override void OnEndInteraction()
        {
            m_StageUI.SetActive(false);
        }

        protected override void Update()
        {
            base.Update();

            float speed = (IsEnterPlayer ? 80f : 20f) * Time.deltaTime;
            m_RotateCube.RotateAround(transform.position, Vector3.up, speed);
        }

    }
}
