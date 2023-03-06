using System;
using System.Drawing;
using System.Windows.Forms;

namespace SWENG421_FinalProject
{
    public partial class Form1 : Form
    {
        private GameProgramIF gameProgram;
        private ReadWriteLock readWriteLock;
        private TrickStackIF trickStack;
        private GameIF game;
        private PlayerIF player1; // team1
        private PlayerIF player2;
        private PlayerIF player3; // team1
        private PlayerIF player4;
        private PartnerIF team1;
        private PartnerIF team2;
        private bool humanPlayerTurn;
        private string playedCardSuite;
        private string playedCardFace;

        public Form1()
        {
            InitializeComponent();
        }

        // Player1's name
        private void playerName_TextChanged(object sender, EventArgs e)
        {
            partnerComboBox.Enabled = true;
        }

        // Partner's name
        private void partnerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            gameVariationComboBox.Enabled = true;
        }

        // Game variation
        private void gameVariationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            startButton.Enabled = true;
        }

        // start button
        private void startButton_Click(object sender, EventArgs e)
        {
            startPanel.Hide();
            startPanel.Enabled = false;

            startGame();
        }

        private void startGame()
        {
            loadProgram();

            // initialize important key class instances
            readWriteLock = new ReadWriteLock();
            trickStack = new TrickStack(readWriteLock);
            game = new Game(gameProgram, trickStack, this);

            gameProgram.setEnvironment(game);
            gameProgram.start();

            setPlayerNames();
            enableGamePanels();

            game.playGame();
        }

        private void loadProgram()
        {
            string gameName = gameVariationComboBox.Text;
            if (gameName.Equals("24 Cards"))
                gameProgram = new Card24Game();
            else if (gameName.Equals("28 Cards"))
                gameProgram = new Card28Game();
            else if (gameName.Equals("32 Cards"))
                gameProgram = new Card32Game();
        }

        private void setPlayerNames()
        {
            player1 = new Player(playerName.Text, trickStack, true);
            player3 = new Player(partnerComboBox.Text, trickStack, false);
            team1 = new Partnership(player1, player3);

            player2 = new Player("Sam", trickStack, false);
            player4 = new Player("Ricky", trickStack, false);
            team2 = new Partnership(player2, player4);

            player1Label1.Text = player1.getName();
            player1Label2.Text = player1.getName();
            player1Label3.Text = player1.getName();
            player1Label4.Text = player1.getName();

            player2Label1.Text = player2.getName();
            player2Label2.Text = player2.getName();
            player2Label3.Text = player2.getName();
            player2Label4.Text = player2.getName();

            player3Label1.Text = player3.getName();
            player3Label2.Text = player3.getName();
            player3Label3.Text = player3.getName();
            player3Label4.Text = player3.getName();

            player4Label1.Text = player4.getName();
            player4Label2.Text = player4.getName();
            player4Label3.Text = player4.getName();
            player4Label4.Text = player4.getName();

            team1Label1.Text = player1.getName() + " & " + player3.getName();
            team1Label2.Text = player1.getName() + " & " + player3.getName();
            team1Label3.Text = player1.getName() + " & " + player3.getName();
            team1Label4.Text = player1.getName() + " & " + player3.getName();

            team2Label1.Text = player2.getName() + " & " + player4.getName();
            team2Label2.Text = player2.getName() + " & " + player4.getName();
            team2Label3.Text = player2.getName() + " & " + player4.getName();
            team2Label4.Text = player2.getName() + " & " + player4.getName();

            // forcing the order to be such so that partners are either both even or odd indexes
            game.initializePlayers(player2, player3, player4, player1);
            game.initializePartners(team1, team2);
        }

        private void enableGamePanels()
        {
            player1GamePanel.Enabled = true;
            player1GamePanel.Visible = true;
            player2GamePanel.Enabled = true;
            player2GamePanel.Visible = true;
            player3GamePanel.Enabled = true;
            player3GamePanel.Visible = true;
            player4GamePanel.Enabled = true;
            player4GamePanel.Visible = true;
        }

        public void setDealerName(string name)
        {
            dealerLabel1.Text = "Dealer: " + name;
            dealerLabel2.Text = "Dealer: " + name;
            dealerLabel3.Text = "Dealer: " + name;
            dealerLabel4.Text = "Dealer: " + name;
        }

        public void setCurrentPlayerName(string name)
        {
            currentPlayerLabel1.Text = "Current Player: " + name;
            currentPlayerLabel2.Text = "Current Player: " + name;
            currentPlayerLabel3.Text = "Current Player: " + name;
            currentPlayerLabel4.Text = "Current Player: " + name;
        }

        private void setTextColorRed(Label label)
        {
            label.BackColor = label.BackColor;
            label.ForeColor = Color.Red;
        }

        public void setTrumpTextBox(string suit)
        {
            trumpLabel1.Text = "Trump: " + suit;
            trumpLabel2.Text = "Trump: " + suit;
            trumpLabel3.Text = "Trump: " + suit;
            trumpLabel4.Text = "Trump: " + suit;
            if (suit.Equals("\u2666") || suit.Equals("\u2665"))
            {
                setTextColorRed(trumpLabel1);
                setTextColorRed(trumpLabel2);
                setTextColorRed(trumpLabel3);
                setTextColorRed(trumpLabel4);
            }
            else
            {
                setTextColorNormal(trumpLabel1);
                setTextColorNormal(trumpLabel2);
                setTextColorNormal(trumpLabel3);
                setTextColorNormal(trumpLabel4);
            }
        }

        public void setCard(int playerNumber, int cardNumber, string suit, string face)
        {
            switch (cardNumber)
            {
                case 1:
                    if(playerNumber == 1)
                        updateCard(card1_1, suit, face);
                    else if(playerNumber == 2)
                        updateCard(card1_2, suit, face);
                    else if(playerNumber == 3)
                        updateCard(card1_3, suit, face);
                    else
                        updateCard(card1_4, suit, face);
                    break;
                case 2:
                    if (playerNumber == 1)
                        updateCard(card2_1, suit, face);
                    else if (playerNumber == 2)
                        updateCard(card2_2, suit, face);
                    else if (playerNumber == 3)
                        updateCard(card2_3, suit, face);
                    else
                        updateCard(card2_4, suit, face);
                    break;
                case 3:
                    if (playerNumber == 1)
                        updateCard(card3_1, suit, face);
                    else if (playerNumber == 2)
                        updateCard(card3_2, suit, face);
                    else if (playerNumber == 3)
                        updateCard(card3_3, suit, face);
                    else
                        updateCard(card3_4, suit, face);
                    break;
                case 4:
                    if (playerNumber == 1)
                        updateCard(card4_1, suit, face);
                    else if (playerNumber == 2)
                        updateCard(card4_2, suit, face);
                    else if (playerNumber == 3)
                        updateCard(card4_3, suit, face);
                    else
                        updateCard(card4_4, suit, face);
                    break;
                case 5:
                    if (playerNumber == 1)
                        updateCard(card5_1, suit, face);
                    else if (playerNumber == 2)
                        updateCard(card5_2, suit, face);
                    else if (playerNumber == 3)
                        updateCard(card5_3, suit, face);
                    else
                        updateCard(card5_4, suit, face);
                    break;
                case 6:
                    if (playerNumber == 1)
                        updateCard(card6_1, suit, face);
                    else if (playerNumber == 2)
                        updateCard(card6_2, suit, face);
                    else if (playerNumber == 3)
                        updateCard(card6_3, suit, face);
                    else
                        updateCard(card6_4, suit, face);
                    break;
                case 7:
                    if (playerNumber == 1)
                        updateCard(card7_1, suit, face);
                    else if (playerNumber == 2)
                        updateCard(card7_2, suit, face);
                    else if (playerNumber == 3)
                        updateCard(card7_3, suit, face);
                    else
                        updateCard(card7_4, suit, face);
                    break;
                case 8:
                        updateCard(card8_1, suit, face);
                        updateCard(card8_2, suit, face);
                        updateCard(card8_3, suit, face);
                        updateCard(card8_4, suit, face);
                    break;
                case 9:
                        updateCard(card9_1, suit, face);
                        updateCard(card9_2, suit, face);
                        updateCard(card9_3, suit, face);    
                        updateCard(card9_4, suit, face);
                    break;
                case 10:
                        updateCard(card10_1, suit, face);
                        updateCard(card10_2, suit, face);
                        updateCard(card10_3, suit, face);
                        updateCard(card10_4, suit, face);
                    break;
                case 11:
                        updateCard(card11_1, suit, face);
                        updateCard(card11_2, suit, face);
                        updateCard(card11_3, suit, face);
                        updateCard(card11_4, suit, face);
                    break;
            }
        }

        private void updateCard(Label card, string suit, string face)
        {
            bool setRed = false;

            if (suit.Equals("\u2666") || suit.Equals("\u2665"))
                setRed = true;

            card.Text = suit + Environment.NewLine + face;
            card.Visible = true;
            if (setRed)
                setTextColorRed(card);
        }

        public string getSuitSymbol(string suit)
        {
            if (suit.Equals("Club"))
                return "\u2663";
            else if (suit.Equals("Diamond"))
                return "\u2666";
            else if (suit.Equals("Heart"))
                return "\u2665";
            else if (suit.Equals("Spade"))
                return "\u2660";

            return "";
        }

        public string getFaceSymbol(string face)
        {
            if (face.Equals("Ten"))
                return "10";
            else if (face.Equals("Nine"))
                return "9";
            else if (face.Equals("Eight"))
                return "8";
            else if (face.Equals("Seven"))
                return "7";
            else
                return face.Substring(0, 1);
        }

        public void setHumanPlayerTurn(bool turn)
        {
            humanPlayerTurn = turn;
        }

        public string getPlayedCardSuit()
        {
            if (playedCardSuite.Equals("\u2663"))
                return "Club";
            else if (playedCardSuite.Equals("\u2666"))
                return "Diamond";
            else if (playedCardSuite.Equals("\u2665"))
                return "Heart";
            else
                return "Spade";
        }

        public string getPlayedCardFace()
        {
            if (playedCardFace.Equals("10"))
                return "Ten";
            else if (playedCardFace.Equals("9"))
                return "Nice";
            else if (playedCardFace.Equals("8"))
                return "Eight";
            else if (playedCardFace.Equals("7"))
                return "Seven";
            else if (playedCardFace.Equals("A"))
                return "Ace";
            else if (playedCardFace.Equals("K"))
                return "King";
            else if (playedCardFace.Equals("Q"))
                return "Queen";
            else
                return "Jack";
        }

        public void resetTrickCards()
        {
            card8_1.Text = "";
            card8_1.Visible = false;
            setTextColorNormal(card8_1);

            card9_2.Text = "";
            card9_2.Visible = false;
            setTextColorNormal(card9_2);

            card10_3.Text = "";
            card10_3.Visible = false;
            setTextColorNormal(card10_3);

            card11_4.Text = "";
            card11_4.Visible = false;
            setTextColorNormal(card11_4);

            card9_1.Text = "";
            card9_1.Visible = false;
            setTextColorNormal(card9_1);

            card10_2.Text = "";
            card10_2.Visible = false;
            setTextColorNormal(card10_2);

            card11_3.Text = "";
            card11_3.Visible = false;
            setTextColorNormal(card11_3);

            card8_4.Text = "";
            card8_4.Visible = false;
            setTextColorNormal(card8_4);

            card10_1.Text = "";
            card10_1.Visible = false;
            setTextColorNormal(card10_1);

            card11_2.Text = "";
            card11_2.Visible = false;
            setTextColorNormal(card11_2);

            card8_3.Text = "";
            card8_3.Visible = false;
            setTextColorNormal(card8_3);

            card9_4.Text = "";
            card9_4.Visible = false;
            setTextColorNormal(card9_4);

            card11_1.Text = "";
            card11_1.Visible = false;
            setTextColorNormal(card11_1);

            card8_2.Text = "";
            card8_2.Visible = false;
            setTextColorNormal(card8_2);

            card9_3.Text = "";
            card9_3.Visible = false;
            setTextColorNormal(card9_3);

            card10_4.Text = "";
            card10_4.Visible = false;
            setTextColorNormal(card10_4);
        }

        public void resetPlayerCards()
        {
            card1_1.Text = "";
            card1_1.Visible = false;
            setTextColorNormal(card1_1);

            card1_2.Text = "";
            card1_2.Visible = false;
            setTextColorNormal(card1_2);

            card1_3.Text = "";
            card1_3.Visible = false;
            setTextColorNormal(card1_3);

            card1_4.Text = "";
            card1_4.Visible = false;
            setTextColorNormal(card1_4);

            card2_1.Text = "";
            card2_1.Visible = false;
            setTextColorNormal(card2_1);

            card2_2.Text = "";
            card2_2.Visible = false;
            setTextColorNormal(card2_2);

            card2_3.Text = "";
            card2_3.Visible = false;
            setTextColorNormal(card2_3);

            card2_4.Text = "";
            card2_4.Visible = false;
            setTextColorNormal(card2_4);

            card3_1.Text = "";
            card3_1.Visible = false;
            setTextColorNormal(card3_1);

            card3_2.Text = "";
            card3_2.Visible = false;
            setTextColorNormal(card3_2);

            card3_3.Text = "";
            card3_3.Visible = false;
            setTextColorNormal(card3_3);

            card3_4.Text = "";
            card3_4.Visible = false;
            setTextColorNormal(card3_4);

            card4_1.Text = "";
            card4_1.Visible = false;
            setTextColorNormal(card4_1);

            card4_2.Text = "";
            card4_2.Visible = false;
            setTextColorNormal(card4_2);

            card4_3.Text = "";
            card4_3.Visible = false;
            setTextColorNormal(card4_3);

            card4_4.Text = "";
            card4_4.Visible = false;
            setTextColorNormal(card4_4);

            card5_1.Text = "";
            card5_1.Visible = false;
            setTextColorNormal(card5_1);

            card5_2.Text = "";
            card5_2.Visible = false;
            setTextColorNormal(card5_2);

            card5_3.Text = "";
            card5_3.Visible = false;
            setTextColorNormal(card5_3);

            card5_4.Text = "";
            card5_4.Visible = false;
            setTextColorNormal(card5_4);

            card6_1.Text = "";
            card6_1.Visible = false;
            setTextColorNormal(card6_1);

            card6_2.Text = "";
            card6_2.Visible = false;
            setTextColorNormal(card6_2);

            card6_3.Text = "";
            card6_3.Visible = false;
            setTextColorNormal(card6_3);

            card6_4.Text = "";
            card6_4.Visible = false;
            setTextColorNormal(card6_4);

            card7_1.Text = "";
            card7_1.Visible = false;
            setTextColorNormal(card7_1);

            card7_2.Text = "";
            card7_2.Visible = false;
            setTextColorNormal(card7_2);

            card7_3.Text = "";
            card7_3.Visible = false;
            setTextColorNormal(card7_3);

            card7_4.Text = "";
            card7_4.Visible = false;
            setTextColorNormal(card7_4);

        }

        public void setTextColorNormal(Label label)
        {
            label.BackColor = Control.DefaultBackColor;
            label.ForeColor = Control.DefaultForeColor;
        }

        public void setGameOver(string playerA, string playerB)
        {
            gameOverLabel1.Text = "Game Over " + playerA + " and " + playerB + " Win!";
            gameOverLabel1.Visible = true;
            gameOverLabel2.Text = "Game Over " + playerA + " and " + playerB + " Win!";
            gameOverLabel2.Visible = true;
            gameOverLabel3.Text = "Game Over " + playerA + " and " + playerB + " Win!";
            gameOverLabel3.Visible = true;
            gameOverLabel4.Text = "Game Over " + playerA + " and " + playerB + " Win!";
            gameOverLabel4.Visible = true;
        }

        public void updateTeam1Score(string score)
        {
            scoreTeam1Label1.Text = score;
            scoreTeam1Label2.Text = score;
            scoreTeam1Label3.Text = score;
            scoreTeam1Label4.Text = score;
        }

        public void updateTeam2Score(string score)
        {
            scoreTeam2Label1.Text = score;
            scoreTeam2Label2.Text = score;
            scoreTeam2Label3.Text = score;
            scoreTeam2Label4.Text = score;
        }

        public void updateTeam1TricksWon(string score)
        {
            tricksWonTeam1Label1.Text = score;
            tricksWonTeam1Label2.Text = score;
            tricksWonTeam1Label3.Text = score;
            tricksWonTeam1Label4.Text = score;
        }

        public void updateTeam2TricksWon(string score)
        {
            tricksWonTeam2Label1.Text = score;
            tricksWonTeam2Label2.Text = score;
            tricksWonTeam2Label3.Text = score;
            tricksWonTeam2Label4.Text = score;
        }

        private void updatePlayedCard(Label cardA, Label cardB)
        {
            cardA.Text = cardB.Text;
            cardA.Visible = true;

            cardB.Visible = false;
            string temp = cardB.Text.ToString();
            playedCardSuite = temp.Substring(0, 1);
            playedCardFace = temp.Substring(3);
            cardB.Text = "";
        }

        public void setCardPlayed(int player, string suit, string face)
        {
            Label label = new Label();
            label.Text = suit + Environment.NewLine + face;
            if(player == 2)
            {
                if (card1_2.Text == label.Text)
                    updatePlayedCard(card8_1, card1_2);
                else if (card2_2.Text == label.Text)
                    updatePlayedCard(card8_1, card2_2);
                else if (card3_2.Text == label.Text)
                    updatePlayedCard(card8_1, card3_2);
                else if (card4_2.Text == label.Text)
                    updatePlayedCard(card8_1, card4_2);
                else if (card5_2.Text == label.Text)
                    updatePlayedCard(card8_1, card5_2);
                else if (card6_2.Text == label.Text)
                    updatePlayedCard(card8_1, card6_2);
                else if (card7_2.Text == label.Text)
                    updatePlayedCard(card8_1, card7_2);
            }
            else if (player == 3)
            {
                if (card1_3.Text == label.Text)
                    updatePlayedCard(card9_1, card1_3);
                else if (card2_3.Text == label.Text)
                    updatePlayedCard(card9_1, card2_3);
                else if (card3_3.Text == label.Text)
                    updatePlayedCard(card9_1, card3_3);
                else if (card4_3.Text == label.Text)
                    updatePlayedCard(card9_1, card4_3);
                else if (card5_3.Text == label.Text)
                    updatePlayedCard(card9_1, card5_3);
                else if (card6_3.Text == label.Text)
                    updatePlayedCard(card9_1, card6_3);
                else if (card7_3.Text == label.Text)
                    updatePlayedCard(card9_1, card7_3);

            }
            else if (player == 4)
            {
                if (card1_4.Text == label.Text)
                    updatePlayedCard(card10_1, card1_4);
                else if (card2_4.Text == label.Text)
                    updatePlayedCard(card10_1, card2_4);
                else if (card3_4.Text == label.Text)
                    updatePlayedCard(card10_1, card3_4);
                else if (card4_4.Text == label.Text)
                    updatePlayedCard(card10_1, card4_4);
                else if (card5_4.Text == label.Text)
                    updatePlayedCard(card10_1, card5_4);
                else if (card6_4.Text == label.Text)
                    updatePlayedCard(card10_1, card6_4);
                else if (card7_4.Text == label.Text)
                    updatePlayedCard(card10_1, card7_4);
            }
        }

        private void card1_1_Click(object sender, EventArgs e)
        {
            // don't do anything if its not the human player's turn
            if (humanPlayerTurn)
            {
                updatePlayedCard(card11_1, card1_1);
                game.playRound();
            }
        }

        private void card2_1_Click(object sender, EventArgs e)
        {
            // don't do anything if its not the human player's turn
            if (humanPlayerTurn)
            {
                updatePlayedCard(card11_1, card2_1);
                game.playRound();
            }
        }

        private void card3_1_Click(object sender, EventArgs e)
        {
            // don't do anything if its not the human player's turn
            if (humanPlayerTurn)
            {
                updatePlayedCard(card11_1, card3_1);
                game.playRound();
            }
        }

        private void card4_1_Click(object sender, EventArgs e)
        {
            // don't do anything if its not the human player's turn
            if (humanPlayerTurn)
            {
                updatePlayedCard(card11_1, card4_1);
                game.playRound();
            }
        }

        private void card5_1_Click(object sender, EventArgs e)
        {
            // don't do anything if its not the human player's turn
            if (humanPlayerTurn)
            {
                updatePlayedCard(card11_1, card5_1);
                game.playRound();
            }
        }

        private void card6_1_Click(object sender, EventArgs e)
        {
            // don't do anything if its not the human player's turn
            if (humanPlayerTurn)
            {
                updatePlayedCard(card11_1, card6_1);
                game.playRound();
            }
        }

        private void card7_1_Click(object sender, EventArgs e)
        {
            // don't do anything if its not the human player's turn
            if (humanPlayerTurn)
            {
                updatePlayedCard(card11_1, card7_1);
                game.playRound();
            }
        }
    }
}
