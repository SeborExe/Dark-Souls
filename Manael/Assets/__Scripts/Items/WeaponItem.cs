using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("One Hand attack animations")]
        public string OH_Light_attack_01;//One hand light attack one
        public string OH_Light_attack_02;
        public string OH_Heavy_attack_01;
        public string OH_Heavy_attack_02;
    }
}

