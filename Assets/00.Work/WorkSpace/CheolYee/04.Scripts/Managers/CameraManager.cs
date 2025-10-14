using System;
using _00.Work.Scripts.Managers;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Core;
using Unity.Cinemachine;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Managers
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private PlayerInputSo playerInputSo;
        [SerializeField] private Collider2D confinerCollider;
        [SerializeField] private float moveSpeed = 3f;
        
        [Header("Zoom")]
        [SerializeField] private float zoomAmount;
        [SerializeField] private float minOrthographicSize;
        [SerializeField] private float maxOrthographicSize;
        
        private float _orthoSize;
        private float _targetSize;
        
        private CinemachineCamera _camera;
        private CinemachineBasicMultiChannelPerlin _perlin;
        
        private Vector3 _targetPos;
        
        private float _halfWidth;
        private float _halfHeight;

        protected override void Awake()
        {
            base.Awake();
            _camera = GetComponent<CinemachineCamera>();
            _perlin = _camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Start()
        {
            _orthoSize = _camera.Lens.OrthographicSize;
            
            if (Camera.main == null) return;
            
            _halfHeight = Camera.main.orthographicSize;
            _halfWidth = _halfHeight * Camera.main.aspect;
        }

        public void StartShake(float shakeDuration, float shakeIntensity)
        {
            _perlin.AmplitudeGain = shakeDuration;
            _perlin.FrequencyGain = shakeIntensity;
        }

        private void Update()
        {
            _targetSize -= playerInputSo.ScrollDelta * zoomAmount;
            _targetSize = Mathf.Clamp(_targetSize, minOrthographicSize, maxOrthographicSize);
            
            _orthoSize = Mathf.Lerp(_orthoSize, _targetSize, Time.deltaTime * 5);
            _camera.Lens.OrthographicSize = _orthoSize;
        }

        void LateUpdate()
        {
            transform.Translate(playerInputSo.MoveInput * (Time.deltaTime * moveSpeed));
            
            Bounds bounds = confinerCollider.bounds;
            Vector3 pos = transform.position;

            pos.x = Mathf.Clamp(pos.x,
                bounds.min.x + _halfWidth,
                bounds.max.x - _halfWidth);

            pos.y = Mathf.Clamp(pos.y,
                bounds.min.y + _halfHeight,
                bounds.max.y - _halfHeight);

            transform.position = pos;
        }

        public void StopShake()
        {
            _perlin.AmplitudeGain = 0;
            _perlin.FrequencyGain = 0;
        }
    }
}