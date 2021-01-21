using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAddOn : MonoBehaviour
{
    Button button;
    bool canChange = false;
    bool canChangeBack = false;
    Image image;
    Color basecolor;

    float scaleChange = .0008f;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>(); 
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        basecolor = image.color;
        button.onClick.AddListener(ButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (canChange)
        {
            button.interactable = false;
            // Changes per tick becuase we're in Update.
            image.color = new Color(image.color.r,image.color.g - 1,image.color.b - 1,image.color.a);
            button.transform.localScale = new Vector2(button.transform.localScale.x - scaleChange, button.transform.localScale.y - scaleChange);
        }
        if (canChangeBack)
        {
            button.transform.localScale = new Vector2(button.transform.localScale.x + scaleChange, button.transform.localScale.y + scaleChange);
            image.color = new Color(image.color.r, image.color.g + 1, image.color.b + 1, image.color.a);
            if (image.color == basecolor) 
            {
                canChangeBack = false;
                button.interactable = true;
            }
        }
    }

    void ButtonClicked()
    {
        print("button clicked!");
        canChange = true;
        audioSource.Play();
        StartCoroutine(DelayToChangeColor());

    }

    IEnumerator DelayToChangeColor() 
    {
        yield return new WaitForSeconds(.5f);
        canChange = false;
        canChangeBack = true;
    }
}
