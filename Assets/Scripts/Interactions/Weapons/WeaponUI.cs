using System.Collections;
using UnityEngine;

namespace YourName.SurvivalShooter.Interactions
{
    public class WeaponUI : MonoBehaviour
    {
        private void OnEnable()
        {
            PlayerStatus.Get.Shooting.enabled = false;
            PlayerStatus.Get.Movement.enabled = false;
        }

        private void OnDisable()
        {
            PlayerStatus.Get.Shooting.enabled = true;
            PlayerStatus.Get.Movement.enabled = true;
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape)) gameObject.SetActive(false);
        }

    }
}
