using System.Collections.Generic;
using HarmonyLib;
using MAIN;
using UnityEngine;

namespace ChineseBrideModelReplace.Patches
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
{

    [HarmonyPatch(typeof(DressGirlAI))]
    internal class DressGirlChineseModelPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPatch(DressGirlAI __instance)
        {
            Transform transformDressGirlModel = __instance.transform.Find("DressGirlModel");
            Transform animationTransform = transformDressGirlModel.Find("AnimContainer").Find("metarig");
            SkinnedMeshRenderer skinnedMeshRenderer = transformDressGirlModel.Find("basemesh").GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer != null && skinnedMeshRenderer.enabled)
            {
                //禁用游戏本体的渲染
                skinnedMeshRenderer.enabled = false;

                foreach (MeshRenderer item in animationTransform.GetComponentsInChildren<MeshRenderer>())
                {
                    item.enabled = false;
                }

                //加载实例
                GameObject ChineseModelObject = Object.Instantiate(Plugin.ChineseModelPrefab1);
                ChineseModelObject.transform.SetParent(transformDressGirlModel);
                ChineseModelObject.transform.localPosition = Vector3.zero;
                ChineseModelObject.transform.localRotation = Quaternion.identity;
                ChineseModelObject.transform.localScale = Vector3.one;

                //替换骨骼
                Transform rigTransform = ChineseModelObject.transform.Find("metarig");
                rigTransform.SetParent(animationTransform.parent, worldPositionStays: true);
                rigTransform.transform.localScale = animationTransform.transform.localScale;
                rigTransform.transform.localRotation = animationTransform.transform.localRotation;
                rigTransform.transform.localPosition = animationTransform.transform.localPosition;

                List<SkinnedMeshRenderer> list = new List<SkinnedMeshRenderer>(__instance.skinnedMeshRenderers);
                // 替换网格和材质
                Transform[] meshes = { ChineseModelObject.transform.Find("body"), ChineseModelObject.transform.Find("dress"), ChineseModelObject.transform.Find("head") };
                  foreach (var meshTransform in meshes)
                    {
                        if (meshTransform != null)
                        {
                            SkinnedMeshRenderer meshRenderer = meshTransform.GetComponent<SkinnedMeshRenderer>();
                            if (meshRenderer != null)
                            {
                                meshRenderer.rootBone = rigTransform;
                                meshRenderer.gameObject.tag = "DoNotSet";
                                meshRenderer.gameObject.layer = LayerMask.NameToLayer("EnemiesNotRendered");
                                meshRenderer.material = Plugin.ChineseGirlMaterial1;

                                list.Add(meshRenderer);
                            }

                        }

                        __instance.skinnedMeshRenderers = list.ToArray();
                        animationTransform.name = "old_metarig2";

                    }
                }

        }           
    }



    [HarmonyPatch(typeof(DressGirlAI))]
    internal class DressGirlChineseModelPatch2
        {

            [HarmonyPatch("Start")]
            [HarmonyPostfix]
            public static void StartPatch(DressGirlAI __instance)
            {
                Transform transformDressGirlModel = __instance.transform.Find("DressGirlModel");
                Transform animationTransform = transformDressGirlModel.Find("AnimContainer").Find("metarig");
                SkinnedMeshRenderer skinnedMeshRenderer = transformDressGirlModel.Find("basemesh").GetComponent<SkinnedMeshRenderer>();
                if (skinnedMeshRenderer != null && skinnedMeshRenderer.enabled)
                {
                    //禁用游戏本体的渲染
                    skinnedMeshRenderer.enabled = false;
                    foreach (MeshRenderer item in animationTransform.GetComponentsInChildren<MeshRenderer>())
                    {
                        item.enabled = false;
                    }

                    //加载实例
                    GameObject ChineseModelObject = Object.Instantiate(Plugin.ChineseModelPrefab2);
                    ChineseModelObject.transform.SetParent(transformDressGirlModel);
                    ChineseModelObject.transform.localPosition = Vector3.zero;
                    ChineseModelObject.transform.localRotation = Quaternion.identity;
                    ChineseModelObject.transform.localScale = Vector3.one;


                    //替换骨骼
                    Transform rigTransform = ChineseModelObject.transform.Find("metarig");
                    rigTransform.SetParent(animationTransform.parent, worldPositionStays: true);
                    rigTransform.transform.localScale = animationTransform.transform.localScale;
                    rigTransform.transform.localRotation = animationTransform.transform.localRotation;
                    rigTransform.transform.localPosition = animationTransform.transform.localPosition;


                    List<SkinnedMeshRenderer> list = new List<SkinnedMeshRenderer>(__instance.skinnedMeshRenderers);
                    // 替换网格和材质
                    Transform[] meshes = { ChineseModelObject.transform.Find("body"), ChineseModelObject.transform.Find("dress"), ChineseModelObject.transform.Find("head") };
                    foreach (var meshTransform in meshes)
                    {
                        if (meshTransform != null)
                        {
                            SkinnedMeshRenderer meshRenderer = meshTransform.GetComponent<SkinnedMeshRenderer>();
                            if (meshRenderer != null)
                            {
                                meshRenderer.rootBone = rigTransform;
                                meshRenderer.gameObject.tag = "DoNotSet";
                                meshRenderer.gameObject.layer = LayerMask.NameToLayer("EnemiesNotRendered");
                                meshRenderer.material = Plugin.ChineseGirlMaterial2;

                                list.Add(meshRenderer);
                            }
                        }

                        __instance.skinnedMeshRenderers = list.ToArray();
                        animationTransform.name = "old_metarig3";
                    }
                }
            }
        }

}




