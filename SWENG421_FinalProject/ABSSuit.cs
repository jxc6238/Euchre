namespace SWENG421_FinalProject
{
    public class ABSSuit : SuitIF
    {
        private string color;

        public ABSSuit(string color)
        {
            this.color = color;
        }
        public string getColor()
        {
            return color;
        }
        public string getSuitType()
        {
            string type = GetType().ToString();
            string[] typeSplit = type.Split('.');
            return typeSplit[1];
        }
        public string getSuitColor()
        {
            return getColor();
        }
        public int getFaceValue()
        {
            return 0;
        }

        public string getFaceType()
        {
            return null;
        }
    }
}
