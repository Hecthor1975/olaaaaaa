using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Arcos.Taller;

public class GetCurrentScene : MonoBehaviour
{
    public DataChangerString dataChanger;

    public IEnumerator Start()
    {
        while (DataContainer.singleton == null)
        {
            yield return null;
        }
        GetCurrentSceneToData();
    }
    private void GetCurrentSceneToData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        dataChanger.ChangeValue(sceneName);
    }
}
