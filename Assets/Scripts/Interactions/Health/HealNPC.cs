using System.Collections;
using UnityEngine;

namespace YourName.SurvivalShooter.Interactions.NPCs
{
    public class HealNPC : BaseInteraction
    {
        [SerializeField] private Animation m_Animation;

        protected override void OnStartInteraction()
        {
            PlayerStatus.Get.CurrentHP = PlayerStatus.Get.MaxHP;

            m_Animation.Play();
        }

        protected override void OnEndInteraction()
        {
        }

    }
}
