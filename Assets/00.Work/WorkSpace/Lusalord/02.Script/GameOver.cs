using UnityEngine;

public class GameOver : MonoBehaviour
{
    private static readonly int EffectStrength = Shader.PropertyToID("_EffectStrength");
    [SerializeField] private Material deathMaterial;
    private bool _isActive;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _isActive = !_isActive;
            deathMaterial.SetFloat(EffectStrength, _isActive ? 1f : 0f); // 끄기
            // 켜기
        }
    }
}
