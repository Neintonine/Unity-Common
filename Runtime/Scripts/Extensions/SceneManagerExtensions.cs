using UnityEngine.SceneManagement;

namespace Common.Runtime.Extensions
{
    public class SceneManagerExtensions
    {
        public static bool IsSceneLoaded(string name)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == name) return true;
            }

            return false;
        }
        public static bool IsSceneLoaded(int buildIndex)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.buildIndex == buildIndex) return true;
            }

            return false;
            
        } 
    }
}