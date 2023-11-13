using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FullScreenRenderFeature : ScriptableRendererFeature
{
    public Material material;

    class CustomRenderPass : ScriptableRenderPass
    {
        private RenderTargetIdentifier sourceTexture;
        RTHandle tempTexture;
        private Material material;

        public CustomRenderPass(Material material) : base()
        {
            this.material = material;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            sourceTexture = renderingData.cameraData.renderer.cameraColorTarget;
            tempTexture = RTHandles.Alloc(new RenderTargetIdentifier("_TempTexture"), name: "_TempTexture");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer commandBuffer = CommandBufferPool.Get("Full Screen Render Feature");

            RenderTextureDescriptor targetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            targetDescriptor.depthBufferBits = 0;
            commandBuffer.GetTemporaryRT(Shader.PropertyToID(tempTexture.name), targetDescriptor, FilterMode.Bilinear);

            Blit(commandBuffer, sourceTexture, tempTexture, material);
            Blit(commandBuffer, tempTexture, sourceTexture);

            context.ExecuteCommandBuffer(commandBuffer);
            CommandBufferPool.Release(commandBuffer);
        }

        // Cleanup any allocated resources that were created during the execution of this render pass.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            tempTexture.Release();
        }
    }

    CustomRenderPass m_ScriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        m_ScriptablePass = new CustomRenderPass(material);

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(m_ScriptablePass);
    }
}