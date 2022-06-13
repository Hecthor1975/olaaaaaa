namespace Arcos.Taller
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class DataChanger : MonoBehaviour
    {
        public string id;
        public float defaultValue;
        public UnityEvent<float> onStartScene;
        [Header("Compare to Start")]
        public float compareValue;
        public UnityEvent<float> onStartSceneMoreThan;
        public UnityEvent<float> onStartSceneLessThan;
        private bool loadedOnce = false;
        private float lastLoadedValue;
        

        public void ChangeValue(float value)
        {
            if (DataContainer.singleton != null)
            {
                lastLoadedValue = value;
                DataContainer.singleton.SetDataPrefs(id, value);
            }
        }

        private void Update()
        {
            CheckAndSetStart();
        }

        public void CheckAndSetStart()
        {
            if (DataContainer.singleton == null)
            {
                return;
            }

            if (DataContainer.singleton != null && loadedOnce == false)
            {
                lastLoadedValue = DataContainer.singleton.GetDataPrefs(id, defaultValue);
                CheckConditionCalls();
                loadedOnce = true;
            }
        }
        public void CheckConditionCalls()
        {
            if (onStartScene != null)
            {
                onStartScene.Invoke(lastLoadedValue);
            }
            if(compareValue < lastLoadedValue)
            {
                onStartSceneMoreThan?.Invoke(lastLoadedValue);
            }
            if(compareValue > lastLoadedValue)
            {
                onStartSceneLessThan?.Invoke(lastLoadedValue);
            }
        }

        public void ResetMyData()
        {
            DataContainer.singleton.RemoveGameDataSingle(id);
        }

        public void ResetAllGameData()
        {
            DataContainer.singleton.ResetGameData();
        }
    }
}
