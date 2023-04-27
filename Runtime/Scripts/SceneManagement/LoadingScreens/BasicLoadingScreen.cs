using UnityEngine;
using UnityEngine.Events;

namespace Common.Runtime.SceneManagement.LoadingScreens
{
    public sealed class BasicLoadingScreen : LoadingScreen
    {
        [SerializeField] private UnityEvent _showEvent;
        [SerializeField] private UnityEvent _hideEvent;
        [SerializeField] private UnityEvent<LoadingContext> _loadingStateChanged;

        public override void Show()
        {
            this.gameObject.SetActive(true);
            this._showEvent.Invoke();
        }

        public override void Hide()
        {
            this._hideEvent.Invoke();
            this.gameObject.SetActive(false);
        }

        public override void UpdateScreen(LoadingContext context)
        {
            this._loadingStateChanged.Invoke(context);
        }
    }
}