﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using YourName.SurvivalShooter.Weapons;

namespace YourName.SurvivalShooter.Characters
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private Transform m_WeaponTransform;
        [SerializeField] private BaseWeapon m_Weapon;
        [SerializeField] private BaseAmmo m_Ammo;

        private void Awake()
        {
            if (m_Ammo == null)
                m_Ammo = Resources.Load<GameObject>("Ammos/9mm/9mm").GetComponent<BaseAmmo>();

            if (m_Weapon == null)
                m_Weapon = m_WeaponTransform.GetChild(0).GetComponent<BaseWeapon>();
        }

        public BaseWeapon Weapon
        {
            get => m_Weapon;
            set
            {
                var newWeapon = Instantiate(value, m_WeaponTransform);
                newWeapon.transform.localPosition = Vector3.zero;
                newWeapon.transform.localRotation = Quaternion.identity;
                newWeapon.name = newWeapon.name.Substring(0, newWeapon.name.Length - 7);

                Destroy(m_Weapon.gameObject);
                m_Weapon = newWeapon;
            }
        }

        public BaseAmmo Ammo
        {
            get => m_Ammo;
            set => m_Ammo = value;
        }

    }
}
