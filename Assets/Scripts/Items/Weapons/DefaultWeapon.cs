using UnityEngine;

namespace YourName.SurvivalShooter.Weapons
{
    public class DefaultWeapon : BaseWeapon
    {
        public override void Buy()
        {
            if (PlayerStatus.Get.Inventory.Weapon.name == name) return;
            PlayerStatus.Get.Inventory.Weapon = this;
        }

        public override void Sell()
        {
            throw new System.NotImplementedException();
        }

        public override void Use()
            => throw new System.NotImplementedException();

        protected override void CreateAmmo()
        {
            var ammo = Instantiate(GetAmmo, m_ShootTransform.position, m_ShootTransform.rotation, null);
            ammo.Create(Damage);
        }
    }
}
