using UnityEngine;

public class CardData : MonoBehaviour
{
    [SerializeField] private CardDataSO _cardDataSO;
    // Start is called once

    public CardDataSO GetCardData()
    {
        return _cardDataSO;
    }

    public void SelectedCard()
    {
        transform.localScale = Vector3.one * 1.2f;
    }

    public void UnSelectedCard()
    {
        transform.localScale = Vector3.one;
    }
}