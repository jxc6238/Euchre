using System;
using System.Collections.Generic;
using System.Threading;

namespace SWENG421_FinalProject
{
    public class Game : GameIF
    {
        private Deck deck;
        private List<PlayerIF> players = new List<PlayerIF>();
        private List<PartnerIF> partners = new List<PartnerIF>();
        private string trump;
        private GameProgramIF gameProgram;
        private TrickStackIF trickStack;
        private PlayerIF dealer;
        private PlayerIF currentPlayer;
        private Form1 form1;
        private string leadSuit = "";

        public Game(GameProgramIF gameProgram, TrickStackIF trickStack, Form1 form1)
        {
            this.gameProgram = gameProgram;
            this.trickStack = trickStack;
            this.form1 = form1;
        }

        public void initializeDeck(Deck deck)
        {
            this.deck = deck;
        }

        public void initializePlayers(PlayerIF player1, PlayerIF player2, PlayerIF player3, PlayerIF player4)
        {
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);
            trickStack.addObserver(player1);
            trickStack.addObserver(player2);
            trickStack.addObserver(player3);
            trickStack.addObserver(player4);
        }

        public void initializePartners(PartnerIF team1, PartnerIF team2)
        {
            partners.Add(team1);
            partners.Add(team2);
        }

        public void playGame()
        {

            // check if its the beginning of the game
            if (partners[0].getScore() == 0 && partners[1].getScore() == 0)
                setDealer(players[0]);
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (dealer == players[i])
                    {
                        if (i < 3)
                            setDealer(players[i + 1]);
                        else
                            setDealer(players[0]);
                        break;
                    }
                }
            }
            dealCards();
            playRound();
        }

        public void setDealer(PlayerIF dealer)
        {
            this.dealer = dealer;
            form1.setDealerName(dealer.getName());
            setCurrentPlayer(dealer);
        }

        public PlayerIF getDealer()
        {
            return dealer;
        }

        public void setCurrentPlayer(PlayerIF player)
        {
            for (int i = 0; i < 4; i++)
            {
                if (players[i] == player)
                {
                    if (i < 3)
                        currentPlayer = players[i + 1];
                    else
                        currentPlayer = players[0];
                    break;
                }
            }
            form1.setCurrentPlayerName(currentPlayer.getName());
            if (currentPlayer.isHuman())
                form1.setHumanPlayerTurn(true);
            else
                form1.setHumanPlayerTurn(false);
        }

        public PlayerIF getCurrentPlayer()
        {
            return currentPlayer;
        }

        public void dealCards()
        {
            deck.shuffleDeck();
            List<CardIF> tempCards = deck.getCards();
            int count = 0;
            int cardsPerPlayer = (tempCards.Count / 4) - 1;
            for (int i = 0; i < cardsPerPlayer; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    players[j].addCardToHand(tempCards[count]);
                    count++;
                }
            }
            setTrump(tempCards[count].getSuitType());

            // display the main player's cards in the GUI
            tempCards = players[3].getHand();
            for (int i = 0; i < cardsPerPlayer; i++)
            {
                string suit = tempCards[i].getSuitType();
                suit = form1.getSuitSymbol(suit);

                string face = tempCards[i].getFaceType();
                face = form1.getFaceSymbol(face);
                form1.setCard(1, i + 1, suit, face);
            }

            // display the player2's cards in the GUI
            tempCards = players[0].getHand();
            for (int i = 0; i < cardsPerPlayer; i++)
            {
                string suit = tempCards[i].getSuitType();
                suit = form1.getSuitSymbol(suit);

                string face = tempCards[i].getFaceType();
                face = form1.getFaceSymbol(face);
                form1.setCard(2, i + 1, suit, face);
            }

            // display the player3's cards in the GUI
            tempCards = players[1].getHand();
            for (int i = 0; i < cardsPerPlayer; i++)
            {
                string suit = tempCards[i].getSuitType();
                suit = form1.getSuitSymbol(suit);

                string face = tempCards[i].getFaceType();
                face = form1.getFaceSymbol(face);
                form1.setCard(3, i + 1, suit, face);
            }

            // display the player4's cards in the GUI
            tempCards = players[2].getHand();
            for (int i = 0; i < cardsPerPlayer; i++)
            {
                string suit = tempCards[i].getSuitType();
                suit = form1.getSuitSymbol(suit);

                string face = tempCards[i].getFaceType();
                face = form1.getFaceSymbol(face);
                form1.setCard(4, i + 1, suit, face);
            }
        }

        public void setTrump(string trump)
        {
            this.trump = trump;
            form1.setTrumpTextBox(form1.getSuitSymbol(trump));
        }

        public string getTrump()
        {
            return trump;
        }
        public void playRound()
        {
            if (!currentPlayer.isHuman())
            {
                while (!currentPlayer.isHuman() && trickStack.getTricks().Count != 4)
                {

                    List<CardIF> hand = currentPlayer.getHand();
                    CardIF card = hand[0];
                    int value = 0;

                    // must follow the lead suit if the player has it, otherwise just play the highest card
                    if (!leadSuit.Equals(""))
                    {
                        for (int i = 0; i < hand.Count; i++)
                        {
                            if (hand[i].getSuitType().Equals(leadSuit))
                            {
                                value = hand[i].getFaceValue();
                                card = hand[i];
                            }
                            // ToDo need to add logic for the right bower (the other jack of the same color) here
                        }
                        if (value == 0)
                            card = hand[0];
                    }
                    else
                        card = hand[0];

                    //currentPlayer.playCard(card);
                    Thread t1 = new Thread(() => currentPlayer.playCard(card));
                    t1.Start();
                    // ToDo - this is for testing get rid of this later
                    Console.WriteLine(currentPlayer.getName() + ": " + card.getSuitType() + " " + card.getFaceType());

                    // check which player it is so the GUI can be updated to show the card played
                    for (int i = 0; i < 4; ++i)
                    {
                        string suit = card.getSuitType();
                        if (trickStack.getTricks().Count == 1)
                            setLeadSuit(suit);
                        suit = form1.getSuitSymbol(suit);

                        string face = card.getFaceType();
                        face = form1.getFaceSymbol(face);

                        if (players[i] == currentPlayer)
                        {
                            switch (i)
                            {
                                case 0:
                                    form1.setCard(2, 8, suit, face);
                                    form1.setCardPlayed(2, suit, face);
                                    break;
                                case 1:
                                    form1.setCard(3, 9, suit, face);
                                    form1.setCardPlayed(3, suit, face);
                                    break;
                                case 2:
                                    form1.setCard(4, 10, suit, face);
                                    form1.setCardPlayed(4, suit, face);
                                    break;
                            }
                        }
                    }
                    if (trickStack.getTricks().Count != 4)
                    {
                        setCurrentPlayer(currentPlayer);
                    }
                    else
                        scoreTricks();
                }
            }
            // if current player is human
            else
            {
                // ToDo should add logic to enforce that player must follow the lead suit
                string tempSuit = form1.getPlayedCardSuit();
                string tempFace = form1.getPlayedCardFace();
                CardIF card = currentPlayer.getHand()[0];

                for (int i = 0; i < currentPlayer.getHand().Count; ++i)
                {
                    if (currentPlayer.getHand()[i].getSuitType() == tempSuit && currentPlayer.getHand()[i].getFaceType() == tempFace)
                    {
                        card = currentPlayer.getHand()[i];
                        break;
                    }     
                }

                currentPlayer.playCard(card);
                string suit = card.getSuitType();
                if (trickStack.getTricks().Count == 1)
                    setLeadSuit(suit);

                // ToDo - this is for testing get rid of this later
                Console.WriteLine(currentPlayer.getName() + ": " + card.getSuitType() + " " + card.getFaceType());

                if (trickStack.getTricks().Count != 4)
                {
                    setCurrentPlayer(currentPlayer);
                    // Continue the round if the newly set current player isn't human & the trickStack isn't full
                    if (!currentPlayer.isHuman())
                        playRound();
                }
                else
                    scoreTricks();
            }
        }
        public void setLeadSuit(string suit)
        {
            leadSuit = suit;
        }

        public void scoreTricks()
        {
            // ToDo remove this later --- this is for debugging
            Console.WriteLine("<<<Scoring Trick>>>");
            Console.WriteLine("Trump: " + trump);

            List<TrickIF> tricks = trickStack.getTricks();
            TrickIF winningTrick = tricks[0];

            for (int i = 1; i < tricks.Count; i++)
            {
                SuitFactoryIF suitFactory = new SuitFactory();
                SuitIF trumpSuit = suitFactory.createSuit(trump);

                // first determine if the winning trick has a Jack and if the Jack is trump
                if (winningTrick.getCard().getFaceType().Equals("Jack") && (winningTrick.getCard().getSuitType().Equals(trump)
                    || winningTrick.getCard().getSuitColor() == trumpSuit.getSuitColor()))
                {
                    // then determine if the compare trick has a Jack and if the Jack is trump
                    if (tricks[i].getCard().getFaceType().Equals("Jack") && (tricks[i].getCard().getSuitType().Equals(trump)
                    || tricks[i].getCard().getSuitColor() == trumpSuit.getSuitColor()))
                    {
                        // if the compare trick is the left bower, make it the winning trick
                        if (tricks[i].getCard().getSuitType().Equals(trump))
                            winningTrick = tricks[i];
                    }
                }
                // otherwise if the compare trick is trump and a Jack make it the winning trick no matter what
                else if (tricks[i].getCard().getFaceType().Equals("Jack") && (tricks[i].getCard().getSuitType().Equals(trump)
                    || tricks[i].getCard().getSuitColor() == trumpSuit.getSuitColor()))
                    winningTrick = tricks[i];
                // if both are trump, but not Jacks, make the higher one the winner
                else if (winningTrick.getCard().getSuitType().Equals(trump) && tricks[i].getCard().getSuitType().Equals(trump)
                    && tricks[i].getCard().getFaceValue() > winningTrick.getCard().getFaceValue())
                    winningTrick = tricks[i];
                // if the compare is trump but not a Jack, but the winning trick is not trump, make the compare the winner
                else if (tricks[i].getCard().getSuitType().Equals(trump) && !winningTrick.getCard().getSuitType().Equals(trump))
                    winningTrick = tricks[i];
                // otherwise if they are the same suit, make the higher on the winning trick
                else if (winningTrick.getCard().getSuitType() == tricks[i].getCard().getSuitType()
                    && tricks[i].getCard().getFaceValue() > winningTrick.getCard().getFaceValue())
                    winningTrick = tricks[i];
            }

            // make the winning player the currentPlayer so they can start the next trick
            currentPlayer = winningTrick.getPlayer();
            form1.setCurrentPlayerName(currentPlayer.getName());
            if (!currentPlayer.isHuman())
                form1.setHumanPlayerTurn(false);
            else
                form1.setHumanPlayerTurn(true);

            // track the winning team that won the trick
            for (int i = 0; i < 2; i++)
            {
                List<PlayerIF> playerList = partners[i].getPartners();
                if (currentPlayer == playerList[0] || currentPlayer == playerList[1])
                {
                    partners[i].updateTricksWon(1);
                    if (i == 0)
                        form1.updateTeam1TricksWon(partners[i].getTricksWon().ToString());
                    else
                        form1.updateTeam2TricksWon(partners[i].getTricksWon().ToString());
                }
            }
            // empty the trickstack
            trickStack.clearStack();
            form1.resetTrickCards();

            // ToDo remove this later ... this is for debugging
            Console.WriteLine("Team1 Tricks Won Total = " + partners[0].getTricksWon());
            Console.WriteLine("Team2 Tricks Won Total = " + partners[1].getTricksWon());

            // if the currentplayer's hand is empty, then score the round
            if (currentPlayer.getHand().Count == 0)
                scoreRound();
            // continue the round if the player is not human
            else if (!currentPlayer.isHuman())
                playRound();
            // otherwise continue the round, but wait for the human player to make a play
            else
                return;
        }

        public void scoreRound()
        {
            // ToDo remove this later --- this is for debugging
            Console.WriteLine("<<<Scoring Round>>>");

            if (partners[0].getTricksWon() > partners[1].getTricksWon())
            {
                if (partners[0].getTricksWon() >= 5)
                    partners[0].updateScore(2);
                else
                    partners[0].updateScore(1);
            }
            else if (partners[0].getTricksWon() == partners[1].getTricksWon())
            {
                partners[0].updateScore(1);
                partners[1].updateScore(1);
            }
            else
            {
                if (partners[1].getTricksWon() >= 5)
                    partners[1].updateScore(2);
                else
                    partners[1].updateScore(1);
            }

            // ToDo Remove this later ... this is for debugging
            Console.WriteLine("Team1 Score = " + partners[0].getScore());
            Console.WriteLine("Team1 Score = " + partners[1].getScore());

            form1.updateTeam1TricksWon("0");
            form1.updateTeam2TricksWon("0");
            form1.resetPlayerCards();

            partners[0].updateTricksWon(0);
            partners[1].updateTricksWon(0);

            form1.updateTeam1Score(partners[0].getScore().ToString());
            form1.updateTeam2Score(partners[1].getScore().ToString());

            // check if the game is over
            if (partners[0].getScore() >= 10)
            {
                List<PlayerIF> players = partners[0].getPartners();
                form1.resetPlayerCards();
                form1.resetTrickCards();
                form1.setGameOver(players[0].getName(), players[1].getName());
            }
            // check if the game is over
            else if (partners[1].getScore() >= 10)
            {
                List<PlayerIF> players = partners[1].getPartners();
                form1.resetPlayerCards();
                form1.resetTrickCards();
                form1.setGameOver(players[1].getName(), players[1].getName());
            }
            else
                playGame();
        }
    }
}
