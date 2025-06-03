using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripPreview : PreviewSlot
{
    public override void SetData(ItemConfig config)
    {
        if(config is StripeConfig sConfig)
        {
            base.SetData(sConfig);
        }
    }
}
