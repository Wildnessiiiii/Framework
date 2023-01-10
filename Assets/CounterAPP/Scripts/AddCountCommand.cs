using FrameworkDesign;

namespace CounterAPP
{
    public struct AddCountCommand : ICommand
    {
        public void Execute()
        {
            CounterApp.Get<ICounterModel>().Count.Value++;
            //CounterModel.Instance.Count.Value++;
        }
    }

}
