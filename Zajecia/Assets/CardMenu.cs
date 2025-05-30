using UnityEngine;
using UnityEngine.UI;

public class CardMenu : MonoBehaviour
{
    public int cardIndex;
    public MeshRenderer meshRenderer;
    public Material defaultMaterial;
    public Material wrongMaterial;
    public Material correctMaterial;

    private bool isRevealed = false;

    private void OnMouse()
    {
        
        {
            mouseButton();
        }
    }


    public void RevealWrong()
    {
        if (isRevealed) return;
        isRevealed = true;
        meshRenderer.material = wrongMaterial;
    }
    
    public void RevealCorrect()
    {
        if (isRevealed) return;
        isRevealed = true;
        meshRenderer.material = correctMaterial;   // ustaw materia³ dla poprawnej karty
    }

    public void ResetCard()
    {
        isRevealed = false;
        meshRenderer.material = defaultMaterial;   // resetuj do domyœlnego wygl¹du
    }
}