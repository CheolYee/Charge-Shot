using System;
using _00.Work.Resource.Scripts.Managers;
using _00.Work.Resource.Scripts.UI;
using _00.Work.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.UI
{
    public class GameClearUI : MonoBehaviour
    {
        [SerializeField] private Button leaveButton;

        private void Awake()
        {
            leaveButton.onClick.AddListener(LeaveGame);
        }

        public void GameClear()
        {
            Time.timeScale = 0;
        }

        private void LeaveGame()
        {
            FadeManager.Instance.FadeToScene(0);
        }
    }
}