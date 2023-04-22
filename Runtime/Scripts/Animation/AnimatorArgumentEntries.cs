using System.Collections.Generic;
using UnityEngine;

namespace Common.Runtime.Animation
{
    [AddComponentMenu("Animation/Animator Arguments - Entry Manager")]
    [RequireComponent(typeof(AnimatorArguments))]
    public sealed class AnimatorArgumentEntries : MonoBehaviour
    {
        public List<AnimatorArgumentDataEntry> Entries => this._entries;
        [SerializeField] private List<AnimatorArgumentDataEntry> _entries = new List<AnimatorArgumentDataEntry>();

        private AnimatorArguments _arguments;


        private void Awake()
        {
            this._arguments = this.GetComponent<AnimatorArguments>();
        }

        private void Start()
        {
            foreach (AnimatorArgumentDataEntry entry in this._entries)
            {
                this._arguments.Set(entry.Name, entry.Data);
            }
        }
    }
}