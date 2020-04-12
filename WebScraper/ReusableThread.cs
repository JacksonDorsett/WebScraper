using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace WebScraper
{
    class ReusableThread
    {
        private ConcurrentQueue<ReusableThread> poolRef;

        internal ReusableThread(ConcurrentQueue<ReusableThread> pool)
        {
            poolRef = pool;
        }

        public void Start(ThreadStart start)
        {
            mThread = new Thread(start);
            mThread.Start();
            poolRef.Enqueue(this);
        }
        private Thread mThread;
    }
}
