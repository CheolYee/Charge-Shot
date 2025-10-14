using System;
using _00.Work.WorkSpace.Lusalord._02.Script.Main;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.UI
{
    public class SelectUI : MonoBehaviour
    {
        [Header("Select Button Elements")]
        [SerializeField] private Button openBtn;
        [SerializeField] private float moveDistance = 150f;
        [SerializeField] private float duration = 0.3f;
        
        [Header("ShopButton Elements")]
        [SerializeField] private Button shopBtn;
        
        [Header("SettingMachineState Elements")]
        [SerializeField] private Button capacitorBtn;
        
        [Header("Window Elements")]
        [SerializeField] private GameObject selectWindow;
        [SerializeField] private GameObject shopWindow;
        [SerializeField] private GameObject capacitorWindow;

        private SettingMachineState _settingMachineState;
        private Vector3 _originalPos;
        private Vector3 _shopOriginalPos;
        private bool _isOpen;
        private bool _isShopOpen;
        private bool _isCapacitorOpen;

        private RectTransform _selectRect;
        private RectTransform _shopRect;
        private Tween _currentTween;
        private Tween _shopTween;

        private void Start()
        {
            _selectRect = selectWindow.GetComponent<RectTransform>();
            _originalPos = _selectRect.localPosition;
            _selectRect.localPosition = _originalPos - new Vector3(0, moveDistance, 0);
            selectWindow.SetActive(false);
            
            _shopRect = shopWindow.GetComponent<RectTransform>();
            _shopOriginalPos = _shopRect.localPosition;
            _shopRect.localPosition = _shopOriginalPos - new Vector3(0, moveDistance, 0);
            shopWindow.SetActive(false);
            
            _settingMachineState = capacitorWindow.GetComponent<SettingMachineState>();
            
            openBtn.onClick.AddListener(OpenSelectGui);
            shopBtn.onClick.AddListener(OpenShopWindow);
            capacitorBtn.onClick.AddListener(OpenCapacitorWindow);
        }

        private void OpenSelectGui()
        {
            // 🔹 다른 창이 열려 있다면 닫기
            if (_isShopOpen)
                CloseShopWindow();

            if (_isCapacitorOpen)
                CloseCapacitorWindow();

            // 🔹 중복 트윈 방지
            if (_currentTween != null && _currentTween.IsActive()) _currentTween.Kill();

            if (!_isOpen)
            {
                selectWindow.SetActive(true);
                _selectRect.localPosition = _originalPos - new Vector3(0, moveDistance, 0);

                _currentTween = _selectRect.DOLocalMoveY(_originalPos.y, duration)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() => _isOpen = true);
            }
            else
            {
                _currentTween = _selectRect.DOLocalMoveY(_originalPos.y - moveDistance, duration)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        _isOpen = false;
                        selectWindow.SetActive(false);
                    });
            }
        }

        private void OpenShopWindow()
        {
            if (_shopTween != null && _shopTween.IsActive()) _shopTween.Kill();

            if (_isOpen)
            {
                _currentTween = _selectRect.DOLocalMoveY(_originalPos.y - moveDistance, duration)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        _isOpen = false;
                        selectWindow.SetActive(false);
                    });
            }

            if (!_isShopOpen)
            {
                shopWindow.SetActive(true);
                _shopRect.localPosition = _shopOriginalPos - new Vector3(0, moveDistance, 0);

                _shopTween = _shopRect.DOLocalMoveY(_shopOriginalPos.y, duration)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() => _isShopOpen = true);
            }
        }

        private void CloseShopWindow()
        {
            if (_shopTween != null && _shopTween.IsActive()) _shopTween.Kill();

            _shopTween = _shopRect.DOLocalMoveY(_shopOriginalPos.y - moveDistance, duration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    _isShopOpen = false;
                    shopWindow.SetActive(false);
                });
        }

        // ✅ SettingMachineState 창 열기
        private void OpenCapacitorWindow()
        {
            if (_isCapacitorOpen) return;

            // Select 창 닫기
            if (_isOpen)
            {
                _currentTween = _selectRect.DOLocalMoveY(_originalPos.y - moveDistance, duration)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        _isOpen = false;
                        selectWindow.SetActive(false);
                    });
            }

            // SettingMachineState 열기
            _settingMachineState.OpenMachineUI();
            _isCapacitorOpen = true;
        }

        // ✅ SettingMachineState 창 닫기
        private void CloseCapacitorWindow()
        {
            if (!_isCapacitorOpen) return;
            
            _settingMachineState.CloseMachineUI();
            _isCapacitorOpen = false;
        }
    }
}
