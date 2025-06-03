using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory 
{
    public AccessoryConfig AccessoryConfig { get; private set; }
    public AccessoryConfig TransferedStats { get; private set; }
    public AccessoryConfig TransferedYellowStats { get; private set; }
    public StripeConfig AttachedStrip { get; private set; }
    public bool IsImprovedTo13 { get; private set; }
    public int Level
    {
        get
        {
            var level = this.level;
            if (IsImprovedTo13) level++;
            return level;
        }
    }

    public Action<Accessory> OnChanged;
    public bool HasTransferedStats => TransferedStats != null;
    public bool HasTransferedYellowStats => TransferedYellowStats != null;
    public bool HasStripe => AttachedStrip != null;

    private int level;

    public StatType[] ImprovingStats => HasTransferedStats ? TransferedStats.ImprovingStats : AccessoryConfig.ImprovingStats;

    public Accessory(AccessoryConfig mainAccessory)
    {
        AccessoryConfig = mainAccessory;
    }

    public Accessory()
    {

    }

    public AccessoryStat[] GetStandartStats() => HasTransferedStats ? TransferedStats.DefaultStats : AccessoryConfig.DefaultStats;
    public AccessoryStat[] GetYellowStats() => HasTransferedYellowStats ? TransferedYellowStats.YellowStats : AccessoryConfig.YellowStats;

    public List<AccessoryStat> GetAllStats()
    {
        Dictionary<StatType, float> statsMap = new Dictionary<StatType, float>();

        var yellowStats = GetYellowStats();

        AddStatToDictionary(statsMap, yellowStats);

        var stats = GetStandartStats();

        AddStatToDictionary(statsMap, stats);

        var isSkin = AccessoryConfig.SlotType == SlotType.Skin;
        var statsByImprove = Core.StatsData.GetStatsByImprove(ImprovingStats, Level, isSkin);

        AddStatToDictionary(statsMap, statsByImprove);

        List<AccessoryStat> result = new List<AccessoryStat>();

        foreach (var stat in statsMap.Keys)
        {
            AccessoryStat accessoryStat = new AccessoryStat(stat, statsMap[stat]);
            result.Add(accessoryStat);
        }

        if (HasStripe)
        {
            result.Add(AttachedStrip.stat);
        }

        return result;
    }

    private void AddStatToDictionary(Dictionary<StatType, float> statsMap, AccessoryStat[] stats)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            var stat = stats[i];

            statsMap[stat.StatType] = statsMap.GetValueOrDefault(stat.StatType) + stat.Amount;
        }
    }

    public void ImproveTo13(bool enabled)
    {
        IsImprovedTo13 = enabled;

        OnChanged?.Invoke(this);
    }

    public void ChangeStatLevel(int level)
    {
        this.level += level;

        if(this.level <= 0)
            this.level = 0;

        if(this.level > 12)
            this.level = 12;

        OnChanged?.Invoke(this);
    }

    public void TransferStat(AccessoryConfig config)
    {
        TransferedStats = config;

        OnChanged?.Invoke(this);
    }

    public void TransferYellowStats(AccessoryConfig config)
    {
        TransferedYellowStats = config;

        OnChanged?.Invoke(this);
    }

    public void AttachStrip(StripeConfig stripeConfig)
    {
        AttachedStrip = stripeConfig;

        OnChanged?.Invoke(this);
    }
}
