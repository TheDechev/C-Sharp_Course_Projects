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
            Figure.e_SquareType weakPlayer;
            string previousMove = string.Empty;
            Move inputMove = new Move();

            ConsoleInterface.ClearScreen();
            ConsoleInterface.PrintBoard(m_Board);

            while (!inputMove.Equals(null))
            {
                
                currentPlayer = whichTurn();
                ConsoleInterface.PrintTurn(previousMove, currentPlayer.Name);

                currentPlayer.UpdateObligatoryMoves(m_Board);
                currentPlayer.UpdateAvailableMovesIndicator(m_Board);
                weakPlayer = getWeakPlayer();

                if (currentPlayer.PlayerType != Figure.e_SquareType.playerPC)
                {
                    inputMove = ConsoleInterface.getUserMove(m_Board.Size);
                }
                else
                {
                    Thread.Sleep(1200);
                    inputMove = currentPlayer.ComputerMove(m_Board);
                }

                while (object.ReferenceEquals(inputMove, null) && currentPlayer.PlayerType != Figure.e_SquareType.playerPC)
                {
                    if (currentPlayer.PlayerType == weakPlayer)
                    {

                        //TODO: add to function
                        if (!playAnotherRound())
                        {
                            ConsoleInterface.printEndGame(m_PlayerOne, m_PlayerTwo);
                            return;
                        }
                        ConsoleInterface.ClearScreen();
                        currentPlayer.UpdateObligatoryMoves(m_Board);
                        currentPlayer.UpdateAvailableMovesIndicator(m_Board);
                        previousMove = string.Empty; // new game, not relevant
                        ConsoleInterface.PrintTurn(previousMove, currentPlayer.Name);
                        inputMove = ConsoleInterface.getUserMove(m_Board.Size);
                        break;
                    }
                    else
                    {
                        inputMove = ConsoleInterface.getMoveFromStrongPlayer(m_Board.Size);
                    }
                }



                if (currentPlayer.ObligatoryMovesCount > 0)
                {
                    playObligatoryMove(currentPlayer, ref inputMove);
                }
                else if (currentPlayer.hasAvailableMove)
                {
                    moveWasSuccessful = m_Board.updateBoardAfterMove(inputMove, currentPlayer, false);
                    while (!moveWasSuccessful)
                    {
                        ConsoleInterface.printInvalidMsg();
                        inputMove = ConsoleInterface.getUserMove(m_Board.Size);
                        moveWasSuccessful = m_Board.updateBoardAfterMove(inputMove, currentPlayer, false);
                    }

                }

                ConsoleInterface.ClearScreen();
                ConsoleInterface.PrintBoard(m_Board);

                if (!isRoundOver())
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

            isOver = ConsoleInterface.printEndGame(m_PlayerOne, m_PlayerTwo);

            return isOver;
        }

        public bool playAnotherRound()
        {

            if (ConsoleInterface.playerWantsAnotherRound())
            {
                m_Board = new Board(m_Board.Size);
                m_Board.InitBoard();
                this.m_PlayerTwo.figuresNum = this.m_Board.Size;
                this.m_PlayerTwo.initFigures(this.m_Board.Size);

                this.m_PlayerOne.figuresNum = this.m_Board.Size;
                this.m_PlayerOne.initFigures(this.m_Board.Size);
                this.m_Board.addPlayersToBoard(this.m_PlayerOne, this.m_PlayerTwo);
                ConsoleInterface.PrintBoard(m_Board);
                return true;
            }
            else
            {
                ConsoleInterface.printEndGame(m_PlayerOne,m_PlayerTwo);
                return false;
            }
        }

        public Player whichTurn()
        {
            Player whichPlayer;

            // first players turn
            if (this.m_TurnCounter % 2 == 0)
            {
                whichPlayer = m_PlayerOne;
            }
            // second players turn
            else
            {
                whichPlayer = m_PlayerTwo;
            }

            return whichPlayer;
        }

        public void initGame()
        {

            this.AddNewPlayer(Figure.e_SquareType.playerOne);

            this.m_Board = new Board(ConsoleInterface.getBoardSizeFromUser());

            this.m_PlayerOne.figuresNum = this.m_Board.Size;
            this.m_PlayerOne.initFigures(this.m_Board.Size);
            this.m_PlayerOne.Score = m_PlayerOne.figuresNum;

            int playerChoice = ConsoleInterface.getOpponnetOptions();

            if (playerChoice == 1)
            {
                this.AddNewPlayer(Figure.e_SquareType.playerTwo);
            }
            else
            {
                this.AddNewPlayer(Figure.e_SquareType.playerPC);
            }

            this.m_PlayerTwo.figuresNum = this.m_Board.Size;
            this.m_PlayerTwo.initFigures(this.m_Board.Size);
            this.m_PlayerTwo.Score = m_PlayerTwo.figuresNum;
            this.m_Board.addPlayersToBoard(this.m_PlayerOne, this.m_PlayerTwo);
        }

        public void AddNewPlayer(Figure.e_SquareType playerType)
        {

            string playerName = string.Empty;

            if (playerType != Figure.e_SquareType.playerPC)
            {
                playerName = ConsoleInterface.getPlayerName();
            }

            if (playerType == Figure.e_SquareType.playerOne)
            {
                this.m_PlayerOne = new Player();
                this.m_PlayerOne.Name = playerName;
                this.m_PlayerOne.PlayerType = Figure.e_SquareType.playerOne;
            }
            else
            {
                this.m_PlayerTwo = new Player();
                if(playerType == Figure.e_SquareType.playerTwo)
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
            Figure figureToDelete;
            bool movedSuccesfully;

            int opponnentCol = ((i_UserInput.FigureTo.Col - i_UserInput.FigureFrom.Col) / 2) + i_UserInput.FigureFrom.Col;
            int opponnentRow = ((i_UserInput.FigureTo.Row - i_UserInput.FigureFrom.Row) / 2) + +i_UserInput.FigureFrom.Row;


            movedSuccesfully = m_Board.updateBoardAfterMove(i_UserInput, i_CurrentPlayer, true);


            while (!movedSuccesfully)
            {
                Console.WriteLine("Invalid move, try again . . .");
                i_UserInput = ConsoleInterface.getUserMove(m_Board.Size);
                movedSuccesfully = m_Board.updateBoardAfterMove(i_UserInput, i_CurrentPlayer, true);
                opponnentCol = ((i_UserInput.FigureTo.Col - i_UserInput.FigureFrom.Col) / 2) + i_UserInput.FigureFrom.Col;
                opponnentRow = ((i_UserInput.FigureTo.Row - i_UserInput.FigureFrom.Row) / 2) + +i_UserInput.FigureFrom.Row;

            }


            m_Board.updateBoard(opponnentRow, opponnentCol, Figure.e_SquareType.none);
            figureToDelete = new Figure(opponnentRow, opponnentCol);

            if (i_CurrentPlayer.PlayerType == Figure.e_SquareType.playerOne)
            {
                if (m_Board.getSquareStatus(figureToDelete) == Figure.e_SquareType.playerOneKing)
                {
                    m_PlayerTwo.Score -= 4;
                }
                else
                {
                    m_PlayerTwo.Score -= 1;
                }


                m_PlayerTwo.deleteFigure(figureToDelete);
            }
            else
            {

                if (m_Board.getSquareStatus(figureToDelete) == Figure.e_SquareType.playerTwoKing)
                {
                    m_PlayerOne.Score -= 4;
                }
                else
                {
                    m_PlayerOne.Score -= 1;
                }

                m_PlayerOne.deleteFigure(figureToDelete);
            }




        }

        public void playObligatoryMove(Player i_CurrentPlayer, ref Move io_InputMove)
        {
            while (i_CurrentPlayer.ObligatoryMovesCount != 0)  // Player Must Kill the rival 
            {
                if(i_CurrentPlayer.PlayerType == Figure.e_SquareType.playerPC)
                {
                    io_InputMove = i_CurrentPlayer.RandomObligatoryMove();
                }

                if (i_CurrentPlayer.isMoveObligatory(io_InputMove)) // Move was one of the obligatory options
                {
                    eliminateOpponent(io_InputMove, i_CurrentPlayer);
                    i_CurrentPlayer.UpdateObligatoryMoves(m_Board);
                    if (i_CurrentPlayer.ObligatoryMovesCount > 0)
                    {
                        ConsoleInterface.printAnotherTurn(m_Board, i_CurrentPlayer.Name);

                        if(i_CurrentPlayer.PlayerType == Figure.e_SquareType.playerPC)
                        {
                            io_InputMove = i_CurrentPlayer.RandomObligatoryMove();
                        }
                        else
                        {
                            io_InputMove = ConsoleInterface.getUserMove(m_Board.Size);
                        }
                        Thread.Sleep(1200);
                    }
                }
                else
                {
                    io_InputMove = ConsoleInterface.getObligatoryMove(m_Board.Size);
                }
            }
        }

        //public void printScore()
        //{
        //    Console.WriteLine(string.Concat(Enumerable.Repeat("==", m_Board.Size * 2 + 2)) + Environment.NewLine);
        //    Console.WriteLine(m_PlayerOne.Name + "'s Score is: " + m_PlayerOne.Score);
        //    Console.WriteLine(m_PlayerTwo.Name + "'s Score is: " + m_PlayerTwo.Score + Environment.NewLine);
        //    Console.WriteLine(string.Concat(Enumerable.Repeat("==", m_Board.Size*2 + 2)) + Environment.NewLine + Environment.NewLine);
        //}

        public Figure.e_SquareType getWeakPlayer()
        {
            int weakIndicator = m_PlayerOne.Score - m_PlayerTwo.Score;
            Figure.e_SquareType weakRes = Figure.e_SquareType.none; 

            if (weakIndicator < 0)
            {
                weakRes = Figure.e_SquareType.playerOne;
            }
            else if (weakIndicator > 0)
            {
                weakRes = Figure.e_SquareType.playerTwo;
            }

            return weakRes;

        }
    }
}
