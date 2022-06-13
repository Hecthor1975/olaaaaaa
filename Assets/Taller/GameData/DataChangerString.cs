namespace Arcos.Taller
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class DataChangerString : MonoBehaviour
    {
        public string id;
        public string defaultValue;
        public UnityEvent<string> onStartScene;
        [Header("Compare to Start")]
        public string compareValue;
        public UnityEvent<string> onStartSceneEqual;
        public UnityEvent<string> onStartSceneNotEqual;
        private bool loadedOnce = false;
        private string lastLoadedValue;
        

        public void ChangeValue(string value)
        {
            if (DataContainer.singleton != null)
            {
                lastLoadedValue = value;
                DataContainer.singleton.SetDataPrefsString(id, value);
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
                lastLoadedValue = DataContainer.singleton.GetDataPrefsString(id, defaultValue);
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
            if(compareValue.Equals(lastLoadedValue))
            {
                onStartSceneEqual?.Invoke(lastLoadedValue);
            }
            else
            {
                onStartSceneNotEqual?.Invoke(lastLoadedValue);
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
