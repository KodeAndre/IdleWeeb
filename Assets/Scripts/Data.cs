using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BreakInfinity;

public class Data
{
    public BigDouble currency;

    public List<BigDouble> clickUpgradeLevel;

    public Data() {
        currency = 0;

        clickUpgradeLevel = new BigDouble[4].ToList();
    }

}
