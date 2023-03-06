using System;
using System.Collections.Generic;

namespace SWENG421_FinalProject
{
    public class Deck : DeckIF
    {
        private List<CardIF> cards = new List<CardIF>();
        Random random = new Random();

        public void addCard(CardIF card)
        {
            cards.Add(card);
        }


        public void shuffleDeck()
        {
            List<int> randomOrder = new List<int>();
            int min = 0;
            int max = cards.Count;
            int tmp;
            while (randomOrder.Count != cards.Count)
            {
                tmp = random.Next(min, max);
                if (randomOrder.Contains(tmp) == false)
                {
                    randomOrder.Add(tmp);
                }
            }

            List<CardIF> tmpList = new List<CardIF>();
            for (int i = 0; i < randomOrder.Count; i++)
            {
                tmpList.Add(cards[randomOrder[i]]);
            }
            cards = tmpList;
        }

        public List<CardIF> getCards()
        {
            return cards;
        }
    }
}
