namespace Arcos.Taller
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class OnStartSceneAudio : MonoBehaviour
    {
        [SerializeField] AudioClip audioClip;
        // Start is called before the first frame update
        void Start()
        {
            StaticAudio.instance.backgroundMusic.Stop();
            StaticAudio.instance.backgroundMusic.clip = audioClip;
            StaticAudio.instance.backgroundMusic.Play();
        }
    }
}
