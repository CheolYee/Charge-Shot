using System;
using _00.Work.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.WorkSpace.Lusalord._02.Script.Main
{
    public class SettingMachineState : MonoSingleton<SettingMachineState>
    {
        [SerializeField] private RectTransform machineUI;

        private SwitchMode SwitchMode { get; set; }
        private UIFlip UIFlip { get; set; }
        
        public event Action<bool> OkCapacitor;

        private bool _isSwitch;
        private bool _isJunja;
        protected override void Awake()
        {
            base.Awake();
            SwitchMode = GetComponentInChildren<SwitchMode>();
            UIFlip = GetComponentInChildren<UIFlip>();

            SwitchMode.OnSwitch += onSwitch =>
            {
                _isSwitch = onSwitch;
                IsBothSuccess();
            };
            UIFlip.OkJunja += onJunja =>
            {
                _isJunja = onJunja;
                IsBothSuccess();  
            };
        }

        private void IsBothSuccess()
        {
            if (_isSwitch && _isJunja)
            {
                OkCapacitor?.Invoke(true);
            }
            else
            {
                OkCapacitor?.Invoke(false);
            }
        }
        
        
        public void ResetSwitchMode()
        {
            SwitchMode.ResetSwitch();
            UIFlip.RandomRotation();
            IsBothSuccess();
        }

        private void Start()
        {
            machineUI.localScale = new Vector3(0, 0, 1);
        }
    
        public void OpenMachineUI()
        {
            machineUI.localScale = new Vector3(0, 0.01f, 1);
            machineUI.DOScaleX(0.7f, 0.5f).OnComplete(OnOpenX);
        }

        private void OnOpenX()
        {
            machineUI.DOScaleY(0.7f, 0.5f);
        }

        public void CloseMachineUI()
        {
            machineUI.DOScaleY(0.01f, 0.5f).OnComplete(OnCloseY);
        }

        private void OnCloseY()
        {
            machineUI.DOScaleX(0, 0.5f);
        }
    }
}