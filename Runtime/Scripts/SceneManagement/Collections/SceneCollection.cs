using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SceneManagement.Runtime;
using UnityEngine;

namespace Common.Runtime.SceneManagement.Collections
{
    [CreateAssetMenu(fileName = "Scene Collection", menuName = "Scene Collection", order = 0)]
    public class SceneCollection : ScriptableObject, ILoadingCollection
    {
        public bool IgnoreDefaultScenes => _ignoreDefaultScenes;
        [SerializeField] private bool _ignoreDefaultScenes;
        [SerializeField] private List<SceneReference> _scenes;
        public IEnumerable<string> GetScenes()
        {
            return _scenes.Select(a => a.ScenePath);
        }

        public void Load()
        {
            LoadingManager.LoadScene(this);
        }
        
#if UNITY_EDITOR

        public void LoadEditor()
        {
            
        }
        
#endif
    }
}