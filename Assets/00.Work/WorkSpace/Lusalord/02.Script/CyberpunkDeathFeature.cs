using _00.Work.WorkSpace.Lusalord._02.Script;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CyberpunkDeathFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public Material material;
    }

    public Settings settings = new Settings();

    private GlitchEffect _pass;

    public override void Create()
    {
        if (settings.material != null)
        {
            _pass = new GlitchEffect(settings.material);
        }
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (_pass != null)
        {
            renderer.EnqueuePass(_pass);
        }
    }
}
