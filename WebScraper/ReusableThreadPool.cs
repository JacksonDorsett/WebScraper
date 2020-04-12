using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace WebScraper
{
    class ReusableThreadPool
    {
        ConcurrentQueue<ReusableThread> threadPool;
        public ReusableThreadPool(int MaxThreads)
        {
            threadPool = new ConcurrentQueue<ReusableThread>();
            for (int i = 0; i < MaxThreads; ++i)
            {
                threadPool.Enqueue(new ReusableThread(threadPool));
            }
        }

        public void StartThread(ThreadStart start)
        {
            if(IsThreadAvailable())
            {
                ReusableThread rt;
                if(threadPool.TryDequeue(out rt))
                {
                    
                    rt.Start(start);
                }
            }
        }
        public bool IsThreadAvailable()
        {
            return threadPool.Count != 0;
        }
        
    }
}
