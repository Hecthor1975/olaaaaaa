using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcos.Taller;

public class GetSetFireHpBar : MonoBehaviour
{
    public FireHpBar myHpBar;
    public DataChanger dataChanger;
    
    public void UpdateData()
    {
        int data = myHpBar.totalLifes;
        dataChanger.ChangeValue(data);
    }

    public void AssignData(float data)
    {
        int dataInt = (int)data;
        myHpBar.totalLifes = dataInt;
    }
}
