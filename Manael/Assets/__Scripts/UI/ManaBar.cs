using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SH
{
    public class ManaBar : MonoBehaviour
    {
        public Slider slider;

        public void SetMaxMana(float maxMana)
        {
            slider.maxValue = maxMana;
            slider.value = maxMana;
        }

        public void SetCurrentMana(float currentMana)
        {
            slider.value = currentMana;
        }
    }

}
