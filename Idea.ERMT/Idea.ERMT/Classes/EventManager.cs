using System;

namespace Idea.ERMT
{
    public static class EventManager
    {
        #region OnModelChanged
        public static event EventHandler OnModelChanged;

        public static void RaiseModelChanged()
        {
            RaiseModelChanged(new object());
        }

        public static void RaiseModelChanged(object sender)
        {
            if (OnModelChanged != null)
            {
                OnModelChanged(sender, null);
            }
        }
        #endregion

        #region OnModelUpdated

        public static event EventHandler OnModelUpdated;

        public static void RaiseModelUpdated()
        {
            RaiseModelUpdated(new object());
        }

        public static void RaiseModelUpdated(object sender)
        {
            if (OnModelUpdated != null)
            {
                OnModelUpdated(sender,new EventArgs());
            }
        }

        #endregion

        #region OnModelDeleted
        public static event EventHandler OnModelDeleted;

        public static void RaiseModelDeleted()
        {
            RaiseModelDeleted(new object());
        }

        public static void RaiseModelDeleted(object sender)
        {
            if (OnModelDeleted != null)
            {
                OnModelDeleted(sender, new EventArgs());
            }
        }
        #endregion
    }
}
