namespace Arcos.Taller
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class DataChangerBool : MonoBehaviour
    {
        public string id;
        public bool defaultValue;
        public UnityEvent<bool> onStartScene;
        public UnityEvent<bool> onStartSceneNot;
        [Header("Compare to Start")]
        public UnityEvent<bool> onStartSceneTrue;
        public UnityEvent<bool> onStartSceneFalse;
        private bool loadedOnce = false;
        private bool lastLoadedValue;


        public void ChangeValue(bool value)
        {
            if (DataContainer.singleton != null)
            {
                lastLoadedValue = value;
                DataContainer.singleton.SetDataPrefsBool(id, value);
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
                lastLoadedValue = DataContainer.singleton.GetDataPrefsBool(id, defaultValue);
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
            if (onStartSceneNot != null)
            {
                onStartSceneNot.Invoke(!lastLoadedValue);
            }
            if (lastLoadedValue == true)
            {
                onStartSceneTrue?.Invoke(lastLoadedValue);
            }
            if (lastLoadedValue == false)
            {
                onStartSceneFalse?.Invoke(lastLoadedValue);
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
