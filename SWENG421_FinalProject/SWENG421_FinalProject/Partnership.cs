using System.Collections.Generic;

namespace SWENG421_FinalProject
{
    public class Partnership : PartnerIF
    {
        private List<PlayerIF> partners = new List<PlayerIF>();
        private int score = 0;
        private int tricksWon = 0;

        public Partnership(PlayerIF player1, PlayerIF player2)
        {
            partners.Add(player1);
            partners.Add(player2);
        }

        public List<PlayerIF> getPartners()
        {
            return partners;
        }

        public int getScore()
        {
            return score;
        }
        public void updateScore(int score)
        {
            this.score += score;
        }

        public int getTricksWon()
        {
            return tricksWon;
        }

        public void updateTricksWon(int count)
        {
            if (count != 0)
                tricksWon += count;
            else
                tricksWon = 0;
        }
    }
}
