using System.Collections.Generic;
using _00.Work.Resource.Scripts.Managers;
using _00.Work.Resource.Scripts.SO;
using _00.Work.Scripts.Managers;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Enemies;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Portals;
using UnityEngine;
using IPoolable = _00.Work.Scripts.SO.IPoolable;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Managers
{
    public enum WaveState
    {
        Idle,       // 쉬는 중
        Spawning,   // 웨이브 진행 중 (스폰 중/적 살아있음)
        Finished    // 웨이브 종료
    }   
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [SerializeField] private PoolItem portalItemPrefab; // 풀링할 포탈
        [SerializeField] private List<PortalDataSo> portalData;    // 포탈 설정 데이터
        [SerializeField] private Transform portalSpawnPoint;
        [SerializeField] private int maxSpawnCount;    //최대 스폰 카운트
        
        public WaveState CurrentWaveState { get; private set; } = WaveState.Idle;
        
        
        private bool _isBossSpawn;
        
        private Transform _targetTrm;
        private Portal _portal;


        public List<Enemy> Enemys { get; set; } = new();
        

        void Start()
        {
            _targetTrm = GameManager.Instance.TargetTransform;
            SpawnWave();
            
            GameManager.Instance.NextWave += NextWave;
        }   

        public bool CanSpawn()
        {
            return Enemys?.Count < maxSpawnCount;
        }

        public void IsLastEnemy()
        {
            if (Enemys?.Count == maxSpawnCount)
            {
                _portal.ClosePortal();
                GameManager.Instance.Finish();
                CurrentWaveState = WaveState.Finished;
            }
        }

        public void NextWave()
        {
            if (CurrentWaveState == WaveState.Finished)
            {
                Enemys.Clear();
                CurrentWaveState = WaveState.Spawning;
                SpawnWave();
            }
        }

        public void SpawnWave()
        {
            ClearAllEnemies();

            if (portalData?.Count < GameManager.Instance.currentWave - 1)
            {
                Debug.LogError("유효한 포탈 데이터가 다음에 존재하지 않습니다.");
                return;
            }
            
            maxSpawnCount = portalData[GameManager.Instance.currentWave - 1].enemyCount;
            
            Debug.Assert(portalSpawnPoint, "Portal spawn point is invalid");
            
            Portal portal = PoolManager.Instance.Pop(portalItemPrefab.poolName) as Portal;
            if (portal != null)
            {
                portal.transform.position = portalSpawnPoint.position;
                portal.Initialize(portalData[GameManager.Instance.currentWave - 1], true);
                
                _portal = portal;
            }
        }
        

        public void ClearAllEnemies()
        {
            Enemys.Clear();
            
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None); // 씬에 있는 모든 Enemy 컴포넌트 검색
            foreach (Enemy enemy in enemies)
            {
                if (enemy.TryGetComponent(out IPoolable pool))
                {
                    PoolManager.Instance.Push(pool);
                }
            }
        }
    }
}
