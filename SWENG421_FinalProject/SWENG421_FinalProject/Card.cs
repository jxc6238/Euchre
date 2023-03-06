namespace SWENG421_FinalProject
{
    public class Card : CardIF
    {
        private SuitIF suit;
        private FaceIF face;

        public Card(SuitIF suit, FaceIF face)
        {
            this.suit = suit;
            this.face = face;
        }
        public string getSuitType()
        {
            string type = suit.GetType().ToString();
            string[] typeSplit = type.Split('.');
            return typeSplit[1];
        }
        public string getSuitColor()
        {
            return suit.getColor();
        }
        public int getFaceValue()
        {
            return face.getFaceValue();
        }

        public string getFaceType()
        {
            string type = face.GetType().ToString();
            string[] typeSplit = type.Split('.');
            return typeSplit[1];
        }
    }
}
