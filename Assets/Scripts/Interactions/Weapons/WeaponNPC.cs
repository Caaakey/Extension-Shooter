using System.Collections;
using UnityEngine;

namespace YourName.SurvivalShooter.Interactions.NPCs
{
    public class WeaponNPC : BaseInteraction
    {
        [SerializeField] private GameObject m_WeaponUI;

        protected override void StartInteraction()
        {
            m_WeaponUI.SetActive(true);
        }

        
    }
}
