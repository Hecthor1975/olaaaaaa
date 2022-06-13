namespace Arcos.Taller
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;

    #region ClassDefinition
    [System.Serializable]
    public class DataPrefs
    {
        public float Value { get { return value; } }
        public string ID { get { return id; } }
        [SerializeField] private float value;
        [SerializeField] private string id;
        private bool justLoaded = false;

        public DataPrefs() { }

        public DataPrefs(string id, float value)
        {
            this.value = value;
            this.id = id;
            GetInitialValue();
        }

        public void ChangeValue(float newValue)
        {
            this.value = newValue;
            PlayerPrefs.SetFloat(id, newValue);
        }

        public void AddToValue(float addValue)
        {
            this.value += addValue;
            PlayerPrefs.SetFloat(id, this.value);
        }

        public void SaveCurrentValue()
        {
            PlayerPrefs.SetFloat(id, value);
        }

        public float GetInitialValue()
        {
            this.value = PlayerPrefs.GetFloat(id, 0.5f);
            justLoaded = true;
            return this.value;
        }

        public string GetID()
        {
            return id;
        }
    }

    [System.Serializable]
    public class DataPrefsBool
    {
        public bool Value { get { return value; } }
        public string ID { get { return id; } }
        [SerializeField] private bool value;
        [SerializeField] private string id;
        private bool justLoaded = false;

        public DataPrefsBool() { }

        public DataPrefsBool(string id, bool value)
        {
            this.value = value;
            this.id = id;
            GetInitialValue();
        }

        public void ChangeValue(bool newValue)
        {
            this.value = newValue;
            PlayerPrefs.SetInt(id, newValue ? 1 : 0);
        }

        public void SaveCurrentValue()
        {
            PlayerPrefs.SetInt(id, value ? 1 : 0);
        }

        public bool GetInitialValue()
        {
            this.value = PlayerPrefs.GetInt(id, 0) == 0? false : true;
            justLoaded = true;
            return this.value;
        }

        public string GetID()
        {
            return id;
        }
    }

    [System.Serializable]
    public class DataPrefsString
    {
        public string Value { get { return value; } }
        public string ID { get { return id; } }
        [SerializeField] private string value;
        [SerializeField] private string id;
        private bool justLoaded = false;

        public DataPrefsString() { }

        public DataPrefsString(string id, string value)
        {
            this.value = value;
            this.id = id;
            GetInitialValue();
        }

        public void ChangeValue(string newValue)
        {
            this.value = newValue;
            PlayerPrefs.SetString(id, newValue);
        }

        public void SaveCurrentValue()
        {
            PlayerPrefs.SetString(id, value);
        }

        public string GetInitialValue()
        {
            this.value = PlayerPrefs.GetString(id, "");
            justLoaded = true;
            return this.value;
        }

        public string GetID()
        {
            return id;
        }
    }
    #endregion ClassDefinition

    public class DataContainer : MonoBehaviour
    {
        public List<DataPrefs> data = new List<DataPrefs>();
        public List<DataPrefsBool> dataBool = new List<DataPrefsBool>();
        public List<DataPrefsString> dataString = new List<DataPrefsString>();
        public static DataContainer singleton;

        
        public void OnEnable()
        {
            if (singleton == null)
            {
                DontDestroyOnLoad(this);
                singleton = this;
                Debug.Log("Singleton ?");
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        #region Get
        public float GetDataPrefs(string id, float defaultValue)
        {
            bool exist = data.Exists((x) => { return x.ID == id; });
            if (!exist)
            {
                // Debug.Log("Exist ?");
                AddGameData(id, defaultValue, true);
            }
            return data.Find((x) => { return x.ID == id; }).Value;
        }

        public bool GetDataPrefsBool(string id, bool defaultValue)
        {
            bool exist = dataBool.Exists((x) => { return x.ID == id; });
            if (!exist)
            {
                Debug.Log("Exist ? " + id);
                AddGameDataBool(id, defaultValue, true);
            }
            return dataBool.Find((x) => { return x.ID == id; }).Value;
        }

        public string GetDataPrefsString(string id, string defaultValue)
        {
            bool exist = dataString.Exists((x) => { return x.ID == id; });
            if (!exist)
            {
                // Debug.Log("Exist ?");
                AddGameDataString(id, defaultValue, true);
            }
            return dataString.Find((x) => { return x.ID == id; }).Value;
        }

        #endregion Get

        #region Set
        public void SetDataPrefs(string id, float value)
        {
            bool exist = data.Exists((x) => { return x.ID == id; });
            if (!exist)
            {
                AddGameData(id, 0, false);
            }
            data.Find((x) => { return x.ID == id; }).ChangeValue(value);
        }

        public void SetDataPrefsBool(string id, bool value)
        {
            bool exist = dataBool.Exists((x) => { return x.ID == id; });
            if (!exist)
            {
                AddGameDataBool(id, false, false);
            }
            dataBool.Find((x) => { return x.ID == id; }).ChangeValue(value);
        }

        public void SetDataPrefsString(string id, string value)
        {
            bool exist = dataString.Exists((x) => { return x.ID == id; });
            if (!exist)
            {
                AddGameDataString(id, "", false);
            }
            dataString.Find((x) => { return x.ID == id; }).ChangeValue(value);
        }
        #endregion Set

        #region RemoveData
        public void ResetGameData()
        {
            PlayerPrefs.DeleteAll();
        }

        public void RemoveGameDataSingle(string id)
        {
            for (int i = 0, length = data.Count; i < length; i++)
            {
                if (data[i].ID == id)
                {
                    data[i].ChangeValue(0);
                    PlayerPrefs.DeleteKey(data[i].GetID());
                }
            }

            for (int i = 0, length = dataBool.Count; i < length; i++)
            {
                if (dataBool[i].ID == id)
                {
                    dataBool[i].ChangeValue(false);
                    PlayerPrefs.DeleteKey(dataBool[i].GetID());
                }
            }

            for (int i = 0, length = dataString.Count; i < length; i++)
            {
                if (dataString[i].ID == id)
                {
                    dataString[i].ChangeValue("");
                    PlayerPrefs.DeleteKey(dataString[i].GetID());
                }
            }
        }

        private void AddGameData(string id, float value, bool replaceInitialValue)
        {
            bool exist = PlayerPrefs.HasKey(id);
            DataPrefs dp = new DataPrefs(id, value);
            // Debug.Log(id + " " + value + ", " + dp.Value);
            if (!exist && replaceInitialValue)
            {
                dp.ChangeValue(value);
            }
            else
            {
                dp.SaveCurrentValue();
            }
            data.Add(dp);
        }

        private void AddGameDataBool(string id, bool value, bool replaceInitialValue)
        {
            bool exist = PlayerPrefs.HasKey(id);
            DataPrefsBool dpb = new DataPrefsBool(id, value);
            // Debug.Log(id + " " + value + ", " + dpb.Value);
            if (!exist && replaceInitialValue)
            {
                dpb.ChangeValue(value);
            }
            else
            {
                dpb.SaveCurrentValue();
            }
            dataBool.Add(dpb);
        }

        private void AddGameDataString(string id, string value, bool replaceInitialValue)
        {
            bool exist = PlayerPrefs.HasKey(id);
            DataPrefsString dpb = new DataPrefsString(id, value);
            // Debug.Log(id + " " + value + ", " + dpb.Value);
            if (!exist && replaceInitialValue)
            {
                dpb.ChangeValue(value);
            }
            else
            {
                dpb.SaveCurrentValue();
            }
            dataString.Add(dpb);
        }
    #endregion RemoveData

    }
}
