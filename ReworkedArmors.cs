using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using JetBrains.Annotations;
using Jotunn.Managers;
using ReworkedArmors.Armor;
using System.IO;

namespace ReworkedArmors
{
  [BepInPlugin("Detalhes.ReworkedArmors", "ReworkedArmors", "1.0.2")]
  public class ReworkedArmors : BaseUnityPlugin
  {
    private const string Guid = "Detalhes.ReworkedArmors";
    private readonly Harmony _harmony = new Harmony(Guid);
    private static readonly string ModPath = Path.GetDirectoryName(typeof(ReworkedArmors).Assembly.Location);
    public static Root Root = new();

    [UsedImplicitly] public static ConfigEntry<int> NexusId;

    [UsedImplicitly]
    private void Awake()
    {
      Root = SimpleJson.SimpleJson.DeserializeObject<Root>(File.ReadAllText(Path.Combine(ModPath, "armorConfig.json")));

      NexusId = Config.Bind("General", "NexusID", 1420, "Nexus mod ID for updates");
      ItemManager.OnVanillaItemsAvailable += AddArmorSets;
      _harmony.PatchAll();
    }

    private void AddArmorSets()
    {
      ArmorHelper.AddArmorSet("leather");
      ArmorHelper.AddArmorPiece("rags", "chest");
      ArmorHelper.AddArmorPiece("rags", "legs");
      ArmorHelper.AddArmorSet("trollLeather");
      ArmorHelper.AddArmorSet("bronze");
      ArmorHelper.AddArmorSet("iron");
      ArmorHelper.AddArmorSet("silver");

      ItemManager.OnVanillaItemsAvailable -= AddArmorSets;
    }
  }
}
