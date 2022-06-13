using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcos.Taller;

public class GetSetHealthFromHealthSystem : MonoBehaviour
{
    [SerializeField] private HealthSystemAttribute hsa;
    [SerializeField] private DataChanger dataToChange;

    public void UpdateData()
    {
        dataToChange.ChangeValue(hsa.health);
    }

    // 5 ... 10 - 5 = 5 -> 5 - 10 = -5
    // 3 ... 10 - 7 = 3 -> 3 - 10 = -7
    public void ChangeHPFromData(float newValue)
    {
        int actualHealth = hsa.health; // Total
        hsa.ModifyHealth((int)(newValue - actualHealth));
    }

    public void ChangeHPAndAdd(float newValue)
    {
        hsa.ModifyHealth((int)(newValue));
    }
}
