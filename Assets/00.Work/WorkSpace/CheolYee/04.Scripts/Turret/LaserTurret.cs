using System;
using _00.Work.Resource.Scripts.Managers;
using _00.Work.Resource.Scripts.SO;
using _00.Work.Scripts.Managers;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Core;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Core.Effects;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Managers;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Projectiles.Laser;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Turret
{
    public class LaserTurret : MonoBehaviour
    {
        [Header("Laser Turret")]
        [SerializeField] private PlayerInputSo playerInput;
        [SerializeField] private Transform firePos;
        [SerializeField] private PoolItem laserPrefab;
        [SerializeField] private PoolItem laserEffectPrefab;
        
        [Header("Charge Settings")]
        [SerializeField] private float chargeCooldown = 5;
        [SerializeField] private float maxChargeTime = 3;
        [SerializeField] private float maxChargeSpeed = 2;
        
        private float _chargeTime;
        private float _lastChargeTime;
        private EffectPlayerSystem _currentEffect;

        private void Awake()
        {
            _lastChargeTime = -chargeCooldown;
        }

        private void Update()
        {
            if (playerInput.IsSpace && Time.time >= chargeCooldown + _lastChargeTime)
            {
                if (_currentEffect == null)
                {
                    _currentEffect = PoolManager.Instance.Pop(laserEffectPrefab.poolName) as EffectPlayerSystem;
                    if (_currentEffect != null)
                    {
                        _currentEffect.SetPosAndPlay(firePos.position);
                    }
                }
                else
                {
                    float ratio = Mathf.Clamp01(_chargeTime / maxChargeTime);
                    CameraManager.Instance.StartShake(ratio * 2, 2);
                    var main = _currentEffect.ParticleSystem.main;
                    main.simulationSpeed = Mathf.Lerp(1, maxChargeSpeed, ratio);
                }
                
                _chargeTime += Time.deltaTime;
                _chargeTime = Mathf.Clamp(_chargeTime, 0, maxChargeTime);
            }
            else
            {
                _currentEffect?.GoToPoolOfEffect();
                _currentEffect = null;
                
                if (_chargeTime >= maxChargeTime)
                {
                    FireLaser();
                }
                else
                {
                    CameraManager.Instance.StopShake();
                }
                
                _chargeTime = 0;
            }
        }

        private void FireLaser()
        {
            float chargeRatio = _chargeTime / maxChargeTime;
            float damage = Mathf.Lerp(10f, 50f, chargeRatio);
            float knockback = Mathf.Lerp(1f, 5f, chargeRatio);

            
            Laser laser = PoolManager.Instance.Pop(laserPrefab.poolName) as Laser;

            if (laser != null) laser.Initialize(firePos, firePos.right, damage, knockback, 0);
            
            _lastChargeTime = Time.time;
        }
    }
}