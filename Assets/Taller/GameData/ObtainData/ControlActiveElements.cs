using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcos.Taller;

public class ControlActiveElements : MonoBehaviour
{
    [SerializeField] private List<GameObject> elements;
    [SerializeField] private DataChangerString dataChanger;

    public void UpdateData()
    {
        string activeItems = "";
        for (int i = 0, length = elements.Count; i < length; i++)
        {
            if(elements[i] == null || elements[i].gameObject.activeInHierarchy == false)
            {
                activeItems += 0;
            }
            else
            {
                activeItems += 1;
            }
        }

        dataChanger.ChangeValue(activeItems);
    }

    // 0111010 -> string, donde 1 segnigica que estara apagado y 1 prendido
    public void ChangeItemsFromData(string newValue)
    {
        for (int i = 0, length = newValue.Length; i < length; i++)
        {
            if (elements != null && elements.Count > i && elements[i] != null)
            {
                if (newValue[i] == '0')
                {
                    elements[i].SetActive(false);
                    
                }
                else
                {
                    elements[i].SetActive(true);
                }
            }
        }
    }
}
