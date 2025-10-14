using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _00.Work.WorkSpace.Lusalord._02.Script.Main
{
    public class UIFlip : MonoBehaviour
    {
        public bool isRotated;
        [SerializeField] private RectTransform visual;

        public event Action<bool> OkJunja;
        
        private void Start()
        {
            isRotated = false;
        }

        public void RandomRotation()
        {
            int randomIndex = Random.Range(0, 2);
            if (randomIndex == 1) isRotated = true;
            else isRotated = false;
        }

        private void FixedUpdate()
        {
            float targetY = isRotated ? 180f : 0f;
            visual.localRotation = Quaternion.Euler(0f, targetY, 0f);
        }

        public void OnButtonClicked()
        {
            isRotated = !isRotated;
            OkJunja?.Invoke(isRotated);
        }
    }
}
