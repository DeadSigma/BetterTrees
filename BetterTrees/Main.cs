using UnityEngine;
using HarmonyLib;
using HMLLibrary;
using System;
using System.Reflection;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace BetterTrees
{
    public class Main : Mod
    {
        public Harmony harmonyInstance;
        public static int HitsToBreak_StoneAxe = 5;
        public static int HitsToBreak_MetalAxe = 4;
        public static int HitsToBreak_TitaniumAxe = 3;
        public static string logPrefix = "[BetterTrees]";

        private bool haveSettingsBeenLoadedByAPI = false;

        public void Start()
        {
            try
            {
                ApplyHardcodedDefaultSettings();
                gameObject.AddComponent<BetterTreesSaplingInput>();
                try { BetterTreesSaplings.RegisterItems(); }
                catch (Exception ex) { Debug.LogError($"{logPrefix} Failed to register saplings: {ex}"); }

                harmonyInstance = new Harmony("com.bettertrees.mod");
                harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Debug.LogError($"{logPrefix} Failed to initialize mod: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public void OnModUnload()
        {
            try
            {
                harmonyInstance?.UnpatchAll(harmonyInstance.Id);
                BetterTreesSaplings.ClearForUnload();
            }
            catch (Exception ex)
            {
                Debug.LogError($"{logPrefix} Error during mod unload: {ex.Message}");
            }
        }

        private void ApplyHardcodedDefaultSettings()
        {
            HitsToBreak_StoneAxe = 5;
            HitsToBreak_MetalAxe = 4;
            HitsToBreak_TitaniumAxe = 3;

        }

        private void LoadSettingsFromAPI()
        {
            try
            {

                HitsToBreak_StoneAxe = Mathf.Max(1, (int)ExtraSettingsAPI_GetInputValue("HitsToBreak_StoneAxe").ParseFloat(HitsToBreak_StoneAxe));
                HitsToBreak_MetalAxe = Mathf.Max(1, (int)ExtraSettingsAPI_GetInputValue("HitsToBreak_MetalAxe").ParseFloat(HitsToBreak_MetalAxe));
                HitsToBreak_TitaniumAxe = Mathf.Max(1, (int)ExtraSettingsAPI_GetInputValue("HitsToBreak_TitaniumAxe").ParseFloat(HitsToBreak_TitaniumAxe));

                haveSettingsBeenLoadedByAPI = true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"{logPrefix} Error loading settings from API: {ex.Message}. Using current/default values.");
            }
        }

        void ExtraSettingsAPI_Load()
        {
            LoadSettingsFromAPI();
        }

        void ExtraSettingsAPI_SettingsClose()
        {
            if (haveSettingsBeenLoadedByAPI)
            {

                LoadSettingsFromAPI();
            }

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        string ExtraSettingsAPI_GetInputValue(string SettingName)
        {
            return "";
        }
    }

    public static class ExtensionMethods
    {
        public static float ParseFloat(this string value, float fallback = 1f)
        {
            if (string.IsNullOrWhiteSpace(value))
                return fallback;
            value = value.Replace(',', '.');
            return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result) ? result : fallback;
        }
    }
}
