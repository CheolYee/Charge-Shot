using _00.Work.WorkSpace.CheolYee._04.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.UI
{
    public class WaveUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI waveText;
        
        [Header("Next Wave Btn")]
        [SerializeField] private Button waveButton;
        [SerializeField] private RectTransform waveButtonRect;
        [SerializeField] private float duration = 0.4f;
        [SerializeField] private float offsetX = 300;
        
        private Vector2 _startPos;
        private Sequence _sequence;

        private bool _isShowing;
        
        private void Awake()
        {
            waveButton.onClick.AddListener(StartNextWave);
            _startPos = waveButtonRect.anchoredPosition;
            waveButtonRect.anchoredPosition = _startPos + new Vector2(offsetX, 0);
        }

        private void StartNextWave()
        {
            GameManager.Instance.StartNextWave();
            HideButton();
        }

        private void HideButton()
        {
            waveButton.interactable = false;
            _isShowing = false;
            
            _sequence.Kill();
            
            _sequence = DOTween.Sequence();
            _sequence.Append(waveButtonRect.DOAnchorPos(_startPos + new Vector2(offsetX, 0), duration).SetEase(Ease.OutCubic));
            
        }

        private void Start()
        {
            GameManager.Instance.FinishWave += ShowButton;
            GameManager.Instance.NextWave += ChangeWaveText;
            ChangeWaveText();
        }

        private void ChangeWaveText()
        {
            waveText.text = $"Wave {GameManager.Instance.currentWave}";
        }

        private void ShowButton()
        {
            if (_isShowing) return;
            
            _isShowing = true;
            
            _sequence?.Kill();
            waveButton.interactable = true;
            
            waveButtonRect.anchoredPosition = _startPos + new Vector2(offsetX, 0);
            
            _sequence = DOTween.Sequence();
            _sequence.Append(waveButtonRect.DOAnchorPos(_startPos, duration).SetEase(Ease.OutCubic));
        }
    }
}