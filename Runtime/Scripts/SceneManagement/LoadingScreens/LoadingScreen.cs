using UnityEngine;

namespace Common.Runtime.SceneManagement.LoadingScreens
{
    public abstract class LoadingScreen: MonoBehaviour
    {
        public abstract void Show();
        public abstract void Hide();
        public abstract void UpdateScreen(LoadingContext context);
    }
}