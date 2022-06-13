using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HSandTime
{
    public HealthSystemAttribute hs;
    public float timer;

    public HSandTime() { }
    public HSandTime(HealthSystemAttribute hs, float timer)
    {
        this.hs = hs;
        this.timer = timer;
    }
}

public class HurtOverTime : MonoBehaviour
{
    public float timeToHurt = 5f;
    List<HSandTime> healthSystems = new List<HSandTime>();
    List<int> instancesIDS = new List<int>();
    public List<int> ignoredIDS = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckAndHurt();
    }

    private void CheckAndHurt()
    {
        for (int i = 0, length = healthSystems.Count; i < length; i++)
        {
            healthSystems[i].timer += Time.deltaTime;
            if (healthSystems[i].timer >= timeToHurt)
            {
                healthSystems[i].hs.ModifyHealth(-1);
                healthSystems[i].timer = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        int instanceID = collision.gameObject.GetInstanceID();
        if (!instancesIDS.Contains(instanceID) && !ignoredIDS.Contains(instanceID))
        {
            HealthSystemAttribute hs = collision.GetComponent<HealthSystemAttribute>();
            if (hs != null) {
                instancesIDS.Add(instanceID);
                healthSystems.Add(new HSandTime(hs, 0));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int instanceID = collision.gameObject.GetInstanceID();
        RemoveInstanceHS(instanceID);
    }

    private void RemoveInstanceHS(int instanceID)
    {
        if (instancesIDS.Contains(instanceID))
        {
            int idToRemove = -1;
            instancesIDS.Remove(instanceID);
            for (int i = 0, length = healthSystems.Count; i < length; i++)
            {
                if (healthSystems[i].hs.gameObject.GetInstanceID() == instanceID)
                {
                    idToRemove = i;
                    break;
                }
            }
            if (idToRemove != -1)
            {
                healthSystems.RemoveAt(idToRemove);
            }
        }
    }

    public void AddRemoveToIgnore(GameObject go, bool add)
    {
        int instanceID = go.GetInstanceID();
        if (add)
        {
            ignoredIDS.Add(instanceID);
            RemoveInstanceHS(instanceID);
        }
        else
        {
            ignoredIDS.Remove(instanceID);

        }
    }
}
