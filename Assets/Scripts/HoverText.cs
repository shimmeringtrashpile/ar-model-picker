using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI textBox;
    char[] textArray;
    int currentPosition;

    // Makes visibile in inspector. 
    // This is the prefered way to do this. Don't just make it public. 
    [SerializeField]
    float maxChars = 20;

    // List is the type. Char is the type of data in the list.
    List<char> current = new List<char>();
    string defaultText;
    bool isHovering = false;
    bool canChange = true;

    [SerializeField]
    bool slider = false;

    // Begin variables for sliding mechanism approach.
    float defaultWidth;
    float defaultLocation;
    float currentWidth;
 
    [SerializeField]
    float sliderSpeed = 1;

    float timesMoved = 0;
    bool canMove = true;

    [SerializeField]
    Transform image;

    float changePosition = 1;
    float localChangedTextNumber = 0;
    bool canDelete = false;
    bool isDeleteCoroutineRunning = false;
    bool canHover = false;



    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<TextMeshProUGUI>();
        defaultText = textBox.text;
        textArray = textBox.text.ToCharArray();

        defaultWidth = textBox.rectTransform.rect.width;
        defaultLocation = textBox.rectTransform.rect.x;
        print(defaultWidth);
        print(defaultLocation);
        currentWidth = textBox.rectTransform.rect.width;

        if (textBox.text.Length <= maxChars)
        {
            maxChars = textBox.text.Length;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        isHovering = canHover;

        if (slider == false)
        {

            if (isHovering)
            {
                // This first approach removes the front letter.
                if (canChange)
                {
                    textBox.text = "";
                    canChange = false;
                    // Coroutines are kinda like async in Javascript.
                    // Recharge-style approach.
                    StartCoroutine(DelayToChangingNumber());
                    print("the mouse is hovering");
                    for (int i = currentPosition; i < maxChars + currentPosition; i++)
                    {
                        if (textArray.Length - currentPosition >= maxChars)
                        {
                            if (i >= textArray.Length)
                            {
                            }
                            else
                            {
                                print(i + " " + textArray.Length);
                                current.Add(textArray[i]);
                                textBox.text += textArray[i];
                            }
                        }
                    }
                    print(current.ToString());
                    if (textArray.Length - currentPosition > maxChars)
                    {
                        currentPosition++;
                        current.Clear();
                    }
                }
            }
            else
            {
                StopCoroutine(DelayToChangingNumber());
                textBox.text = defaultText;
                currentPosition = 0;
            }
        } else 
        {
           /* This second approach moves the box to the left and 
              subtracts chars from the beginning of the string.
              We also make the box size smaller.
            */
           if (isHovering)
            {
                if (canMove)
                {
                    canMove = false;
                    StartCoroutine(DelayToMoving());
                    if (textBox.text.Length > maxChars)
                    {
                        if (this.transform.position.x - sliderSpeed >= image.position.x)
                        {
                            // Move the line.
                            this.transform.position = new Vector3(this.transform.position.x - sliderSpeed, this.transform.position.y, this.transform.position.z);
                            // Resize the box.
                            textBox.rectTransform.sizeDelta = new Vector2((currentWidth - 15) + sliderSpeed, 50);
                            currentWidth = textBox.rectTransform.rect.width;
                            localChangedTextNumber += sliderSpeed;
                        }
                    }

                    if (localChangedTextNumber > changePosition)
                    {
                        if (isDeleteCoroutineRunning == false)
                        {
                            isDeleteCoroutineRunning = true;
                            StartCoroutine(DelayToDeletingLetter());
                        }


                    }

                }
            } else
            {
                ResetPanel();
            }

        }

        if (canDelete)
        {
            canDelete = false;
            StartCoroutine(DelayToDeletingLetter());
            if (textBox.text.Length > maxChars) 
            {
                textBox.text = textBox.text.Remove(0, 1);
                localChangedTextNumber = 0;
            }
        }
    }

    private void ResetPanel()
    {
        currentWidth = defaultWidth;
        StopCoroutine(DelayToMoving());
        StopCoroutine(DelayToDeletingLetter());
        canDelete = false;
        isDeleteCoroutineRunning = false;
        // Put the line back.
        this.transform.position = new Vector3(this.transform.position.x + (timesMoved * sliderSpeed), this.transform.position.y, this.transform.position.z);
        textBox.rectTransform.sizeDelta = new Vector2(defaultWidth, 50);
        timesMoved = 0;
        textBox.text = defaultText;
    }

    // OnPointers are Unity functions. This must have a button. Requires IPointerEnterHandler and IPointerExitHandler at the top.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // isHovering = true;
        StartCoroutine(DelayToHovering());
        print("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(DelayToHovering()) ;
        isHovering = false;
        canHover = false;
        ResetPanel();
        print("OnPointerExit");

    }

    IEnumerator DelayToChangingNumber()
    {
        // Ignore the return and call a new object
        yield return new WaitForSeconds(.05f);
        canChange = true;
    }

    IEnumerator DelayToMoving()
    {
        //start sound
        yield return new WaitForSeconds(.05f);
        if (textBox.text.Length > maxChars) 
        { 
            timesMoved += 1;
        }
        canMove = true;
        //stop sound
    }

    IEnumerator DelayToDeletingLetter()
    {
        yield return new WaitForSeconds(.01f);
        canDelete = true;
    }    
    IEnumerator DelayToHovering()
    {
        yield return new WaitForSeconds(.03f);
        canHover = true;
    }
}
