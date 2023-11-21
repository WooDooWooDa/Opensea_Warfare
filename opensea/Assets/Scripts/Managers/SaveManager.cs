using Assets.Scripts.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public interface ISaveable<T>
    {
        public void Save(ref T data);
    }

    public interface ILoadable<T, X>
    {
        public void Load(T data, X info);
    }

    /// <summary>
    /// T is the saving data class (ex: BuildingData)
    /// X is the scriptable object information class (ex: BuildingInformations)
    /// </summary>
    public interface ISavableLoadable<T, X> : ISaveable<T>, ILoadable<T, X> { }

    public class SaveManager : Manager
    {
        private static readonly string SAVE_NAME = "openseaData";
        private static readonly string SAVE_EXTENTION = ".save";

        private BinaryFormatter m_formatter = null;
        private SaveData m_internalData = new SaveData();

        public override void Initialize()
        {
            base.Initialize();

            m_formatter = GetFormatter();

            IsInitialize = true;
        }

        public  bool Load(ref SaveData data)
        {
            return true;

            if (LoadFromFile()) {
                data = m_internalData;

                return true;
            } else if (SaveToFile()) {
                data = m_internalData;

                return true;
            }
            return false;
        }

        public  bool Save(ref SaveData data)
        {
            return true;

            data.TimeStamp = System.DateTime.Now.ToFileTime();

            if (m_internalData.TimeStamp < data.TimeStamp) {
                m_internalData = data;
            }

            return SaveToFile();
        }

        private bool LoadFromFile()
        {
            string path = GetSavePath() + SAVE_NAME + SAVE_EXTENTION;

            if (!File.Exists(path)) {
                return false;
            }

            FileStream file = File.Open(path, FileMode.Open);

            try {
                m_internalData = (SaveData)m_formatter.Deserialize(file);
                file.Close();
            } catch {
                return false;
            }

            return true;
        }

        private bool SaveToFile()
        {
            if (!Directory.Exists(GetSavePath())) {
                Directory.CreateDirectory(GetSavePath());
            }

            string path = GetSavePath() + SAVE_NAME + SAVE_EXTENTION;

            FileStream file = File.Create(path);

            m_formatter.Serialize(file, m_internalData);

            file.Close();

            return true;
        }

        private BinaryFormatter GetFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            SurrogateSelector selector = new SurrogateSelector();

            Vector2SerializationSurrogate vector3Surrogate = new Vector2SerializationSurrogate();
            QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();

            selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

            formatter.SurrogateSelector = selector;

            return formatter;
        }

        private string GetSavePath()
        {
            return Application.persistentDataPath + "/saves/";
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public long TimeStamp = 0;

    }
}
