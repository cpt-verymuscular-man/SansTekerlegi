using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardButtonScript : MonoBehaviour
{
    public void SetLetter()
    {
        FindObjectOfType<ButtonManager>().currentLetter = GetComponentInChildren<TextMeshProUGUI>().text; 
    }
}
