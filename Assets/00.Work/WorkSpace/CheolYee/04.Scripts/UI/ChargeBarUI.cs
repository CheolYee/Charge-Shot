using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.UI
{
    public class ChargeBarUI : MonoBehaviour
    {
        [SerializeField] private Image chargeBar;
        [SerializeField] private TextMeshProUGUI chargeText;
        [SerializeField] private Color startColor;
        [SerializeField] private Color endColor;

        private float _currentFill;

        public void UpdateBar(float fill)
        {
            chargeText.text = $"{fill * 100:N0}%";
            
            _currentFill = Mathf.Clamp01(fill);
            chargeBar.fillAmount = _currentFill;
            
            chargeBar.color = Color.Lerp(startColor, endColor, _currentFill);
        }
    }
}