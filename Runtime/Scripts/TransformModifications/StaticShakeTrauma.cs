using System;
using UnityEngine;

namespace Common.Runtime.TransformModifications
{
    public class StaticShakeTrauma : MonoBehaviour
    {
        [SerializeField] private float _staticTrauma;
        private ITraumatic[] _traumaObjects;

        private void Awake()
        {
            _traumaObjects = GetComponents<ITraumatic>();
        }

        private void Update()
        {
            foreach (ITraumatic traumaObject in _traumaObjects)
            {
                traumaObject.CurrentStaticTrauma = _staticTrauma;
            }
        }
    }
}