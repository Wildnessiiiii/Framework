using FrameworkDesign;

namespace CounterAPP
{
    public struct SubCountCommand : ICommand
    {
        public void Execute()
        {
            CounterApp.Get<ICounterModel>().Count.Value--;
        }
    }

}
