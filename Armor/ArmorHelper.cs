using Jotunn.Entities;
using Jotunn.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReworkedArmors.Armor
{
  internal static class ArmorHelper
  {
    public static void AddArmorPiece(string setName, string setPart)
    {
      global::ReworkedArmors.Armor.Armor armorConfig = ReworkedArmors.Root.armors.FirstOrDefault(x => x.type == setName);
      if (armorConfig != null)
      {
        int startingTier = armorConfig.startingTier;
        string armorId = "";
        if (setPart == "head") armorId = armorConfig.helmetID;
        if (setPart == "chest") armorId = armorConfig.chestID;
        if (setPart == "legs") armorId = armorConfig.legsID;

        for (int i = startingTier; i <= 5; ++i)
        {
          Tier armorTier = ReworkedArmors.Root.tiers.FirstOrDefault(x => x.tier == i);
          CustomItem customItem = new CustomItem(armorId + "T" + i, armorId);
          customItem.ItemDrop.m_itemData.m_shared.m_name = $"{customItem.ItemDrop.m_itemData.m_shared.m_name} T{i}";

          if (armorTier != null)
          {
            customItem.ItemDrop.m_itemData.m_shared.m_armor = armorTier.baseArmor;
            customItem.ItemDrop.m_itemData.m_shared.m_armorPerLevel = armorTier.armorPerLevel;

            if (setPart == "head")
            {
              customItem.ItemDrop.m_itemData.m_shared.m_helmetHideHair = false;
            }
            else
            {
              customItem.ItemDrop.m_itemData.m_shared.m_movementModifier = (float) armorTier.moveSpeed;
            }

            Recipe instance = ScriptableObject.CreateInstance<Recipe>();
            instance.name = $"Recipe_{armorId}T{i}";
            List<Piece.Requirement> requirementList = new List<Piece.Requirement>();
            string tier = i == startingTier ? "" : "T" + (i - 1);

            requirementList.Add(MockRequirement.Create(armorId + tier, 1, false));
            requirementList.Last().m_amountPerLevel = 0;

            foreach (var cost in armorTier.costs.Where(x => x.itemType == setPart))
            {
              requirementList.Add(MockRequirement.Create(cost.item, cost.amount));
              requirementList.Last().m_amountPerLevel = cost.perLevel;
            }

            instance.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>(armorTier.station);
            instance.m_minStationLevel = armorTier.minLevel;
            instance.m_resources = requirementList.ToArray();
            instance.m_item = customItem.ItemDrop;
            CustomRecipe customRecipe = new CustomRecipe(instance, true, true);

            ItemManager.Instance.AddItem(customItem);
            ItemManager.Instance.AddRecipe(customRecipe);
          }
        }
      }
    }

    public static void AddArmorSet(string setName)
    {
      AddArmorPiece(setName, "head");
      AddArmorPiece(setName, "chest");
      AddArmorPiece(setName, "legs");
    }
  }
}
