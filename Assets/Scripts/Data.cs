using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BreakInfinity;

[Serializable]
public class Data
{
    public BigDouble currency;

    public List<BigDouble> clickUpgradeLevel;
    public List<BigDouble> productionUpgradeLevel;

    public Data() {
        currency = 0;

        clickUpgradeLevel = new BigDouble[4].ToList();
        productionUpgradeLevel = new BigDouble[4].ToList();
    }

}
