using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcos.Taller;

public class GetSetScoreFromUserInterace : MonoBehaviour
{

    [SerializeField] private UIScript userI;
    [SerializeField] private DataChanger dataToChange;


    public void UpdateData()
    {
        dataToChange.ChangeValue(userI.GetScore());
    }

    public void AddScoreFromData(float newValue)
    {
        userI.AddPoints(0, (int)newValue);
    }
}
