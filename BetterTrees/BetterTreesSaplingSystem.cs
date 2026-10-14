using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Reflection;
using Steamworks;

namespace BetterTrees
{
    internal static class SaplingLocale
    {
        private static readonly Dictionary<string, string> English = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> Russian = new Dictionary<string, string>();
        private static bool resourcesLoaded;
        private static string language = "";
        private static float nextLanguageCheck;
        private static PropertyInfo currentLanguageProperty;

        private static string VanillaSeedName(SaplingKind kind)
        {
            if (kind == SaplingKind.Palm || kind == SaplingKind.VanillaPalm) return "Seed_Palm";
            if (kind == SaplingKind.Birch || kind == SaplingKind.VanillaBirch) return "Seed_Birch";
            if (kind == SaplingKind.Mango) return "Seed_Mango";
            if (kind == SaplingKind.Banana || kind == SaplingKind.DecorativeBanana) return "Seed_Banana";
            return "Seed_Pine";
        }

        internal static string NameOnly(string localized)
        {
            if (string.IsNullOrEmpty(localized)) return string.Empty;
            int separator = localized.IndexOf('@');
            return (separator >= 0 ? localized.Substring(0, separator) : localized).Trim();
        }

        private static string DescriptionOnly(string localized)
        {
            if (string.IsNullOrEmpty(localized)) return string.Empty;
            int separator = localized.IndexOf('@');
            return separator >= 0 ? localized.Substring(separator + 1).Trim() : string.Empty;
        }

        private static string GetVanillaItemText(ItemInstance_Inventory original)
        {
            if (original == null || string.IsNullOrEmpty(original.LocalizationTerm)) return null;
            string translated = Helper.GetTerm(original.LocalizationTerm, false);
            return string.IsNullOrEmpty(translated) || translated == original.LocalizationTerm ? null : translated;
        }

        private static void ReadResource(Dictionary<string, string> entries, string resourceName)
        {
            Assembly assembly = typeof(SaplingLocale).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return;
                }
                using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8, true))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim().TrimStart('\uFEFF');
                        if (line.Length == 0 || line[0] == '#') continue;
                        int equals = line.IndexOf('=');
                        if (equals <= 0) continue;
                        entries[line.Substring(0, equals).Trim()] = line.Substring(equals + 1).Trim();
                    }
                }
            }
        }

        private static void EnsureResources()
        {
            if (resourcesLoaded) return;
            resourcesLoaded = true;
            try
            {
                ReadResource(English, "BetterTrees.Localization.en.txt");
                ReadResource(Russian, "BetterTrees.Localization.ru.txt");
            }
            catch (Exception) { }
        }

        private static bool IsRussian()
        {
            if (language.Length == 0 || Time.unscaledTime >= nextLanguageCheck)
            {
                nextLanguageCheck = Time.unscaledTime + 2f;
                string detected = null;
                try
                {
                    if (currentLanguageProperty == null)
                    {
                        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                        {
                            Type type = assembly.GetType("I2.Loc.LocalizationManager", false);
                            if (type == null) continue;
                            currentLanguageProperty = type.GetProperty("CurrentLanguage", BindingFlags.Static | BindingFlags.Public);
                            if (currentLanguageProperty != null) break;
                        }
                    }
                    if (currentLanguageProperty != null)
                        detected = currentLanguageProperty.GetValue(null, null) as string;
                }
                catch (Exception) { }
                if (string.IsNullOrEmpty(detected))
                    detected = Application.systemLanguage == SystemLanguage.Russian ? "Russian" : "English";
                string lower = detected.ToLowerInvariant();
                string chosen = lower.StartsWith("ru") || lower.StartsWith("рус") ||
                    lower.Contains("russian") ? "ru" : "en";
                if (chosen != language)
                {
                    language = chosen;
                }
            }
            return language == "ru";
        }

        private static string Text(string key, string fallbackEnglish, string fallbackRussian)
        {
            EnsureResources();
            bool russian = IsRussian();
            string value;
            if (russian && Russian.TryGetValue(key, out value)) return value;
            if (!russian && English.TryGetValue(key, out value)) return value;
            if (English.TryGetValue(key, out value)) return value;
            return russian ? fallbackRussian : fallbackEnglish;
        }

        internal static string CustomName(SaplingKind kind)
        {
            string nameEn;
            string nameRu;
            switch (kind)
            {
                case SaplingKind.Palm: nameEn = "Big Palm Seeds"; nameRu = "Семена большой пальмы"; break;
                case SaplingKind.Pine: nameEn = "Giant Pine Seeds"; nameRu = "Семена гигантской сосны"; break;
                case SaplingKind.Birch: nameEn = "Giant Birch Seeds"; nameRu = "Семена гигантской берёзы"; break;
                case SaplingKind.Fir: nameEn = "Giant Fir Seeds"; nameRu = "Семена гигантской ели"; break;
                case SaplingKind.SnowyPine: nameEn = "Snowy Pine Seeds"; nameRu = "Семена снежной сосны"; break;
                case SaplingKind.DecorativeBanana: nameEn = "Decorative Banana Seeds"; nameRu = "Семена декоративного банана"; break;
                case SaplingKind.DeadTree: nameEn = "Dead Tree Seeds"; nameRu = "Семена сухого дерева"; break;
                case SaplingKind.BalboaBush: nameEn = "Balboa Bush Seeds"; nameRu = "Семена куста Бальбоа"; break;
                default: return kind.ToString();
            }
            return Text("sapling_" + kind + "_name", nameEn, nameRu);
        }

        internal static void ApplyVanillaSeedText(ItemInstance_Inventory settings, SaplingKind kind)
        {
            if (settings == null) return;
            if (BetterTreesSaplings.IsCustomKind(kind))
            {
                // Ванильный LocalizationTerm переопределяет DisplayName, поэтому он очищается
                settings.LocalizationTerm = string.Empty;
                settings.DisplayName = CustomName(kind);
                settings.Description = Text("sapling_" + kind + "_description",
                    "Plant this seed directly in island soil. It grows into a tree.",
                    "Посадите семечко в почву острова, чтобы вырастить дерево");
                return;
            }
            Item_Base vanilla = ItemManager.GetItemByName(VanillaSeedName(kind));
            if (vanilla == null || vanilla.settings_Inventory == null) return;
            ItemInstance_Inventory original = vanilla.settings_Inventory;
            string translated = GetVanillaItemText(original);
            settings.LocalizationTerm = original.LocalizationTerm;
            settings.DisplayName = NameOnly(translated ?? original.DisplayName);
            string description = DescriptionOnly(translated);
            settings.Description = string.IsNullOrEmpty(description) ? original.Description : description;
        }

        internal static string PlantPrompt(SaplingKind kind)
        {
            if (BetterTreesSaplings.IsCustomKind(kind))
                LocalizationParameters.itemX = CustomName(kind);
            else
            {
                Item_Base vanilla = ItemManager.GetItemByName(VanillaSeedName(kind));
                if (vanilla != null && vanilla.settings_Inventory != null)
                {
                    ItemInstance_Inventory original = vanilla.settings_Inventory;
                    LocalizationParameters.itemX = NameOnly(GetVanillaItemText(original) ?? original.DisplayName);
                }
            }
            string gameText = Helper.GetTerm("Game/PlantItemX", true);
            return string.IsNullOrEmpty(gameText) || gameText == "Game/PlantItemX" ?
                "Plant" : gameText;
        }
    }

    internal static class SaplingSettings
    {
        internal const float GrowSeconds = 4f * 20f * 60f;
        internal const float InitialSize = 0.12f;
        internal const float DroppedSaplingScale = 0.032f;
        internal const float DecorativeBananaDropScale = DroppedSaplingScale * 3f;
        internal const float PlantDistance = 5f;
        internal const int FirstItemIndex = 26001;
    }

    internal enum SaplingKind { Pine = 0, Birch = 1, Fir = 2, SnowyPine = 3, Palm = 4, Mango = 5, Banana = 6, VanillaPalm = 7, VanillaPine = 8, VanillaBirch = 9, DecorativeBanana = 10, DeadTree = 11, BalboaBush = 12 }

    internal sealed class SaplingMeshPart
    {
        internal Mesh Mesh;
        internal Material[] Materials;
        internal Vector3 Position;
        internal Quaternion Rotation;
        internal Vector3 Scale;
    }

    internal sealed class SaplingColliderPart
    {
        internal string SourceName;
        internal string Shape;
        internal Vector3 Position;
        internal Quaternion Rotation;
        internal Vector3 Scale;
        internal Vector3 Center;
        internal Vector3 Size;
        internal float Radius;
        internal float Height;
        internal int Direction;
        internal Mesh Mesh;
        internal bool Convex;
        internal PhysicMaterial Material;
    }

    internal sealed class SaplingTreeTemplate
    {
        internal string SourceName;
        internal Quaternion SourceRotation;
        internal Vector3 FullScale;
        internal List<SaplingMeshPart> Parts = new List<SaplingMeshPart>();
        internal List<Cost> HarvestYield = new List<Cost>();
        internal List<SaplingColliderPart> Colliders = new List<SaplingColliderPart>();
        internal Vector3 SourceVisualBottomOffset;
    }

    internal static class BetterTreesSaplings
    {
        private static readonly Dictionary<SaplingKind, SaplingTreeTemplate> Templates =
            new Dictionary<SaplingKind, SaplingTreeTemplate>();
        private static readonly Dictionary<SaplingKind, Item_Base> RegisteredItems =
            new Dictionary<SaplingKind, Item_Base>();
        private static readonly Dictionary<SaplingKind, float> NextPrefabLookup =
            new Dictionary<SaplingKind, float>();
        private static readonly HashSet<GameObject> PlantedObjects = new HashSet<GameObject>();
        private static float nextSceneTemplateLookup;

        internal static readonly SaplingKind[] CustomKinds = {
            SaplingKind.Pine, SaplingKind.Birch, SaplingKind.Fir, SaplingKind.SnowyPine,
            SaplingKind.Palm, SaplingKind.DecorativeBanana,
            SaplingKind.DeadTree, SaplingKind.BalboaBush
        };

        internal static bool IsCustomKind(SaplingKind kind)
        {
            for (int i = 0; i < CustomKinds.Length; i++)
                if (CustomKinds[i] == kind) return true;
            return false;
        }

        internal static bool TryGetKind(string treeName, out SaplingKind kind)
        {
            kind = SaplingKind.Pine;
            if (string.IsNullOrEmpty(treeName)) return false;
            string name = treeName.ToLowerInvariant();
            if (name.Contains("bigpalm")) { kind = SaplingKind.Palm; return true; }
            if (name.Contains("banana_bush")) { kind = SaplingKind.DecorativeBanana; return true; }
            if (name.Contains("balboa_bigbush")) { kind = SaplingKind.BalboaBush; return true; }
            if (name.Contains("deadtree") && !name.Contains("deadtree_treestumps"))
            {
                kind = SaplingKind.DeadTree;
                return true;
            }
            if (name.Contains("tp_pinetreesnowy")) { kind = SaplingKind.SnowyPine; return true; }
            if (name.Contains("birchtree_big")) { kind = SaplingKind.Birch; return true; }
            if (name.Contains("firtree_big")) { kind = SaplingKind.Fir; return true; }
            if (name.Contains("pinetree_big")) { kind = SaplingKind.Pine; return true; }
            return false;
        }

        internal static bool TryGetSelected(PlayerInventory inventory, out SaplingKind kind,
            out string consumedItemName)
        {
            kind = SaplingKind.Pine;
            consumedItemName = null;
            if (inventory == null || inventory.hotbar == null) return false;
            int selected = inventory.hotbar.GetSelectedSlotIndex();
            if (selected < 0 || selected >= inventory.allSlots.Count) return false;
            Slot slot = inventory.allSlots[selected];
            if (slot == null || slot.IsEmpty || !slot.HasValidItemInstance()) return false;
            ItemInstance item = slot.itemInstance;
            int index = item.UniqueIndex - SaplingSettings.FirstItemIndex;
            if (index >= 0 && IsCustomKind((SaplingKind)index) &&
                item.UniqueName == ItemName((SaplingKind)index))
            {
                kind = (SaplingKind)index;
                consumedItemName = item.UniqueName;
                RefreshInventoryItemLocalization(item, kind);
                return true;
            }

            // Только древесные семена — не цветы, ягоды или сельскохозяйственные культуры
            switch (item.UniqueName)
            {
                case "Seed_Palm": kind = SaplingKind.VanillaPalm; break;
                case "Seed_Pine": kind = SaplingKind.VanillaPine; break;
                case "Seed_Birch": kind = SaplingKind.VanillaBirch; break;
                case "Seed_Mango": kind = SaplingKind.Mango; break;
                case "Seed_Banana": kind = SaplingKind.Banana; break;
                default: return false;
            }
            consumedItemName = item.UniqueName;
            return true;
        }

        private static void RefreshInventoryItemLocalization(ItemInstance instance, SaplingKind kind)
        {
            if (instance == null || instance.settings_Inventory == null) return;
            SaplingLocale.ApplyVanillaSeedText(instance.settings_Inventory, kind);
        }

        private static void RefreshItemLocalization(Item_Base item, SaplingKind kind)
        {
            if (item == null || item.settings_Inventory == null) return;
            SaplingLocale.ApplyVanillaSeedText(item.settings_Inventory, kind);
        }

        internal static int ItemIndex(SaplingKind kind) { return SaplingSettings.FirstItemIndex + (int)kind; }
        internal static string ItemName(SaplingKind kind) { return "BT_Sapling_" + kind; }

        internal static void RegisterItems()
        {
            List<Item_Base> items = ItemManager.GetAllItems();
            if (items == null)
            {
                return;
            }
            foreach (SaplingKind kind in CustomKinds)
            {
                int id = ItemIndex(kind);
                Item_Base existing = items.Find(x => x != null && x.UniqueIndex == id);
                if (existing != null)
                {
                    if (existing.UniqueName == ItemName(kind))
                    {
                        RegisteredItems[kind] = existing;
                        RefreshItemLocalization(existing, kind);
                    }
                    continue;
                }
                if (RegisteredItems.ContainsKey(kind))
                {
                    items.Add(RegisteredItems[kind]);
                    continue;
                }
                string baseName = kind == SaplingKind.DecorativeBanana ? "Seed_Banana" :
                    (kind == SaplingKind.Birch || kind == SaplingKind.VanillaBirch) ? "Seed_Birch" :
                    (kind == SaplingKind.Palm || kind == SaplingKind.VanillaPalm ||
                     kind == SaplingKind.DeadTree || kind == SaplingKind.BalboaBush) ?
                    "Seed_Palm" : "Seed_Pine";
                Item_Base vanilla = ItemManager.GetItemByName(baseName);
                if (vanilla == null)
                {
                    continue;
                }
                Item_Base sapling = UnityEngine.Object.Instantiate(vanilla);
                sapling.name = ItemName(kind);
                sapling.hideFlags = HideFlags.DontSave;
                // Initialize() is intentionally not used: cloned items inherit initialized=true.
                Traverse.Create(sapling).Field("uniqueName").SetValue(ItemName(kind));
                Traverse.Create(sapling).Field("uniqueIndex").SetValue(id);
                sapling.settings_Inventory = vanilla.settings_Inventory.Clone();
                SaplingLocale.ApplyVanillaSeedText(sapling.settings_Inventory, kind);
                RegisteredItems.Add(kind, sapling);
                items.Add(sapling);
            }
        }

        // A source model may be available on the island even if it was initialized
        // before the mod's Landmark patch. This fallback caches its mesh once.
        internal static void EnsureTemplatesFromScene()
        {
            BetterTreesChoppable[] trees = UnityEngine.Object.FindObjectsOfType<BetterTreesChoppable>();
            foreach (BetterTreesChoppable state in trees)
            {
                if (state == null) continue;
                HarvestableTree tree = state.GetComponent<HarvestableTree>();
                if (tree == null || tree.Depleted ||
                    tree.GetComponent<BetterTreesSaplingGrowth>() != null) continue;
                SaplingKind kind;
                if (!TryGetKind(tree.name, out kind) || Templates.ContainsKey(kind)) continue;
                try { RememberSource(tree); }
                catch (Exception) { }
            }
        }

        // Древесные префабы берутся из оригинального PlantManager.
        // Нельзя клонировать всю сцену или родительский объект острова.
        internal static bool EnsurePlantTemplate(SaplingKind kind)
        {
            if (Templates.ContainsKey(kind)) return true;
            if (kind != SaplingKind.VanillaPine && kind != SaplingKind.VanillaBirch &&
                kind != SaplingKind.VanillaPalm && kind != SaplingKind.Mango &&
                kind != SaplingKind.Banana) return false;

            float retryAt;
            if (NextPrefabLookup.TryGetValue(kind, out retryAt) && Time.unscaledTime < retryAt)
                return false;
            NextPrefabLookup[kind] = Time.unscaledTime + 5f;

            string seed = kind == SaplingKind.Mango ? "Seed_Mango" :
                kind == SaplingKind.Banana ? "Seed_Banana" :
                (kind == SaplingKind.Palm || kind == SaplingKind.VanillaPalm) ? "Seed_Palm" :
                (kind == SaplingKind.Birch || kind == SaplingKind.VanillaBirch) ? "Seed_Birch" : "Seed_Pine";
            Network_Player local = RAPI.GetLocalPlayer();
            PlantManager manager = local != null ? local.PlantManager : null;
            if (manager == null)
            {
                return false;
            }
            List<Plant> prefabs = Traverse.Create(manager).Field("allPlants").GetValue<List<Plant>>();
            if (prefabs == null)
            {
                return false;
            }
            foreach (Plant plant in prefabs)
            {
                if (plant == null || plant.item == null || plant.item.UniqueName != seed)
                    continue;
                try
                {
                    if (RememberVanillaPlantPrefab(plant, kind)) return true;
                }
                catch (Exception) { }
            }
            return false;
        }

        private static Bounds BoundsFromParts(List<SaplingMeshPart> parts)
        {
            Bounds total = new Bounds();
            bool initialized = false;
            foreach (SaplingMeshPart part in parts)
            {
                if (part.Mesh == null) continue;
                Bounds b = part.Mesh.bounds;
                Matrix4x4 matrix = Matrix4x4.TRS(part.Position, part.Rotation, part.Scale);
                for (int x = 0; x < 2; x++)
                    for (int y = 0; y < 2; y++)
                        for (int z = 0; z < 2; z++)
                        {
                            Vector3 corner = matrix.MultiplyPoint3x4(new Vector3(
                                x == 0 ? b.min.x : b.max.x,
                                y == 0 ? b.min.y : b.max.y,
                                z == 0 ? b.min.z : b.max.z));
                            if (!initialized) { total = new Bounds(corner, Vector3.zero); initialized = true; }
                            else total.Encapsulate(corner);
                        }
            }
            return total;
        }

        private static bool RememberVanillaPlantPrefab(Plant prefab, SaplingKind kind)
        {
            // Plant_Palm реализует дерево, но идентификация выполняется по наличию HarvestableTree.
            // Это позволит использовать и другие оригинальные типы деревьев.
            HarvestableTree harvestable = prefab.GetComponentInChildren<HarvestableTree>(true);
            if (harvestable == null)
            {
                return false;
            }

            // Рендеры могут быть на родителе HarvestableTree, но внутри одного префаба Plant.
            MeshRenderer[] all = prefab.GetComponentsInChildren<MeshRenderer>(true);
            List<MeshRenderer> selected = new List<MeshRenderer>();
            LODGroup[] groups = prefab.GetComponentsInChildren<LODGroup>(true);
            if (groups.Length > 1)
            {
                return false;
            }
            foreach (MeshRenderer renderer in all)
            {
                if (renderer == null) continue;
                MeshFilter filter = renderer.GetComponent<MeshFilter>();
                if (filter == null || filter.sharedMesh == null) continue;
                string rn = renderer.name.ToLowerInvariant();
                if (rn.Contains("billboard") || rn.Contains("collision") || rn.Contains("shadowproxy"))
                    continue;
                LODGroup group = renderer.GetComponentInParent<LODGroup>();
                if (group != null)
                {
                    LOD[] lods = group.GetLODs();
                    if (lods.Length > 0)
                    {
                        bool first = false;
                        foreach (Renderer firstRenderer in lods[0].renderers)
                            if (firstRenderer == renderer) { first = true; break; }
                        if (!first) continue;
                    }
                }
                selected.Add(renderer);
            }
            if (selected.Count == 0 || selected.Count > 12)
            {
                return false;
            }
            SaplingTreeTemplate template = new SaplingTreeTemplate();
            template.SourceName = "Vanilla_" + kind;
            template.SourceRotation = Quaternion.identity;
            template.FullScale = Vector3.one;

            // Начало координат для саженца = нижняя точка модели в исходном префабе.
            // Изменение масштаба больше не может приподнять ствол над землёй.
            Vector3 origin = prefab.transform.position;
            foreach (MeshRenderer renderer in selected)
            {
                MeshFilter filter = renderer.GetComponent<MeshFilter>();
                SaplingMeshPart part = new SaplingMeshPart();
                part.Mesh = filter.sharedMesh;
                part.Materials = renderer.sharedMaterials;
                part.Position = renderer.transform.position - origin;
                part.Rotation = renderer.transform.rotation;
                part.Scale = renderer.transform.lossyScale;
                template.Parts.Add(part);
            }
            // Renderer.bounds у неактивных prefab assets иногда возвращает пустые границы.
            // Границы рассчитываются напрямую из Mesh.bounds и трансформаций мешей.
            Bounds bounds = BoundsFromParts(template.Parts);
            if (!BoundsSafeForOneTree(bounds, kind))
            {
                return false;
            }
            float visualBottom = float.PositiveInfinity;
            foreach (SaplingMeshPart part in template.Parts)
                visualBottom = Mathf.Min(visualBottom, MeshPartBottom(part));
            if (float.IsNaN(visualBottom) || float.IsInfinity(visualBottom)) return false;
            foreach (SaplingMeshPart part in template.Parts)
                part.Position.y -= visualBottom;
            template.SourceVisualBottomOffset = Vector3.zero;

            // У ванильных префабов могут быть собственные коллайдеры,
            // даже если растение ещё не выросло и коллайдеры выключены
            CacheVanillaColliders(prefab, origin, visualBottom, template);

            // Сохраняется оригинальный лут дерева; тип определяется игрой.
            PickupItem pickup = harvestable.GetComponent<PickupItem>();
            if (pickup != null && pickup.yieldHandler != null && pickup.yieldHandler.yieldAsset != null)
            {
                foreach (Cost cost in pickup.yieldHandler.yieldAsset.yieldAssets)
                    if (cost != null && cost.item != null && cost.amount > 0)
                        template.HarvestYield.Add(new Cost(cost.item, cost.amount));
            }
            if (template.HarvestYield.Count == 0)
            {
                Item_Base plank = ItemManager.GetItemByName("Plank");
                if (plank != null) template.HarvestYield.Add(new Cost(plank, 4));
            }

            Templates[kind] = template;
            return true;
        }

        private static bool PhysicsMeshReadable(Mesh mesh)
        {
            if (mesh == null) return false;
            try { return mesh.isReadable; }
            catch (Exception) { return false; }
        }

        private static void CacheVanillaColliders(Plant prefab, Vector3 origin,
            float visualBottom, SaplingTreeTemplate template)
        {
            Collider[] colliders = prefab.GetComponentsInChildren<Collider>(true);
            int skippedUnreadable = 0;
            foreach (Collider source in colliders)
            {
                if (template.Colliders.Count >= 16) break;
                if (source == null || source.isTrigger) continue;
                string name = source.name;
                if (name.IndexOf("collider_interaction_new", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    name.IndexOf("Log_InteractionCollider", StringComparison.OrdinalIgnoreCase) >= 0)
                    continue;

                SaplingColliderPart part = new SaplingColliderPart();
                part.SourceName = name;
                part.Position = source.transform.position - origin - Vector3.up * visualBottom;
                part.Rotation = source.transform.rotation;
                part.Scale = source.transform.lossyScale;
                part.Material = source.sharedMaterial;

                MeshCollider sourceMesh = source as MeshCollider;
                BoxCollider sourceBox = source as BoxCollider;
                CapsuleCollider sourceCapsule = source as CapsuleCollider;
                SphereCollider sourceSphere = source as SphereCollider;
                if (sourceMesh != null)
                {
                    if (!PhysicsMeshReadable(sourceMesh.sharedMesh))
                    {
                        skippedUnreadable++;
                        continue;
                    }
                    part.Shape = "Mesh";
                    part.Mesh = sourceMesh.sharedMesh;
                    part.Convex = sourceMesh.convex;
                }
                else if (sourceBox != null)
                {
                    part.Shape = "Box";
                    part.Center = sourceBox.center;
                    part.Size = sourceBox.size;
                }
                else if (sourceCapsule != null)
                {
                    part.Shape = "Capsule";
                    part.Center = sourceCapsule.center;
                    part.Radius = sourceCapsule.radius;
                    part.Height = sourceCapsule.height;
                    part.Direction = sourceCapsule.direction;
                }
                else if (sourceSphere != null)
                {
                    part.Shape = "Sphere";
                    part.Center = sourceSphere.center;
                    part.Radius = sourceSphere.radius;
                }
                else continue;

                template.Colliders.Add(part);
            }
        }

        private static float HorizontalDistance(Vector3 a, Vector3 b)
        {
            float dx = a.x - b.x;
            float dz = a.z - b.z;
            return Mathf.Sqrt(dx * dx + dz * dz);
        }

        private static bool BoundsSafeForOneTree(Bounds bounds, SaplingKind kind)
        {
            // Large palms are significantly wider than 12 metres at the crown.
            // Safety against copying an entire island is enforced separately by
            // requiring an individual source with at most one LODGroup.
            float maxWidth = kind == SaplingKind.Palm ? 28f : 22f;
            return bounds.size.x <= maxWidth && bounds.size.z <= maxWidth &&
                   bounds.size.y <= 55f && bounds.size.y >= 0.3f;
        }

        private static Bounds CombinedBounds(List<MeshRenderer> renderers)
        {
            Bounds bounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Count; i++)
                bounds.Encapsulate(renderers[i].bounds);
            return bounds;
        }

        private static Vector3 GetTreeAnchor(Transform root)
        {
            // The interaction collider is attached to the individual harvestable tree,
            // even if its visual parent also contains meshes from neighboring trees.
            string[] names = { "CollisionObject0", "CollisionObject1", "collider_interaction_new", "Log_InteractionCollider" };
            for (int i = 0; i < names.Length; i++)
            {
                Transform colliderTransform = root.Find(names[i]);
                if (colliderTransform == null) continue;
                Collider collider = colliderTransform.GetComponent<Collider>();
                if (collider != null) return collider.bounds.center;
                return colliderTransform.position;
            }
            return root.position;
        }

        private static List<MeshRenderer> GetMeshCandidates(HarvestableTree source)
        {
            Transform root = source.transform;
            List<MeshRenderer> results = new List<MeshRenderer>();
            foreach (MeshRenderer renderer in root.GetComponentsInChildren<MeshRenderer>(true))
            {
                if (renderer == null || renderer.GetComponent<MeshFilter>() == null ||
                    renderer.GetComponent<MeshFilter>().sharedMesh == null) continue;
                string n = renderer.name.ToLowerInvariant();
                if (n.Contains("billboard") || n.Contains("shadowproxy") || n.Contains("collision")) continue;

                // Nested harvestables are OTHER trees, not part of this source.
                HarvestableTree owner = renderer.GetComponentInParent<HarvestableTree>();
                if (owner != null && owner != source) continue;

                LODGroup group = renderer.GetComponentInParent<LODGroup>();
                if (group != null && (group.transform == root || group.transform.IsChildOf(root)))
                {
                    LOD[] lods = group.GetLODs();
                    if (lods.Length > 0)
                    {
                        bool atHighDetail = false;
                        for (int j = 0; j < lods[0].renderers.Length; j++)
                            if (lods[0].renderers[j] == renderer) { atHighDetail = true; break; }
                        if (!atHighDetail) continue;
                    }
                }
                results.Add(renderer);
            }
            return results;
        }

        private static List<MeshRenderer> IsolateOneTree(HarvestableTree source, SaplingKind kind,
            List<MeshRenderer> candidates, Vector3 anchor)
        {
            if (candidates.Count == 0) return new List<MeshRenderer>();

            // Best case: each tree owns its own LODGroup. Select exactly one group,
            // never all LODGroups under a landmark/island root.
            List<MeshRenderer> bestGroup = null;
            float bestScore = float.MaxValue;
            LODGroup[] groups = source.GetComponentsInChildren<LODGroup>(true);
            foreach (LODGroup group in groups)
            {
                LOD[] lods = group.GetLODs();
                if (lods.Length == 0) continue;
                List<MeshRenderer> members = new List<MeshRenderer>();
                for (int i = 0; i < lods[0].renderers.Length; i++)
                {
                    MeshRenderer renderer = lods[0].renderers[i] as MeshRenderer;
                    if (renderer != null && candidates.Contains(renderer) && !members.Contains(renderer))
                        members.Add(renderer);
                }
                if (members.Count == 0 || members.Count > 12) continue;
                Bounds bounds = CombinedBounds(members);
                if (!BoundsSafeForOneTree(bounds, kind)) continue;
                float score = HorizontalDistance(group.transform.position, anchor);
                if (score < bestScore) { bestScore = score; bestGroup = members; }
            }
            if (bestGroup != null)
            {
                return bestGroup;
            }

            // A tree without a separate LODGroup can still be an isolated prefab.
            if (candidates.Count <= 4 && BoundsSafeForOneTree(CombinedBounds(candidates), kind))
                return new List<MeshRenderer>(candidates);

            // Mixed landmark geometry: select ONE tight cluster of mesh pivots.
            // If all mesh pivots are identical (shared island root), cluster on
            // world renderer bounds centers instead. Do not use distance to an
            // arbitrary shared parent for positioning new trees.
            Vector3 pivotMin = candidates[0].transform.position;
            Vector3 pivotMax = pivotMin;
            foreach (MeshRenderer renderer in candidates)
            {
                pivotMin = Vector3.Min(pivotMin, renderer.transform.position);
                pivotMax = Vector3.Max(pivotMax, renderer.transform.position);
            }
            bool useBoundsCenter = HorizontalDistance(pivotMin, pivotMax) < 0.75f;
            MeshRenderer nearest = null;
            float nearestDistance = float.MaxValue;
            foreach (MeshRenderer renderer in candidates)
            {
                Vector3 visualPoint = useBoundsCenter ? renderer.bounds.center : renderer.transform.position;
                float distance = HorizontalDistance(visualPoint, anchor);
                if (distance < nearestDistance) { nearestDistance = distance; nearest = renderer; }
            }
            List<MeshRenderer> cluster = new List<MeshRenderer>();
            Vector3 origin = useBoundsCenter ? nearest.bounds.center : nearest.transform.position;
            foreach (MeshRenderer renderer in candidates)
            {
                Vector3 visualPoint = useBoundsCenter ? renderer.bounds.center : renderer.transform.position;
                if (HorizontalDistance(visualPoint, origin) <= 1.5f)
                    cluster.Add(renderer);
            }
            // A very large cluster is ambiguous; better to refuse than spawn 13 palms.
            if (cluster.Count > 8 || cluster.Count == 0 ||
                !BoundsSafeForOneTree(CombinedBounds(cluster), kind))
            {
                return new List<MeshRenderer>();
            }
            return cluster;
        }

        private static float MeshPartBottom(SaplingMeshPart part)
        {
            Bounds bounds = part.Mesh.bounds;
            Matrix4x4 matrix = Matrix4x4.TRS(part.Position, part.Rotation, part.Scale);
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;
            float bottom = float.PositiveInfinity;
            for (int x = 0; x < 2; x++)
                for (int y = 0; y < 2; y++)
                    for (int z = 0; z < 2; z++)
                    {
                        Vector3 corner = matrix.MultiplyPoint3x4(new Vector3(
                            x == 0 ? min.x : max.x,
                            y == 0 ? min.y : max.y,
                            z == 0 ? min.z : max.z));
                        bottom = Mathf.Min(bottom, corner.y);
                    }
            return bottom;
        }

        private static bool IsOriginalColliderCandidate(HarvestableTree source, Collider candidate)
        {
            if (candidate == null || !candidate.enabled || candidate.isTrigger ||
                !candidate.gameObject.activeInHierarchy) return false;
            HarvestableTree owner = candidate.GetComponentInParent<HarvestableTree>();
            if (owner != null && owner != source) return false;
            Bounds bounds = candidate.bounds;
            if (bounds.size.sqrMagnitude < 0.0001f || bounds.size.y > 55f ||
                bounds.size.x > 35f || bounds.size.z > 35f) return false;
            if (HorizontalDistance(candidate.transform.position, source.transform.position) > 14f)
                return false;
            return candidate is BoxCollider || candidate is CapsuleCollider ||
                candidate is SphereCollider || candidate is MeshCollider;
        }

        private static void RememberColliders(HarvestableTree source, Vector3 basePoint,
            SaplingTreeTemplate template)
        {
            Collider[] sourceColliders = source.GetComponentsInChildren<Collider>(true);
            List<Collider> accepted = new List<Collider>();
            int skipped = 0;
            foreach (Collider collider in sourceColliders)
            {
                if (IsOriginalColliderCandidate(source, collider)) accepted.Add(collider);
                else skipped++;
            }
            // Искусственный коллайдер взаимодействия не заменяет собственную физику дерева
            bool hasOriginal = accepted.Exists(c =>
                c.name.IndexOf("collider_interaction_new", StringComparison.OrdinalIgnoreCase) < 0);
            if (hasOriginal)
                accepted.RemoveAll(c => c.name.IndexOf("collider_interaction_new",
                    StringComparison.OrdinalIgnoreCase) >= 0);

            foreach (Collider collider in accepted)
            {
                if (template.Colliders.Count >= 16) break;
                SaplingColliderPart part = new SaplingColliderPart();
                part.SourceName = collider.name;
                part.Position = collider.transform.position - basePoint;
                part.Rotation = collider.transform.rotation;
                part.Scale = collider.transform.lossyScale;
                part.Material = collider.sharedMaterial;
                if (collider is MeshCollider)
                {
                    MeshCollider mesh = (MeshCollider)collider;
                    if (!PhysicsMeshReadable(mesh.sharedMesh))
                    {
                        continue;
                    }
                    part.Shape = "Mesh";
                    part.Mesh = mesh.sharedMesh;
                    part.Convex = mesh.convex;
                }
                else if (collider is BoxCollider)
                {
                    BoxCollider box = (BoxCollider)collider;
                    part.Shape = "Box";
                    part.Center = box.center;
                    part.Size = box.size;
                }
                else if (collider is CapsuleCollider)
                {
                    CapsuleCollider capsule = (CapsuleCollider)collider;
                    part.Shape = "Capsule";
                    part.Center = capsule.center;
                    part.Height = capsule.height;
                    part.Radius = capsule.radius;
                    part.Direction = capsule.direction;
                }
                else if (collider is SphereCollider)
                {
                    SphereCollider sphere = (SphereCollider)collider;
                    part.Shape = "Sphere";
                    part.Center = sphere.center;
                    part.Radius = sphere.radius;
                }
                else continue;
                template.Colliders.Add(part);
            }
        }

        private static float FindTerrainHeightAtSource(Transform root, out bool found)
        {
            found = false;
            Ray ray = new Ray(root.position + Vector3.up * 6f, Vector3.down);
            RaycastHit[] hits = Physics.RaycastAll(ray, 15f, ~0, QueryTriggerInteraction.Ignore);
            float nearest = float.MaxValue;
            float result = root.position.y;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider == null ||
                    !hit.collider.name.StartsWith("Terrain", StringComparison.OrdinalIgnoreCase))
                    continue;
                float distance = Mathf.Abs(root.position.y - hit.point.y);
                if (distance > 3f || distance >= nearest) continue;
                nearest = distance;
                result = hit.point.y;
                found = true;
            }
            return result;
        }

        internal static void RememberSource(HarvestableTree source)
        {
            if (source == null)
            {
                return;
            }
            SaplingKind kind;
            if (!TryGetKind(source.gameObject.name, out kind))
            {
                return;
            }
            if (Templates.ContainsKey(kind)) return;
            Transform root = source.transform;
            Vector3 anchor = GetTreeAnchor(root);
            LODGroup[] sourceLods = root.GetComponentsInChildren<LODGroup>(true);
            // BigPalm (the island container) has ~15 LODGroups. BigPalm1(Clone)
            // has ONE. Never create a sapling from that shared island container.
            if (sourceLods.Length > 1)
            {
                return;
            }
            List<MeshRenderer> candidates = GetMeshCandidates(source);
            List<MeshRenderer> selected = IsolateOneTree(source, kind, candidates, anchor);
            if (selected.Count == 0)
            {
                return;
            }
            Bounds visualBounds = CombinedBounds(selected);
            if (!BoundsSafeForOneTree(visualBounds, kind))
            {
                return;
            }
            if (HorizontalDistance(visualBounds.center, root.position) > 14f)
            {
                return;
            }
            // Для посадки используется уровень основания исходного дерева, а не край кроны
            bool groundFound;
            float sourceGroundY = FindTerrainHeightAtSource(root, out groundFound);
            Vector3 treeBase = new Vector3(root.position.x, sourceGroundY, root.position.z);
            SaplingTreeTemplate template = new SaplingTreeTemplate();
            template.SourceName = source.name;
            template.SourceRotation = Quaternion.identity;
            template.FullScale = Vector3.one;
            foreach (MeshRenderer renderer in selected)
            {
                MeshFilter filter = renderer.GetComponent<MeshFilter>();
                if (filter == null || filter.sharedMesh == null) continue;
                SaplingMeshPart part = new SaplingMeshPart();
                part.Mesh = filter.sharedMesh;
                part.Materials = renderer.sharedMaterials;
                part.Position = renderer.transform.position - treeBase;
                part.Rotation = renderer.transform.rotation;
                part.Scale = renderer.transform.lossyScale;
                template.Parts.Add(part);
            }
            if (template.Parts.Count == 0) return;
            float visualBottomLocal = float.PositiveInfinity;
            foreach (SaplingMeshPart part in template.Parts)
                visualBottomLocal = Mathf.Min(visualBottomLocal, MeshPartBottom(part));
            template.SourceVisualBottomOffset = new Vector3(0f, visualBottomLocal, 0f);
            RememberColliders(source, treeBase, template);
            PickupItem pi = source.PickupItem;
            if (pi != null && pi.yieldHandler != null && pi.yieldHandler.yieldAsset != null)
                foreach (Cost cost in pi.yieldHandler.yieldAsset.yieldAssets)
                    if (cost != null && cost.item != null && cost.amount > 0)
                        template.HarvestYield.Add(new Cost(cost.item, cost.amount));
            if (template.HarvestYield.Count == 0)
            {
                Item_Base plank = ItemManager.GetItemByName("Plank");
                if (plank != null) template.HarvestYield.Add(new Cost(plank, 8));
            }
            Templates.Add(kind, template);
        }

        internal static bool DropOne(HarvestableTree source, Vector3 position, SaplingKind? explicitKind = null)
        {
            if (!Raft_Network.IsHost)
            {
                return false;
            }
            if (source == null)
            {
                return false;
            }
            SaplingKind kind;
            if (explicitKind.HasValue) kind = explicitKind.Value;
            else if (!TryGetKind(source.gameObject.name, out kind))
            {
                return false;
            }
            try { RegisterItems(); }
            catch (Exception) { return false; }
            Item_Base item;
            if (!RegisteredItems.TryGetValue(kind, out item))
            {
                return false;
            }
            try
            {
                Vector3 spawnPosition = position + Vector3.up * 0.65f;
                Helper.DropItem(new ItemInstance(item, 1, item.MaxUses), spawnPosition,
                    Vector3.up, false);
                return true;
            }
            catch (Exception) { return false; }
        }

        internal static bool DropVanillaSeed(string seedName, HarvestableTree source)
        {
            if (!Raft_Network.IsHost || source == null || string.IsNullOrEmpty(seedName)) return false;
            Item_Base item = ItemManager.GetItemByName(seedName);
            if (item == null)
            {
                return false;
            }
            try
            {
                Helper.DropItem(new ItemInstance(item, 1, item.MaxUses),
                    source.transform.position + Vector3.up * 0.65f, Vector3.up, false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool TryGetVanillaSeed(HarvestableTree source, out string seedName)
        {
            seedName = null;
            if (source == null) return false;

            Plant originalPlant = source.GetComponentInParent<Plant>();
            if (originalPlant != null && originalPlant.item != null)
            {
                string plantSeed = originalPlant.item.UniqueName;
                if (plantSeed == "Seed_Birch" || plantSeed == "Seed_Banana")
                {
                    seedName = plantSeed;
                    return true;
                }
            }

            // Малые ванильные деревья не перенастраиваются модом - добавляются только семена
            string treeName = source.name.ToLowerInvariant();
            Transform parent = source.transform.parent;
            if (parent != null && parent.GetComponent<Landmark>() == null)
                treeName += " " + parent.name.ToLowerInvariant();
            if (treeName.Contains("birch") && !treeName.Contains("birchtree_big"))
                seedName = "Seed_Birch";
            else if ((treeName.Contains("firtree") || treeName.Contains("fir_tree") ||
                treeName.Contains("tree_fir") || treeName.Contains("firsmall")) &&
                !treeName.Contains("firtree_big"))
                seedName = "Seed_Pine";
            else if ((treeName.Contains("bananatree") || treeName.Contains("banana_tree") ||
                treeName.Contains("banana_plant")) && !treeName.Contains("banana_bush"))
                seedName = "Seed_Banana";
            return seedName != null;
        }

        internal static string SeedNameForKind(SaplingKind kind)
        {
            switch (kind)
            {
                case SaplingKind.VanillaBirch: return "Seed_Birch";
                case SaplingKind.VanillaPine: return "Seed_Pine";
                case SaplingKind.VanillaPalm: return "Seed_Palm";
                case SaplingKind.Mango: return "Seed_Mango";
                case SaplingKind.Banana: return "Seed_Banana";
                default: return null;
            }
        }

        internal static bool IsSaplingItem(ItemInstance item, out SaplingKind kind)
        {
            kind = SaplingKind.Pine;
            if (item == null) return false;
            int kindIndex = item.UniqueIndex - SaplingSettings.FirstItemIndex;
            if (kindIndex < 0 || !IsCustomKind((SaplingKind)kindIndex)) return false;
            kind = (SaplingKind)kindIndex;
            return item.UniqueName == ItemName(kind);
        }

        internal static bool AttachDropModel(PickupItem pickup)
        {
            if (pickup == null) return true;
            if (pickup.transform.Find("BT_Miniature") != null) return true;
            SaplingKind kind;
            if (!IsSaplingItem(pickup.itemInstance, out kind)) return true;
            if (pickup.itemInstance.settings_Inventory != null)
                SaplingLocale.ApplyVanillaSeedText(pickup.itemInstance.settings_Inventory, kind);
            SaplingTreeTemplate template;
            if (!Templates.TryGetValue(kind, out template)) return false;
            GameObject miniature = new GameObject("BT_Miniature");
            miniature.transform.SetParent(pickup.transform, false);
            bool smallVegetation = kind == SaplingKind.DeadTree || kind == SaplingKind.BalboaBush;
            miniature.transform.localPosition = new Vector3(0f,
                kind == SaplingKind.DecorativeBanana || smallVegetation ? 0f : -0.25f, 0f);
            miniature.transform.localRotation = Quaternion.identity;
            float dropScale = kind == SaplingKind.DecorativeBanana ?
                SaplingSettings.DecorativeBananaDropScale : SaplingSettings.DroppedSaplingScale;
            if (smallVegetation)
            {
                // Миниатюра низкого кустарника не должна теряться в траве
                float height = BoundsFromParts(template.Parts).size.y;
                if (height > 0.01f)
                    dropScale = Mathf.Clamp(0.4f / height, 0.04f, 0.3f);
            }
            miniature.transform.localScale = template.FullScale * dropScale;
            foreach (SaplingMeshPart part in template.Parts)
            {
                if (part.Mesh == null) continue;
                GameObject child = new GameObject("SaplingMesh");
                child.transform.SetParent(miniature.transform, false);
                child.transform.localPosition = part.Position;
                child.transform.localRotation = part.Rotation;
                child.transform.localScale = part.Scale;
                child.AddComponent<MeshFilter>().sharedMesh = part.Mesh;
                child.AddComponent<MeshRenderer>().sharedMaterials = part.Materials;
            }
            // Preserve the physical pickup and its collision/interaction scripts;
            // only hide the generic box visual.
            foreach (MeshRenderer renderer in pickup.GetComponentsInChildren<MeshRenderer>(true))
                if (renderer != null && !renderer.transform.IsChildOf(miniature.transform))
                    renderer.enabled = false;
            pickup.transform.rotation = Quaternion.identity;
            Rigidbody body = pickup.GetComponent<Rigidbody>();
            if (body != null) body.constraints |= RigidbodyConstraints.FreezeRotation;
            return true;
        }

        internal static bool HasTemplate(SaplingKind kind)
        {
            if (Templates.ContainsKey(kind)) return true;
            if (IsCustomKind(kind))
            {
                if (Time.unscaledTime >= nextSceneTemplateLookup)
                {
                    nextSceneTemplateLookup = Time.unscaledTime + 1f;
                    EnsureTemplatesFromScene();
                }
                return Templates.ContainsKey(kind);
            }
            return EnsurePlantTemplate(kind);
        }

        internal static bool CanPlant(SaplingKind kind, Vector3 position)
        {
            if (!Templates.ContainsKey(kind)) EnsureTemplatesFromScene();
            if (!Templates.ContainsKey(kind)) EnsurePlantTemplate(kind);
            if (!Templates.ContainsKey(kind))
            {
                return false;
            }
            Collider[] hits = Physics.OverlapSphere(position + Vector3.up * 0.4f, 0.4f,
                ~0, QueryTriggerInteraction.Ignore);
            foreach (Collider hit in hits)
            {
                if (hit == null) continue;
                if (hit.GetComponentInParent<HarvestableTree>() != null)
                {
                    return false;
                }
            }
            return true;
        }

        private static Collider CreateOriginalCollider(GameObject target, SaplingColliderPart part)
        {
            Collider result;
            if (part.Shape == "Mesh")
            {
                if (!PhysicsMeshReadable(part.Mesh)) return null;
                MeshCollider collider = target.AddComponent<MeshCollider>();
                collider.sharedMesh = part.Mesh;
                collider.convex = part.Convex;
                result = collider;
            }
            else if (part.Shape == "Box")
            {
                BoxCollider collider = target.AddComponent<BoxCollider>();
                collider.center = part.Center;
                collider.size = part.Size;
                result = collider;
            }
            else if (part.Shape == "Capsule")
            {
                CapsuleCollider collider = target.AddComponent<CapsuleCollider>();
                collider.center = part.Center;
                collider.radius = part.Radius;
                collider.height = part.Height;
                collider.direction = part.Direction;
                result = collider;
            }
            else if (part.Shape == "Sphere")
            {
                SphereCollider collider = target.AddComponent<SphereCollider>();
                collider.center = part.Center;
                collider.radius = part.Radius;
                result = collider;
            }
            else return null;
            result.sharedMaterial = part.Material;
            result.isTrigger = false;
            result.enabled = false;
            return result;
        }

        internal static bool Plant(SaplingKind kind, Vector3 point, bool removeLocalItem,
            string consumedItemName = null, Transform groundTransform = null,
            BetterTreesSavedTree restored = null)
        {
            if (!Templates.ContainsKey(kind)) EnsureTemplatesFromScene();
            if (!Templates.ContainsKey(kind)) EnsurePlantTemplate(kind);
            SaplingTreeTemplate template;
            if (!Templates.TryGetValue(kind, out template))
            {
                return false;
            }
            GameObject root = new GameObject("BT_Planted_" + template.SourceName);
            root.transform.position = point;
            root.transform.rotation = restored != null ? restored.Rotation : template.SourceRotation;
            Landmark placementLandmark = BetterTreesTreePersistence.FindPlantingLandmark(
                point, groundTransform, restored);
            if (placementLandmark != null)
                root.transform.SetParent(placementLandmark.transform, true);
            root.transform.localScale = Vector3.Scale(template.FullScale,
                Vector3.one * SaplingSettings.InitialSize);
            root.tag = "Untagged";
            GameObject visuals = new GameObject("BT_TreeVisuals");
            visuals.transform.SetParent(root.transform, false);
            List<Collider> matureColliders = new List<Collider>();
            List<GameObject> meshVisuals = new List<GameObject>();
            foreach (SaplingMeshPart part in template.Parts)
            {
                if (part.Mesh == null) continue;
                GameObject mesh = new GameObject("Mesh");
                mesh.transform.SetParent(visuals.transform, false);
                mesh.transform.localPosition = part.Position;
                mesh.transform.localRotation = part.Rotation;
                mesh.transform.localScale = part.Scale;
                mesh.AddComponent<MeshFilter>().sharedMesh = part.Mesh;
                mesh.AddComponent<MeshRenderer>().sharedMaterials = part.Materials;
                meshVisuals.Add(mesh);
            }
            PickupItem pickup = root.AddComponent<PickupItem>();
            pickup.canBePickedUp = false;
            pickup.pickupItemType = PickupItemType.Default;
            pickup.requiresHands = false;
            PickupItem_Networked pickupNet = root.AddComponent<PickupItem_Networked>();
            pickup.networkID = pickupNet;
            YieldHandler yield = root.AddComponent<YieldHandler>();
            SO_ItemYield yieldAsset = ScriptableObject.CreateInstance<SO_ItemYield>();
            yieldAsset.hideFlags = HideFlags.DontSave;
            yieldAsset.yieldAssets = new List<Cost>();
            foreach (Cost c in template.HarvestYield)
                yieldAsset.yieldAssets.Add(new Cost(c.item, c.amount));
            yield.yieldAsset = yieldAsset;
            yield.ResetYield();
            pickup.yieldHandler = yield;
            HarvestableTree tree = root.AddComponent<HarvestableTree>();
            Traverse.Create(tree).Field("hideOnDepleted").SetValue(true);
            Traverse.Create(tree).Field("particleSystemName").SetValue("Particle_TreeBreak");
            Traverse.Create(tree).Field("particleSpawnPositions").SetValue(new Transform[] { root.transform });
            root.AddComponent<BetterTreesChoppable>();
            int treeLayer = LayerMask.NameToLayer("Tree");
            if (treeLayer < 0) treeLayer = 0;

            foreach (SaplingColliderPart part in template.Colliders)
            {
                try
                {
                    GameObject holder = new GameObject("BT_OriginalCollider_" + part.SourceName);
                    holder.transform.SetParent(root.transform, false);
                    holder.transform.localPosition = part.Position;
                    holder.transform.localRotation = part.Rotation;
                    holder.transform.localScale = part.Scale;
                    holder.layer = treeLayer;
                    holder.tag = "Tree";
                    Collider cloned = CreateOriginalCollider(holder, part);
                    if (cloned != null)
                    {
                        holder.AddComponent<RaycastInteractable>();
                        matureColliders.Add(cloned);
                    }
                    else UnityEngine.Object.Destroy(holder);
                }
                catch (Exception)
                {
                }
            }

            if (matureColliders.Count == 0)
            {
                // Резервная коллизия повторяет меш дерева, а не форму цилиндра
                foreach (GameObject mesh in meshVisuals)
                {
                    try
                    {
                        MeshFilter filter = mesh.GetComponent<MeshFilter>();
                        if (filter == null || filter.sharedMesh == null) continue;
                        if (!PhysicsMeshReadable(filter.sharedMesh))
                        {
                            continue;
                        }
                        MeshCollider collider = mesh.AddComponent<MeshCollider>();
                        collider.convex = false;
                        collider.sharedMesh = filter.sharedMesh;
                        collider.isTrigger = false;
                        collider.enabled = false;
                        mesh.layer = treeLayer;
                        mesh.tag = "Tree";
                        mesh.AddComponent<RaycastInteractable>();
                        matureColliders.Add(collider);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            if (matureColliders.Count == 0)
            {
                // Для unreadable-мешей Unity не может создать MeshCollider
                // Резервный BoxCollider покрывает нижнюю часть ствола
                Bounds model = BoundsFromParts(template.Parts);
                if (model.size.y > 0.1f && model.size.x > 0.01f && model.size.z > 0.01f)
                {
                    GameObject holder = new GameObject("BT_TrunkCollider_Fallback");
                    holder.transform.SetParent(root.transform, false);
                    holder.layer = treeLayer;
                    holder.tag = "Tree";
                    float height = Mathf.Clamp(model.size.y * 0.45f, 1.5f, 8f);
                    float width = Mathf.Clamp(Mathf.Min(model.size.x, model.size.z) * 0.16f,
                        0.35f, 1.3f);
                    float x = model.min.x <= 0f && model.max.x >= 0f ? 0f : model.center.x;
                    float z = model.min.z <= 0f && model.max.z >= 0f ? 0f : model.center.z;
                    BoxCollider fallback = holder.AddComponent<BoxCollider>();
                    fallback.center = new Vector3(x, model.min.y + height * 0.5f, z);
                    fallback.size = new Vector3(width, height, width);
                    fallback.isTrigger = false;
                    holder.AddComponent<RaycastInteractable>();
                    matureColliders.Add(fallback);
                }
            }

            if (matureColliders.Count == 0)
            {
                UnityEngine.Object.Destroy(root);
                return false;
            }

            BetterTreesSaplingGrowth growth = root.AddComponent<BetterTreesSaplingGrowth>();
            growth.Initialize(pickup, matureColliders, template.FullScale, point);
            growth.SaplingType = kind;
            BetterTreesTreePersistence.Attach(kind, growth, placementLandmark, restored);
            if (restored != null) growth.RestoreProgress(restored.GrowthSeconds);
            PlantedObjects.Add(root);
            if (removeLocalItem)
            {
                Network_Player player = RAPI.GetLocalPlayer();
                if (player != null && player.IsLocalPlayer)
                    player.Inventory.RemoveItem(string.IsNullOrEmpty(consumedItemName) ?
                        ItemName(kind) : consumedItemName, 1);
            }
            return true;
        }

        internal static void ClearForUnload()
        {
            BetterTreesTreePersistence.Reset();
            foreach (GameObject root in PlantedObjects) if (root != null) UnityEngine.Object.Destroy(root);
            PlantedObjects.Clear();
            Templates.Clear();
            NextPrefabLookup.Clear();
            nextSceneTemplateLookup = 0f;
            // Do not destroy registered items while the inventory can still reference them.
        }
    }

    internal sealed class BetterTreesSaplingGrowth : MonoBehaviour
    {
        private float elapsed;
        private Vector3 fullScale;
        private PickupItem pickup;
        private readonly List<Collider> matureColliders = new List<Collider>();
        private bool initialized;
        internal string PersistentId { get; private set; }
        internal SaplingKind SaplingType { get; set; }

        internal void BindPersistence(string id) { PersistentId = id; }

        internal float ElapsedSeconds { get { return elapsed; } }

        internal void RestoreProgress(float seconds)
        {
            elapsed = Mathf.Clamp(seconds, 0f, SaplingSettings.GrowSeconds);
            ApplyProgress();
            if (elapsed >= SaplingSettings.GrowSeconds)
                FinishGrowth();
            BetterTreesTreePersistence.UpdateGrowth(PersistentId, elapsed, transform);
        }

        internal void Initialize(PickupItem pickupItem, List<Collider> colliders,
            Vector3 originalScale, Vector3 plantedPoint)
        {
            pickup = pickupItem;
            matureColliders.AddRange(colliders);
            fullScale = originalScale;
            initialized = true;
            foreach (Collider collider in matureColliders)
            {
                if (collider == null) continue;
                collider.gameObject.tag = "Untagged";
                collider.enabled = true;
            }
            Physics.SyncTransforms();
        }

        private void ApplyProgress()
        {
            float fraction = Mathf.Clamp01(elapsed / SaplingSettings.GrowSeconds);
            transform.localScale = fullScale * Mathf.Lerp(SaplingSettings.InitialSize, 1f, fraction);
        }

        private void FinishGrowth()
        {
            pickup.canBePickedUp = true;
            transform.tag = "Tree";
            foreach (Collider collider in matureColliders)
            {
                if (collider == null) continue;
                collider.gameObject.tag = "Tree";
                collider.enabled = true;
            }
            Physics.SyncTransforms();
            enabled = false;
        }

        private void Update()
        {
            if (!initialized) return;
            elapsed = Mathf.Min(SaplingSettings.GrowSeconds, elapsed + Time.deltaTime);
            ApplyProgress();
            BetterTreesTreePersistence.UpdateGrowth(PersistentId, elapsed, transform);
            float fraction = Mathf.Clamp01(elapsed / SaplingSettings.GrowSeconds);
            if (fraction >= 1f) FinishGrowth();
        }

        private void OnDestroy()
        {
            BetterTreesTreePersistence.Detach(PersistentId, gameObject);
        }
    }

    internal sealed class BetterTreesSaplingInput : MonoBehaviour
    {
        private float nextItemCheck;
        private bool isDisplayingPlantText;
        private bool worldWasEntered;
        private CanvasHelper canvas;

        private void HidePlantText()
        {
            if (!isDisplayingPlantText) return;
            if (canvas == null) canvas = ComponentManager<CanvasHelper>.Value;
            if (canvas != null && canvas.displayTextManager != null)
                canvas.displayTextManager.HideDisplayTexts(0);
            isDisplayingPlantText = false;
        }

        private void OnDisable()
        {
            HidePlantText();
        }

        private void OnDestroy()
        {
            HidePlantText();
        }

        private static bool IsPlantableIslandGround(RaycastHit hit)
        {
            if (hit.collider == null || hit.transform == null) return false;
            if (Vector3.Dot(hit.normal, Vector3.up) < 0.7f) return false;
            if (hit.transform.GetComponentInParent<HarvestableTree>() != null) return false;

            // Сейчас разрешена только поверхность Terrain, отдельные объекты камней исключаются
            string surfaceName = hit.collider.name;
            if (surfaceName.IndexOf("rock", StringComparison.OrdinalIgnoreCase) >= 0 ||
                surfaceName.IndexOf("stone", StringComparison.OrdinalIgnoreCase) >= 0 ||
                surfaceName.IndexOf("boulder", StringComparison.OrdinalIgnoreCase) >= 0 ||
                surfaceName.IndexOf("cliff", StringComparison.OrdinalIgnoreCase) >= 0)
                return false;
            return surfaceName.StartsWith("Terrain", StringComparison.OrdinalIgnoreCase);
        }

        private void ShowPlantText(SaplingKind kind, InputAction action)
        {
            if (canvas == null) canvas = ComponentManager<CanvasHelper>.Value;
            if (canvas == null || canvas.displayTextManager == null) return;
            string label = SaplingLocale.PlantPrompt(kind);
            canvas.displayTextManager.ShowText(label,
                action.GetBindingDisplayString((InputBinding.DisplayStringOptions)0, null),
                MyInput.Keybinds["Interact"].MainKey, 0, 0, false);
            isDisplayingPlantText = true;
        }

        private void Update()
        {
            if (Time.unscaledTime >= nextItemCheck)
            {
                nextItemCheck = Time.unscaledTime + 2f;
                try { BetterTreesSaplings.RegisterItems(); }
                catch (Exception) { }
            }
            if (Raft_Network.InMenuScene)
            {
                if (worldWasEntered)
                {
                    BetterTreesSaplings.ClearForUnload();
                    worldWasEntered = false;
                }
                HidePlantText();
                return;
            }
            worldWasEntered = true;
            if (CanvasHelper.ActiveMenu != MenuType.None)
            {
                HidePlantText();
                return;
            }
            BetterTreesTreePersistence.PollRestore();
            Network_Player player = RAPI.GetLocalPlayer();
            if (player == null || !player.IsLocalPlayer)
            {
                HidePlantText();
                return;
            }
            SaplingKind kind;
            string consumedItemName;
            if (!BetterTreesSaplings.TryGetSelected(player.Inventory, out kind, out consumedItemName))
            {
                HidePlantText();
                return;
            }
            InputAction action;
            try
            {
                PlayerInput playerInput = PlayerInput.GetPlayerByIndex(0);
                action = playerInput != null && playerInput.actions != null ?
                    playerInput.actions["Interact"] : null;
            }
            catch (Exception)
            {
                HidePlantText();
                return;
            }
            if (action == null)
            {
                HidePlantText();
                return;
            }

            RaycastHit hit;
            if (!Helper.HitAtCursor(out hit, SaplingSettings.PlantDistance, ~0,
                QueryTriggerInteraction.Ignore))
            {
                HidePlantText();
                return;
            }
            bool validGround = IsPlantableIslandGround(hit);
            bool pressed = SimpleMonoBehaviourSingleton<CustomInputConfig>.Instance != null &&
                SimpleMonoBehaviourSingleton<CustomInputConfig>.Instance.WasPressedThisFrame(action, "Interact");
            if (!validGround || !Raft_Network.IsHost || !BetterTreesSaplings.HasTemplate(kind))
            {
                HidePlantText();
                return;
            }

            try
            {
                ShowPlantText(kind, action);
            }
            catch (Exception) { }
            if (!pressed) return;
            HidePlantText();
            if (!BetterTreesSaplings.CanPlant(kind, hit.point)) return;
            bool planted;
            try
            {
                planted = BetterTreesSaplings.Plant(kind, hit.point, true, consumedItemName, hit.transform);
            }
            catch (Exception)
            {
                return;
            }
            if (!planted) return;
            if (player.Animator != null)
                player.Animator.SetAnimation(PlayerAnimation.Trigger_Plant, true);
            if (player.PlantManager != null)
                FMODUnity.RuntimeManager.PlayOneShot(player.PlantManager.er_Plant, hit.point);
        }

    }
}

namespace BetterTrees
{
    [HarmonyPatch(typeof(UseItemController), "GetItemNameFromUsable")]
    internal static class BetterTreesSaplingHandPatch
    {
        private static readonly string[] FallbackSeeds =
        {
            "Seed_Palm", "Seed_Pine", "Seed_Birch", "Seed_Banana", "Seed_Mango"
        };

        [HarmonyPrefix]
        private static bool Prefix(UseItemController __instance, Item_Base item, ref string __result)
        {
            if (__instance == null || item == null) return true;
            int kindIndex = item.UniqueIndex - SaplingSettings.FirstItemIndex;
            SaplingKind kind = (SaplingKind)kindIndex;
            if (kindIndex < 0 || !BetterTreesSaplings.IsCustomKind(kind) ||
                item.UniqueName != BetterTreesSaplings.ItemName(kind))
                return true;

            string preferredSeed = kind == SaplingKind.Palm ||
                kind == SaplingKind.DeadTree || kind == SaplingKind.BalboaBush ? "Seed_Palm" :
                kind == SaplingKind.Birch ? "Seed_Birch" :
                kind == SaplingKind.DecorativeBanana ? "Seed_Banana" : "Seed_Pine";

            string connection = FindVanillaConnection(__instance, preferredSeed);
            if (connection == null)
            {
                foreach (string fallback in FallbackSeeds)
                {
                    connection = FindVanillaConnection(__instance, fallback);
                    if (connection != null) break;
                }
            }
            if (connection == null) return true;
            __result = connection;
            return false;
        }

        private static string FindVanillaConnection(UseItemController controller, string seedName)
        {
            Item_Base vanilla = ItemManager.GetItemByName(seedName);
            if (vanilla == null) return null;
            string connectionName = controller.GetItemNameFromUsable(vanilla);
            if (string.IsNullOrEmpty(connectionName)) return null;
            foreach (ItemConnection connection in controller.allConnections)
            {
                if (connection != null && connection.inventoryItem != null &&
                    connection.inventoryItem.UniqueName == connectionName)
                    return connectionName;
            }
            return null;
        }
    }
}

namespace BetterTrees
{
    // A networked vanilla dropped-item box becomes a miniature of the source tree.
    // If the mesh cache arrives a few frames later, the helper retries.
    [HarmonyPatch(typeof(PickupObjectManager), "DropItem")]
    internal static class BetterTreesSaplingPickupPatch
    {
        [HarmonyPostfix]
        internal static void Postfix(ItemInstance item, uint dropObjectIndex)
        {
            SaplingKind kind;
            if (!BetterTreesSaplings.IsSaplingItem(item, out kind)) return;
            if (item.settings_Inventory != null)
                SaplingLocale.ApplyVanillaSeedText(item.settings_Inventory, kind);
            PickupItem_Networked networkID =
                NetworkIDManager.GetNetworkIDFromObjectIndex<PickupItem_Networked>(dropObjectIndex);
            if (networkID != null && networkID.PickupItem != null)
            {
                if (networkID.GetComponent<BetterTreesSaplingPickupVisual>() == null)
                    networkID.gameObject.AddComponent<BetterTreesSaplingPickupVisual>();
            }
        }
    }

    internal sealed class BetterTreesSaplingPickupVisual : MonoBehaviour
    {
        private float elapsed;
        private void Update()
        {
            PickupItem pickup = GetComponent<PickupItem>();
            if (pickup == null) { Destroy(this); return; }
            if (BetterTreesSaplings.AttachDropModel(pickup))
            {
                Destroy(this);
                return;
            }
            elapsed += Time.deltaTime;
            if (elapsed > 15f)
            {
                Destroy(this);
            }
        }
    }
}

namespace BetterTrees
{
    [HarmonyPatch(typeof(PickupItem), "GetPickupName")]
    internal static class BetterTreesSaplingPickupNamePatch
    {
        [HarmonyPostfix]
        private static void Postfix(PickupItem __instance, ref string __result)
        {
            if (__instance == null) return;
            SaplingKind kind;
            if (!BetterTreesSaplings.IsSaplingItem(__instance.itemInstance, out kind)) return;
            if (__instance.itemInstance.settings_Inventory != null)
            {
                SaplingLocale.ApplyVanillaSeedText(__instance.itemInstance.settings_Inventory, kind);
                __result = __instance.itemInstance.settings_Inventory.DisplayName;
            }
            else
                __result = SaplingLocale.NameOnly(__result);
        }
    }
}

namespace BetterTrees
{
    // Посаженные деревья сохраняются отдельно от ванильных грядок
    internal sealed class BetterTreesSavedTree
    {
        internal string Id;
        internal SaplingKind Kind;
        internal Vector3 WorldPoint;
        internal Vector3 LocalPoint;
        internal Quaternion Rotation;
        internal uint LandmarkIndex;
        internal float GrowthSeconds;
        internal GameObject RuntimeRoot;
    }

    internal static class BetterTreesTreePersistence
    {
        private const string SaveFileName = "BetterTrees-PlantedTrees-v1.txt";
        private static readonly Dictionary<string, BetterTreesSavedTree> Trees =
            new Dictionary<string, BetterTreesSavedTree>();
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
        private static float nextRestore;
        private static bool loadInProgress;

        internal static Landmark FindPlantingLandmark(Vector3 point, Transform ground,
            BetterTreesSavedTree restored)
        {
            if (restored != null)
                return restored.LandmarkIndex != 0 ? FindLandmarkByIndex(restored.LandmarkIndex) : null;
            Landmark parent = ground != null ? ground.GetComponentInParent<Landmark>() : null;
            if (parent != null && parent.uniqueLandmarkIndex != 0) return parent;
            // Для поверхности без родительского Landmark выбирается ближайший остров
            Landmark nearest = null;
            float minDistance = 160f * 160f;
            if (WorldManager.AllLandmarks != null)
            {
                foreach (Landmark landmark in WorldManager.AllLandmarks)
                {
                    if (landmark == null || !landmark.isSpawned ||
                        landmark.uniqueLandmarkIndex == 0) continue;
                    float sqr = (landmark.transform.position - point).sqrMagnitude;
                    if (sqr < minDistance) { nearest = landmark; minDistance = sqr; }
                }
            }
            return nearest;
        }

        private static Landmark FindLandmarkByIndex(uint index)
        {
            if (index == 0 || WorldManager.AllLandmarks == null) return null;
            foreach (Landmark landmark in WorldManager.AllLandmarks)
                if (landmark != null && landmark.isSpawned &&
                    landmark.uniqueLandmarkIndex == index) return landmark;
            return null;
        }

        internal static void Attach(SaplingKind kind, BetterTreesSaplingGrowth growth,
            Landmark landmark, BetterTreesSavedTree restored)
        {
            BetterTreesSavedTree record = restored ?? new BetterTreesSavedTree();
            if (restored == null)
            {
                record.Id = Guid.NewGuid().ToString("N");
                record.Kind = kind;
                record.GrowthSeconds = 0f;
            }
            record.RuntimeRoot = growth.gameObject;
            record.WorldPoint = growth.transform.position;
            record.Rotation = growth.transform.rotation;
            record.LandmarkIndex = landmark != null ? landmark.uniqueLandmarkIndex : 0;
            record.LocalPoint = landmark != null ?
                landmark.transform.InverseTransformPoint(growth.transform.position) : Vector3.zero;
            Trees[record.Id] = record;
            loadInProgress = true;
            growth.BindPersistence(record.Id);
        }

        internal static void UpdateGrowth(string id, float elapsed, Transform t)
        {
            if (string.IsNullOrEmpty(id)) return;
            BetterTreesSavedTree record;
            if (!Trees.TryGetValue(id, out record)) return;
            record.GrowthSeconds = Mathf.Clamp(elapsed, 0f, SaplingSettings.GrowSeconds);
            if (t != null)
            {
                record.WorldPoint = t.position;
                record.Rotation = t.rotation;
                Landmark owner = FindLandmarkByIndex(record.LandmarkIndex);
                if (owner != null) record.LocalPoint = owner.transform.InverseTransformPoint(t.position);
            }
        }

        internal static void Detach(string id, GameObject source)
        {
            if (string.IsNullOrEmpty(id)) return;
            BetterTreesSavedTree record;
            if (Trees.TryGetValue(id, out record) && record.RuntimeRoot == source)
                record.RuntimeRoot = null;
        }

        internal static void RemoveDepleted(BetterTreesSaplingGrowth growth)
        {
            if (growth == null || string.IsNullOrEmpty(growth.PersistentId)) return;
            Trees.Remove(growth.PersistentId);
        }

        internal static void Reset()
        {
            Trees.Clear();
            loadInProgress = false;
            nextRestore = 0f;
        }

        private static string SaveDirectory()
        {
            if (string.IsNullOrEmpty(SaveAndLoad.WorldPath) ||
                string.IsNullOrEmpty(SaveAndLoad.CurrentGameFileName)) return null;
            return Path.Combine(SaveAndLoad.WorldPath,
                SaveAndLoad.SanitizeFileName(SaveAndLoad.CurrentGameFileName));
        }

        private static string LatestSaveFile()
        {
            string dir = SaveDirectory();
            if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir)) return null;
            string[] candidates = Directory.GetDirectories(dir, "*-Latest");
            if (candidates.Length == 0) return null;
            Array.Sort(candidates, StringComparer.OrdinalIgnoreCase);
            return Path.Combine(candidates[candidates.Length - 1], SaveFileName);
        }

        private static string F(float n) { return n.ToString("R", Culture); }
        private static string V(Vector3 v) { return F(v.x) + "," + F(v.y) + "," + F(v.z); }
        private static string Q(Quaternion q) {
            return F(q.x) + "," + F(q.y) + "," + F(q.z) + "," + F(q.w);
        }
        private static float Number(string number) {
            return float.Parse(number, NumberStyles.Float, Culture);
        }
        private static Vector3 ReadV(string v) {
            string[] c = v.Split(',');
            if (c.Length != 3) throw new FormatException("Vector3 length");
            return new Vector3(Number(c[0]), Number(c[1]), Number(c[2]));
        }
        private static Quaternion ReadQ(string q) {
            string[] c = q.Split(',');
            if (c.Length != 4) throw new FormatException("Quaternion length");
            return new Quaternion(Number(c[0]), Number(c[1]), Number(c[2]), Number(c[3]));
        }

        // Запись выполняется после сохранения мира
        internal static void Save()
        {
            if (!Raft_Network.IsHost || Raft_Network.InMenuScene) return;
            try
            {
                string path = LatestSaveFile();
                if (string.IsNullOrEmpty(path))
                {
                    return;
                }
                List<string> lines = new List<string> { "BT_PLANTED_TREES|2" };
                foreach (BetterTreesSavedTree r in Trees.Values)
                {
                    if (r.RuntimeRoot != null)
                    {
                        HarvestableTree ht = r.RuntimeRoot.GetComponent<HarvestableTree>();
                        if (ht != null && ht.Depleted) continue;
                        BetterTreesSaplingGrowth growth = r.RuntimeRoot.GetComponent<BetterTreesSaplingGrowth>();
                        if (growth != null) UpdateGrowth(r.Id, growth.ElapsedSeconds, growth.transform);
                    }
                    lines.Add(r.Id + "|" + (int)r.Kind + "|" + V(r.WorldPoint) +
                        "|" + V(r.LocalPoint) + "|" + Q(r.Rotation) + "|" +
                        r.LandmarkIndex.ToString(Culture) + "|" + F(r.GrowthSeconds));
                }
                string temp = path + ".tmp";
                File.WriteAllLines(temp, lines.ToArray());
                if (File.Exists(path)) File.Replace(temp, path, null);
                else File.Move(temp, path);
            }
            catch (Exception e) { Debug.LogError("[BetterTrees] Не удалось сохранить деревья: " + e); }
        }

        // Модели восстанавливаются после загрузки островов
        internal static void BeginLoad()
        {
            if (!Raft_Network.IsHost) return;
            Reset();
            try
            {
                string path = LatestSaveFile();
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    return;
                }
                string[] lines = File.ReadAllLines(path);
                if (lines.Length == 0) return;
                bool legacyGrowth = lines[0] == "BT_PLANTED_TREES|1";
                if (!legacyGrowth && lines[0] != "BT_PLANTED_TREES|2") return;
                for (int i = 1; i < lines.Length; i++)
                {
                    try
                    {
                        string[] parts = lines[i].Split('|');
                        if (parts.Length != 7) throw new FormatException("Tree record parts");
                        SaplingKind kind = (SaplingKind)int.Parse(parts[1], Culture);
                        if (!Enum.IsDefined(typeof(SaplingKind), kind)) continue;
                        BetterTreesSavedTree record = new BetterTreesSavedTree();
                        record.Id = parts[0]; record.Kind = kind;
                        record.WorldPoint = ReadV(parts[2]);
                        record.LocalPoint = ReadV(parts[3]);
                        record.Rotation = ReadQ(parts[4]);
                        record.LandmarkIndex = uint.Parse(parts[5], Culture);
                        float savedGrowth = Number(parts[6]);
                        // Старый прогресс переводится из 60 секунд в долю нового цикла роста
                        if (legacyGrowth) savedGrowth *= SaplingSettings.GrowSeconds / 60f;
                        record.GrowthSeconds = Mathf.Clamp(savedGrowth, 0f, SaplingSettings.GrowSeconds);
                        if (!string.IsNullOrEmpty(record.Id)) Trees[record.Id] = record;
                    }
                    catch (Exception) { }
                }
                loadInProgress = Trees.Count > 0;
            }
            catch (Exception e) { Debug.LogError("[BetterTrees] Не удалось загрузить деревья: " + e); }
        }

        internal static void PollRestore()
        {
            if (!Raft_Network.IsHost || Raft_Network.InMenuScene || SaveAndLoad.IsGameLoading ||
                !loadInProgress || Time.unscaledTime < nextRestore) return;
            nextRestore = Time.unscaledTime + 2f;
            foreach (BetterTreesSavedTree r in new List<BetterTreesSavedTree>(Trees.Values))
            {
                if (r.RuntimeRoot != null) continue;
                Landmark landmark = r.LandmarkIndex == 0 ? null : FindLandmarkByIndex(r.LandmarkIndex);
                if (r.LandmarkIndex != 0 && landmark == null) continue;
                Vector3 point = landmark != null ? landmark.transform.TransformPoint(r.LocalPoint) : r.WorldPoint;
                if (!BetterTreesSaplings.HasTemplate(r.Kind)) continue;
                BetterTreesSaplings.Plant(r.Kind, point, false, null,
                    landmark != null ? landmark.transform : null, r);
            }
        }
    }

    [HarmonyPatch(typeof(SaveAndLoad), "SaveGame")]
    internal static class BetterTreesWorldSavePatch
    {
        [HarmonyPostfix]
        private static void Postfix() { BetterTreesTreePersistence.Save(); }
    }

    [HarmonyPatch(typeof(SaveAndLoad), "RestoreRGDGame")]
    internal static class BetterTreesWorldLoadPatch
    {
        [HarmonyPostfix]
        private static void Postfix() { BetterTreesTreePersistence.BeginLoad(); }
    }

    internal sealed class BetterTreesHarvestSeedState : MonoBehaviour
    {
        internal bool Spawned;
    }

    [HarmonyPatch(typeof(HarvestableTree), "Harvest")]
    internal static class BetterTreesPlantedTreeDepletePatch
    {
        [HarmonyPostfix]
        private static void Postfix(HarvestableTree __instance, bool __result)
        {
            if (!__result || __instance == null || !__instance.Depleted) return;

            BetterTreesSaplingGrowth growth = __instance.GetComponent<BetterTreesSaplingGrowth>();
            if (growth != null) BetterTreesTreePersistence.RemoveDepleted(growth);
            if (!Raft_Network.IsHost) return;

            BetterTreesHarvestSeedState state = __instance.GetComponent<BetterTreesHarvestSeedState>();
            if (state != null && state.Spawned) return;

            SaplingKind customKind;
            bool custom = false;
            string vanillaSeed = null;
            if (growth != null)
            {
                custom = BetterTreesSaplings.IsCustomKind(growth.SaplingType);
                if (!custom) vanillaSeed = BetterTreesSaplings.SeedNameForKind(growth.SaplingType);
            }
            else if (__instance.GetComponent<BetterTreesChoppable>() != null)
            {
                custom = BetterTreesSaplings.TryGetKind(__instance.name, out customKind);
            }
            else BetterTreesSaplings.TryGetVanillaSeed(__instance, out vanillaSeed);

            if (!custom && vanillaSeed == null)
            {
                return;
            }
            if (state == null) state = __instance.gameObject.AddComponent<BetterTreesHarvestSeedState>();
            if (state.Spawned) return;

            bool spawned = custom ? BetterTreesSaplings.DropOne(__instance,
                __instance.transform.position, growth != null ? (SaplingKind?)growth.SaplingType : null) :
                BetterTreesSaplings.DropVanillaSeed(vanillaSeed, __instance);
            if (spawned)
            {
                state.Spawned = true;
            }
        }
    }

    [HarmonyPatch(typeof(YieldHandler), "ResetYield")]
    internal static class BetterTreesHarvestSeedResetPatch
    {
        [HarmonyPostfix]
        private static void Postfix(YieldHandler __instance)
        {
            if (__instance == null) return;
            BetterTreesHarvestSeedState state = __instance.GetComponent<BetterTreesHarvestSeedState>();
            if (state != null) state.Spawned = false;
        }
    }
}
