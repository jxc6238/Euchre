using System.Collections.Generic;

namespace SWENG421_FinalProject
{
    public interface TrickStackIF
    {
        void addTrick(TrickIF trick);
        List<TrickIF> getTricks();
        void clearStack();
        void addObserver(PlayerIF player);
        void removeObserver(PlayerIF player);
        void notifyObserver();
    }
}
