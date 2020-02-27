using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player1, player2;

    public Pioche_Script pioche;
    public Main_Script main;
    public Defausse_Script defausse;

    public GameObject screenCanvas;
    public Text passButton_txt;

    public Text player1Info;
    public Text player2Info;

    public Text player1CardCount;
    public Text player2CardCount;

    public Board_Script playingBoard, notPlayingBoard;

    public enum Turn { player1Turn, player2Turn};

    public Turn turn = Turn.player1Turn;

    public bool passTurn = false;

    public float timerEndTurn_Start;
    public float timerEndTurn;

    public delegate void NewTurn();
    public static NewTurn newTurn;

    public delegate void newManche();
    public static newManche newmanche;

    private void Start()
    {
        Case_Script.EndTheTurn += EndTurn;
        Main_Script.EndTheTurn += EndTurn;

        Board_Script.boardUpdate += UpdatePlayerInfo;
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
        newmanche();

        player1.asPassed = false;
        player2.asPassed = false;

        player1.point = 0;
        player2.point = 0;

        notPlayingBoard.ClearBoard();
        playingBoard.ClearBoard();

        timerEndTurn = 0f;
        passTurn = true;

        UpdatePlayerInfo();
    }

    public void EndTurn()
    {
        if (player1.asPassed || player2.asPassed)
        {
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
            if (!player1.asPassed)
            {
                SavePlayer(player2);
                PlayerTurn(player1);

                LigneHighlight_Script.activeBoard = 1;
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
            if (!player2.asPassed)
            {
                SavePlayer(player1);
                PlayerTurn(player2);

                LigneHighlight_Script.activeBoard = 2;
                turn = Turn.player1Turn;
            }
            else
            {
                SavePlayer(player1);
                PlayerTurn(player1);
            }
        }
        newTurn();
        screenCanvas.SetActive(false);
        UpdatePlayerCard();
    }

    void SavePlayer(Player player)
    {
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

        //notPlayingBoard.CountPoint();
        //playingBoard.CountPoint();
    }

    void UpdatePlayerInfo()
    {
        if(turn == Turn.player2Turn)
        {
            player1.point = playingBoard.point;
            player2.point = notPlayingBoard.point;
        }
        else
        {
            player2.point = playingBoard.point;
            player1.point = notPlayingBoard.point;
        }

        player1Info.text = " Joueur 1 : Vie : " + player1.life + " / Points: " + player1.point;
        player2Info.text = " Joueur 2 : Vie : " + player2.life + " / Points: " + player2.point;
    }

    void UpdatePlayerCard()
    {
        player1CardCount.text = " / Cartes :" + player1.allCard.Count.ToString();
        player2CardCount.text = " / Cartes :" + player2.allCard.Count.ToString();
    }

    void FinManche()
    {
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

    public void DEBUG_ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
