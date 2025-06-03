using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AccessoryView : MonoBehaviour
{
    [SerializeField] private TMP_Text accessoryDescribtion;
    [SerializeField] private SetBuilder setBuilder;

    [SerializeField] private AccessoryPreviewSlot transferedAccessoryView;
    [SerializeField] private AccessoryPreviewSlot transferedYellowStatsView;
    [SerializeField] private StripPreview stripeView;

    [SerializeField] private GameObject improvePanel;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Toggle improveTo13;

    private Accessory currentAccessory;

    private void Awake()
    {
        Init();
    }

    public void Show(Accessory accessory)
    {
        if (accessory == null)
            return;

        if (currentAccessory != null)
            currentAccessory.OnChanged -= Show;

        gameObject.SetActive(true);

        currentAccessory = accessory;

        ShowTransferedSlots(accessory);
        WriteDescribtion(accessory);

        setBuilder.UpdateSetStatsView();

        accessory.OnChanged += Show;
    }

    public void Hide()
    {
        if(currentAccessory != null)
        {
            gameObject.SetActive(false);
            setBuilder.TakeOffAccessory(currentAccessory);
        }
    }

    private void ShowTransferedSlots(Accessory accessory)
    {
        if (accessory.HasTransferedStats) transferedAccessoryView.SetData(accessory.TransferedStats);
        else transferedAccessoryView.HideItem();

        if(accessory.HasTransferedYellowStats) transferedYellowStatsView.SetData(accessory.TransferedYellowStats);
        else transferedYellowStatsView.HideItem();

        if(accessory.HasStripe) stripeView.SetData(accessory.AttachedStrip);
        else stripeView.HideItem();

        improveTo13.SetIsOnWithoutNotify(accessory.IsImprovedTo13);
        levelText.text = accessory.Level.ToString();

        transferedAccessoryView.ChangeSlotType(accessory.AccessoryConfig.SlotType);
        transferedYellowStatsView.ChangeSlotType(accessory.AccessoryConfig.SlotType);
    }

    public void ChangeLevel(int level)
    {
        if(currentAccessory != null)
        {
            levelText.text = currentAccessory.Level.ToString();
            currentAccessory.ChangeStatLevel(level);
        }
    }

    private void Init()
    {
        transferedAccessoryView.ConfigChanged += HandleTransferedStatsChange;
        transferedYellowStatsView.ConfigChanged += HandleTransferedYellowStatsChange;
        stripeView.ConfigChanged += HandleStripeChange;
        
        improveTo13.onValueChanged.AddListener(ImproveTo13);
    }

    private void ImproveTo13(bool value)
    {
        if(currentAccessory != null)
        {
            levelText.text = currentAccessory.Level.ToString();
            currentAccessory.ImproveTo13(value);
        }
    }

    public void RemoveTransferedStats()
    {
        if(currentAccessory != null)
        {
            currentAccessory.TransferStat(null);
            transferedAccessoryView.HideItem();
        }
    }

    public void RemoveTransferedYellowStats()
    {
        if(currentAccessory != null)
        {
            currentAccessory.TransferYellowStats(null);
            transferedYellowStatsView.HideItem();
        }
    }

    public void RemoveStripe()
    {
        if (currentAccessory != null)
        {
            currentAccessory.AttachStrip(null);
            stripeView.HideItem();
        }
    }

    private void HandleTransferedStatsChange(PreviewSlot _, ItemConfig newConfig)
    {
        if(currentAccessory != null && newConfig is AccessoryConfig accessoryConfig)
        {
            currentAccessory.TransferStat(accessoryConfig);
        }
    }

    private void HandleTransferedYellowStatsChange(PreviewSlot _, ItemConfig newConfig)
    {
        if (currentAccessory != null && newConfig is AccessoryConfig accessoryConfig)
        {
            currentAccessory.TransferYellowStats(accessoryConfig);
        }
    }

    private void HandleStripeChange(PreviewSlot _, ItemConfig newConfig)
    {
        if (currentAccessory != null && newConfig is StripeConfig stripeConfig)
        {
            currentAccessory.AttachStrip(stripeConfig);
        }
    }

    private void WriteDescribtion(Accessory accessory)
    {
        string result = $"\nПредмет: <color=yellow>{accessory.AccessoryConfig.Name}</color>";
        result += $"\n{accessory.AccessoryConfig.Description}";
        result += $"\nСлот аксессуара: <color=yellow>{Core.Localization.LocalizeSlot(accessory.AccessoryConfig.SlotType)}</color>";

        var yellowStats = accessory.GetYellowStats();

        if(yellowStats.Length > 0)
        {
            result += "\n";

            for (int i = 0; i < yellowStats.Length; i++)
            {
                result += $"\n<color=yellow>Данный предмет дополнительно дает {Core.Localization.ConverStatToString(yellowStats[i])}</color>";
            }

            result += "\n";
        }

        var stats = accessory.GetStandartStats();

        if(stats.Length != 0 && !accessory.HasTransferedStats)
        {
            result += "\n";

            result += "\n<color=#8B0000>Данный предмет имеет следующие характиристики по-умолчанию:</color>";

            result += "\n";

            for (int i = 0; i < stats.Length; i++)
            {
                result += $"\n{Core.Localization.ConvertStatToLongString(stats[i])}";
            }
        }

        if (accessory.HasStripe)
        {
            result += "\n";

            var stat = accessory.AttachedStrip.stat;

            result += $"\n<color=#FFA500>Встроена нашивка:</color> ({Core.Localization.ConverStatToString(stat)})";
        }

        var improveStats = accessory.ImprovingStats;

        if (improveStats.Length != 0)
        {
            result += "\n";
            result += "\nХарактеристики:";
            result += $"\n-Улучшение: <color=#FFA500>[{accessory.Level}/13]</color>";
            result += $"\n-Бонус от улучшения:";

            var allStats = accessory.GetAllStats();

            foreach (var stat in allStats)
            {
                if(stat.Amount > 0) result += $"<color=#B22222>[{Core.Localization.ConverStatToString(stat)}]</color>";
            }

            result += "\n";
            result += $"\nПри улучшении увеличивает ";

            for (int i = 0; i < improveStats.Length; i++)
            {
                result += $"{Core.Localization.LocalizeStat(improveStats[i])}, ";
            }
        }

        if (accessory.HasTransferedStats)
        {
            result += $"\nУ данного аксессуара применены характеристики с предмета: '<color=yellow>{accessory.TransferedStats.Name}'</color>";
        }

        accessoryDescribtion.text = result;

        improvePanel.SetActive(improveStats.Length > 0);
        improveTo13.gameObject.SetActive(accessory.AccessoryConfig.CanBeImprovedTo13);
    }
}
