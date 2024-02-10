using System.Collections.Generic;
using HarmonyLib;
using MAIN;
using UnityEngine;

namespace NewYearModelReplace.Patches
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!
//Sincerely thank RamenNoodle/qqrz997 for providing technical assistance!!!

{
    [HarmonyPatch(typeof(DressGirlAI))]
    internal class DressGirlNewYearModelPatch
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
                skinnedMeshRenderer.enabled = false;
                foreach (MeshRenderer item in animationTransform.GetComponentsInChildren<MeshRenderer>())
                {
                    item.enabled = false;
                }

                GameObject NYModelObject = Object.Instantiate(Plugin.NYModelPrefab);
                NYModelObject.transform.SetParent(transformDressGirlModel);
                NYModelObject.transform.localPosition = Vector3.zero;
                NYModelObject.transform.localRotation = Quaternion.identity;
                NYModelObject.transform.localScale = Vector3.one;

                Transform rigTransform = NYModelObject.transform.Find("metarig");
                rigTransform.SetParent(animationTransform.parent, worldPositionStays: true);
                rigTransform.transform.localScale = animationTransform.transform.localScale;
                rigTransform.transform.localRotation = animationTransform.transform.localRotation;
                rigTransform.transform.localPosition = animationTransform.transform.localPosition;

                List<SkinnedMeshRenderer> list = new List<SkinnedMeshRenderer>(__instance.skinnedMeshRenderers);
                Transform[] meshes = { NYModelObject.transform.Find("girl"), NYModelObject.transform.Find("dress") };
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
                            meshRenderer.material = Plugin.NewYearGirlMaterial; 
                            list.Add(meshRenderer);
                        }
                    }
                }
                __instance.skinnedMeshRenderers = list.ToArray();
                animationTransform.name = "old_metarig";


            }
        }
    }


   
}
