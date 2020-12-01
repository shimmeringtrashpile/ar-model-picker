using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverText : MonoBehaviour
{
    TextMeshProUGUI text;
    char[] textArray;
    int currentPosition;
    float maxChars = 20;
    List<char> current = new List<char>();
    string defaultText;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        defaultText = text.text;
        textArray = text.text.ToCharArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
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
}
