using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI text;
    char[] textArray;
    int currentPosition;
    [SerializeField]
    float maxChars = 20;
    List<char> current = new List<char>();
    string defaultText;
    bool isHovering = false;
    bool canChange = true;

    [SerializeField]
    bool slider = false;

    float defaultWidth;
    float defaultLocation;
    float currentWidth;
    float currentLocation;
    float timesMoved = 0;
    bool canMove = true;




    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        defaultText = text.text;
        textArray = text.text.ToCharArray();

        defaultWidth = text.rectTransform.rect.width;
        defaultLocation = text.rectTransform.rect.x;
        print(defaultWidth);
        print(defaultLocation);
    }

    // Update is called once per frame
    void Update()
    {
        if (slider == false)
        {

            if (isHovering)
            {
                if (canChange)
                {
                    text.text = "";
                    canChange = false;
                    StartCoroutine(DelayToChangingNumber());
                    print("the mouse is hovering");
                    for (int i = currentPosition; i < maxChars + currentPosition; i++)
                    {
                        if (textArray.Length - currentPosition >= maxChars)
                        {
                            if (i >= textArray.Length)
                            {
                                //int local = i - textArray.Length;
                                //current.Add(textArray[local]);
                                //text.text += textArray[local];
                            }
                            else
                            {
                                print(i + " " + textArray.Length);
                                current.Add(textArray[i]);
                                text.text += textArray[i];
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
                text.text = defaultText;
                currentPosition = 0;
            }
        } else 
        {
           if (isHovering)
            {
                if (canMove)
                {
                    currentWidth = text.rectTransform.rect.width;
                    canMove = false;
                    StartCoroutine(DelayToMoving());
                    this.transform.position = new Vector3(this.transform.position.x - 5, this.transform.position.y, this.transform.position.z);
                    currentLocation = this.transform.position.x;
                    text.rectTransform.sizeDelta = new Vector2(currentWidth + 5, 50);
                    currentWidth = text.rectTransform.rect.width;

                }
            } else
            {
                this.transform.position = new Vector3(this.transform.position.x + (timesMoved * 5), this.transform.position.y, this.transform.position.z);
                text.rectTransform.sizeDelta = new Vector2(currentWidth - (timesMoved * 5), 50);
                timesMoved = 0;
            }

        }

    }

    private void OnMouseOver()
    {
        print("the mouse is hovering");
        for (int i = currentPosition; i < maxChars + currentPosition; i++)
        {
            current.Add(textArray[i]);
        }
        print(current.ToString());
        currentPosition++;
        current.Clear();
    }

    private void OnMouseExit()
    {
        text.text = defaultText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    IEnumerator DelayToChangingNumber()
    {
        yield return new WaitForSeconds(.05f);
        canChange = true;
    }

    IEnumerator DelayToMoving()
    {
        yield return new WaitForSeconds(.05f);
        canMove = true;
        timesMoved += 1;
    }
}
