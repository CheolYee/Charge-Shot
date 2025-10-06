using System;
using UnityEngine;

namespace _00.Work.WorkSpace.Lusalord._02.Script.Main
{
    public class UIFlip : MonoBehaviour
    {
        public bool isRotated ;
        [SerializeField] private RectTransform visual;

        private void Start()
        {
            isRotated = false;
        }

        private void FixedUpdate()
        {
            float targetY = isRotated ? 180f : 0f;
            visual.localRotation = Quaternion.Euler(0f, targetY, 0f);
        }

        public void OnButtonClicked()
        {
            isRotated = !isRotated;
        }
    }
}
