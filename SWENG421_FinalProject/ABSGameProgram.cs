namespace SWENG421_FinalProject
{
    public abstract class ABSGameProgram : GameProgramIF
    {
        private GameIF game;

        public GameIF getEnvironment()
        {
            return game;
        }

        public void setEnvironment(GameIF game)
        {
            this.game = game;
        }

        public abstract void start();
    }
}
