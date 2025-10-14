using System;
using UnityEngine;

namespace _00.Work.WorkSpace.Lusalord._02.Script.Main
{
    public class SwitchMode : MonoBehaviour
    {
        public bool switchMode;

        public GameObject switchOn;
        public GameObject switchOff;
        public event Action<bool> OnSwitch;

        private void Start()
        {
            switchMode = false; // off 상태로 시작
        }

        private void FixedUpdate()
        {
            switchOn.SetActive(switchMode); // On 인거 넣어야함
            switchOff.SetActive(!switchMode); // Off 인거 넣어야함
        }

        public void ResetSwitch()
        {
            switchMode = false;
            OnSwitch?.Invoke(switchMode);
        }


        public void OnClickSwitch()
        {
            switchMode = !switchMode;
            OnSwitch?.Invoke(switchMode);
        }
    }
}
