using System;
using _00.Work.Resource.Scripts.Managers;
using _00.Work.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.UI
{
    public class StartUI : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
        }

        private void Start()
        {
            SoundManager.Instance.PlayBgm("START");
        }

        private void StartGame()
        {
            FadeManager.Instance.FadeToScene(1);
        }
    }
}