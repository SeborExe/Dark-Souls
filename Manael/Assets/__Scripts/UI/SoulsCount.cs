using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SH
{
    public class SoulsCount : MonoBehaviour
    {
        public Text soulsCountText;

        public void SetSoulsCountText(int soulsCount)
        {
            soulsCountText.text = soulsCount.ToString();
        }
    }
}

