using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    private int randomvalue;
    private float timeInterval;
    private bool coroutineAllowed;
    private int finalAngle;

    [SerializeField]
    private Text winText;


    // Use this for initialization
    private void Start()
    {
        coroutineAllowed = true;

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && coroutineAllowed)
            StartCoroutine(Spin());
    }
    private IEnumerator Spin()
    {
        coroutineAllowed = false;
        randomvalue = Random.Range(20, 30);
        timeInterval = 0.1f;


        for (int i = 0; i < randomvalue; i++)
        {

            transform.Rotate(0, 0, 22.5f);
            if (i > Mathf.RoundToInt(randomvalue * 0.5f))
                timeInterval = 0.2f;
            if (i > Mathf.RoundToInt(randomvalue * 0.85f))
                timeInterval = 0.4f;
            yield return new WaitForSeconds(timeInterval);
        }
        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0)
            transform.Rotate(0, 0, 22.5f);
        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);

        switch (finalAngle)
        {
            case 0:
                winText.text = "You Luck 5%";
                break;
            case 1:
                winText.text = "You Luck 10%";
                break;
            case 2:
                winText.text = "You Luck 12%";
                break;
            case 3:
                winText.text = "You Luck 30%";
                break;
            case 4:
                winText.text = "You Luck 43%";
                break;
            case 5:
                winText.text = "You Luck 66%";
                break;
            case 6:
                winText.text = "You Luck 70%";
                break;
            case 7:
                winText.text = "You Luck 99%";
                break;


        }
        coroutineAllowed = true;
    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WheelManager : MonoBehaviour
//{
//    public float rotationValue;
//    public bool isRotating = false;
//    public int selectedPoint;
//    public Transform Wheel;

//    public void InitiateTurn()
//    {
//        isRotating = true;
//        //puani belirleyen kod 1 yesil, 2 turkuaz,...., 8 sari
//        selectedPoint = Random.Range(1, 9);

//        //gorseli kac derece dondurucez
//        rotationValue = 1080 + selectedPoint * 45;

//    }

//    private void FixedUpdate()
//    {
//        if (isRotating && rotationValue > 1)
//        {
//            Wheel.Rotate(Vector3.forward, rotationValue / 45);

//            rotationValue = Mathf.MoveTowards(rotationValue, 0, 10);
//        }
//        if (isRotating && rotationValue <= 1)
//        {
//            isRotating = false;



//        }
//    }




//}
