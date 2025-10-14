using System;
using _00.Work.Resource.Scripts.Managers;
using _00.Work.Resource.Scripts.SO;
using _00.Work.Scripts.Managers;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Agents;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Core;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Core.Effects;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Managers;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Projectiles.Laser;
using _00.Work.WorkSpace.CheolYee._04.Scripts.UI;
using _00.Work.WorkSpace.Lusalord._02.Script.Main;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Turret
{
    public class LaserTurret : Agent
    {
        [Header("Turret Settings")]
        [field: SerializeField] public CapacitorData CapacitorData { get; private set; }
        
        [Header("Laser Turret")]
        [SerializeField] private PlayerInputSo playerInput;
        [SerializeField] private Transform firePos;
        [SerializeField] private PoolItem laserPrefab;
        [SerializeField] private PoolItem laserEffectPrefab;
        
        [Header("Charge Settings")]
        [SerializeField] private ChargeBarUI chargeBarUI;
        [SerializeField] private float maxDamage;
        [SerializeField] private float minDamage;
        [SerializeField] private float chargeCooldown = 5;
        [SerializeField] private float maxChargeTime = 3;
        [SerializeField] private float maxChargeSpeed = 2;
        
        public bool IsCapacitorOk { get; private set; }
        public event Action NoCapacitor;
        
        private bool _firstCharge = true;
        
        private float _chargeTime;
        private float _lastChargeTime;
        private EffectPlayerSystem _currentEffect;
        private SpriteRenderer _spriteRenderer;

        protected override void Awake()
        {
            base.Awake();            
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _lastChargeTime = -chargeCooldown;
        }

        private void Start()
        {
            SettingMachineState.Instance.OkCapacitor += SetIsCapacitorOk;
            chargeBarUI.UpdateBar(_chargeTime/maxChargeTime);
            InsertingCapacitorData();

            HealthComponent.Initialize(400, this);
        }

        private void SetIsCapacitorOk(bool isCapacitorOk)
        {
            IsCapacitorOk = isCapacitorOk;
            Debug.Log(IsCapacitorOk);
        }

        private void InsertingCapacitorData()
        {
            maxDamage = CapacitorData.maxDamage;
            minDamage = CapacitorData.minDamage;
            chargeCooldown = CapacitorData.chargeCooldown;
            maxChargeTime = CapacitorData.maxChargeTime;
        }

        public void SetDead()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            chargeBarUI.UpdateBar(_chargeTime/maxChargeTime);
            
            if (playerInput.IsSpace && Time.time >= chargeCooldown + _lastChargeTime)
            {
                if (IsCapacitorOk == false)
                {
                    NoCapacitor?.Invoke();
                    return;
                }
                
                
                if (_currentEffect == null && IsCapacitorOk)
                {
                    if (_firstCharge)
                    {
                        SoundManager.Instance.PlaySfx("CHARGE");
                        _firstCharge = false;
                    }
                    
                    _currentEffect = PoolManager.Instance.Pop(laserEffectPrefab.poolName) as EffectPlayerSystem;
                    if (_currentEffect != null)
                    {
                        _currentEffect.SetPosAndPlay(firePos.position, CapacitorData.color);
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
                    _firstCharge = true;
                    FireLaser();
                }
                else
                {
                    CameraManager.Instance.StopShake();
                    _firstCharge = true;
                }
                
                _chargeTime = 0;
            }
            
            
        }

        private void FireLaser()
        {
            float chargeRatio = _chargeTime / maxChargeTime;
            float damage = Mathf.Lerp(minDamage, maxDamage, chargeRatio);
            float knockback = Mathf.Lerp(1f, 5f, chargeRatio);

            
            Laser laser = PoolManager.Instance.Pop(laserPrefab.poolName) as Laser;

            if (laser != null) laser.Initialize(firePos, firePos.right, damage, 
                knockback, 0, CapacitorData.color);
            
            _lastChargeTime = Time.time;
            SettingMachineState.Instance.ResetSwitchMode();
        }

        public void ChangeCapacitor(CapacitorData capacitorData)
        {
            CapacitorData = capacitorData;
            _spriteRenderer.color = CapacitorData.color;
        }
    }
}