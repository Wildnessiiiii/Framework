using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class IOCExample:MonoBehaviour
    {
        private void Start()
        {
            IOCContainer container = new IOCContainer();

            container.Register<IBluetoothManager>(new BluetoothManager());

            IBluetoothManager bluetoothManager = container.Get<IBluetoothManager>();

            bluetoothManager.Connect();
        }
    }

    public interface IBluetoothManager
    {
        void Connect();
    }

    public class BluetoothManager: IBluetoothManager
    {
        public void Connect()
        {
            Debug.Log("链接成功");
        }
    }
}


