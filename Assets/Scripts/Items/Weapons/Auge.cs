using UnityEngine;

namespace YourName.SurvivalShooter.Weapons
{
    public class Auge : BaseWeapon
    {
        public override void Buy()
        {
            if (PlayerStatus.Get.Inventory.Weapon.name == name) return;

            if (PlayerStatus.Get.Inventory[name] != 0)
                PlayerStatus.Get.Inventory.Weapon = this;
            else if (PlayerStatus.Get.Money >= Price)
            {
                PlayerStatus.Get.Money -= Price;
                PlayerStatus.Get.Inventory.Weapon = this;

                var ui = FindObjectOfType<Interactions.WeaponUI>();
                if (ui != null) ui.Refresh();
            }
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
