using System;
using _00.Work.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Managers
{
    public class MoneyManager : MonoSingleton<MoneyManager>
    {
        [field: SerializeField] public int Money { get; private set; }
        
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI moneyText;

        private void Start()
        {
            moneyText.text = $"{Money}$";
        }

        public void ChangeMoney(int money)
        {
            Money += money;
            ChangeMoneyText();
        }

        public void ChangeMoneyText()
        {
            DOTween.Kill(moneyText);
            
            Sequence seq = DOTween.Sequence();
            seq.Append(moneyText.rectTransform.DOScale(1.1f, 0.15f).SetEase(Ease.OutBack));
            seq.Append(moneyText.rectTransform.DOScale(1f, 0.15f).SetEase(Ease.InBack));
            moneyText.text = $"{Money:N0}";
        }
        
    }
}