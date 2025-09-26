using System;
using System.Collections;
using _00.Work.Scripts.Managers;
using _00.Work.Scripts.SO;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Core.Effects
{
    public class EffectPlayerSystem : MonoBehaviour, IPoolable
    {
        [SerializeField] private string itemName;
        
        public ParticleSystem ParticleSystem { get; set; }
        private float _duration;
        private WaitForSeconds _waitForSeconds;
        public string ItemName => itemName;
        public GameObject GameObject => gameObject;

        private void Awake()
        {
            ParticleSystem = GetComponent<ParticleSystem>();
        }

        public void SetPosAndPlay(Vector3 pos)
        {
            transform.position = pos;
            ParticleSystem.Play();
        }
        
        public void SetPosAndPlay(Vector3 pos, float duration)
        {
            transform.position = pos;
            ParticleSystem.Play();
            StartCoroutine(DelayAndGoToPool());
        }
        
        private IEnumerator DelayAndGoToPool(float duration = 1)
        {
            if (_waitForSeconds == null)
            {
                _waitForSeconds = new WaitForSeconds(duration);
            }
            yield return _waitForSeconds;
            PoolManager.Instance.Push(this);
        }

        public void GoToPoolOfEffect()
        {
            PoolManager.Instance.Push(this);               
        }

        public void ResetItem()
        {
            ParticleSystem.Stop(); //멈추기
            ParticleSystem.Simulate(0); //0초로 되감기 (처음으로 가기)
        }
    }
}