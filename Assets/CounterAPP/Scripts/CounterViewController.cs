using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace CounterAPP
{
    public class CounterViewController : MonoBehaviour,IController
    {
        private ICounterModel mCounterrModel;

        void Start()
        {
            mCounterrModel = this.GetModel<ICounterModel>();

            mCounterrModel.Count.OnValueChanged += OnCountChanged;
            OnCountChanged(mCounterrModel.Count.Value);
            //CounterModel.Instance.Count.OnValueChanged += OnCountChanged;
            //CounterModel.OnCountChanged += OnCountChanged;
            transform.Find("BtnAdd").GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    // 交互逻辑
                    this.SendCommand<AddCountCommand>();
                });

            transform.Find("BtnSub").GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    // 交互逻辑
                    this.SendCommand<SubCountCommand>();
                });

        }

        private void OnDestroy()
        {
            mCounterrModel.Count.OnValueChanged -= OnCountChanged;
            mCounterrModel = null;
        }

        private void OnCountChanged(int newCount)
        {
            transform.Find("CountText").GetComponent<Text>().text = newCount.ToString();
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }

    public class OnCountChangeEvent
    {

    }

    public interface ICounterModel : IModel
    {
        BindableProperty<int> Count { get; }
    }

    public class CounterModel:AbstractModel, ICounterModel
    {
        public void OnVaueChanged(int value)
        {
            PlayerPrefs.SetInt("COUNTERR_COUNT", value);
        }

        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();
            Count.Value = storage.LoadInt("COUNTERR_COUNT", 0);
            //Count.OnValueChanged += OnValueChanged;
            Count.OnValueChanged += count =>
            {
                storage.SaveInt("COUNTERR_COUNT", count);
            };
        }

        public BindableProperty<int> Count { get; } = new BindableProperty<int> 
        {             
            Value = 0 
        };
    }
}
