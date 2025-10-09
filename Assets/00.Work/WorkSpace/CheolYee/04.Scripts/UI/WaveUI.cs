using _00.Work.WorkSpace.CheolYee._04.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.UI
{
    public class WaveUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private Button waveButton;


        private void Awake()
        {
            waveButton.onClick.AddListener(StartNextWave);
            HideButton();
        }

        private void StartNextWave()
        {
            GameManager.Instance.StartNextWave();
            HideButton();
        }

        private void HideButton()
        {
            waveButton.gameObject.SetActive(false);
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
            waveButton.gameObject.SetActive(true);
        }
    }
}