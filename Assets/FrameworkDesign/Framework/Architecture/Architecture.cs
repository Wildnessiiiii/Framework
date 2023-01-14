using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public interface IArchitecture
    {
        /// <summary>
        /// 注册model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterModel<T>(T instance) where T : IModel;
        /// <summary>
        /// 获取工具
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetUtility<T>() where T : class;

        /// <summary>
        /// 注册工具
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterUtility<T>(T instance);
    }

    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        private static T mArchitecture;

        /// <summary>
        /// 是否初始化完毕
        /// </summary>
        private bool mInited = false;

        private List<IModel> mModels = new List<IModel>();

        /// <summary>
        /// 
        /// </summary>
        public static Action<T> OnRegisterPatch = temp => { };

        static void MakeSureArchitecture()
        {
            Debug.Log(mArchitecture);
            Debug.Log(mArchitecture==null);
            if (mArchitecture == null)
            {
                mArchitecture = new T();
                //模块初始化
                mArchitecture.Init();
                Debug.Log("AAAAAAAAAAAAAAAAAAA");
                OnRegisterPatch?.Invoke(mArchitecture);

                //模块内的model初始化
                foreach (var item in mArchitecture.mModels)
                {
                    item.Init();
                }
                Debug.Log("AAAAAAAAAAAAAAAAAAA");
                mArchitecture.mModels.Clear();
                mArchitecture.mInited = true;
            }
            Debug.Log(mArchitecture.mInited);
        }

        protected abstract void Init();

        private IOCContainer mContainer = new IOCContainer();
        public static T Get<T>() where T : class
        {
            MakeSureArchitecture();
            Debug.Log("Get<T>:" + typeof(T));
            return mArchitecture.mContainer.Get<T>();
        }

        public static void Register<T>(T instance)
        {
            Debug.Log("Register: " + typeof(T));
            MakeSureArchitecture();
            mArchitecture.mContainer.Register<T>(instance);
        }

        /// <summary>
        /// 注册model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void RegisterModel<T>(T model) where T : IModel
        {
            Debug.Log("RegisterModel<T>:" + typeof(T));
            //model赋值
            model.Architecture = this;
            mContainer.Register<T>(model);

            if (!mInited)
            {
                Debug.Log("RegisterModel<T>:ADD()  " + typeof(T));
                mModels.Add(model);
            }
            else
            {
                Debug.Log("RegisterModel<T>:Init  ()  " + typeof(T));
                model.Init();
            }
        }

        public T GetUtility<T>() where T : class
        {
            Debug.Log("GetUtility<T>():" + typeof(T));
            return mContainer.Get<T>();
        }

        public void RegisterUtility<T>(T instance)
        {
            mContainer.Register<T>(instance);
        }
    }
}

