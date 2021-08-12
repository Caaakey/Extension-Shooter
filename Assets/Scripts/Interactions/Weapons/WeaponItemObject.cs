using UnityEngine;
using UnityEngine.UI;

namespace YourName.SurvivalShooter.Interactions
{
    public class WeaponItemObject : MonoBehaviour
    {
        [SerializeField] private GameObject WeaponPrefab;
        [SerializeField] private Text WeaponInfo;
        [SerializeField] private Image CheckIcon;

        public void OnClick()
        {
            BaseItem item = WeaponPrefab.GetComponent<BaseItem>();
            item.Buy();
        }

    }
}
