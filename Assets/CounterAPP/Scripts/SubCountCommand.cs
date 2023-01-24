using FrameworkDesign;

namespace CounterAPP
{
    public class SubCountCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            //CounterApp.Get<ICounterModel>().Count.Value++;
            this.GetModel<ICounterModel>().Count.Value--;
        }
    }

}
