using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace FFmpegOut
//{
    public sealed class MainThreadDispatcher : MonoBehaviour
    {
        private static readonly Queue<System.Action> _executionQueue = new Queue<System.Action>();

        // Update is called once per frame
        public void Update()
        {
            lock (_executionQueue)
            {
                while (_executionQueue.Count > 0)
                {
                    _executionQueue.Dequeue().Invoke();
                }
            }
        }

        public static void Enqueue(System.Action action)
        {
            lock (_executionQueue)
            {
                _executionQueue.Enqueue(action);
            }
        }
    }
//}