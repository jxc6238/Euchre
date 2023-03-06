namespace SWENG421_FinalProject
{
    public interface CardIF : UpperCardIF
    {
        string getSuitType();
        string getSuitColor();
        int getFaceValue();

        string getFaceType();
    }
}
