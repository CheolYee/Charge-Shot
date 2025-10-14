using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace _00.Work.WorkSpace.Lusalord._02.Script
{
    public class GlitchEffect : ScriptableRenderPass
    {
        private Material _material;
        private RTHandle _tempTexture;

        public GlitchEffect(Material material)
        {
            _material = material;
            renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
        }

        [Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            // 카메라 타겟을 RTHandle로 가져오기
            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
            descriptor.depthBufferBits = 0;
            RenderingUtils.ReAllocateIfNeeded(ref _tempTexture, descriptor, name: "_TempCyberpunkTex");
        }

        [Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (_material == null) return;

            CommandBuffer cmd = CommandBufferPool.Get("CyberpunkDeathEffect");

            // 🔹 Unity 6에서는 Blit 대신 Blitter 사용
            Blitter.BlitCameraTexture(cmd, renderingData.cameraData.renderer.cameraColorTargetHandle, _tempTexture, _material, 0);
            Blitter.BlitCameraTexture(cmd, _tempTexture, renderingData.cameraData.renderer.cameraColorTargetHandle);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if (_tempTexture != null)
                _tempTexture.Release();
        }
    }
}
