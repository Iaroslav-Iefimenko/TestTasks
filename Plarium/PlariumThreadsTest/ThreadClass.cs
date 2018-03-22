using System.Threading;

namespace PlariumThreadsTest
{
    abstract class ThreadClass
    {
        public void Start()
        {
            Thread thread = new Thread(ThreadFunc);
            thread.Start();
        }

        protected abstract void ThreadFunc();
    }
}
