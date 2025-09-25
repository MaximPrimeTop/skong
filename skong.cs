using BepInEx;
using BepInEx.Logging;
using GlobalEnums;
using HarmonyLib;
using UnityEngine;
using System;
using InControl;
namespace skong
{
    [BepInPlugin("MaximPrime.skong", "skong", "1.0.0")]
    public class Skong : BaseUnityPlugin
    {

        internal static ManualLogSource Log = BepInEx.Logging.Logger.CreateLogSource("skong");

        public void Awake()
        {
            Log.LogInfo("skonging really good");
            Harmony.CreateAndPatchAll(typeof(Skong), null);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UberPostprocess), "OnRenderImage")]
        public static bool OnRenderImagePatch(ref RenderTexture source, ref RenderTexture destination)
        {
            Graphics.Blit(source, destination);
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ControllerDetect), "ShowController")]
        public static void ShowControllerPatch(ref GamepadType gamepadType)
        {
            Log.LogInfo("skonged the silk to the PS4 song");
            gamepadType = GamepadType.PS4;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UIButtonSkins), "GetButtonSkinFor", new Type[] { typeof(InputControlType)})]
        public static void GetButtonSkinForPS4Patch(UIButtonSkins __instance)
        {
            try
            {
                var ihField = __instance.GetType().GetField("ih", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var inputHandler = (InputHandler)ihField.GetValue(__instance);
                if (inputHandler != null)
                {
                    inputHandler.activeGamepadType = GamepadType.PS4;
                    Log.LogInfo("mwahahah i changed to da ps4!!");
                }
            }
            catch
            {

            }
        }
    }
}
