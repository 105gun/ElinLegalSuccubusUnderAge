using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace LegalSuccubusMod;

[BepInPlugin("105gun.legalsuccubus.mod", "Legal Succubus Underage", "1.0.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static bool inSuccubusVisitCheck = false;
    private void Start()
    {
        var harmony = new Harmony("105gun.legalsuccubus.mod");
        harmony.PatchAll();
        System.Console.WriteLine("[LegalSuccubusMod] Initialization completed.");
    }
}

// ConSleep.SuccubusVisit
[HarmonyPatch(typeof(ConSleep), "SuccubusVisit")]
public class SuccubusVisitPatch
{
    static void Prefix()
    {
        Plugin.inSuccubusVisitCheck = true;
    }

    static void Postfix()
    {
        Plugin.inSuccubusVisitCheck = false;
    }
}

// Biography.IsUnderAge get
[HarmonyPatch(typeof(Biography), "IsUnderAge", MethodType.Getter)]
public class IsUnderAgePatch
{
    static void Postfix(ref bool __result)
    {
        if (Plugin.inSuccubusVisitCheck)
        {
            __result = false;
            Plugin.inSuccubusVisitCheck = false; // Just in case some strange things happen
        }
    }
}