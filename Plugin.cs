using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using PersonalBoombox;
using UnityEngine;



namespace LutschlonsCustomBoomnbox
{
  [BepInPlugin(modGUID, modName, modVersion)]
[BepInProcess("Lethal Company.exe")]
[BepInDependency("ImoutoSama.PersonalBoombox", "1.4.2")]
public class LutschlonsCustomBoomboxPlugin : BaseUnityPlugin
  {
    private const string modGUID = "Lutschlon.LutschlonsCustomBoombox";
    private const string modName = "Lutschlons Custom Boombox";
    private const string modVersion = "1.0.0";

    private readonly Harmony harmony = new Harmony(modGUID);

    internal ManualLogSource logger;

    public ConfigEntry<bool> overridePriceFlagConfig;
    public ConfigEntry<int> overridePriceValueConfig;

void Awake()
{
    logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);
    logger.LogInfo($"Plugin {modName} has been added!");

    overridePriceFlagConfig = Config.Bind("General", "Override Price Flag", false, "If you want to override price of the boomboxes, you MUST set this to true.");
    overridePriceValueConfig = ConfigBindClamp("General", "Override Price Value", 30, "Overrides the price of the boomboxes by this value. Clamped from 0 to 1000.", 0, 1000);

    var request = PersonalBoombox.PersonalBoomboxPlugin.AddFromAssemblyDll(Assembly.GetExecutingAssembly().Location);
    request.overridePriceFlag = overridePriceFlagConfig.Value;
    request.overridePriceValue = overridePriceValueConfig.Value;
}

public ConfigEntry<int> ConfigBindClamp(string section, string key, int defaultValue, string description, int min, int max)
{
    var config = Config.Bind(section, key, defaultValue, description);
    config.Value = Mathf.Clamp(config.Value, min, max);
    return config;
}

}
 
}

     

