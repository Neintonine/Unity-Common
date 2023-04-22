using System;
using Common.Runtime.Time;
using JetBrains.Annotations;
using UnityEngine;

namespace Common.Runtime.Animation
{
    public class AnimatorArgumentData
    {
        [field: SerializeField]
        public string Name { get; set; }
        public object Object { get; set; }

        public AnimatorArgumentData()
        {
            this.Name = "";
            this.Object = null;
        }
        
        public AnimatorArgumentData(
            string name,
            object obj
        )
        {
            this.Name = name;
            this.Object = obj;
        }
    }

    [Serializable]
    public sealed class AnimatorArgumentDataEntry : AnimatorArgumentData
    {
        public UnityEngine.Object Data
        {
            get => this._data;
            set => this._data = value;
        }

        [SerializeField] private UnityEngine.Object _data;

        public AnimatorArgumentDataEntry(string name, UnityEngine.Object obj) : base(name, null)
        {
            this._data = obj;
        }
    }

    public sealed class AnimatorArgumentData<T> : AnimatorArgumentData
    {
        public T Data
        {
            get => (T)this.Object;
            set => this.Object = value;
        }

        public AnimatorArgumentData(string name, T obj) : base(name, obj)
        { }

        public void SetDataLerp(T toValue, float speed = 1)
        {
            speed *= TimeScale.Unity.DeltaTime;

            switch (toValue)
            {
                case float value:
                    this.Object = Mathf.Lerp((float)this.Object, value, speed);
                    return;
                case Vector2 value:
                    this.Object = Vector2.Lerp((Vector2)this.Object, value, speed);
                    return;
                case Vector3 value:
                    this.Object = Vector3.Lerp((Vector3)this.Object, value, speed);
                    return;
                case Vector4 value:
                    this.Object = Vector4.Lerp((Vector4)this.Object, value, speed);
                    return;
            }
        }
    }
}