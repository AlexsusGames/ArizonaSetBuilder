using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class StatsData
{
    private Dictionary<StatType, int[]> statMap = new Dictionary<StatType, int[]>
    {
        {StatType.MaxArmour, new int[] { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60 } },
        {StatType.Damage, new int[] { 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 } },
        {StatType.CritStrike, new int[] { 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 } },
        {StatType.Defence, new int[] { 0, 0, 0, 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 } },
        {StatType.RegenHP, new int[] { 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 } },
    };

    private Dictionary<StatType, int[]> skinStatMap = new Dictionary<StatType, int[]>
    {
        {StatType.Damage, new int[] {0, 0, 0, 0, 1, 1, 2, 2, 2, 3, 3, 3, 4 } },
        {StatType.CritStrike, new int[] { 0, 0, 0, 0, 1, 1, 2, 2, 2, 3, 3, 3, 4 } },
        {StatType.Defence, new int[] { 0, 0, 0, 0, 1, 1, 2, 3, 4, 5, 6, 7, 8 } },
    };

    private int GetStatByImprove(StatType type, int level) => statMap[type][level];
    private int GetSkinStatByImprove(StatType type, int level) => skinStatMap[type][level];
    public AccessoryStat[] GetStatsByImprove(StatType[] stats, int level, bool isSkin)
    {
        List<AccessoryStat> result = new List<AccessoryStat>();

        for (int i = 0; i < stats.Length; i++)
        {
            var statByLevel = isSkin ? GetSkinStatByImprove(stats[i], level) : GetStatByImprove(stats[i], level);

            result.Add(new AccessoryStat(stats[i], statByLevel));
        }

        if(level == 13)
        {
            result.AddRange(Get13LevelBonus());
        }

        return result.ToArray();
    }

    private AccessoryStat[] Get13LevelBonus()
    {
        AccessoryStat[] stats = new AccessoryStat[]
        {
            new AccessoryStat(StatType.MaxArmour, 9),
            new AccessoryStat(StatType.MaxHP, 4)
        };

        return stats;
    }
}
