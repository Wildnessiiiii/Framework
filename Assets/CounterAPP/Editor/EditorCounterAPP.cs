using CounterAPP;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CounterAPP.Editor
{
    public class EditorCounterAPP : EditorWindow
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

        private void OnGUI()
        {
            if(GUILayout.Button("+"))
            {
                new AddCountCommand().Execute();
            }

            GUILayout.Label(CounterApp.Get<ICounterModel>().Count.Value.ToString());

            if (GUILayout.Button("-"))
            {
                new SubCountCommand().Execute();
            }

        }

      
    }

}

