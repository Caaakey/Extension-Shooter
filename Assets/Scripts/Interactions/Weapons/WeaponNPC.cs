using System.Collections;
using UnityEngine;

namespace YourName.SurvivalShooter.Interactions.NPCs
{
    public class WeaponNPC : BaseInteraction
    {
        [SerializeField] private GameObject m_WeaponUI;

        protected override void OnStartInteraction()
        {
            m_WeaponUI.SetActive(true);
        }

        protected override void OnEndInteraction()
        {
            m_WeaponUI.SetActive(false);
        }
        
    }
}
