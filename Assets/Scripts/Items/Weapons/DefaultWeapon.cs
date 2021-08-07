using UnityEngine;

namespace YourName.SurvivalShooter.Weapons
{
    public class DefaultWeapon : BaseWeapon
    {
        [Header("Weapon Ammo", order = 2)]
        [SerializeField] private BaseAmmo m_Ammo;

        protected override void CreateAmmo()
        {
            var ammo = Instantiate(m_Ammo, m_ShootTransform.position, m_ShootTransform.rotation, null);
            ammo.Create(Damage);
        }
    }
}
