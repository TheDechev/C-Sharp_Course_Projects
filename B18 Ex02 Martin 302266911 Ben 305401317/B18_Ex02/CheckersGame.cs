using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B18_Ex02
{
    public class CheckersGame
    {
        public enum e_RoundOptions
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
        private Board m_Board;
        private int m_TurnCounter = 0;

       
        public Player PlayerOne
        {
            get
            {
                return m_PlayerOne;
            }
        }

        public Player PlayerTwo
        {
            get
            {
                return m_PlayerTwo;
            }
        }

        public Board Board{
            get
            {
                return m_Board;
            }
        }

        public e_RoundOptions newRound(string i_UserMove)
        {
            Player currentPlayer;
            Move inputMove = Move.Parse(i_UserMove);
            Square.e_SquareType weakPlayer;
            bool moveWasSuccessful;
            e_RoundOptions roundStatus = e_RoundOptions.passRound;

            currentPlayer = this.getCurrentPlayer();
            currentPlayer.UpdateObligatoryMoves(this.m_Board);
            currentPlayer.UpdateAvailableMovesIndicator(this.m_Board);
            weakPlayer = this.getWeakPlayer();

            inputMove = Move.Parse(i_UserMove);

            if (i_UserMove == "Q" || i_UserMove == "q")
            {
                if (currentPlayer.PlayerType == weakPlayer)
                {
                    updateScore(currentPlayer.PlayerType);
                    roundStatus = e_RoundOptions.weakPlayerQuits;
                }
                else
                {
                    m_TurnCounter--;
                    roundStatus = e_RoundOptions.strongPlayerWantsToQuit;
                }
            } 
            else if (currentPlayer.ObligatoryMovesCount > 0)
            {
                roundStatus = this.playObligatoryMove(currentPlayer, ref inputMove);
                if(roundStatus != e_RoundOptions.passRound)
                {
                    m_TurnCounter--;
                }
            }
            else if (currentPlayer.hasAvailableMove)
            {
                moveWasSuccessful = this.m_Board.updateBoardAfterMove(inputMove, currentPlayer, false);
                if (!moveWasSuccessful)
                {
                    roundStatus = e_RoundOptions.playerEnteredInvalidMove;
                    m_TurnCounter--;
                }

            } else
            {
                roundStatus = e_RoundOptions.passRound;
            }

            this.m_TurnCounter++;

            return roundStatus;
    }

        private void updateScore(Square.e_SquareType i_WinningPlayer)
        {
            if(i_WinningPlayer == Square.e_SquareType.playerOne)
            {
                m_PlayerOne.BonusScore += m_PlayerOne.Score;

            } else
            {
                m_PlayerTwo.BonusScore += m_PlayerTwo.Score;
            }
        }

        public Player getCurrentPlayer()
        {
            Player whichPlayer;

            // first players turn
            if (this.m_TurnCounter % 2 == 0)
            {
                whichPlayer = this.m_PlayerOne;
            }
            // second players turn
            else
            {
                whichPlayer = this.m_PlayerTwo;
            }

            return whichPlayer;
        }

        public void AddNewPlayer(string i_PlayerName, Square.e_SquareType playerType)
        {
            
            if (playerType == Square.e_SquareType.playerOne)
            {
                this.m_PlayerOne = new Player();
                this.m_PlayerOne.Name = i_PlayerName;
                this.m_PlayerOne.PlayerType = Square.e_SquareType.playerOne;
            }
            else
            {
                this.m_PlayerTwo = new Player();
                if(playerType == Square.e_SquareType.playerTwo)
                {
                    this.m_PlayerTwo.Name = i_PlayerName;
                }
                else 
                {
                    this.m_PlayerTwo.Name = "Computer";
                }

                this.m_PlayerTwo.PlayerType = playerType;
            }
        }

        public bool eliminateOpponent(Move i_UserInput, Player i_CurrentPlayer)
        {
            Square squareToDelete;
            bool isEliminationSuccessful = false;
            int opponnentCol = ((i_UserInput.SquareTo.Col - i_UserInput.SquareFrom.Col) / 2) + i_UserInput.SquareFrom.Col;
            int opponnentRow = ((i_UserInput.SquareTo.Row - i_UserInput.SquareFrom.Row) / 2) + +i_UserInput.SquareFrom.Row;

            if(this.m_Board.updateBoardAfterMove(i_UserInput, i_CurrentPlayer, true))
            {
                this.m_Board.updateBoard(opponnentRow, opponnentCol, Square.e_SquareType.none,null);
                squareToDelete = new Square(opponnentRow, opponnentCol);

                if (i_CurrentPlayer.PlayerType == Square.e_SquareType.playerOne)
                {
                    if (this.m_Board.getSquareStatus(squareToDelete) == Square.e_SquareType.playerOneKing)
                    {
                        this.m_PlayerTwo.Score -= 4;
                    }
                    else
                    {
                        this.m_PlayerTwo.Score -= 1;
                    }

                    this.m_PlayerTwo.deleteSquare(squareToDelete);
                }
                else
                {
                    if (this.m_Board.getSquareStatus(squareToDelete) == Square.e_SquareType.playerTwoKing)
                    {
                        this.m_PlayerOne.Score -= 4;
                    }
                    else
                    {
                        this.m_PlayerOne.Score -= 1;
                    }

                    this.m_PlayerOne.deleteSquare(squareToDelete);
                }

                isEliminationSuccessful = true;
            }

            return isEliminationSuccessful;

        }

        public e_RoundOptions playObligatoryMove(Player i_CurrentPlayer, ref Move io_InputMove)
        {
            e_RoundOptions obligitoryMoveRes = e_RoundOptions.passRound;

            if (i_CurrentPlayer.isMoveObligatory(io_InputMove)) // Move was one of the obligatory options
            {
                if(this.eliminateOpponent(io_InputMove, i_CurrentPlayer))
                {
                    i_CurrentPlayer.UpdateObligatoryMoves(this.m_Board);
                    if (i_CurrentPlayer.ObligatoryMovesCount > 0)
                    {
                        obligitoryMoveRes = e_RoundOptions.currentPlayerHasAnotherRound;
                    }
                }
                else
                {
                    obligitoryMoveRes = e_RoundOptions.playerEnteredInvalidMove;
                }

            }
            else
            {
                obligitoryMoveRes = e_RoundOptions.playerDidntEnterObligatoryMove;
            }
            

            return obligitoryMoveRes;
        }

        public Square.e_SquareType getWeakPlayer()
        {
            int weakIndicator = this.m_PlayerOne.Score - this.m_PlayerTwo.Score;
            Square.e_SquareType weakRes = Square.e_SquareType.none; 

            if (weakIndicator < 0)
            {
                weakRes = Square.e_SquareType.playerOne;
            }
            else if (weakIndicator > 0)
            {
                weakRes = Square.e_SquareType.playerTwo;
            }

            return weakRes;
        }

        public void createGameBoard(int i_BoardSize)
        {
            this.m_Board = new Board(i_BoardSize);
            this.m_PlayerOne.initPlayer(i_BoardSize);
            this.m_PlayerTwo.initPlayer(i_BoardSize);
            this.m_Board.addPlayersToBoard(this.m_PlayerOne, this.m_PlayerTwo);
        }

        public e_RoundOptions checkGameStatus()
        {
            e_RoundOptions gameStatus = e_RoundOptions.passRound;
            if(!m_PlayerOne.hasAvailableMove && !m_PlayerTwo.hasAvailableMove)
            {
                gameStatus = e_RoundOptions.gameIsATie;
            }
            if (m_PlayerOne.squaresNum == 0 || !m_PlayerOne.hasAvailableMove) 
            {
                gameStatus = e_RoundOptions.playerTwoWon;
            }
            else if (m_PlayerTwo.squaresNum == 0 || !m_PlayerTwo.hasAvailableMove) 
            {
                gameStatus = e_RoundOptions.playerOneWon;
            }

            return gameStatus;
        }

    }
}
