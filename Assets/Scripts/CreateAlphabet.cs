using UnityEngine;
using TMPro;

public class CreateAlphabet : MonoBehaviour
{
    public GameObject ButtonPrefab; //keys of keyboard
    public Transform ButtonParent; //layout object for keyboard
    public string Alphabet = "QWERTYUIOPASDFGHJKLZXCVBNM"; //each letter in alphabet

    void Start()
    {
        for (int i = 0; i < Alphabet.Length; i++)
        {
            //instantiate button
            GameObject newButton = Instantiate(ButtonPrefab, transform.position, Quaternion.identity, ButtonParent);

            //get the text object and change it
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = Alphabet[i].ToString();

        }
    }
}
