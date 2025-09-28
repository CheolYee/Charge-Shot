using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class SettingMachineState : MonoBehaviour
{
    [SerializeField] private RectTransform machineUI;
    private bool _canState; // UI를 킬 수 있는지 상태
    private bool _isPlaying;

    private void Start()
    {
        machineUI.localScale = new Vector2(0, 0);
    }

    private void Update()
    {
        SetMachineUI();
    }

    private void SetMachineUI()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame && !_isPlaying)
        {
            _canState = !_canState;

            if (_canState)
            {
                OpenMachineUI();
            }
            else
            {
                CloseMachineUI();
            }
        }
    }
    private void OpenMachineUI()
    {
        _isPlaying = true;
        machineUI.localScale = new Vector2(0, 0.01f);
        machineUI.DOScaleX(1, 0.5f).OnComplete(OnOpenX);
    }

    private void OnOpenX()
    {
        _isPlaying = false;
        machineUI.DOScaleY(1, 0.5f);
    }

    private void CloseMachineUI()
    {
        _isPlaying = true;
        machineUI.DOScaleY(0.01f, 0.5f).OnComplete(OnCloseY);
    }

    private void OnCloseY()
    {
        _isPlaying = false;
        machineUI.DOScaleX(0, 0.5f);
    }
}