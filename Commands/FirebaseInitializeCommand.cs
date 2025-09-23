#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR || UNITY_STANDALONE_WIN

using Build1.PostMVC.Core.MVCS.Commands;
using Build1.PostMVC.Unity.Firebase.Impl;

namespace Build1.PostMVC.Unity.Firebase.Commands
{
    public sealed class FirebaseInitializeCommand : Command
    {
        public override void Execute()
        {
            if (FirebaseAdapter.Initialized)
                return;

            Retain();

            FirebaseAdapter.OnInitialized += FirebaseAdapterOnOnInitialized;
            
            if (!FirebaseAdapter.Initializing)
                FirebaseAdapter.Initialize();
        }

        private void FirebaseAdapterOnOnInitialized()
        {
            FirebaseAdapter.OnInitialized -= FirebaseAdapterOnOnInitialized;

            Release();
        }
    }
}
#endif