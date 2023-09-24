using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace PromptSkip
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class PromptSkipPlugin : BaseUnityPlugin
    {
        internal const string ModName = "PromptSkip";
        internal const string ModVersion = "1.0.0";
        internal const string Author = "Azumatt";
        private const string ModGUID = Author + "." + ModName;
        private readonly Harmony _harmony = new(ModGUID);

        public void Awake()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            _harmony.PatchAll(assembly);
        }
    }

    [HarmonyPatch(typeof(UIDisclaimer), nameof(UIDisclaimer.Awake))]
    static class UIDisclaimerAwakePatch
    {
        static void Postfix(UIDisclaimer __instance)
        {
            __instance.ButtonContinue();
        }
    }

    [HarmonyPatch(typeof(UIMenu), nameof(UIMenu.Awake))]
    static class UIMenuAwakePatch
    {
        static void Postfix(UIMenu __instance)
        {
            __instance.transform.Find("PanelImportantNotice").gameObject.SetActive(false);
        }
    }
}