using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Common.Runtime.Animation
{
    [AddComponentMenu("Animation/Animator Arguments")]
    [RequireComponent(typeof(Animator))]
    public sealed class AnimatorArguments : MonoBehaviour
    {
        private List<AnimatorArgumentData> _data = new();

        public void AddObjects(params AnimatorArgumentData[] data)
        {
            this._data.AddRange(data);
        }

        public AnimatorArgumentData[] GetObject(string name)
        {
            return this._data.FindAll(a => a.Name == name).ToArray();
        }

        public AnimatorArgumentData<T> GetObject<T>(string name)
        {
            AnimatorArgumentData data = this._data.Find(a => a.Name == name && a.Object is T);
            return data as AnimatorArgumentData<T>;
        }
        
        public T GetValue<T>(string name)
        { 
            AnimatorArgumentData<T> obj = GetObject<T>(name);
            return obj.Data;
        }

        public AnimatorArgumentData<T> Set<T>(string name, T value)
        {
            AnimatorArgumentData<T> obj = GetObject<T>(name);
            if (obj == null)
            {
                obj = new AnimatorArgumentData<T>(name: name, value);
                return obj;
            }

            obj.Data = value;
            return obj;
        }
    }
}