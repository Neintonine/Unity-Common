using System.Collections.Generic;

namespace Common.Runtime.Randomization
{
    public static partial class Random
    {
        private static FastNoise _noise;
        public static FastNoise Noise
        {
            get
            {
                return _noise ??= new FastNoise();
            }
        }
        
        /// <summary>
        /// Gets a random float.
        /// </summary>
        /// <returns></returns>
        public static float GetFloat()
        {
            return UnityEngine.Random.value;
        }
        /// <summary>
        /// Gets a random float.
        /// </summary>
        /// <param name="max">Maximum Value inclusive</param>
        /// <returns></returns>
        public static float GetFloat(float max)
        {
            return UnityEngine.Random.Range(0, max);
        }
        /// <summary>
        /// Gets a random float.
        /// </summary>
        /// <param name="min">Minimum Value inclusive</param>
        /// <param name="max">Maximum Value inclusive</param>
        /// <returns></returns>
        public static float GetFloat(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        
        /// <summary>
        /// Gets a random boolean.
        /// </summary>
        /// <param name="edge">Edge value. Above = true.</param>
        /// <returns>Random Boolean</returns>
        public static bool GetBool(float edge = .5f)
        {
            return UnityEngine.Random.value > edge;
        }
        /// <summary>
        /// Gets a random int.
        /// </summary>
        /// <param name="max">Maximum Value exclusive</param>
        /// <returns></returns>
        public static int GetInt(int max)
        {
            return UnityEngine.Random.Range(0, max);
        }
        /// <summary>
        /// Gets a random int.
        /// </summary>
        /// <param name="min">Minimum Value inclusive</param>
        /// <param name="max">Maximum Value exclusive</param>
        /// <returns></returns>
        public static int GetInt(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary>
        /// Sets the seed randomly.
        /// </summary>
        public static void SetSeed()
        {
            SetSeed(Random.GetInt(int.MaxValue));
        }
        
        /// <summary>
        /// Sets the seed.
        /// </summary>
        /// <param name="seedValue"></param>
        /// <returns></returns>
        public static void SetSeed(int seedValue)
        {
            UnityEngine.Random.InitState(seedValue);
            _noise?.SetSeed(seedValue);
        }
        
        /// <summary>
        /// Returns a random item in the list.
        /// </summary>
        /// <param name="list">The origin list</param>
        /// <param name="value">The item that gets chosen</param>
        /// <typeparam name="T">The list type</typeparam>
        /// <returns>Was is successful?</returns>
        public static bool RandomItem<T>(this IList<T> list, out T value)
        {
            if (list.Count < 1)
            {
                value = default;
                return false;
            }

            value = list[GetInt(0, list.Count)];
            return true;
        }
        
        
    }
}