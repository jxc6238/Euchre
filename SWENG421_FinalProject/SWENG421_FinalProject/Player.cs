using System;
using System.Collections.Generic;
using System.Threading;

namespace SWENG421_FinalProject
{
    public class Player : PlayerIF
    {
        private string name;
        private List<CardIF> hand = new List<CardIF>();
        private TrickStackIF trickStack;
        private List<TrickIF> tricks = new List<TrickIF>();
        private bool human;

        public Player(string name, TrickStackIF trickStack, bool human)
        {
            this.name = name;
            this.trickStack = trickStack;
            this.human = human;
        }
        public void playCard(CardIF card)
        {
            hand.Remove(card);
            trickStack.addTrick(new Trick(card, this));

        }
        public void addCardToHand(CardIF card)
        {
            hand.Add(card);
            for (int i = 1; i < hand.Count; i++)
            {
                for (int j = 0; j < hand.Count - 1; j++)
                {
                    if (hand[j].getFaceValue() < hand[j + 1].getFaceValue())
                    {
                        CardIF temp = hand[j];
                        hand[j] = hand[j + 1];
                        hand[j + 1] = temp;
                    }
                }
            }
        }

        string PlayerIF.getName()
        {
            return name;
        }
        
        public void readUpdated()
        {
            PlayerIF player = this;
            Thread t1 = new Thread(() => player.updatePlayerTrickStack());
            t1.Start();
        }
        public void updatePlayerTrickStack()
        {
            tricks = trickStack.getTricks();
        }
        public List<TrickIF> getGUITricks()
        {
            return tricks;
        }
        // for testing 
        // ToDo remove this later
        public void doSomething()
        {
            FaceIF ten = new Ten();
            SuitIF spade = new Spade();
            CardIF card = new Card(spade, ten);
            TrickIF trick = new Trick(card, this);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(this.name + " is getting the lock");
                trickStack.addTrick(trick);
            }
        }
        // for testing 
        // ToDo remove this later
        public void doSomething2()
        {
            List<TrickIF> tmp = new List<TrickIF>();
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(name + " is reading");
                tmp = trickStack.getTricks();
            }
        }

        public bool isHuman()
        {
            return human;
        }

        public List<CardIF> getHand()
        {
            return hand;
        }
    }
}
