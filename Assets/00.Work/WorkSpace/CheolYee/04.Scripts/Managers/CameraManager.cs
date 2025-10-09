using _00.Work.Scripts.Managers;
using Unity.Cinemachine;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Managers
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        private CinemachineCamera _camera;
        private CinemachineBasicMultiChannelPerlin _perlin;

        protected override void Awake()
        {
            base.Awake();
            _camera = GetComponent<CinemachineCamera>();
            _perlin = _camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void StartShake(float shakeDuration, float shakeIntensity)
        {
            _perlin.AmplitudeGain = shakeDuration;
            _perlin.FrequencyGain = shakeIntensity;
        }

        public void StopShake()
        {
            _perlin.AmplitudeGain = 0;
            _perlin.FrequencyGain = 0;
        }
    }
}