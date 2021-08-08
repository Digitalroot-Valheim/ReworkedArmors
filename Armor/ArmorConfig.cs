using System.Collections.Generic;

namespace ReworkedArmors.Armor
{
  public class Armor
  {
    public string type { get; set; }
    public int baseArmor { get; set; }
    public int armorPerLevel { get; set; }
    public int startingTier { get; set; }
    public string helmetID { get; set; }
    public string chestID { get; set; }
    public string legsID { get; set; }
  }

  public class Cost
  {
    public string itemType { get; set; }
    public string item { get; set; }
    public int amount { get; set; }
    public int perLevel { get; set; }
  }

  public class Tier
  {
    public int tier { get; set; }
    public string station { get; set; }
    public int minLevel { get; set; }
    public int baseArmor { get; set; }
    public int armorPerLevel { get; set; }
    public double moveSpeed { get; set; }
    public List<Cost> costs { get; set; }
  }

  public class Root
  {
    public List<Armor> armors { get; set; }
    public List<Tier> tiers { get; set; }
  }
}