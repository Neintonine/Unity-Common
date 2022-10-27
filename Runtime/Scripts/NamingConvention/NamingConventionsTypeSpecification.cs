using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Common.Runtime.NamingConvention
{
    public partial class NamingConventions
    {
        private static Dictionary<Type, string> _typePrefixDeleration = new Dictionary<Type, string>()
        {
            { typeof(AnimationClip), ANIMATION_CLIP },
            { typeof(RuntimeAnimatorController), ANIMATOR_CONTROLLER},
            { typeof(AnimatorOverrideController), ANIMATOR_OVERRIDE_CONTROLLER},
            { typeof(AvatarMask), AVATAR_MASK },
            { typeof(GameObject), PREFAB },
            { typeof(Shader), SHADER},
            { typeof(Material), MATERIAL},
            {typeof(PhysicMaterial), PHYSICAL_MATERIAL},
            { typeof(Texture), TEXTURE},
            { typeof(Cubemap), TEXTURE_CUBE},
            {typeof(RenderTexture), RENDER_TARGET},
            { typeof(AudioClip), AUDIO_CLIP},
            { typeof(VideoClip), VIDEO_CLIP},
            { typeof(Font), FONT},
        };
    }
}