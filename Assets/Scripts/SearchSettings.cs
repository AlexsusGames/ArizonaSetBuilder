public struct SearchSettings 
{
    public string Name;

    public StatType StatType;
    public SlotType SlotType;

    public SearchSettings(string name, SlotType slotType, StatType statType)
    {
        Name = name;
        StatType = statType;
        SlotType = slotType;
    }
}
