using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    enum ButtonStates {buttondown, buttonup};
    ButtonStates myState;
    bool canClick = true;
    bool shouldMoveDown = false;
    bool shouldMoveUp = false;

    // Button is all the way down, will come back up to latched state.

    // Use SerializeField if you want a variable to be editable.
    [SerializeField]
    Vector3 fullDown;

    // Button is in 2/3rds of the way down state to show pressed condition.
    [SerializeField]
    Vector3 latched;

    // Button is all the way up.
    Vector3 fullUp;

    [SerializeField]
    Material lightgreen; 


    // Start is called before the first frame update
    void Start()
    {
        myState = ButtonStates.buttonup;
        fullUp = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        print(myState);
        switch (myState)
        {
            case ButtonStates.buttondown:
                if (shouldMoveDown)
                {
                    print("buttondown shouldMoveDown");
                    if (this.gameObject.transform.position.y > fullDown.y)
                    {
                        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (2 * Time.deltaTime), this.gameObject.transform.position.z);
                    }
                    else
                    {
                        StartCoroutine(DelayForMovingBackUp());
                    }
                }
                if (shouldMoveUp)
                {
                    print("buttondown shouldMoveUp");
                    if (this.gameObject.transform.position.y < fullUp.y)
                    {
                        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + (3 * Time.deltaTime), this.gameObject.transform.position.z);

                    }
                }


                break;

            case ButtonStates.buttonup:
                if (shouldMoveDown)
                {
                    print("buttonup shouldMoveDown");
                    if (this.gameObject.transform.position.y > fullDown.y)
                    {
                        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (2 * Time.deltaTime), this.gameObject.transform.position.z);
                    }
                    else
                    {
                        StartCoroutine(DelayForMoving());
                    }
                }
                if (shouldMoveUp)
                {
                    print("buttonup shouldMoveUp");
                    this.gameObject.GetComponent<Renderer>().material = lightgreen;
                    if (this.gameObject.transform.position.y >= latched.y)
                    {
                        myState = ButtonStates.buttondown;
                        shouldMoveUp = false;
                        shouldMoveDown = false;
                    }

                    if (this.gameObject.transform.position.y < latched.y)
                    {
                        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + (3 * Time.deltaTime), this.gameObject.transform.position.z);

                    }
                    else
                    {
                        myState = ButtonStates.buttondown;
                        shouldMoveUp = false;
                        shouldMoveDown = false;
                    }
                }
                break;

            default:
                break;
        }


    }

    // Void means "no return." Alternately, Int would mean integer return.
    private void OnMouseDown()
    {
        // 3 states: off, fully down, and latched
        switch (myState)
        {
            case ButtonStates.buttondown:
                shouldMoveUp = false;
                shouldMoveDown = true;
                break;

            case ButtonStates.buttonup:
                shouldMoveDown = true;
                break;

            default:
                break;
        }
    }

    IEnumerator DelayForMoving()
    {
        yield return new WaitForSeconds(1.0f);
        shouldMoveDown = false;
        shouldMoveUp = true;
    }

    IEnumerator DelayForMovingBackUp()
    {
        yield return new WaitForSeconds(1.0f);
        shouldMoveDown = false;
        shouldMoveUp = true;
    }
}
