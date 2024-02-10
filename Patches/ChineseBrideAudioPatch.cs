using GameNetcodeStuff;
using HarmonyLib;
using MAIN;
using UnityEngine;
namespace ChineseBrideAudioReplace.Patches
{
    internal class ChineseBrideAudioPatch
    {
        // 定义静态计数器
        private static int count = 0;

        [HarmonyPatch(typeof(DressGirlAI), "Start")]// 在Start方法执行时重置计数
        [HarmonyPostfix]
        public static void ResetCountOnStart()
        {
            count = 0;
            Debug.LogError("重置计数为0");
        }

        [HarmonyPatch(typeof(DressGirlAI), "OnCollideWithPlayer")]
        [HarmonyPostfix]
        public static void OnCollideWithPlayerPostfix(DressGirlAI __instance)
        {
            // 检查currentBehaviourStateIndex是否为1
            if (__instance.currentBehaviourStateIndex == 1)
            {
                // 如果为1，则重置计数器
                Debug.LogError("重置计数为0");
            }
        }


        [HarmonyPatch(typeof(DressGirlAI), "SetHauntStarePosition")]// 根据计数器的值设置不同的音频组
        [HarmonyPrefix]
        public static bool PlayRandomClipPrefix(DressGirlAI __instance)
        {
            // 计数器递增
            count++;
            
            switch (count)
            {
                case 1:
                    __instance.appearStaringSFX = new AudioClip[] { Plugin.ChineseGirlclipP1 };
                    __instance.breathingSFX = Plugin.ChineseGirlclipB1;
                    break;
                case 2:
                    __instance.appearStaringSFX = new AudioClip[] { Plugin.ChineseGirlclipP2 };
                    __instance.breathingSFX = Plugin.ChineseGirlclipB2;
                    break;
                case 3:
                    __instance.appearStaringSFX = new AudioClip[] { Plugin.ChineseGirlclipP3 };
                    __instance.breathingSFX = Plugin.ChineseGirlclipB3;
                    break;
                default:
                    if (count > 3)
                    {
                        // 当计数大于3时，使用多个音频
                        __instance.appearStaringSFX = new AudioClip[]
                        {
                    // 这里列出你想要播放的多个音频
                    Plugin.ChineseGirlclipH1, Plugin.ChineseGirlclipH2, Plugin.ChineseGirlclipH3, Plugin.ChineseGirlclipH4,
                    Plugin.ChineseGirlclipL1, Plugin.ChineseGirlclipL2, Plugin.ChineseGirlclipL3, Plugin.ChineseGirlclipL4, Plugin.ChineseGirlclipL5,
                        };
                        __instance.breathingSFX = Plugin.ChineseGirlclipbreath;
                    }
                    break;
            }

            // 允许原方法继续执行
            return true;
        }

    }

    [HarmonyPatch(typeof(DressGirlAI), "SetHauntStarePosition")]
    internal class ChineseBreathingSFXPatch // 替换心跳声（钟声）的音频
    {
        [HarmonyPrefix]
        public static void StartPatch(DressGirlAI __instance)
        {
            // 在方法内部设置随机数种子
            UnityEngine.Random.InitState(System.Environment.TickCount);

            // 从 ChineseheartbeatMusicClips 数组中随机选择一个音频剪辑
            int randomIndex = UnityEngine.Random.Range(0, Plugin.ChineseheartbeatMusicClips.Length);
            __instance.heartbeatMusic.clip = Plugin.ChineseheartbeatMusicClips[randomIndex];
            // 播放选定的音频剪辑
            __instance.heartbeatMusic.Play();
        }
    }

    internal static class ChinesePlayerDieAudioPatch //替换死亡音频
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
                // 从音频组数组中随机选择一个音频剪辑
                UnityEngine.Random.InitState(System.Environment.TickCount);
                int randomIndex = UnityEngine.Random.Range(0, Plugin.ChineseheartbeatMusicClips.Length);
                audioSource.clip = Plugin.ChineseheartbeatMusicClips[randomIndex];
                audioSource.Play();
                Debug.LogError("音频准备开始播放！");
                shouldPlayAudio = false; // 重置条件
            }
        }

        public static AudioSource CreateAndSetupAudioSource(DressGirlAI dressGirl)
        {
            // 创建新的 GameObject 并设置 AudioSource
            GameObject audioObject = new GameObject("Die Audio 2");
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
            Debug.LogError("音频加载成功！");
            source.volume = 1f;
        }
    }
}

