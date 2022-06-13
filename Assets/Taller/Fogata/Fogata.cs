using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fogata : MonoBehaviour
{
    public HurtOverTime hurtBox;
    public float sizeIncreasePerLife = 1.2f;
    public float actualLife = 1f;
    public float maxLife = 10f;
    public float timeToLoseLife = 4f;
    public float TimeBetweenSizeAdjust = 1f;
    float actualTimer = 0;
    bool adjustingSize = false;
    Vector3 initialScale;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        AdjustSize();
        CheckInnerTimer();
    }

    void AdjustSize()
    {
        if (adjustingSize) return ;
        Vector3 newScale = initialScale;
        if (actualLife == 0)
        {
            newScale = Vector3.zero;
        }
        else
        {
            newScale = actualLife == 1 ? initialScale : initialScale + (initialScale * sizeIncreasePerLife * (actualLife - 1));
        }

        if (newScale != this.transform.localScale) {
            StartCoroutine(ChangeSize(this.transform.localScale, newScale));
            // Change scale depending on life
            //this.transform.localScale = newScale;
        }
    }

    IEnumerator ChangeSize(Vector3 intial, Vector3 final)
    {
        for (float timeCounter = 0; timeCounter < TimeBetweenSizeAdjust; timeCounter += 0.02f)
        {
            yield return new WaitForSeconds(0.02f);
            Vector3 newScale = Vector3.Lerp(intial, final, timeCounter / TimeBetweenSizeAdjust);
            this.transform.localScale = newScale;
        }
        adjustingSize = false;
    }

    public void ModifyHealth(int value)
    {
        actualLife += value;

        if (actualLife <= 0) actualLife = 0;
        if (actualLife >= maxLife) actualLife = maxLife;
        actualTimer = 0;
    }

    void CheckInnerTimer()
    {
        if (adjustingSize) return ;

        actualTimer += Time.deltaTime;
        if(actualTimer >= timeToLoseLife)
        {
            ModifyHealth(-1);            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Safe !
        hurtBox.AddRemoveToIgnore(collision.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hurtBox.AddRemoveToIgnore(collision.gameObject, false);
    }
}
