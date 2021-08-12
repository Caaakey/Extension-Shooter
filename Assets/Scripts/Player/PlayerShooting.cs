using UnityEngine;
using YourName.SurvivalShooter.Weapons;

namespace YourName.SurvivalShooter.Characters
{
    public class PlayerShooting : MonoBehaviour
    {
        public BaseWeapon CurrentWeapon
        {
            get => PlayerStatus.Get.Inventory.Weapon;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0)) CurrentWeapon.Shoot();
            if (Input.GetKeyDown(KeyCode.R)) CurrentWeapon.Reload();

        }
    }
}
