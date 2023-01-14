using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace CounterAPP
{
    public class CounterViewController : MonoBehaviour
    {
        private ICounterModel mCounterrModel;

        void Start()
        {
            mCounterrModel = CounterApp.Get<ICounterModel>();

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
    }

    public class OnCountChangeEvent : Event<OnCountChangeEvent>
    {

    }

    public interface ICounterModel : IModel
    {
        BindableProperty<int> Count { get; }
    }

    public class CounterModel: ICounterModel
    {

        public CounterModel()
        {
            
        }

        public void OnVaueChanged(int value)
        {
            PlayerPrefs.SetInt("COUNTERR_COUNT", value);
        }

        public void Init()
        {
            Debug.Log("CounterModel init");
            var storage = Architecture.GetUtility<IStorage>();
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
        public IArchitecture Architecture { get; set; }
    }
}
