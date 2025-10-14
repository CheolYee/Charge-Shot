using System;
using _00.Work.Scripts.Managers;
using _00.Work.WorkSpace.CheolYee._04.Scripts.UI;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [field: SerializeField] public Transform TargetTransform { get; private set; }
        [SerializeField] private GameClearUI gameClearUI;
        public int currentWave = 1;
        public event Action NextWave;
        public event Action FinishWave;

        private void Start()
        {
            SoundManager.Instance.PlayBgm("Main");
        }

        public void Finish()
        {
            if (currentWave == 10)
            {
                gameClearUI.gameObject.SetActive(true);
                gameClearUI.GameClear();
                return;
            }
            
            FinishWave?.Invoke();
        }

        public void StartNextWave()
        {
            currentWave++;
            NextWave?.Invoke();
        }
    }
}