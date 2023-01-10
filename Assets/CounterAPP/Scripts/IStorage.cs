using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CounterAPP
{
    public interface IStorage
    {
        void SaveInt(string key, int value);
        int LoadInt(string key, int value = 0);
    }

    public class PlayerPrefsStorage : IStorage
    {
        public void SaveInt(string key, int value = 0)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int LoadInt(string key, int value)
        {
            return PlayerPrefs.GetInt(key, value);
        }
    }

    public class EditorPrefsStorage : IStorage
    {
        public void SaveInt(string key, int value = 0)
        {
#if UNITY_EDITOR
            PlayerPrefs.SetInt(key, value);
#endif
        }

        public int LoadInt(string key, int value)
        {
#if UNITY_EDITOR
            return PlayerPrefs.GetInt(key, value);
#else
            return 0;
#endif
        }
    }
}
