namespace SWENG421_FinalProject
{
    public class ABSFace : FaceIF
    {
        private int value;
        public ABSFace(int value)
        {
            this.value = value;
        }
        public int getValue()
        {
            return value;
        }
        public string getSuitType()
        {
            return null;
        }
        public string getSuitColor()
        {
            return null;
        }
        public int getFaceValue()
        {
            return value;
        }

        public string getFaceType()
        {
            string type = this.GetType().ToString();
            string[] typeSplit = type.Split('.');
            return typeSplit[1];
        }
    }
}
