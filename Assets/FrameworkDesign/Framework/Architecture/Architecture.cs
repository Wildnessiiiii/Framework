using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public interface IArchitecture
    {
        /// <summary>
        /// 注册System
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterSystem<T>(T system) where T : ISystem;

        /// <summary>
        /// 注册model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterModel<T>(T model) where T : IModel;

        /// <summary>
        /// 注册工具
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterUtility<T>(T utility);

        /// <summary>
        /// 获取工具
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetUtility<T>() where T : class;

        /// <summary>
        /// 获取model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetModel<T>() where T : class,IModel;
    }

    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        private static T mArchitecture;

        /// <summary>
        /// 是否初始化完毕
        /// </summary>
        private bool mInited = false;

        private List<IModel> mModels = new List<IModel>();
        private List<ISystem> mSystems = new List<ISystem>();

        public static Action<T> OnRegisterPatch = temp => { };

        public static IArchitecture Interface
        {
            get
            {
                if(mArchitecture==null)
                {
                    MakeSureArchitecture();
                };

                return mArchitecture;
            }            
        }

        static void MakeSureArchitecture()
        {
            if (mArchitecture == null)
            {
                mArchitecture = new T();
                //模块初始化
                mArchitecture.Init();                
                OnRegisterPatch?.Invoke(mArchitecture);

                //模块内的model初始化
                foreach (var item in mArchitecture.mModels)
                {
                    item.Init();
                }
                mArchitecture.mModels.Clear();

                //模块内的system初始化
                foreach (var item in mArchitecture.mSystems)
                {
                    item.Init();
                }
                mArchitecture.mSystems.Clear();

                mArchitecture.mInited = true;
            }            
        }

        protected abstract void Init();

        private IOCContainer mContainer = new IOCContainer();
        public static T Get<T>() where T : class
        {
            MakeSureArchitecture();
            return mArchitecture.mContainer.Get<T>();
        }

        public static void Register<T>(T instance)
        {
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
            //model赋值
            model.SetArchitecture(this);
            mContainer.Register<T>(model);

            if (!mInited)
            {               
                mModels.Add(model);
            }
            else
            {                
                model.Init();
            }
        }

        public T GetUtility<T>() where T : class
        {
            return mContainer.Get<T>();
        }

        public void RegisterUtility<T>(T instance)
        {
            mContainer.Register<T>(instance);
        }

        public void RegisterSystem<T>(T system) where T : ISystem
        {
            //System赋值
            system.SetArchitecture(this);
            mContainer.Register<T>(system);

            if (!mInited)
            {
                mSystems.Add(system);
            }
            else
            {
                system.Init();
            }
        }

        public T GetModel<T>() where T : class,IModel
        {            
            return mContainer.Get<T>();
        }
    }
}

