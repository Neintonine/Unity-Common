using System;
using Common.Runtime.Time;
using UnityEngine;
using Random = Common.Runtime.Randomization.Random;

namespace Common.Runtime.TransformModifications
{
    [AddComponentMenu("Transform/Shake")]
    [RequireComponent(typeof(TransformModificationDriver))]
    public class ShakeTransform : MonoBehaviour, ITraumatic
    {
        [Flags]
        enum Axis
        {
            None = 0,
            X = 1,
            Y = 2,
            Z = 4,
            All = X | Y | Z
        }
        
        public float CurrentTrauma => GetCurrentTrauma();

        public float CurrentDynamicTrauma
        {
            get => _currentDynamicTrauma;
            set => SetDynamicTrauma(value);
        }
        
        public float CurrentStaticTrauma 
        {
            get => _currentStaticTrauma;
            set => SetStaticTrauma(value);
        }

        [SerializeField] private float _maxTrauma = 1;
        [SerializeField] private float _exponent = 2;
        [SerializeField] private float _timeScale = 1;
        
        [Header("Translation")]
        [SerializeField] private bool _useTranslation;
        [SerializeField] private Axis _translationAxis;
        [SerializeField] private Vector3 _maxTranslation = Vector3.one;

        [Header("Rotation")] 
        [SerializeField] private bool _useRotation;
        [SerializeField] private Axis _rotationAxis;
        [SerializeField] private Vector3 _maxRotation = Vector3.one;

        private TransformModificationDriver _driver;

        private float _currentDynamicTrauma = 0;
        private float _currentStaticTrauma = 0;

        private Vector3 _multiplyVectorTranslation;
        private Vector3 _multiplyVectorRotation;

        private float _time = 0;

        private void Awake()
        {
            _driver = GetComponent<TransformModificationDriver>();
            _multiplyVectorTranslation = GetMultiplyVector(_translationAxis);
            _multiplyVectorRotation = GetMultiplyVector(_rotationAxis);
            _time = Random.GetFloat(0, 500);
        }

        private Vector3 GetMultiplyVector(Axis translationAxis)
        {
            return new Vector3(
                (translationAxis & Axis.X) != 0 ? 1 : 0,
                (translationAxis & Axis.Y) != 0 ? 1 : 0,
                (translationAxis & Axis.Z) != 0 ? 1 : 0
            );
        }

        private float GetCurrentTrauma()
        {
            return Mathf.Min(_currentDynamicTrauma + _currentStaticTrauma, 1);
        }
        
        private void Update()
        {
            _time += TimeScale.Unity.DeltaTime * _timeScale;
            
            _currentDynamicTrauma = Mathf.Max(_currentDynamicTrauma - TimeScale.Unity.DeltaTime * _timeScale, 0);
            float shake = Mathf.Pow(CurrentTrauma, _exponent);

            if (_useTranslation)
            {
                _driver.Translate(CalculateChange(shake, _multiplyVectorTranslation, _maxTranslation, 1));
            }
            
            if (_useRotation)
            {
                _driver.RotateEuler(CalculateChange(shake, _multiplyVectorRotation, _maxRotation, 2));
            }
        }

        private Vector3 CalculateChange(float shake, Vector3 multiplyVector, Vector3 maxValues, float seed)
        {
            float time = _time * 1000;
            
            return new Vector3(
                multiplyVector.x * maxValues.x * shake * Random.Noise.GetPerlin(time + 50,seed),
                multiplyVector.y * maxValues.y * shake * Random.Noise.GetPerlin(time + 100,seed),
                multiplyVector.z * maxValues.z * shake * Random.Noise.GetPerlin(time + 150,seed)
            );
        }

        private void SetDynamicTrauma(float value)
        {
            _currentDynamicTrauma = Mathf.Clamp(value, 0, _maxTrauma);
            AdjustTrauma();
        }

        private void SetStaticTrauma(float value)
        {
            _currentStaticTrauma = Mathf.Clamp(value, 0, _maxTrauma);
            AdjustTrauma();
        }

        private void AdjustTrauma()
        {
            if (CurrentTrauma < _maxTrauma)
            {
                return;
            }

            if (_currentStaticTrauma >= _maxTrauma)
            {
                _currentStaticTrauma = _maxTrauma;
                _currentDynamicTrauma = 0;
                return;
            }

            _currentDynamicTrauma = _maxTrauma - _currentStaticTrauma;
        }
    }
}