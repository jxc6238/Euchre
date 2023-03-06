using System.Collections.Generic;

namespace SWENG421_FinalProject
{
    public interface PartnerIF
    {
        List<PlayerIF> getPartners();
        int getScore();
        void updateScore(int score);

        int getTricksWon();

        void updateTricksWon(int count);
    }
}
