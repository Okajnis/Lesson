using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;

public class DrawCard : MonoBehaviour
{
    [SerializeField] private List<CardDataSO> cardsList; //lista z danymi kart
    [SerializeField] private List<CardDataSO> enemyCardsList; //lista dla enemy
    [SerializeField] private List<CardData> cardGameObjects; //lista obiektów na scenie
    [SerializeField] private List<CardData> enemyCardGameObjects;
    [SerializeField] private TextMeshProUGUI cardNameText; //lista do tekstów
    [SerializeField] private TextMeshProUGUI enemyCardNameText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private int playerPoints = 52;
    [SerializeField] private int enemyPoints = 52;
     
    private List<CardDataSO> remainingCardsList; //lista kart
    private List<CardDataSO> remainingEnemyCardsList;
    private CardData lastHighlightedCard; //ostatnia karta
    private CardData lastHighlightedEnemyCard;

    private void Awake() //awake - odpala sie zanim progam to zrobi
    {
        remainingCardsList = new List<CardDataSO>(cardsList); //tworzy now¹ liste o typie CardDataSO (kopiuje kart listy)
        remainingEnemyCardsList = new List<CardDataSO>(enemyCardsList);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //wciœniêcie spacji
        {
            ShuffleCard();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            {
            Surrender();
            }
    }

    private void ShuffleCard()
    {

        if (remainingCardsList.Count == 0||remainingEnemyCardsList.Count == 0) //jeœli lista wyniesie 0
        {
            return; //zakoñczenie funkcji - return
        }

        int randomIndex = Random.Range(0, remainingCardsList.Count); //random range - liczba losowa od 0 do iloœci liczb w liœcie
        CardDataSO selectedCard = remainingCardsList[randomIndex]; //wybiera siê karte losow¹ kartê o indeksie randomIndex
        playerRenderer.material = selectedCard.cardmat; //ustawia materia³ dla kart
        remainingCardsList.RemoveAt(randomIndex); //usuwamy kartê, któr¹ wybraliœmy

        int computerIndex = Random.Range(0, remainingEnemyCardsList.Count); //random range - liczba losowa od 0 do iloœci liczb w liœcie
        CardDataSO selectedEnemyCard = remainingEnemyCardsList[computerIndex]; //wybiera siê karte losow¹ kartê o indeksie randomIndex
        enemyRenderer.material = selectedEnemyCard.cardmat; //ustawia materia³ dla karty
        remainingEnemyCardsList.RemoveAt(computerIndex); //usuwamy kartê, któr¹ wybraliœmy

        DisplayCardName(selectedCard); //wyœwietl nazwê karty, któr¹ wybraliœmy
        SelectCardObject(selectedCard); //wybraæ obiekt karty, któr¹ wybraliœmy
        SelectEnemyCardObject(selectedEnemyCard);
        CompareCards(selectedCard, selectedEnemyCard);

        if (selectedCard.cardName == "aspik") //jeœli gracz wybierze as pik - punkty zeruje
        {
            playerPoints = 0;
            Debug.Log("Gracz trafi³ na Asa! - punkty wyzerowane!");
        }
        else if (selectedEnemyCard.cardName == "aspik")
        {
            enemyPoints = 0;
            Debug.Log("Komputer trafi³ na Asa Pik! - punkty wyzerowane!");
        }


        bool playerDouble = selectedCard.cardName == "krolkaro"; //bool to minimalna wartoœæ (prawda fa³sz)
        bool enemyDouble = selectedEnemyCard.cardName == "krolkaro";
        int karoPoints = 5;



        if (playerDouble) //jeœli gracz ma krolkaro - dostaje + 5 pkt.
        {
            playerPoints += karoPoints; //staraæ siê liczby pakowaæ w funkcje int np 
        }
        else if (enemyDouble)
        {
            enemyPoints += karoPoints;
        }
    }

    public void Surrender()
    {
        resultText.text = "Podda³eœ siê! Przegra³eœ!";
        enabled = false; //wy³¹cza update i wszystko
    }

    private void DisplayCardName(CardDataSO card) 
    {
        cardNameText.text = card.cardName; //w tekœcie wyœwietla siê jej wartoœæ na cardname wybrany
    }
    private void CompareCards(CardDataSO player, CardDataSO enemy)
    {
        string enemyInfo = enemy.cardName + " Damage Komputera: " + enemy.damage;


        if (player.damage > enemy.damage)
        {
            enemyCardNameText.text = enemyInfo;

            resultText.text = " Wygra³ Gracz ";

            remainingCardsList.Add(enemy);
        }
        else if (player.damage < enemy.damage)
        {
            enemyCardNameText.text = enemyInfo;

            resultText.text = " Wygra³ Przeciwnik";

            remainingEnemyCardsList.Add(player);
        }
        else
        {
            enemyCardNameText.text = enemyInfo;

            resultText.text = " Remis"; 
        }
    }
    private void SelectCardObject(CardDataSO selectedCardSO)
    {
        if (lastHighlightedCard != null) //jeœli siê nie równa nic 
        {
            lastHighlightedCard.UnSelectedCard(); //zwiêksza/zmniejsza rozmiar w CardData
        }

        foreach (CardData cardObj in cardGameObjects)
        {
            if (cardObj.GetCardData() == selectedCardSO)
            {
                cardObj.SelectedCard();
                lastHighlightedCard = cardObj;
                break;
            }
        }
    }
    private void SelectEnemyCardObject(CardDataSO selectedCardSO)
    {
        if (lastHighlightedEnemyCard != null) //jeœli siê nie równa nic 
        {
            lastHighlightedEnemyCard.UnSelectedCard(); //zwiêksza/zmniejsza rozmiar w CardData
        }

        foreach (CardData cardObj in enemyCardGameObjects)
        {
            if (cardObj.GetCardData() == selectedCardSO)
            {
                cardObj.SelectedCard();
                lastHighlightedEnemyCard = cardObj;
                break;
            }
        }
    }

}
