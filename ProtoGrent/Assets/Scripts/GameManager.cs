using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player1, player2;

    public Pioche_Script pioche;
    public Main_Script main;
    public Defausse_Script defausse;

    public Board_Script playingBoard, notPlayingBoard;

    public enum Turn { player1Turn, player2Turn};

    public Turn turn = Turn.player1Turn;

    private void Start()
    {
        Case_Script.EndTheTurn += NextTurn;
        Main_Script.EndTheTurn += NextTurn;
    }

    public void BeginMatch()
    {
        Debug.Log("BEGIN MATCH !!!");

        player1.SetAllCard(pioche.SimplyAddCardsToPlayer(10));
        player2.SetAllCard(pioche.SimplyAddCardsToPlayer(10));

        NewManche();
    }

    void NewManche()
    {
        Debug.Log("NEW MANCHE");

        player1.asPassed = false;
        player2.asPassed = false;

        player1.point = 0;
        player2.point = 0;

        notPlayingBoard.ClearBoard();
        playingBoard.ClearBoard();

        NextTurn();
    }

    public void NextTurn()
    {
        playingBoard.UpdateBoardCases();
        notPlayingBoard.UpdateBoardCases();

        if(player1.asPassed && player2.asPassed)
        {
            FinManche();
        }
        else if(turn == Turn.player1Turn)
        {
            Debug.Log("TURN J1");
            if (!player1.asPassed)
            {
                SavePlayer(player2);
                PlayerTurn(player1);

                EchangeBoard();

                turn = Turn.player2Turn;
            }
            else
            {
                SavePlayer(player2);
                PlayerTurn(player2);
            }
        }
        else
        {
            Debug.Log("TURN J2");
            if (!player2.asPassed)
            {
                SavePlayer(player1);
                PlayerTurn(player2);

                EchangeBoard();

                turn = Turn.player1Turn;
            }
            else
            {
                SavePlayer(player1);
                PlayerTurn(player1);
            }
        }
    }

    void SavePlayer(Player player)
    {
        player.point = player.playingBoard.point;

        List<Card> cards = new List<Card>();

        List<GameObject> allCards = new List<GameObject>();
        allCards = main.carteMain;

        foreach (GameObject item in allCards)
        {
            cards.Add(item.GetComponent<Card_Script>().card);
        }

        if (allCards.Count > 0)
        {
            player.SetAllCard(cards);
        }
    }

    void PlayerTurn(Player player)
    {
        Debug.Log(player.name + " TURN");

        main.UpdateCardOnMain(player.allCard);
    }

    void EchangeBoard()
    {
        Card[,] tmp_Board = notPlayingBoard.GetAllCard();

        notPlayingBoard.SetCardOnBoard(playingBoard.GetAllCard());
        playingBoard.SetCardOnBoard(tmp_Board);

        bool[,] tmp_Bool = notPlayingBoard.allEncouragement;

        notPlayingBoard.SetEncrougement(playingBoard.allEncouragement);
        playingBoard.SetEncrougement(tmp_Bool);
    }

    void FinManche()
    {
        Debug.Log("FIN MANCHE");

        defausse.AddBoardCardToDefausse();

        if(player1.point > player2.point)
        {
            Debug.Log("MANCHE POUR JOEUR 1");
            player2.life--;
            if(player2.life <= 0)
            {
                Debug.Log("VICTOIR JOUEUR 1");
            }
            else
            {
                Debug.Log("coucou");
                turn = Turn.player1Turn;
                NewManche();
            }
        }
        else
        {
            Debug.Log("MANCHE POUR JOEUR 2");
            player1.life--;
            if (player1.life <= 0)
            {
                Debug.Log("VICTOIR JOUEUR 2");
            }
            else
            {
                Debug.Log("coucou2");
                turn = Turn.player2Turn;
                NewManche();
            }
        }
    }
}
