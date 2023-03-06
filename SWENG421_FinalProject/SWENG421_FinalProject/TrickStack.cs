using System.Collections.Generic;

namespace SWENG421_FinalProject
{
    public class TrickStack : TrickStackIF
    {
        private List<TrickIF> tricks = new List<TrickIF>();
        private List<PlayerIF> observers = new List<PlayerIF>();
        private ReadWriteLock lockManager;

        public TrickStack(ReadWriteLock lockManager)
        {
            this.lockManager = lockManager;
        }
        public void addTrick(TrickIF trick)
        {
            lockManager.writeLock();
            tricks.Add(trick);
            lockManager.done();
        }
        public List<TrickIF> getTricks()
        {
            lockManager.readLock();
            lockManager.done();
            return tricks;
        }
        public void clearStack()
        {
            tricks.Clear();
        }
        public void addObserver(PlayerIF player)
        {
            observers.Add(player);
        }
        public void removeObserver(PlayerIF player)
        {
            observers.Remove(player);
        }
        public void notifyObserver()
        {
            foreach(PlayerIF player in observers)
            {
                player.readUpdated();
            }
        }
    }
}
