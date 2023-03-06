namespace SWENG421_FinalProject
{
    public class Card32Game : ABSGameProgram
    {
        public override void start()
        {
            Deck deck = new Deck();
            SuitFactoryIF suitFactoryIF = new SuitFactory();
            FaceFactoryIF faceFactoryIF = new FaceFactory();

            SuitIF diamond = suitFactoryIF.createSuit("Diamond");
            SuitIF heart = suitFactoryIF.createSuit("Heart");
            SuitIF club = suitFactoryIF.createSuit("Club");
            SuitIF spade = suitFactoryIF.createSuit("Spade");
            FaceIF ace = faceFactoryIF.createFace("Ace");
            FaceIF king = faceFactoryIF.createFace("King");
            FaceIF queen = faceFactoryIF.createFace("Queen");
            FaceIF jack = faceFactoryIF.createFace("Jack");
            FaceIF ten = faceFactoryIF.createFace("Ten");
            FaceIF nine = faceFactoryIF.createFace("Nine");

            CardIF card = new Card(diamond, ace);
            deck.addCard(card);
            card = new Card(heart, ace);
            deck.addCard(card);
            card = new Card(club, ace);
            deck.addCard(card);
            card = new Card(spade, ace);
            deck.addCard(card);

            card = new Card(diamond, king);
            deck.addCard(card);
            card = new Card(heart, king);
            deck.addCard(card);
            card = new Card(club, king);
            deck.addCard(card);
            card = new Card(spade, king);
            deck.addCard(card);

            card = new Card(diamond, queen);
            deck.addCard(card);
            card = new Card(heart, queen);
            deck.addCard(card);
            card = new Card(club, queen);
            deck.addCard(card);
            card = new Card(spade, queen);
            deck.addCard(card);

            card = new Card(diamond, jack);
            deck.addCard(card);
            card = new Card(heart, jack);
            deck.addCard(card);
            card = new Card(club, jack);
            deck.addCard(card);
            card = new Card(spade, jack);
            deck.addCard(card);

            card = new Card(diamond, ten);
            deck.addCard(card);
            card = new Card(heart, ten);
            deck.addCard(card);
            card = new Card(club, ten);
            deck.addCard(card);
            card = new Card(spade, ten);
            deck.addCard(card);

            card = new Card(diamond, nine);
            deck.addCard(card);
            card = new Card(heart, nine);
            deck.addCard(card);
            card = new Card(club, nine);
            deck.addCard(card);
            card = new Card(spade, nine);
            deck.addCard(card);

            FaceIF eight = faceFactoryIF.createFace("Eight");
            card = new Card(diamond, eight);
            deck.addCard(card);
            card = new Card(heart, eight);
            deck.addCard(card);
            card = new Card(club, eight);
            deck.addCard(card);
            card = new Card(spade, eight);
            deck.addCard(card);

            FaceIF seven = faceFactoryIF.createFace("Seven");
            card = new Card(diamond, seven);
            deck.addCard(card);
            card = new Card(heart, seven);
            deck.addCard(card);
            card = new Card(club, seven);
            deck.addCard(card);
            card = new Card(spade, seven);
            deck.addCard(card);

            getEnvironment().initializeDeck(deck);
        }
    }
}
