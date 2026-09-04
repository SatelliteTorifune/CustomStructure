namespace Assets.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ModApi;
    using HarmonyLib;
    using ModApi.Common.Extensions;
    using ModApi.Planet;
    using ModApi.Settings;
    using ModApi.Ui;
    using UnityEngine;
    using Assets.Scripts.Flight.Sim;
    using Assets.Scripts.Menu.ListView;

    /// <summary>
    /// Adds every entry of <see cref="Mod.StructurePrefabs"/> to the Planet Studio
    /// "Add Structure" list and makes them instantiate in game.
    ///
    /// Design (mirrors the AddedStructures template, adapted for this mod):
    ///  - LoadItems postfix        : appends our structures to the list (no IL rewriting).
    ///  - UpdatePreview prefix     : loads and previews our prefabs through the mod's
    ///                               resource loader instead of the game's Resources.Load.
    ///  - InstantiateSubStructures : preloads our prefabs through the mod's resource
    ///                               loader, then lets the original method finish the
    ///                               recursion / rigid body / camera layer work.
    /// </summary>
    public static class StructureListChange
    {
        /// <summary>
        /// Appends every entry of <see cref="Mod.StructurePrefabs"/> to the structure list.
        /// </summary>
        [HarmonyPatch(typeof(AddStructureViewModel), "LoadItems")]
        private static class LoadItems_Patch
        {
            [HarmonyPostfix]
            private static void Postfix(AddStructureViewModel __instance)
            {
                foreach (KeyValuePair<string, string> prefab in Mod.StructurePrefabs)
                {
                    AddStructureViewModel.StructureItem structure =
                        new AddStructureViewModel.StructureItem(
                            prefab.Key,
                            prefab.Value,
                            string.Empty,
                            1f,
                            new Color32(100, 100, 100, byte.MaxValue));

                    // The mod name as subtitle doubles as a marker that lets the
                    // UpdatePreview prefix recognize our own items.
                    __instance.ListView.CreateItem(
                        structure.Name,
                        Mod.Instance.ModInfo.Name,
                        structure,
                        null,
                        ListViewScript.SpriteLoadLocation.Resources);
                }

                __instance.ListView.SelectedItem = null;
            }
        }

        /// <summary>
        /// Renders the preview for the mod's own structures (loaded through the mod's
        /// resource loader). Returns false so the stock UpdatePreview is skipped.
        /// </summary>
        [HarmonyPatch(typeof(AddStructureViewModel), "UpdatePreview")]
        private static class UpdatePreview_Patch
        {
            [HarmonyPrefix]
            private static bool Prefix(
                AddStructureViewModel __instance,
                ListViewItemScript item,
                IListViewObjectViewer objectViewer,
                Action completeCallback)
            {
                if (item.Subtitle != Mod.Instance.ModInfo.Name)
                {
                    return true; // not ours, keep stock behavior
                }

                if (__instance.ListView.SelectedItem?.ItemModel is AddStructureViewModel.StructureItem structureItem)
                {
                    GameObject gameObject =
                        UnityEngine.Object.Instantiate(
                            Mod.Instance.ResourceLoader.LoadAsset<GameObject>(structureItem.PrefabPath));
                    Utilities.ChangeLayersOfGameObjectAndChildrenRecursive(gameObject, 31, Array.Empty<int>());
                    objectViewer.PreviewObject(gameObject, 0f, true, new Vector3(-45f, 0f, 0f));
                }
                else
                {
                    objectViewer.PreviewObject(null, 0f, true, null);
                }

                completeCallback?.Invoke();
                return false;
            }
        }

        /// <summary>
        /// Preloads the mod's substructure prefabs through the mod resource loader, then
        /// lets the original method run to finish the recursion / rigid body / unload work.
        /// </summary>
        [HarmonyPatch(typeof(StructureNode), "InstantiateSubStructures")]
        private static class InstantiateSubStructures_Patch
        {
            [HarmonyPrefix]
            private static bool Prefix(Transform parent, IEnumerable<SubStructure> subStructures, int lod, bool insideRigidBody)
            {
                TerrainQualitySettings.StructureDetailQuality structureDetailQuality =
                    Game.InPlanetStudioScene
                        ? TerrainQualitySettings.StructureDetailQuality.High
                        : Game.Instance.QualitySettings.Terrain.StructureDetail.Value;

                foreach (SubStructure subStructure in subStructures)
                {
                    if (structureDetailQuality < subStructure.RequiredQuality)
                    {
                        continue;
                    }

                    if (subStructure.LevelOfDetail > lod && !insideRigidBody)
                    {
                        continue;
                    }

                    if (subStructure.LoadedGameObject != null)
                    {
                        continue; // already loaded (by us or by a previous pass)
                    }

                    GameObject original = Mod.Instance.ResourceLoader.LoadAsset<GameObject>(subStructure.PrefabPath);
                    if (original == null)
                    {
                        continue; // not a mod structure; let the original method handle it
                    }

                    GameObject gameObject = UnityEngine.Object.Instantiate(original);
                    StructureGameObjectScript structureGameObjectScript =
                        gameObject.AddMissingComponent<StructureGameObjectScript>();
                    structureGameObjectScript.StructureNode = null;
                    structureGameObjectScript.SubStructure = subStructure;
                    subStructure.OnGameObjectLoaded(gameObject);

                    Transform transform = gameObject.transform;
                    transform.SetParent(parent, false);
                    transform.SetLocalPositionAndRotation(subStructure.LocalPosition, Quaternion.Euler(subStructure.LocalRotation));
                    transform.localScale = subStructure.LocalScale;

                    subStructure.UpdateDynamicMaterials();
                    StructureNode.FixNegativeBoxColliderScales(subStructure.LoadedGameObject);

                    if (Game.InFlightScene && subStructure.AngularVelocity.HasValue)
                    {
                        gameObject.AddComponent<SubStructureRotateScript>()
                            .Initialize(subStructure.AngularVelocity.Value);
                    }

                    if (subStructure.CameraCollision == SubStructure.CameraCollisionType.Collide)
                    {
                        Utilities.ChangeLayersOfGameObjectAndChildrenRecursive(gameObject, 29, Array.Empty<int>());
                    }
                    else if (subStructure.CameraCollision == SubStructure.CameraCollisionType.NoCollide)
                    {
                        Utilities.ChangeLayersOfGameObjectAndChildrenRecursive(gameObject, 26, Array.Empty<int>());
                    }
                }

                return true; // run the original too: it finishes recursion / rigid bodies / unload
            }
        }
    }
}
