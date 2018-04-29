﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<Move> obligatoryMoves;
            Console.WriteLine("Welcome to the game! ");
            this.initGame();
            string playerChoice = string.Empty;
            bool playedObligatoryMove = false;
            Player.e_PlayerType currentPlayer;

            while (playerChoice != "Exit")
            {
                Ex02.ConsoleUtils.Screen.Clear(); // clearing the screen for a new round
                this.m_Board.PrintBoard();

                if (this.m_TurnCounter % 2 == 0)
                {// first players turn
                    currentPlayer = Player.e_PlayerType.playerOne;
                    Console.Write(this.m_PlayerOne.Name + "'s turn:");
                    obligatoryMoves = m_PlayerOne.getObligatoryMoves(m_Board);
                }
                else
                {// second players turn
                    currentPlayer = Player.e_PlayerType.playerTwo;
                    Console.Write(this.m_PlayerTwo.Name + "'s turn:");
                    obligatoryMoves = m_PlayerTwo.getObligatoryMoves(m_Board);
                }

                Move inputMove = getUserInput();
                
                if (obligatoryMoves.Count != 0 )  /// Player Must Kill the rival 
                {
                    while (!playedObligatoryMove)
                    {
                        foreach (Move optionaObligatorylMove in obligatoryMoves)
                        {
                            if (optionaObligatorylMove.Equals(inputMove))
                            {
                                eliminateOpponent(inputMove,currentPlayer);
                                playedObligatoryMove = true;
                                break;
                            }
                        }
                        if(!playedObligatoryMove)
                        {
                            Console.WriteLine("Invalid move, you must eliminate your opponnent");
                            inputMove = getUserInput();
                        } 
                    }
                }
                else // Player can move normally
                {
                    m_Board.updateBoardAfterMove(inputMove, currentPlayer);
                    if (currentPlayer == Player.e_PlayerType.playerOne) // player one
                    {
                        m_PlayerOne.UpdateFigure(inputMove);
                    }
                    else
                    {
                        m_PlayerTwo.UpdateFigure(inputMove);
                    }
                }

                playedObligatoryMove = false;
                this.m_TurnCounter++;
            }
        }

        public void initGame()
        {
            string playerChoice;

            this.AddNewPlayer(Player.e_PlayerType.playerOne);

            this.getBoardSizeFromUser();

            this.m_PlayerOne.figuresNum = this.m_Board.Size;
            this.m_PlayerOne.initFigures(Player.e_PlayerType.playerOne, this.m_Board.Size);

            Console.WriteLine("Choose your opponent: ");
            Console.WriteLine("1. Another player ");
            Console.WriteLine("2. The PC ");

            playerChoice = Console.ReadLine();

            while (playerChoice != "1" && playerChoice != "2")
            {
                playerChoice = Console.ReadLine();
            }

            if (playerChoice == "1")
            {
                this.AddNewPlayer(Player.e_PlayerType.playerTwo);
                this.m_PlayerTwo.figuresNum = this.m_Board.Size;
                this.m_PlayerTwo.initFigures(Player.e_PlayerType.playerTwo, this.m_Board.Size);
                this.m_Board.initBoard(this.m_PlayerOne, this.m_PlayerTwo);
            }
            else
            {
                //TODO: implement computer playing;
                Console.WriteLine("The Computer is not ready to play againsnt you yet!");
                return;
            }
        }

        public void AddNewPlayer(Player.e_PlayerType playerType)
        {
            string m_PlayerName = string.Empty;

            if (playerType != Player.e_PlayerType.playerPC)
            {

                Console.WriteLine("Please enter your name: ");
                m_PlayerName = Console.ReadLine();

                while (m_PlayerName.Length > 20 || m_PlayerName.Length == 0)
                {
                    Console.WriteLine("Invalid name size, please enter your name again...");
                    m_PlayerName = Console.ReadLine();
                }
            }

            if (playerType == Player.e_PlayerType.playerOne)
            {
                this.m_PlayerOne = new Player();
                this.m_PlayerOne.Shape = 'X';
                this.m_PlayerOne.Name = m_PlayerName;
                this.m_PlayerOne.PlayerType = Player.e_PlayerType.playerOne;
            }
            else if(playerType == Player.e_PlayerType.playerTwo)
            {
                this.m_PlayerTwo = new Player();
                this.m_PlayerTwo.Shape = 'O';
                this.m_PlayerTwo.Name = m_PlayerName;
                this.m_PlayerTwo.PlayerType = Player.e_PlayerType.playerTwo;
            }
            else
            {//// TODO: PC implemantion 
            }
        }

        public void getBoardSizeFromUser()
        {
            string playerChoice;

            Console.WriteLine("Enter the board size: (6/8/10) ");
            playerChoice = Console.ReadLine();
            while (playerChoice != "6" && playerChoice != "8" && playerChoice != "10")
            {
                Console.WriteLine("Invalid board size, please enter the size again...");
                playerChoice = Console.ReadLine();
            }

            this.m_Board = new Board(int.Parse(playerChoice));
        }

        public Move getUserInput()
        {
            string playerChoice = Console.ReadLine();
            bool isCurrent = true;

            Figure currentFigure = new Figure();
            Figure nextMoveFigure = new Figure();

            currentFigure.updateFigurePosAccordingToPlayerMove(playerChoice, isCurrent);
            nextMoveFigure.updateFigurePosAccordingToPlayerMove(playerChoice, !isCurrent);

            return new Move(currentFigure, nextMoveFigure);
        }

        public void eliminateOpponent(Move i_UserInput, Player.e_PlayerType i_CurrentPlayer)
        {
            ///bool isUpdateSuccesful;
            int opponnentCol = ((i_UserInput.FigureTo.Col - i_UserInput.FigureFrom.Col) / 2) + i_UserInput.FigureFrom.Col;
            int opponnentRow = ((i_UserInput.FigureTo.Row - i_UserInput.FigureFrom.Row) / 2) + +i_UserInput.FigureFrom.Row;

            m_Board.updateBoard(opponnentRow, opponnentCol, Player.e_PlayerType.none);

            if (i_CurrentPlayer == Player.e_PlayerType.playerOne)
            {
                m_PlayerTwo.deleteFigure(new Figure(opponnentRow, opponnentCol));
            }
            else
            {
                m_PlayerOne.deleteFigure(new Figure(opponnentRow, opponnentCol));
            }

            m_Board.updateBoardAfterMove(i_UserInput, i_CurrentPlayer);

        }
    }
}
