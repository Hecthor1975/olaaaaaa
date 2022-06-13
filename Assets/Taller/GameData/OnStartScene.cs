namespace Arcos.Taller
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class OnStartScene : MonoBehaviour
    {
        public UnityEvent onStartScene;

        // Start is called before the first frame update
        void Start()
        {
            onStartScene?.Invoke();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
