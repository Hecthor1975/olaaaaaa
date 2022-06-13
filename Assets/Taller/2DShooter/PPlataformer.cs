using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcos.Taller
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PPlataformer : MonoBehaviour
    {
        private Rigidbody2D m_body2d;

        [Header("Movement")]
        public float speed = 5f;

        [Header("Stops")]
        public List<float> waypoints = new List<float>();

        private float[] newWaypoints;
        private int currentTargetIndex;

        // Start is called before the first frame update
        void Start()
        {
            m_body2d = GetComponent<Rigidbody2D>();
            currentTargetIndex = 0;

            newWaypoints = new float[waypoints.Count + 1];
            int w = 0;
            for (int i = 0; i < waypoints.Count; i++)
            {
                newWaypoints[i] = waypoints[i];
                w = i;
            }

            //Add the starting position at the end, only if there is at least another point in the queue - otherwise it's on index 0
            int v = (newWaypoints.Length > 1) ? w + 1 : 0;
            newWaypoints[v] = transform.position.x;

            waypoints = new List<float>(newWaypoints);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float currentTarget = newWaypoints[currentTargetIndex];
            float lastPosition = newWaypoints[currentTargetIndex - 1 >= 0 ? currentTargetIndex - 1 : newWaypoints.Length - 1];
            int moveDirection = lastPosition <= currentTarget ? 1 : -1;
            // HEre ...
            m_body2d.velocity = new Vector2(moveDirection * speed, m_body2d.velocity.y);
            // m_body2d.MovePosition(transform.position + ((Vector3)currentTarget - transform.position).normalized * speed * Time.fixedDeltaTime);

            if ((lastPosition <= currentTarget && (transform.position.x - currentTarget) >= -0.1f) ||
                (lastPosition >= currentTarget && (transform.position.x - currentTarget) <= 0.1f))
            {
                //new waypoint has been reached
                currentTargetIndex = (currentTargetIndex < newWaypoints.Length - 1) ? currentTargetIndex + 1 : 0;
                currentTarget = newWaypoints[currentTargetIndex];
            }
        }

        public void Reset()
        {
            waypoints = new List<float>() { 1f };
            Vector2 thisPosition = transform.position;
            waypoints[0] = 2f + thisPosition.x;
        }
    }
}
