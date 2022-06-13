using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthSystemControl : MonoBehaviour
{
    public Sprite sprite;
    public Image img;

    public HealthSystemAttribute hs;
    public UnityEvent customActionsZeroHP;
    public float maxHealth;
    bool isNotNull = false;

    // Start is called before the first frame update
    void Start()
    {
        isNotNull = hs != null;
    }

    // Update is called once per frame
    void Update()
    {
        if(hs != null || (hs == null && isNotNull))
        {
            CheckHealth();
        }   

        if(hs != null)
        {
            UpdateHealth(hs.health/maxHealth);
        }
    }

    public void CheckHealth()
    {
        if (hs.health <= 0)
        {
            customActionsZeroHP.Invoke();
            UpdateHealth(0);
        }
    }

    public void UpdateHealth(float amount)
    {
        if (img != null)
        {
            img.fillAmount = amount;
        }
    }
}
