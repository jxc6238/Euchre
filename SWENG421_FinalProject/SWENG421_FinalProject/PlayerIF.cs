using System.Collections.Generic;

namespace SWENG421_FinalProject
{
    public interface PlayerIF
    {
        void playCard(CardIF card);
        void addCardToHand(CardIF card);
        string getName();
        bool isHuman();
        List<CardIF> getHand();
        void readUpdated();
        List<TrickIF> getGUITricks();
        void updatePlayerTrickStack();
    }
}
