namespace SWENG421_FinalProject
{
    public class Trick : TrickIF
    {
        private CardIF card;
        private PlayerIF player;

        public Trick(CardIF card, PlayerIF player)
        {
            this.card = card;
            this.player = player;
        }
        public CardIF getCard()
        {
            return card;
        }
        public PlayerIF getPlayer()
        {
            return player;
        }
    }
}
