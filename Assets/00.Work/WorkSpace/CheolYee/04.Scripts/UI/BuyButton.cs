using System;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Managers;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Turret;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.UI
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private Image lockImg;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private CapacitorData capacitorData;

        
        private LaserTurret _laserTurret;
        private Button _buyButton;
        
        public event Action<int, bool> Buy;
        
        private void Awake()
        {
            _buyButton = GetComponentInChildren<Button>();
            priceText.text = $"{capacitorData.price:N0}$";
            _buyButton.onClick.AddListener(BuyAction);
            lockImg.gameObject.SetActive(false);
        }

        private void Start()
        {
            _laserTurret = GameManager.Instance.TargetTransform.GetComponent<LaserTurret>();
        }

        private bool BuyEvent()
        {
            if (MoneyManager.Instance.Money >= capacitorData.price)
            {
                Debug.Log("구매 완료");
                MoneyManager.Instance.ChangeMoney(-capacitorData.price);
                _laserTurret.ChangeCapacitor(capacitorData);
                _buyButton.interactable = false;
                return true;
            }
            Debug.Log("구매 실패");
            return false;
        }

        public void Lock()
        {
            lockImg.gameObject.SetActive(true);
        }

        private void BuyAction()
        {
            Buy?.Invoke(capacitorData.id, BuyEvent());
        }
    }
}