using System;

namespace Checkers_Logic
{
    public class CheckersGame
    {
        private const string k_PcDeafultNameString = "Computer";
        private const int k_KingScoreValue = 4;
        private const int k_RegularCheckerScoreValue = 1;
 

        public enum eRoundOptions
        {
            passRound,
            weakPlayerQuits,
            currentPlayerHasAnotherRound,
            strongPlayerWantsToQuit,
            playerDidntEnterObligatoryMove,
            playerEnteredInvalidMove,
            gameIsATie,
            playerOneWon,
            playerTwoWon,
            gameOver
        }

        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Player m_CurrentPlayer;
        private Board m_Board;
        private int m_TurnCounter = 0;

        public Player PlayerOne
        {
            get
            {
                return this.m_PlayerOne;
            }
        }

        public Player PlayerTwo
        {
            get
            {
                return this.m_PlayerTwo;
            }
        }

        public Board Board
        {
            get
            {
                return this.m_Board;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        public void endRoundScoreUpdate(Square.eSquareType i_LosingpPlayer)
        {
            if (i_LosingpPlayer == Square.eSquareType.playerOne)
            {
                this.m_PlayerTwo.Score += this.m_PlayerTwo.BonusScore;
                this.m_PlayerTwo.BonusScore = this.m_PlayerTwo.Score;
                this.m_PlayerOne.Score = this.m_PlayerOne.BonusScore;
            }
            else
            {
                this.m_PlayerOne.Score += this.m_PlayerOne.BonusScore;
                this.m_PlayerOne.BonusScore = this.m_PlayerOne.Score;
                this.m_PlayerTwo.Score = this.m_PlayerTwo.BonusScore;
            }
        }

        public eRoundOptions NewRound(string i_UserMove)
        {
            
            Move inputMove = Move.Parse(i_UserMove);
            Square.eSquareType weakPlayer;
            bool moveWasSuccessful;
            eRoundOptions roundStatus = eRoundOptions.passRound;

            this.m_CurrentPlayer = this.GetCurrentPlayer();
            this.m_CurrentPlayer.UpdateObligatoryMoves(this.m_Board,null);
            this.m_CurrentPlayer.UpdateAvailableMovesIndicator(this.m_Board);
            weakPlayer = this.GetWeakPlayer();
            inputMove = Move.Parse(i_UserMove);

            if (this.m_CurrentPlayer.ObligatoryMovesCount > Move.k_ZeroObligatoryMoves)
            {
                roundStatus = this.PlayObligatoryMove(this.m_CurrentPlayer, ref inputMove);
                if(roundStatus != eRoundOptions.passRound)
                {
                    this.m_TurnCounter--;
                }
            }
            else if (this.m_CurrentPlayer.HasAvailableMove)
            {
                bool needToEliminate = true;
                moveWasSuccessful = this.m_Board.UpdateBoardAfterMove(inputMove, this.m_CurrentPlayer, !needToEliminate);
                if (!moveWasSuccessful)
                {
                    roundStatus = eRoundOptions.playerEnteredInvalidMove;
                    this.m_TurnCounter--;
                }
            }

            roundStatus = CheckGameStatus(roundStatus);

            // TODO: test
            if (roundStatus == eRoundOptions.playerTwoWon)
            {
                this.endRoundScoreUpdate(Square.eSquareType.playerOne);
                //roundStatus = eRoundOptions.weakPlayerQuits;
            }
            else if (roundStatus == eRoundOptions.playerOneWon)
            {
                this.endRoundScoreUpdate(Square.eSquareType.playerTwo);
                //roundStatus = eRoundOptions.weakPlayerQuits;
            }
            else
            {
                this.m_TurnCounter++;
            }

            this.m_CurrentPlayer = this.GetCurrentPlayer();
            return roundStatus;
        }

        public Player GetCurrentPlayer()
        {
            Player whichPlayer;

            if (this.m_TurnCounter % 2 == 0)
            {   // first players turn
                whichPlayer = this.m_PlayerOne;
            }
            else
            {   // second players turn
                whichPlayer = this.m_PlayerTwo;
            }

            return whichPlayer;
        }

        public void AddNewPlayer(string i_PlayerName, Square.eSquareType playerType)
        {
            if (playerType == Square.eSquareType.playerOne)
            {
                this.m_PlayerOne = new Player();
                this.m_CurrentPlayer = this.m_PlayerOne;
                this.m_PlayerOne.Name = i_PlayerName;
                this.m_PlayerOne.PlayerType = Square.eSquareType.playerOne;
            }
            else
            {
                this.m_PlayerTwo = new Player();
                if(playerType == Square.eSquareType.playerTwo)
                {
                    this.m_PlayerTwo.Name = i_PlayerName;
                }
                else 
                {
                    this.m_PlayerTwo.IsComputer = true;
                    this.m_PlayerTwo.Name = k_PcDeafultNameString;
                }

                this.m_PlayerTwo.PlayerType = playerType;
            }
        }

        public bool EliminateOpponent(Move i_UserInput, Player i_CurrentPlayer)
        {
            Square squareToDelete;
            bool isEliminationSuccessful = false;
            bool needToEliminate = true;
            int opponnentCol = ((i_UserInput.SquareTo.Col - i_UserInput.SquareFrom.Col) / 2) + i_UserInput.SquareFrom.Col;
            int opponnentRow = ((i_UserInput.SquareTo.Row - i_UserInput.SquareFrom.Row) / 2) + +i_UserInput.SquareFrom.Row;

            if(this.m_Board.UpdateBoardAfterMove(i_UserInput, i_CurrentPlayer, needToEliminate))
            {
                Player currentPlayer = null;
                this.m_Board.UpdateBoard(opponnentRow, opponnentCol, Square.eSquareType.none, currentPlayer);
                squareToDelete = new Square(opponnentRow, opponnentCol);

                if (i_CurrentPlayer.PlayerType == Square.eSquareType.playerOne)
                {
                    if (this.m_Board.GetSquareStatus(squareToDelete) == Square.eSquareType.playerOneKing)
                    {
                        this.m_PlayerTwo.Score -= k_KingScoreValue;
                    }
                    else
                    {
                        this.m_PlayerTwo.Score -= k_RegularCheckerScoreValue;
                    }

                    this.m_PlayerTwo.DeleteSquare(squareToDelete);
                }
                else
                {
                    if (this.m_Board.GetSquareStatus(squareToDelete) == Square.eSquareType.playerTwoKing)
                    {
                        this.m_PlayerOne.Score -= k_KingScoreValue;
                    }
                    else
                    {
                        this.m_PlayerOne.Score -= k_RegularCheckerScoreValue;
                    }

                    this.m_PlayerOne.DeleteSquare(squareToDelete);
                }

                isEliminationSuccessful = true;
            }

            return isEliminationSuccessful;
        }

        public eRoundOptions PlayObligatoryMove(Player i_CurrentPlayer, ref Move io_InputMove)
        {
            eRoundOptions obligitoryMoveRes = eRoundOptions.passRound;

            if (i_CurrentPlayer.IsMoveObligatory(io_InputMove))
            {   // Move was one of the obligatory options
                if (this.EliminateOpponent(io_InputMove, i_CurrentPlayer))
                {
                    i_CurrentPlayer.UpdateObligatoryMoves(this.m_Board, io_InputMove.SquareTo);
                    if (i_CurrentPlayer.ObligatoryMovesCount > Move.k_ZeroObligatoryMoves)
                    {
                        obligitoryMoveRes = eRoundOptions.currentPlayerHasAnotherRound;
                    }
                }
                else
                {
                    obligitoryMoveRes = eRoundOptions.playerEnteredInvalidMove;
                }
            }
            else
            {
                obligitoryMoveRes = eRoundOptions.playerDidntEnterObligatoryMove;
            }
            
            return obligitoryMoveRes;
        }

        public Square.eSquareType GetWeakPlayer()
        {
            int weakIndicator = this.m_PlayerOne.Score - this.m_PlayerTwo.Score;
            Square.eSquareType weakRes = Square.eSquareType.none; 

            if (weakIndicator < 0)
            {
                weakRes = Square.eSquareType.playerOne;
            }
            else if (weakIndicator > 0)
            {
                weakRes = Square.eSquareType.playerTwo;
            }

            return weakRes;
        }

        public void CreateGameBoard(int i_BoardSize)
        {
            if(this.m_Board != null)
            {
                this.m_Board.ClearBoard();
            }
            else
            {
                this.m_Board = new Board(i_BoardSize);
            }
            this.m_PlayerOne.InitPlayer(i_BoardSize);
            this.m_PlayerTwo.InitPlayer(i_BoardSize);
            this.m_Board.AddPlayersToBoard(this.m_PlayerOne, this.m_PlayerTwo);
        }

        public eRoundOptions CheckGameStatus(eRoundOptions i_CurrentStatus)
        {
            eRoundOptions gameStatus = i_CurrentStatus;
            this.PlayerOne.UpdateAvailableMovesIndicator(this.m_Board);
            this.PlayerTwo.UpdateAvailableMovesIndicator(this.m_Board);

            if (!this.m_PlayerOne.HasAvailableMove && !this.m_PlayerTwo.HasAvailableMove)
            {
                gameStatus = eRoundOptions.gameIsATie;
            }

            if (this.m_PlayerOne.SquaresNum == 0 || !this.m_PlayerOne.HasAvailableMove) 
            {
                gameStatus = eRoundOptions.playerTwoWon;
            }
            else if (this.m_PlayerTwo.SquaresNum == 0 || !this.m_PlayerTwo.HasAvailableMove) 
            {
                gameStatus = eRoundOptions.playerOneWon;
            }

            return gameStatus;
        }
    }
}
