#if UNITY_EDITOR
namespace NotActual_Dev.OverlayCanvasSorting
{
    public interface IEditorDisposable_OCS
    {
        internal void DisposeIfRedundant();
        internal void Dispose();
    }
}
#endif