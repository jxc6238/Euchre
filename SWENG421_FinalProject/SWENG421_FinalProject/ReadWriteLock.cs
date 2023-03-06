using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SWENG421_FinalProject
{
    public class ReadWriteLock
    {
        private int waitingForReadLock = 0;
        private int outstandingReadLocks = 0;
        private Thread writeLockThread;
        private List<Thread> waitingForWriteLock = new List<Thread>();
        Object writeLockObject = new Object();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void readLock()
        {
            if (writeLockThread != null)
            {
                waitingForReadLock++;
                while (writeLockThread != null)
                {
                    Monitor.Wait(this);
                    Console.WriteLine(this.GetType());
                }
                waitingForReadLock--;
            }
            outstandingReadLocks++;
        }
        public void writeLock()
        {
            Thread currentThread;
            //Object writeLockObject = new Object();
            lock (this)
            {
                //Console.WriteLine("Got write lock");
                if (writeLockThread == null && outstandingReadLocks == 0)
                {
                    writeLockThread = Thread.CurrentThread;
                    return;
                }
                currentThread = Thread.CurrentThread;
                waitingForWriteLock.Add(currentThread);
            }
            lock (writeLockObject)
            {
                while (currentThread != writeLockThread)
                {
                    Monitor.Wait(writeLockObject);
                }
            }
            lock (this)
            {
                waitingForWriteLock.Remove(Thread.CurrentThread);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void done()
        {
            if (outstandingReadLocks > 0)
            {
                outstandingReadLocks--;
                if (outstandingReadLocks == 0 && waitingForWriteLock.Count() > 0)
                {
                    writeLockThread = waitingForWriteLock[0];
                    lock (writeLockObject)
                    {
                        Monitor.PulseAll(writeLockObject);
                    }
                }
            }
            else if (Thread.CurrentThread == writeLockThread)
            {
                // ToDo this is for debugging... remove later
                // Console.WriteLine("letting lock go");
                if (outstandingReadLocks == 0 && waitingForWriteLock.Count() > 0)
                {
                    writeLockThread = (Thread)waitingForWriteLock[0];
                    lock (writeLockObject)
                    {
                        Monitor.PulseAll(writeLockObject);
                    }
                }
                else
                {
                    lock (this)
                    {
                        writeLockThread = null;
                        if (waitingForReadLock > 0)
                        {
                            Monitor.PulseAll(this);
                        }
                    }
                }
            }
            else
            {
                throw new ThreadStateException("Thread does not have lock");
            }
        }
    }
}
