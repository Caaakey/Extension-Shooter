using UnityEngine;
using UnityEngine.UI;
using YourName.SurvivalShooter.Weapons;

namespace YourName.SurvivalShooter.Interactions
{
    public class WeaponItemObject : MonoBehaviour
    {
        [SerializeField] private GameObject WeaponPrefab;
        [SerializeField] private Text WeaponInfo;
        [SerializeField] private Image CheckIcon;

        public void OnEnable()
        {
            if (string.IsNullOrEmpty(WeaponInfo.text))
            {
                var comp = WeaponPrefab.GetComponent<BaseWeapon>();
                WeaponInfo.text =
                    $"Damage : <b>{comp.Damage}</b>\n" +
                    $"Magzine : <b>{comp.Magazine}</b>\n" +
                    $"Range : <b>{comp.Range}</b>\n" +
                    $"FireDelayTime : <b>{comp.FireDelayTime}</b>\n" +
                    $"ReloadTime : <b>{comp.ReloadTime}</b>\n" +
                    $"\nPrice : <b>{comp.Price}</b>";
            }

            CheckIcon.enabled = PlayerStatus.Get.Inventory[WeaponPrefab.name] != 0;
        }

        public void OnClick()
        {
            BaseItem item = WeaponPrefab.GetComponent<BaseItem>();
            item.Buy();
        }

    }
}
