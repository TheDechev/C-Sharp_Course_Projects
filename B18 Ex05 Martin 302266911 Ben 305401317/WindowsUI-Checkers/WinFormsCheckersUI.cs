//using Checkers_Logic;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace WindowsUI_Checkers
//{
//    public class WinFormsCheckersUI
//    {
//        private GameForm m_FormGame = new GameForm();
//        private CheckersGame.eRoundOptions m_RoundStatus;

//        public WinFormsCheckersUI()
//        {
//            m_FormGame.ShowDialog();
            
//        }

//        public void runGame()
//        {

//        }

//        private void OnSquareUpdate(int i_Row, int i_Col, string i_SquareType)
//        {
//            string ButtonToUpdate = string.Format("{0}{1}", Convert.ToChar(i_Col + (int)'A'), Convert.ToChar(i_Row + (int)'a'));
//            this.m_FormGame.Controls[ButtonToUpdate].Text = i_SquareType;
//        }

//        private void handleRound()
//        {

//            if (m_RoundStatus == CheckersGame.eRoundOptions.weakPlayerQuits)
//            {
//                //this.endOfRoundScreen(i_Game, i_CurrentPlayer.PlayerType, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
//            }
//            else if (m_RoundStatus == CheckersGame.eRoundOptions.strongPlayerWantsToQuit)
//            {
//                // another round - Quit
//                MessageBox.Show("You are not the weak player! Enter a valid move.", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            else if (m_RoundStatus == CheckersGame.eRoundOptions.playerDidntEnterObligatoryMove)
//            {
//                // another round - Wrong move
//                MessageBox.Show("Invalid move, you must eliminate your opponnent!", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);

//            }
//            else if (m_RoundStatus == CheckersGame.eRoundOptions.currentPlayerHasAnotherRound)
//            {
//                string warningMsg = string.Format("{0} has another turn.", m_Game.CurrentPlayer.Name);
//                // another round 
//                MessageBox.Show(warningMsg, "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            else if (m_RoundStatus == CheckersGame.eRoundOptions.playerOneWon)
//            {
//                //iswinnigPlayer = true;
//                //i_Game.endRoundScoreUpdate(Square.eSquareType.playerTwo);
//                //this.endOfRoundScreen(i_Game, Square.eSquareType.playerOne, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
//            }
//            else if (m_RoundStatus == CheckersGame.eRoundOptions.playerTwoWon)
//            {
//                //iswinnigPlayer = true;
//                //i_Game.endRoundScoreUpdate(Square.eSquareType.playerOne);
//                //this.endOfRoundScreen(i_Game, Square.eSquareType.playerTwo, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
//            }
//            else if (m_RoundStatus == CheckersGame.eRoundOptions.gameIsATie)
//            {
//                //this.endOfRoundScreen(i_Game, Square.eSquareType.none, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
//            }
//            else if (m_RoundStatus == CheckersGame.eRoundOptions.playerEnteredInvalidMove)
//            {
//                MessageBox.Show("Invalid move! Try again.", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }
//    }
//}
