using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private BuyButton[] buyButtons;

        private void Start()
        {
            foreach (BuyButton buyButton in buyButtons)
            {
                buyButton.Buy += Buy;
            }
        }

        private void Buy(int id, bool state)
        {
            if (state)
            {
                for (int i = 0; i <= id; i++)
                {
                    buyButtons[i].Lock();
                }
            }
            else
            {
                Debug.Log("살 수 없습니다.");
            }
        }
    }
}