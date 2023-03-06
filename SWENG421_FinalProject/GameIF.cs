namespace SWENG421_FinalProject
{
    public interface GameIF
    {
        void initializeDeck(Deck deck);

        void initializePlayers(PlayerIF player1, PlayerIF player2, PlayerIF player3, PlayerIF player4);

        void initializePartners(PartnerIF team1, PartnerIF team2);

        void setDealer(PlayerIF dealer);

        PlayerIF getDealer();

        void setCurrentPlayer(PlayerIF player);

        PlayerIF getCurrentPlayer();

        void dealCards();

        void setTrump(string trump);

        string getTrump();

        void playRound();

        void scoreTricks();

        void scoreRound();

        void playGame();

        void setLeadSuit(string suit);
    }
}
