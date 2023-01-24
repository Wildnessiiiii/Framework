using CounterAPP;
using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CounterAPP.Editor
{
    public class EditorCounterAPP : EditorWindow,IController
    {

        [MenuItem("EditorConterAPP/Open")]
        static void Open()
        {            
            CounterApp.OnRegisterPatch += app =>
            {
                Debug.Log("EditorCounterAPP OnRegisterPatch");
                app.RegisterUtility<IStorage>(new EditorPrefsStorage());
            };
            Debug.Log("EditorCounterAPP");//
            var window =  GetWindow<EditorCounterAPP>();
            window.position = new Rect(100, 100, 400, 600);
            window.titleContent = new GUIContent(nameof(EditorCounterAPP));
            window.Show();

        }

        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }

        private void OnGUI()
        {
            if(GUILayout.Button("+"))
            {
                GetArchitecture().SendCommand<AddCountCommand>();
            }

            GUILayout.Label(CounterApp.Get<ICounterModel>().Count.Value.ToString());

            if (GUILayout.Button("-"))
            {
                GetArchitecture().SendCommand<SubCountCommand>();
            }

        }

      
    }

}

