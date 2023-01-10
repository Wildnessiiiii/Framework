using FrameworkDesign;

namespace CounterAPP
{
    public class CounterApp : Architecture<CounterApp>
    {
        protected override void Init()
        {
            UnityEngine.Debug.Log("CounterApp:Init()");
            RegisterModel<ICounterModel>(new CounterModel());
            //Register<ICounterModel>(new CounterModel());
            Register<IStorage>(new PlayerPrefsStorage());
         
            
        }
    }
}

