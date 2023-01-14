using FrameworkDesign;

namespace CounterAPP
{
    public class CounterApp : Architecture<CounterApp>
    {

        public CounterApp()
        {
            UnityEngine.Debug.Log("CounterApp:CounterApp()");
        }
        protected override void Init()
        {
            UnityEngine.Debug.Log("CounterApp:Init()");

            //Register<ICounterModel>(new CounterModel());
            RegisterModel<ICounterModel>(new CounterModel());
            RegisterUtility<IStorage>(new PlayerPrefsStorage());



        }
    }
}

