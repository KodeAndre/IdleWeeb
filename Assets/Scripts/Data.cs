using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

public class Data
{
    public BigDouble currency;

    public List<BigDouble> clickUpgradeLevel;

    public Data() {
        currency = 0;

        clickUpgradeLevel = Methods.CreateList<BigDouble>(4);
    }

}
