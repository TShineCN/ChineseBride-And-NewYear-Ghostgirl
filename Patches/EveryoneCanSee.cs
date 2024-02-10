using HarmonyLib;

namespace EveryoneCanSeeGirl.Patches
{
    [HarmonyPatch(typeof(DressGirlAI), "Update")]
    internal class EveryoneCanSeeGirlPatch
    {
        [HarmonyPrefix]
        public static bool PrefixUpdate(DressGirlAI __instance, ref bool ___enemyMeshEnabled)
        {
            if (!__instance.IsOwner)
            {
                ___enemyMeshEnabled = true; // 修改为true
                __instance.EnableEnemyMesh(false, true); // 启用敌人网格

                return false; // 阻止原始方法执行
            }

            return true; // 在其他情况下允许原始方法执行
        }
    }

    [HarmonyPatch(typeof(EnemyAI), "EnableEnemyMesh")]
    internal class EveryoneCanSeeGirlPatch2
    {
        [HarmonyPrefix]
        public static void PrefixEnableEnemyMesh(ref bool enable)
        {
            enable = true;
        }
    }
}

