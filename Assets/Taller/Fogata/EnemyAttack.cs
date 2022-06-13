using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public GameObject prefabElement;
    [Range(0,180)]
    public float angle = 10; // Max 180
    public int totalAngles = 10;
    public float distanceBetweenPrefabs = 0.5f;
    public float maxDistance = 10;

    List<GameObject> elementsEmitted = new List<GameObject>();
    public float timeInAttack = 10f;
    public float timeInFinalFrame = 0.5f;
    public List<float> lineMaxPosition = new List<float>();

    public void AttackNow()
    {
        StartCoroutine(AttackNowCorutine());
    }

    IEnumerator AttackNowCorutine()
    {
        yield return 0; // dead frame
        lineMaxPosition = new List<float>();
        // Trow X lines
        for (int i = 0, length = totalAngles; i < length; i++)
        {
            float testingAngle = -angle + i * (angle * 2)/ totalAngles;// from -10 to 10 as example
            Vector2 direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * testingAngle), Mathf.Cos(Mathf.Deg2Rad * testingAngle));
            RaycastHit2D first = Physics2D.Raycast(this.transform.position, direction, maxDistance);
            if (first.transform == null)
            {
                lineMaxPosition.Add(maxDistance);
                Debug.DrawLine(this.transform.position, transform.position + (Vector3)(direction * maxDistance), Color.blue, 2);
            }
            else
            {
                lineMaxPosition.Add(first.distance);
                Debug.DrawLine(this.transform.position, first.point, Color.red, 2);
            }
        }
        // Consider max distace as maxDistance/timeInAttack -> this will happend on the whole time 
        float totalLoops = maxDistance / distanceBetweenPrefabs;// total elements to produce per line, loops
        float timePerLoop = timeInAttack / totalLoops;

        for (int i = 0; i < totalLoops; i++)
        {
            for (int j = 0, length = totalAngles; j < length; j++)
            {
                float testingAngle = -angle + j * (angle * 2) / totalAngles;// from -10 to 10 as example
                Vector2 direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * testingAngle), Mathf.Cos(Mathf.Deg2Rad * testingAngle));
                Vector3 newPosition =  transform.position + (Vector3)(direction * i * distanceBetweenPrefabs);
                Vector3 newPositionPlacement = transform.position + (Vector3)(direction * i * distanceBetweenPrefabs);
                float distance = Vector3.Distance(this.transform.position, newPosition);
                if (distance <= lineMaxPosition[j])
                {
                    GameObject go = Instantiate(prefabElement, this.transform);
                    go.transform.position = newPositionPlacement;
                    go.name = "InnetTile [" + j + ", " + i + "] " + distance;
                    elementsEmitted.Add(go);
                }
            }
            yield return new WaitForSeconds(timePerLoop);
        }

        yield return new WaitForSeconds(timeInFinalFrame);
        for (int i = 0, length = elementsEmitted.Count; i < length; i++)
        {
            Destroy(elementsEmitted[i].gameObject);
        }
        elementsEmitted.Clear();

    }
}
