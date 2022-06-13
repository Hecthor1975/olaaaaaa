using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcos.Taller;

public class GetSetHealth : MonoBehaviour
{
    public Health myHpBar;
    public DataChanger dataChanger;
    
    public void UpdateData()
    {
        int data = myHpBar.health;
        dataChanger.ChangeValue(data);
    }

    public void AssignData(float data)
    {
        int dataInt = (int)data;
        myHpBar.health = dataInt;
    }
}
