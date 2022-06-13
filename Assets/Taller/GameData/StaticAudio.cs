namespace Arcos.Taller
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class StaticAudio : MonoBehaviour
    {
        public static StaticAudio instance;
        public AudioSource backgroundMusic;

        // Start is called before the first frame update
        void Start()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
