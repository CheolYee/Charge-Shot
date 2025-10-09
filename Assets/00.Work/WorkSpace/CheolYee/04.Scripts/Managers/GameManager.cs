using System;
using _00.Work.Scripts.Managers;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [field: SerializeField] public Transform TargetTransform { get; private set; }
        public int currentWave = 1;
        public event Action NextWave;
        public event Action FinishWave;


        public void Finish()
        {
            FinishWave?.Invoke();
        }

        public void StartNextWave()
        {
            currentWave++;
            NextWave?.Invoke();
        }
    }
}