using UnityEngine;

[CreateAssetMenu(fileName = "MenuSO", menuName = "Scriptable Objects/MenuSO")]
public class MenuSO : ScriptableObject
{
    public string cardName;
    public Material cardMat;


    public void SayHello()
    {
        Debug.Log(cardName);
    }
}