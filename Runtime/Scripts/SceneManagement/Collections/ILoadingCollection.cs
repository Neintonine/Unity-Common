using System.Collections.Generic;

namespace Common.Runtime.SceneManagement
{
    public interface ILoadingCollection
    {
        bool IgnoreDefaultScenes { get; }
        IEnumerable<string> GetScenes();
    }
}