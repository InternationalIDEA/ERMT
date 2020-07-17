using ThinkGeo.MapSuite.DesktopEdition;

namespace Idea.ERMT
{
    public class ExtentInteractiveOverlayForContext : ExtentInteractiveOverlay
    {
        private bool _contextMenuJustClosed = false;

        public bool ContextMenuJustClosed
        {
            get { return _contextMenuJustClosed; }
            set { _contextMenuJustClosed = value; }
        }

        protected override InteractiveResult MouseDownCore(InteractionArguments interactionArguments)
        {
            if (!_contextMenuJustClosed)
            {
                return base.MouseDownCore(interactionArguments);
            }

            _contextMenuJustClosed = false;
            return new InteractiveResult();
        }
    }
}
