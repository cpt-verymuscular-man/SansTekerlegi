using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public AudioSource myAudioSource;
    public AudioClip[] audioClips; //0 = found a letter, 1 = no letters found

    public GameObject buttonPrefab; //literally the only prefab we'll be using, name is self explanatory

    public GameObject wrongAnswerImage; //shown when you enter a letter and it isn't in the answer

    public GameObject youWinPanel; //gameover panel

    public Transform buttonParent; //the parent object that we'll spawn prefabs in (button_layout)

    public TextMeshProUGUI questionText; //text object for question

    public TextMeshProUGUI totalPointsText; //text object for points

    public TextMeshProUGUI finalScoreText; //score in the you win panel

    int questionIndex; //which question we are at?

    public string[] questions; //questions

    public string[] answer; //the answer

    public int totalIndex; //hangi gruba gelicek
    public int[] totalPoints; //toplam puan
    public TextMeshProUGUI[] totalPointTexts;

    public int currentPoint; //carktan gelen puan

    public string currentLetter; //currently seletected letter in keyboard;

    public List<TextMeshProUGUI> answerButtonTextList; //text objects of answer buttons, we'll use this when we select a letter as answer
    public List<Button> answerButtonsList; //text objects of answer buttons, we'll use this when we select a letter as answer




    private void Start()
    {
        answerButtonTextList = new List<TextMeshProUGUI>();
        answerButtonsList = new List<Button>();
    }

    public void NextQuestion()
    {
        if (questionIndex == questions.Length)
        {
            youWinPanel.SetActive(true);
            finalScoreText.text = 
                  "Group1: " + totalPoints[0] + "\n" 
                + "Group2: " + totalPoints[1] + "\n"
                + "Group3: " + totalPoints[2] + "\n"
                + "Group4: " + totalPoints[3];
        }
        else
        {
            questionText.text = questions[questionIndex]; //we change the question text

            answerButtonTextList.Clear(); //we clear the list before we add new stuff
            answerButtonsList.Clear(); //same as above

            foreach (Transform child in buttonParent.GetComponentInChildren<Transform>())
            {
                Destroy(child.gameObject);
            }

            CreateAnswer(); //create answer buttons

            questionIndex++; //we will be at next question when we trigger this again
        }
    }


    public void CreateAnswer() //makes buttons for the answer
    {
        for (int i = 0; i < answer[questionIndex].Length; i++) //for each letter in the answer
        {
            //we make a button and keep the value
            GameObject newButton = Instantiate(buttonPrefab, transform.position, Quaternion.identity, buttonParent);

            //we get the text object
            TextMeshProUGUI buttonLetter = newButton.GetComponentInChildren<TextMeshProUGUI>();

            //we change the text
            buttonLetter.text = answer[questionIndex][i].ToString();

            //we add the objects into arrays
            answerButtonTextList.Add(buttonLetter);
            answerButtonsList.Add(newButton.GetComponent<Button>());
        }
    }

    public void AddPoints(int point)
    {
        totalPoints[totalIndex] += point;
    }

    public void ButtonCheckLetter() //I don't know I just can't put the CheckLetter on button click for some reason
    {
        StartCoroutine(CheckLetter());
    }

    public void ButtonCorrectAnswer() //same as above
    {
        StartCoroutine(CorrectAnswer());
    }

    public void MyQuit()
    {
        Application.Quit();
    }

    public void UpdatePoints()
    {
        for (int i = 0; i < totalPointTexts.Length; i++)
        {
            totalPointTexts[i].text = "Group" + (i+1) + ": " + totalPoints[i];
            if(totalIndex == i)
            {
                totalPointTexts[i].fontStyle = FontStyles.Bold;
                totalPointTexts[i].color = Color.red;
            }
            else
            {
                totalPointTexts[i].fontStyle = FontStyles.Normal;
                totalPointTexts[i].color = Color.white;
            }
        }
    }


    //kazandigin puanlari sag ustte goster
    //birden fazla soru yap
    //son soruya gelince tebrikler goster

    IEnumerator WrongAnswer()
    {
        wrongAnswerImage.SetActive(true);
        myAudioSource.PlayOneShot(audioClips[1]);
        myAudioSource.Play();
        yield return new WaitForSeconds(1.5f);
        wrongAnswerImage.SetActive(false);

        totalIndex++;
        if (totalIndex > 3) //sadece 4 grup olmalý, 0 1 2 ve 3
            totalIndex = 0;


    }


    IEnumerator CheckLetter() //check if letter is in the answer
    {
        yield return new WaitForSeconds(1f);

        int foundLetters = 0;

        for (int i = 0; i < answerButtonTextList.Count; i++)
        {
            if (answerButtonTextList[i].text == currentLetter)
            {
                answerButtonsList[i].interactable = true;
                myAudioSource.PlayOneShot(audioClips[0]);
                AddPoints(currentPoint);
                foundLetters++;

                yield return new WaitForSeconds(0.4f);
            }
        }

        if (foundLetters == 0) //hic harf bulunmadiysa
        {
            StartCoroutine(WrongAnswer());
        }


    }


    public IEnumerator CorrectAnswer() //kind of a cheat code, for now I'll not implement writing for full answer, you'll have to memorise the answer
    {
        for (int i = 0; i < answerButtonTextList.Count; i++) //gives point for each unopened letter
        {
            if (!answerButtonsList[i].GetComponentInChildren<TextMeshProUGUI>().enabled)
            {
                answerButtonTextList[i].enabled = true;
                myAudioSource.PlayOneShot(audioClips[0]);
                AddPoints(currentPoint);
                yield return new WaitForSeconds(0.2f);
            }
        }

        totalIndex++;
        if (totalIndex > 3) //sadece 4 grup olmalý, 0 1 2 ve 3
            totalIndex = 0;
    }




}