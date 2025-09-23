using System;
using _00.Work.Scripts.Managers;
using _00.Work.Scripts.SO;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Core;
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
        
        [Header("Charge Settings")]
        [SerializeField] private float maxChargeTime = 3;
        
        private float _chargeTime;


        private void Update()
        {
            if (playerInput.IsSpace)
            {
                _chargeTime += Time.deltaTime;
                Debug.Log($"차징중: {_chargeTime}초");
                _chargeTime = Mathf.Clamp(_chargeTime, 0, maxChargeTime);
            }
            else
            {
                if (_chargeTime >= maxChargeTime)
                {
                    FireLaser();
                }
                
                _chargeTime = 0;
            }
        }

        private void FireLaser()
        {
            float chargeRatio = _chargeTime / maxChargeTime;
            float damage = Mathf.Lerp(10f, 50f, chargeRatio);
            float knockback = Mathf.Lerp(2f, 10f, chargeRatio);

            
            Laser laser = PoolManager.Instance.Pop(laserPrefab.poolName) as Laser;

            if (laser != null) laser.Initialize(firePos, firePos.right, damage, knockback, 0);
        }
    }
}