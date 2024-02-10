using UnityEngine;
using HarmonyLib;
using BepInEx;
using BepInEx.Configuration;
using NewYearModelReplace.Patches;
using ChineseBrideModelReplace.Patches;
using ChineseBrideAudioReplace.Patches;
using NewYearAudioReplace.Patches;
using EveryoneCanSeeGirl.Patches;
using System;

namespace MAIN
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "TShine.ChineseBrideAndNewYearGhostgirl";
        private const string modName = "ChineseBride And NewYear Ghostgirl";
        private const string modVersion = "1.0.0";

        private static ConfigEntry<bool> enableNewYearAudioReplace;
        private static ConfigEntry<bool> enableNewYearModelReplace;
        private static ConfigEntry<string> enableChineseModelReplace;
        private static ConfigEntry<bool> enableChineseAudioReplace;
        private static ConfigEntry<bool> MeMeAudioReplace;
        private static ConfigEntry<bool> EveryoneCanSee;


        private readonly Harmony harmony = new Harmony(modGUID);

        public static Plugin Instance;

        internal static AssetBundle MyPrefabBundle;
        public static GameObject NYModelPrefab;
        public static GameObject ChineseModelPrefab1;
        public static GameObject ChineseModelPrefab2;
        public static Material NewYearGirlMaterial;
        public static Material ChineseGirlMaterial1;
        public static Material ChineseGirlMaterial2;

        public static AudioClip NewYearbreathingAudio;
        public static AudioClip NewYearheartbeatMusicAudio;
        public static AudioClip Girlclip1;
        public static AudioClip Girlclip2;
        public static AudioClip Girlclip3;
        public static AudioClip Girlclip4;
        public static AudioClip Girlclip5;
        public static AudioClip Girlclip6;
        public static AudioClip Girlclip7;
        public static AudioClip Girlclip8;
        public static AudioClip Girlclip9;
        public static AudioClip PlayerDie;
        public static AudioClip MeMeAudio;
        public static AudioClip ChineseGirlclipP1;
        public static AudioClip ChineseGirlclipP2;
        public static AudioClip ChineseGirlclipP3;
        public static AudioClip ChineseGirlclipB1;
        public static AudioClip ChineseGirlclipB2;
        public static AudioClip ChineseGirlclipB3;
        public static AudioClip ChineseGirlclipH1;
        public static AudioClip ChineseGirlclipH2;
        public static AudioClip ChineseGirlclipH3;
        public static AudioClip ChineseGirlclipH4;
        public static AudioClip ChineseGirlclipL1;
        public static AudioClip ChineseGirlclipL2;
        public static AudioClip ChineseGirlclipL3;
        public static AudioClip ChineseGirlclipL4;
        public static AudioClip ChineseGirlclipL5;
        public static AudioClip ChineseGirlclipbreath;

        // 创建一个包含多个音频剪辑的数组
        public static AudioClip[] ChineseheartbeatMusicClips;

        //public static AnimationClip Tpose;

        void Awake()
        {
            if (Instance != null && Instance != this)
            { Destroy(this); }
            else { Instance = this; }

            Debug.Log("ChineseBride And NewYear Ghostgirl Mod loaded  小女孩来喽~~~");

            string dir = Instance.Info.Location.TrimEnd("TShine.ChineseBride And NewYear Ghostgirl.dll".ToCharArray());

            MyPrefabBundle = AssetBundle.LoadFromFile(dir + "ghostgirl");
            if (MyPrefabBundle != null)
            {
                NYModelPrefab = MyPrefabBundle.LoadAsset<GameObject>("Assets/GhostGirl/NY.prefab");
                ChineseModelPrefab1 = MyPrefabBundle.LoadAsset<GameObject>("Assets/GhostGirl/dress.prefab");
                ChineseModelPrefab2 = MyPrefabBundle.LoadAsset<GameObject>("Assets/GhostGirl/nodress.prefab");
                NewYearGirlMaterial = MyPrefabBundle.LoadAsset<Material>("Assets/GhostGirl/Materials/NY.mat");
                ChineseGirlMaterial1 = MyPrefabBundle.LoadAsset<Material>("Assets/GhostGirl/Materials/dress.mat");
                ChineseGirlMaterial2 = MyPrefabBundle.LoadAsset<Material>("Assets/GhostGirl/Materials/nodress.mat");

                NewYearbreathingAudio = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/NY.wav");
                Girlclip1 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/BP.wav");
                Girlclip2 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/laugh1.wav");
                Girlclip3 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/laugh2.wav");
                Girlclip4 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/laugh3.wav");
                Girlclip5 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/laugh4.wav");
                Girlclip6 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/money1.wav");
                Girlclip7 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/money2.wav");
                Girlclip8 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/money3.wav");
                Girlclip9 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/money4.wav");
                PlayerDie = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/die.wav");
                MeMeAudio = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/facai.wav");
                NewYearheartbeatMusicAudio = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/BP2.wav");
                ChineseGirlclipP1 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/P1.wav");
                ChineseGirlclipP2 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/P2.wav");
                ChineseGirlclipP3 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/P3.wav");
                ChineseGirlclipB1 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/B1.wav");
                ChineseGirlclipB2 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/B2.wav");
                ChineseGirlclipB3 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/B3.wav");
                ChineseGirlclipH1 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/H1.wav");
                ChineseGirlclipH2 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/H2.wav");
                ChineseGirlclipH3 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/H3.wav");
                ChineseGirlclipH4 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/H4.wav");
                ChineseGirlclipL1 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/L1.wav");
                ChineseGirlclipL2 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/L2.wav");
                ChineseGirlclipL3 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/L3.wav");
                ChineseGirlclipL4 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/L4.wav");
                ChineseGirlclipL5 = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/L5.wav");
                ChineseGirlclipbreath = MyPrefabBundle.LoadAsset<AudioClip>("Assets/GhostGirl/breath.wav");

                ChineseheartbeatMusicClips = new AudioClip[]
                {
                    ChineseGirlclipL1, ChineseGirlclipL2, ChineseGirlclipL3, ChineseGirlclipL4, ChineseGirlclipL5,
                };

                //Tpose = MyPrefabBundle.LoadAsset<AnimationClip>("Assets/GhostGirl/Tpose.anim");


                Debug.Log("Asset bundle loaded");

             }
            else { Debug.LogError("Asset bundle not loaded！"); }

            // 初始化Config配置项
            EveryoneCanSee = Config.Bind("可见性/Visibility", "EnableAudioReplace", false, "设置为true的时候，所有人都能看到小女孩\n注意：开启此功能会有非常奇怪的模型渲染逻辑，包括无动作，显示逻辑，不同步等问题。\nIf set to true,everyone can see the girl.\nNote:Enabling this feature will make very strange model rendering logic, including issues such as no action, display logic error, and out of sync.");
            MeMeAudioReplace = Config.Bind("刘德华《恭喜发财》/MeMeAudio", "MeMeAudioReplace", false, "设置为true将会把小女孩的音乐由《红包拿来》改为为刘德华的《恭喜发财》\n注意：当音频替换为false但该选项设置为true时，可以替换为恭喜发财的音乐。当音频替换和该选项都设置为true时，音频也将替换为《恭喜发财》\nSet to true will change the girl's music from \"Give me the red envelope\" to Andy Lau's \"Wishing you prosperity\"\nNote: When enableAudioReplace is false but enableMeMeReplaceset is true, it can be replaced with music from \"Wishing you prosperity\". \nWhen both of them set to true, the audio will also be replaced with \"Wishing you prosperity\"");
            enableNewYearAudioReplace = Config.Bind("新年音频/NewYearAudio", "EnableNewYearAudioReplace", false, "true为开启新年音频替换/Set to true to enable the NewYear audio replacement.");
            enableNewYearModelReplace = Config.Bind("新年模型/NewYearModel", "EnableNewYearModelReplace", false, "true为开启小女孩新年模型替换/Set to true to enable the NewYear DressGirl Model replacement");      
            enableChineseAudioReplace = Config.Bind("国风新娘音频/ChineseBrideAudio", "EnableChineseBrideAudioReplace", true, "true为开启小女孩国风新娘音频替换/Set to true to enable the ChineseBride audio replacement.");
            enableChineseModelReplace = Config.Bind<string>("国风新娘模型/ChineseBrideModel", "EnableChineseBrideModelReplace", "dress", "dress为开启小女孩国风新娘模型替换，none为关闭，nodress为去掉裙子。 /Set to \"dress\" to enable the ChineseBride DressGirl Model replacement.\"None\" is closed, and \"nodress\" is removing the skirt.");



            // 判断是否应用Harmony补丁的逻辑


            //是否开启小女孩新年模型替换
            if (enableNewYearModelReplace.Value)
            {
                harmony.PatchAll(typeof(DressGirlNewYearModelPatch));
            }

            //是否开启小女孩国风新娘模型替换
            if (enableChineseModelReplace.Value.Equals("dress", StringComparison.OrdinalIgnoreCase))
            {
                harmony.PatchAll(typeof(DressGirlChineseModelPatch));
            }
            else if (enableChineseModelReplace.Value.Equals("nodress", StringComparison.OrdinalIgnoreCase))
            {
                harmony.PatchAll(typeof(DressGirlChineseModelPatch2));
            }
            else if (enableChineseModelReplace.Value.Equals("none", StringComparison.OrdinalIgnoreCase))
            {
                // 如果值为'none'，不执行任何操作
            }

            //是否开启小女孩国风新娘音频替换
            if (enableChineseAudioReplace.Value)
            {
                harmony.PatchAll(typeof(ChineseBrideAudioPatch));
                harmony.PatchAll(typeof(ChineseBreathingSFXPatch)); 
                harmony.PatchAll(typeof(ChinesePlayerDieAudioPatch)); 
            }

            //是否所有人都能看到
            if (EveryoneCanSee.Value)
            {
                harmony.PatchAll(typeof(EveryoneCanSeeGirlPatch));
                harmony.PatchAll(typeof(EveryoneCanSeeGirlPatch2));
            }

            //是否开启小女孩新年音频替换
            // 第一种情况：当enableAudioReplace和MeMeAudioReplace都为false时，不执行任何方法
            // 第二种情况：当enableAudioReplace为true，MeMeAudioReplace为false时
            if (enableNewYearAudioReplace.Value && !MeMeAudioReplace.Value) 
            {
                harmony.PatchAll(typeof(BreathingSFXPatch));
                harmony.PatchAll(typeof(AppearStaringSFXPatch));
                harmony.PatchAll(typeof(PlayerDieAudioPatch));
            }
            // 第三种情况：当enableAudioReplace为false，MeMeAudioReplace为true时
            if (!enableNewYearAudioReplace.Value && MeMeAudioReplace.Value)
            {
                harmony.PatchAll(typeof(BreathingSFXPatch2));
            }
            // 第四种情况：当enableAudioReplace为true，MeMeAudioReplace为true时
            if (enableNewYearAudioReplace.Value && MeMeAudioReplace.Value)
            {
                harmony.PatchAll(typeof(BreathingSFXPatch2));
                harmony.PatchAll(typeof(AppearStaringSFXPatch));
                harmony.PatchAll(typeof(PlayerDieAudioPatch));
            }


            Logger.LogInfo($"New Year Model Replace Mod: Enabled = {enableNewYearModelReplace.Value}");
            Logger.LogInfo($"New Year Audio Replace Mod: Enabled = {enableNewYearAudioReplace.Value}");
            Logger.LogInfo($"Chinese Bride Model Replace Mod: Enabled = {enableChineseModelReplace.Value}");
            Logger.LogInfo($"Chinese Bride Audio Replace Mod: Enabled = {enableChineseAudioReplace.Value}");
            Logger.LogInfo($"MeMe Audio: Enabled = {MeMeAudioReplace.Value}");
            Logger.LogInfo($"Everyone Can See Girl: Enabled = {EveryoneCanSee.Value}");



        }
    }
}
