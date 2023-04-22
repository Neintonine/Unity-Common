using System.Collections.Generic;
using UnityEngine;

namespace Common.Runtime.Animation
{
    [AddComponentMenu("Animation/Animator Arguments")]
    [RequireComponent(typeof(Animator))]
    public sealed class AnimatorArguments : MonoBehaviour
    {
        private readonly List<AnimatorArgumentData> _data = new();

        public AnimatorArgumentData[] GetAllValues()
        {
            return this._data.ToArray();
        }
        
        public void AddObjects(params AnimatorArgumentData[] data)
        {
            this._data.AddRange(data);
        }

        public AnimatorArgumentData[] GetObject(string objectName)
        {
            return this._data.FindAll(a => a.Name == objectName).ToArray();
        }

        public AnimatorArgumentData<T> GetObject<T>(string objectName)
        {
            AnimatorArgumentData data = this._data.Find(a => a.Name == objectName && a.Object is T);
            return data as AnimatorArgumentData<T>;
        }
        
        public T GetValue<T>(string objectName)
        { 
            AnimatorArgumentData<T> obj = this.GetObject<T>(objectName);
            return obj.Data;
        }

        public AnimatorArgumentData<T> Set<T>(string objectName, T value)
        {
            AnimatorArgumentData<T> obj = this.GetObject<T>(objectName);
            if (obj == null)
            {
                obj = new AnimatorArgumentData<T>(name: objectName, value);
                this._data.Add(obj);
                return obj;
            }

            obj.Data = value;
            return obj;
        }
    }
}