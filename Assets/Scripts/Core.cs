using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Core 
{
    public static Localization Localization { get; private set; }
    public static StatsData StatsData { get; private set; }
    public static void Init()
    {
        Localization = new Localization();
        StatsData = new StatsData();
    }
}
