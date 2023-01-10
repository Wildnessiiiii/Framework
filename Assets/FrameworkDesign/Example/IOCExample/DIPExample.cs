#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class DIPExample : MonoBehaviour
    {
        // 1. 设计模块接口
        public interface IStorage
        {
            void SaveString(string key, string value);
            string LoadString(string key, string defaultValue = "");
        }

        // 2. 实现接口
        // 运行时存储
        public class PlayerPrefsStorage : IStorage
        {
            public void SaveString(string key, string value)
            {
                PlayerPrefs.SetString(key, value);
            }

            public string LoadString(string key, string defaultValue = "")
            {
                return PlayerPrefs.GetString(key, defaultValue);
            }
        }

        // 3. 实现接口
        // 编辑器存储
        public class EditorPrefsStorage : IStorage
        {
            public void SaveString(string key, string value)
            {
#if UNITY_EDITOR
                EditorPrefs.SetString(key, value);
#endif
            }

            public string LoadString(string key, string defaultValue = "")
            {
#if UNITY_EDITOR
                return EditorPrefs.GetString(key, defaultValue);
#else
                return ""
#endif
            }
        }


        // 4. 使用
        private void Start()
        {
            // 创建一个 IOC 容器
            var container = new IOCContainer();

            // 注册运行时模块
            container.Register<IStorage>(new PlayerPrefsStorage());

            var storage = container.Get<IStorage>();

            storage.SaveString("name", "运行时存储");

            Debug.Log(storage.LoadString("name"));

            // 切换实现
            container.Register<IStorage>(new EditorPrefsStorage());

            storage = container.Get<IStorage>();

            Debug.Log(storage.LoadString("name"));
        }
    }
}