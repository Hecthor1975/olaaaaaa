using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcos.Taller;

public class GetSetHealthFromPlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private DataChanger dataToChange;

    public void UpdateData()
    {
        dataToChange.ChangeValue(playerHealth.health);
    }

    // 5 ... 10 - 5 = 5 -> 5 - 10 = -5
    // 3 ... 10 - 7 = 3 -> 3 - 10 = -7
    public void ChangeHPFromDataMinusActual(float newValue)
    {
        float actualHealth = playerHealth.health; // Total
        playerHealth.health = (newValue - actualHealth);
    }

    public void ChangeHPTo(float newValue)
    {
        playerHealth.health = newValue;
    }
}
