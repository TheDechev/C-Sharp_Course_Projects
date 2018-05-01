using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace B18_Ex02
{
    public class Game
    {
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Board m_Board;
        private int m_TurnCounter = 0;

        public void StartGame()
        {
            ConsoleInterface.printWelcomeMsg();
            this.initGame();
            string playerChoice = string.Empty;
            bool moveWasSuccessful = true;
            Player currentPlayer;
            Square.e_SquareType weakPlayer;
            string previousMove = string.Empty;
            Move inputMove = new Move();

            ConsoleInterface.ClearScreen();
            ConsoleInterface.PrintBoard(this.m_Board);

            while (!inputMove.Equals(null))
            {
                currentPlayer = this.whichTurn();
                ConsoleInterface.PrintTurn(previousMove, currentPlayer.Name);

                currentPlayer.UpdateObligatoryMoves(this.m_Board);
                currentPlayer.UpdateAvailableMovesIndicator(this.m_Board);
                weakPlayer = this.getWeakPlayer();

                if (currentPlayer.PlayerType != Square.e_SquareType.playerPC)
                {
                    inputMove = ConsoleInterface.getUserMove(this.m_Board.Size);
                }
                else
                {
                    Thread.Sleep(1200);
                    inputMove = currentPlayer.ComputerMove(this.m_Board);
                }

                while (object.ReferenceEquals(inputMove, null) && currentPlayer.PlayerType != Square.e_SquareType.playerPC)
                {
                    if (currentPlayer.PlayerType == weakPlayer)
                    {
                        //TODO: add to function
                        if (!this.playAnotherRound())
                        {
                            ConsoleInterface.printEndGame(this.m_PlayerOne, this.m_PlayerTwo);
                            return;
                        }

                        ConsoleInterface.ClearScreen();
                        currentPlayer.UpdateObligatoryMoves(this.m_Board);
                        currentPlayer.UpdateAvailableMovesIndicator(this.m_Board);
                        previousMove = string.Empty; // new game, not relevant
                        ConsoleInterface.PrintTurn(previousMove, currentPlayer.Name);
                        inputMove = ConsoleInterface.getUserMove(this.m_Board.Size);
                        break;
                    }
                    else
                    {
                        inputMove = ConsoleInterface.getMoveFromStrongPlayer(this.m_Board.Size);
                    }
                }

                if (currentPlayer.ObligatoryMovesCount > 0)
                {
                    this.playObligatoryMove(currentPlayer, ref inputMove);
                }
                else if (currentPlayer.hasAvailableMove)
                {
                    moveWasSuccessful = this.m_Board.updateBoardAfterMove(inputMove, currentPlayer, false);
                    while (!moveWasSuccessful)
                    {
                        ConsoleInterface.printInvalidMsg();
                        inputMove = ConsoleInterface.getUserMove(this.m_Board.Size);
                        moveWasSuccessful = this.m_Board.updateBoardAfterMove(inputMove, currentPlayer, false);
                    }
                }

                ConsoleInterface.ClearScreen();
                ConsoleInterface.PrintBoard(this.m_Board);

                if (!this.isRoundOver())
                {
                    break;
                }

                previousMove = currentPlayer.Name + "'s move was: " + inputMove.ToString();
                this.m_TurnCounter++;
            }
        }

        public bool isRoundOver()
        {
            bool isOver = true;

            isOver = ConsoleInterface.printEndGame(this.m_PlayerOne, this.m_PlayerTwo);

            return isOver;
        }

        public bool playAnotherRound()
        {
            if (ConsoleInterface.playerWantsAnotherRound())
            {
                this.m_Board = new Board(this.m_Board.Size);
                this.m_Board.InitBoard();
                this.m_PlayerTwo.squaresNum = this.m_Board.Size;
                this.m_PlayerTwo.initSquares(this.m_Board.Size);
                this.m_PlayerOne.squaresNum = this.m_Board.Size;
                this.m_PlayerOne.initSquares(this.m_Board.Size);
                this.m_Board.addPlayersToBoard(this.m_PlayerOne, this.m_PlayerTwo);
                ConsoleInterface.PrintBoard(this.m_Board);
                return true;
            }
            else
            {
                ConsoleInterface.printEndGame(this.m_PlayerOne, this.m_PlayerTwo);
                return false;
            }
        }

        public Player whichTurn()
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

        public void initGame()
        {
            this.AddNewPlayer(Square.e_SquareType.playerOne);
            this.m_Board = new Board(ConsoleInterface.getBoardSizeFromUser());
            this.m_PlayerOne.squaresNum = this.m_Board.Size;
            this.m_PlayerOne.initSquares(this.m_Board.Size);
            this.m_PlayerOne.Score = this.m_PlayerOne.squaresNum;
            int playerChoice = ConsoleInterface.getOpponnetOptions();

            if (playerChoice == 1)
            {
                this.AddNewPlayer(Square.e_SquareType.playerTwo);
            }
            else
            {
                this.AddNewPlayer(Square.e_SquareType.playerPC);
            }

            this.m_PlayerTwo.squaresNum = this.m_Board.Size;
            this.m_PlayerTwo.initSquares(this.m_Board.Size);
            this.m_PlayerTwo.Score = this.m_PlayerTwo.squaresNum;
            this.m_Board.addPlayersToBoard(this.m_PlayerOne, this.m_PlayerTwo);
        }

        public void AddNewPlayer(Square.e_SquareType playerType)
        {
            string playerName = string.Empty;

            if (playerType != Square.e_SquareType.playerPC)
            {
                playerName = ConsoleInterface.getPlayerName();
            }

            if (playerType == Square.e_SquareType.playerOne)
            {
                this.m_PlayerOne = new Player();
                this.m_PlayerOne.Name = playerName;
                this.m_PlayerOne.PlayerType = Square.e_SquareType.playerOne;
            }
            else
            {
                this.m_PlayerTwo = new Player();
                if(playerType == Square.e_SquareType.playerTwo)
                {
                    this.m_PlayerTwo.Name = playerName;
                }
                else 
                {
                    this.m_PlayerTwo.Name = "Computer";
                }

                this.m_PlayerTwo.PlayerType = playerType;
            }
        }

        public void eliminateOpponent(Move i_UserInput, Player i_CurrentPlayer)
        {
            Square squareToDelete;
            bool movedSuccesfully;
            int opponnentCol = ((i_UserInput.SquareTo.Col - i_UserInput.SquareFrom.Col) / 2) + i_UserInput.SquareFrom.Col;
            int opponnentRow = ((i_UserInput.SquareTo.Row - i_UserInput.SquareFrom.Row) / 2) + +i_UserInput.SquareFrom.Row;

            movedSuccesfully = this.m_Board.updateBoardAfterMove(i_UserInput, i_CurrentPlayer, true);

            while (!movedSuccesfully)
            {
                Console.WriteLine("Invalid move, try again . . .");
                i_UserInput = ConsoleInterface.getUserMove(this.m_Board.Size);
                movedSuccesfully = this.m_Board.updateBoardAfterMove(i_UserInput, i_CurrentPlayer, true);
                opponnentCol = ((i_UserInput.SquareTo.Col - i_UserInput.SquareFrom.Col) / 2) + i_UserInput.SquareFrom.Col;
                opponnentRow = ((i_UserInput.SquareTo.Row - i_UserInput.SquareFrom.Row) / 2) + +i_UserInput.SquareFrom.Row;
            }

            this.m_Board.updateBoard(opponnentRow, opponnentCol, Square.e_SquareType.none);
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
        }

        public void playObligatoryMove(Player i_CurrentPlayer, ref Move io_InputMove)
        {
            while (i_CurrentPlayer.ObligatoryMovesCount != 0)  // Player Must Kill the rival 
            {
                if(i_CurrentPlayer.PlayerType == Square.e_SquareType.playerPC)
                {
                    io_InputMove = i_CurrentPlayer.RandomObligatoryMove();
                }

                if (i_CurrentPlayer.isMoveObligatory(io_InputMove)) // Move was one of the obligatory options
                {
                    this.eliminateOpponent(io_InputMove, i_CurrentPlayer);
                    i_CurrentPlayer.UpdateObligatoryMoves(this.m_Board);
                    if (i_CurrentPlayer.ObligatoryMovesCount > 0)
                    {
                        ConsoleInterface.printAnotherTurn(this.m_Board, i_CurrentPlayer.Name);

                        if(i_CurrentPlayer.PlayerType == Square.e_SquareType.playerPC)
                        {
                            io_InputMove = i_CurrentPlayer.RandomObligatoryMove();
                        }
                        else
                        {
                            io_InputMove = ConsoleInterface.getUserMove(this.m_Board.Size);
                        }

                        Thread.Sleep(1200);
                    }
                }
                else
                {
                    io_InputMove = ConsoleInterface.getObligatoryMove(this.m_Board.Size);
                }
            }
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
    }
}
