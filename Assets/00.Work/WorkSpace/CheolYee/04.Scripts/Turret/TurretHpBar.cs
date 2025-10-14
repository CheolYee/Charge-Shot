using System;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Agents;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Turret
{
    public class TurretHpBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private Image hpFill;
        
        private AgentHealth _turretHealth;

        private void Start()
        {
            _turretHealth = GameManager.Instance.TargetTransform.GetComponent<LaserTurret>().HealthComponent;
            _turretHealth.onHit.AddListener(ChangeHealth);
        }

        private void ChangeHealth()
        {
            DOTween.Kill(hpText);
            
            hpText.text = $"HP: {_turretHealth.CurrentHealth}/{_turretHealth.MaxHealth}";
            hpFill.fillAmount = _turretHealth.CurrentHealth / _turretHealth.MaxHealth;
            
            Vector3 textOriginalPos = hpText.rectTransform.localPosition;
            Vector3 fillOriginalScale = hpFill.rectTransform.localScale;
            
            Sequence seq = DOTween.Sequence();

            seq.Append(hpText.rectTransform
                .DOShakePosition(0.2f, strength: new Vector3(5f, 0, 0), vibrato: 10, randomness: 90)
                .SetEase(Ease.OutElastic));

            seq.Join(hpFill.rectTransform
                .DOScale(fillOriginalScale * 1.05f, 0.1f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => hpFill.rectTransform.localScale = fillOriginalScale));

            seq.Play();
        }
    }
}