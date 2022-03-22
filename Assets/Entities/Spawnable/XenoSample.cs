using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XenoSample : Magnetic
{
    [SerializeField] int spaceMoneyValue;
    public override void onPickup()
    {
        base.onPickup();
        Currency.addMoney(spaceMoneyValue);
    }
}
