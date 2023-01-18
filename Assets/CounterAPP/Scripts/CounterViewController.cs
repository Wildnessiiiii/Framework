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
            mCounterrModel = GetArchitecture().GetModel<ICounterModel>();

            mCounterrModel.Count.OnValueChanged += OnCountChanged;
            OnCountChanged(mCounterrModel.Count.Value);
            //CounterModel.Instance.Count.OnValueChanged += OnCountChanged;
            //CounterModel.OnCountChanged += OnCountChanged;
            transform.Find("BtnAdd").GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    // 交互逻辑
                    new AddCountCommand().Execute();
                });

            transform.Find("BtnSub").GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    // 交互逻辑
                    new SubCountCommand().Execute();
                });

        }

        private void OnDestroy()
        {
            //OnCountChangeEvent.UnRegisterEvent(OnCountChanged);
            mCounterrModel.Count.OnValueChanged -= OnCountChanged;
            mCounterrModel = null;
        }

        private void OnCountChanged(int newCount)
        {
            transform.Find("CountText").GetComponent<Text>().text = newCount.ToString();
        }

        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }

    public class OnCountChangeEvent : Event<OnCountChangeEvent>
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
            var storage = GetArchitecture().GetUtility<IStorage>();
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
