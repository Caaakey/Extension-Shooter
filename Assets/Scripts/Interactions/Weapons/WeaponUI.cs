using System.Collections;
using UnityEngine;

namespace YourName.SurvivalShooter.Interactions
{
    public class WeaponUI : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Text m_MoneyText;
        private WeaponItemObject[] m_ItemObjects = null;

        private void Awake()
        {
            m_ItemObjects = GetComponentsInChildren<WeaponItemObject>();
        }

        public void OnEnable()
        {
            PlayerStatus.Get.Shooting.enabled = false;
            PlayerStatus.Get.Movement.enabled = false;

            Refresh();
        }

        public void OnDisable()
        {
            PlayerStatus.Get.Shooting.enabled = true;
            PlayerStatus.Get.Movement.enabled = true;
        }

        public void Refresh()
        {
            m_MoneyText.text = $"Money : {PlayerStatus.Get.Money}";
            for (int i = 0; i < m_ItemObjects.Length; ++i)
                m_ItemObjects[i].OnEnable();
        }

    }
}
