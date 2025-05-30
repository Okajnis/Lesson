using UnityEngine;
using UnityEngine.SceneManagement;

public class CardGameStart : MonoBehaviour
{

    public CardMenu[] cards;
    private int correctCardIndex;

    void Start()
    {
        correctCardIndex = Random.Range(0, cards.Length);
        Debug.Log("Correct card is: " + correctCardIndex);
    }

    public void OnCardClicked(int clickedIndex)
    {
        if (clickedIndex == correctCardIndex)
        {
            Debug.Log("Correct card!");
            cards[clickedIndex].RevealCorrect();
            Invoke("StartMainGame", 1.5f);    // poczekaj chwilê przed zmian¹ sceny
        }
        else
        {
            Debug.Log("Wrong card!");
            cards[clickedIndex].RevealWrong();
        }
    }

    void StartMainGame()
    {
        SceneManager.LoadScene("MainGame");   // wpisz dok³adn¹ nazwê sceny
    }
}


