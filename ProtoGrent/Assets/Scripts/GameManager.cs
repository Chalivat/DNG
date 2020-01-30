using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player1, player2;

    public Pioche_Script pioche;
    public Main_Script main;
    public Defausse_Script defausse;

    public GameObject screenCanvas;
    public Text passButton_txt;

    public Board_Script playingBoard, notPlayingBoard;

    public enum Turn { player1Turn, player2Turn};

    public Turn turn = Turn.player1Turn;

    public bool passTurn = false;

    public float timerEndTurn_Start;
    public float timerEndTurn;

    private void Start()
    {
        Case_Script.EndTheTurn += EndTurn;
        Main_Script.EndTheTurn += EndTurn;
    }

    private void Update()
    {
        if (passTurn)
        {
            timerEndTurn -= Time.deltaTime;
            if (timerEndTurn <= 0)
            {
                if (turn == Turn.player1Turn)
                    if(!player1.asPassed)
                    passButton_txt.text = "BEGIN Tour Player 1";
                else
                        passButton_txt.text = "BEGIN Tour Player 2";
                else
                    if(!player2.asPassed)
                    passButton_txt.text = "BEGIN Tour Player 2";
                else
                    passButton_txt.text = "BEGIN Tour Player 1";

                screenCanvas.SetActive(true);
                passTurn = false;
            }
        }
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

        timerEndTurn = 0f;
        passTurn = true;
    }

    public void EndTurn()
    {
        if (player1.asPassed || player2.asPassed)
        {
            Debug.Log("canplaceCard");
            NextTurn();
        }
        else
        {
            timerEndTurn = timerEndTurn_Start;
            passTurn = true;
        }
    }

    public void NextTurn()
    {
        main.canPlaceCard = true;

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

        screenCanvas.SetActive(false);
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

    public void PassTurn()
    {
        if(turn == Turn.player2Turn)
        {
            player1.asPassed = true;
        }
        else
        {
            player2.asPassed = true;
        }
        timerEndTurn = 0f;
        passTurn = true;
    }

    void EchangeBoard()
    {
        Card[,] tmp_Board = notPlayingBoard.GetAllCard();

        notPlayingBoard.SetCardOnBoard(playingBoard.GetAllCard());
        playingBoard.SetCardOnBoard(tmp_Board);

        bool[,] tmp_Bool = notPlayingBoard.allEncouragement;

        notPlayingBoard.SetEncrougement(playingBoard.allEncouragement);
        playingBoard.SetEncrougement(tmp_Bool);

        Transform[,] tmp_UnitsParent = notPlayingBoard.GetAllUnitsParent();

        notPlayingBoard.SetAllUnitPosition(playingBoard.GetAllUnitsParent());
        playingBoard.SetAllUnitPosition(tmp_UnitsParent);

        notPlayingBoard.UpdateBoardPoint();
        playingBoard.UpdateBoardPoint();

        notPlayingBoard.CountPoint();
        playingBoard.CountPoint();
    }

    void FinManche()
    {
        Debug.Log("FIN MANCHE");

        defausse.AddBoardCardToDefausse();

        if(player1.point > player2.point)
        {
            Debug.Log("MANCHE POUR JOUEUR 1");
            player2.life--;
            if(player2.life <= 0)
            {
                Debug.Log("VICTOIR JOUEUR 1");
            }
            else
            {
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
                turn = Turn.player2Turn;
                NewManche();
            }
        }
    }
}
