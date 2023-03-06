namespace SWENG421_FinalProject
{
    public interface GameProgramIF
    {
        void setEnvironment(GameIF game);

        GameIF getEnvironment();

        void start();
    }
}
