#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR || UNITY_STANDALONE_WIN

using System;
using Firebase;
using Firebase.Extensions;
using UnityEngine;

namespace Build1.PostMVC.Unity.Firebase.Impl
{
    internal class FirebaseAdapter
    {
        public static bool Initialized  { get; private set; }
        public static bool Initializing { get; private set; }

        public static event Action OnInitialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStaticState()
        {
            Initialized = false;
            Initializing = false;
            OnInitialized = null;
        }

        public static void Initialize()
        {
            if (Initialized)
                return;

            Initializing = true;
            
            FirebaseApp.CheckAndFixDependenciesAsync()
                       .ContinueWithOnMainThread(_ =>
                        {
                            Complete();
                        });
        }

        private static void Complete()
        {
            Initialized = true;
            Initializing = false;
            OnInitialized?.Invoke();
        }
    }
}

#endif
