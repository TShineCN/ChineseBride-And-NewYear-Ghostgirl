using GameNetcodeStuff;
using HarmonyLib;
using MAIN;
using UnityEngine;

namespace NewYearAudioReplace.Patches
{
    [HarmonyPatch(typeof(DressGirlAI), "Start")]
    internal class BreathingSFXPatch // 替换呼吸声的音频
    {
        [HarmonyPrefix]
        public static void StartPatch(DressGirlAI __instance)
        {
            __instance.breathingSFX = Plugin.NewYearbreathingAudio;
            __instance.heartbeatMusic.clip = Plugin.NewYearheartbeatMusicAudio;
            __instance.heartbeatMusic.Play();
        }
    }

    [HarmonyPatch(typeof(DressGirlAI), "SetHauntStarePosition")]
    internal class AppearStaringSFXPatch //替换凝视的音频
    {
        [HarmonyPrefix]
        public static bool PlayRandomClipPrefix(DressGirlAI __instance)
        {
            // 替换多个 AudioClip 
            AudioClip clip1 = Plugin.Girlclip1; // 加载的音频
            AudioClip clip2 = Plugin.Girlclip2;
            AudioClip clip3 = Plugin.Girlclip3;
            AudioClip clip4 = Plugin.Girlclip4;
            AudioClip clip5 = Plugin.Girlclip5;
            AudioClip clip6 = Plugin.Girlclip6;
            AudioClip clip7 = Plugin.Girlclip7;
            AudioClip clip8 = Plugin.Girlclip8;
            AudioClip clip9 = Plugin.Girlclip9;


            // 检查 AudioClip 是否有效
            if (clip1 != null && clip2 != null && clip3 != null && clip4 != null && clip5 != null && clip6 != null && clip7 != null && clip8 != null && clip9 != null)
            {
                // 创建包含所有 AudioClip 的数组
                AudioClip[] customappearStaringClips = { clip1, clip2, clip3, clip4, clip5, clip6, clip7, clip8, clip9 };

                // 替换
                __instance.appearStaringSFX = customappearStaringClips;
            }
            else
            {
                Debug.LogError("One or more custom AudioClips are not available.");
            }

            // 允许原方法继续执行，但现在包含的是自定义的音频
            return true;
        }
    }


    internal static class PlayerDieAudioPatch //替换死亡音频
    {

        public static AudioSource audioSource;
        // 用于存储当前行为状态索引的条件是否满足
        private static bool shouldPlayAudio = false;

        // 前置补丁以检查条件
        [HarmonyPatch(typeof(DressGirlAI), "OnCollideWithPlayer")]
        [HarmonyPrefix]
        public static void UpdatePrefix(DressGirlAI __instance)
        {
            // 检查特定条件
            shouldPlayAudio = __instance.currentBehaviourStateIndex == 1;
            if (audioSource == null || !audioSource.gameObject.activeInHierarchy)
            {
                // 创建并设置audioSource
                audioSource = CreateAndSetupAudioSource(__instance);
            }
        }

        // KillPlayer方法的后置补丁
        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")] 
        [HarmonyPostfix]
        public static void KillPlayerPostfix()
        {
            // 如果满足条件，在KillPlayer执行后播放音频
            if (shouldPlayAudio)
            {
                audioSource.Play();
                Debug.LogError("音频准备开始播放！");
                shouldPlayAudio = false; // 重置条件
            }
        }

        public static AudioSource CreateAndSetupAudioSource(DressGirlAI dressGirl)
        {
            // 创建新的 GameObject 并设置 AudioSource
            GameObject audioObject = new GameObject("Die Audio");
            audioObject.transform.SetParent(dressGirl.transform);
            audioObject.transform.localPosition = Vector3.zero;
            AudioSource newAudioSource = audioObject.AddComponent<AudioSource>();
            SetupAudioSource(newAudioSource);
            return newAudioSource;
        }

        public static void SetupAudioSource(AudioSource source)
        {
            // 配置 AudioSource 属性
            source.loop = false;
            source.clip = Plugin.PlayerDie;
            Debug.LogError("音频加载成功！");
            source.volume = 1f;
        }
    }

    [HarmonyPatch(typeof(DressGirlAI), "Start")]
    internal class BreathingSFXPatch2 // 替换呼吸声的MeMe音频
    {
        [HarmonyPrefix]
        public static void StartPatch(DressGirlAI __instance)
        {
            __instance.breathingSFX = Plugin.MeMeAudio;
            __instance.heartbeatMusic.clip = Plugin.NewYearheartbeatMusicAudio;
            __instance.heartbeatMusic.Play();
        }
    }
}