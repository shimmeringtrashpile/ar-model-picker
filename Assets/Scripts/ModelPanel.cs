using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModelPanel : MonoBehaviour
{
    [SerializeField]
    public string name, description, creator, date;

    [SerializeField]
    Image icon;

    [SerializeField]
    TextMeshProUGUI nameText, descriptionText, creatorText, dateText;

    [SerializeField]
    public DateTime lastUsedDate;

    [SerializeField]
    float daysFromToday;



    // Start is called before the first frame update
    void Start()
    {
        lastUsedDate = DateTime.Today.AddDays(daysFromToday);
        nameText.text = name;
        descriptionText.text = description;
        creatorText.text = creator;
        dateText.text = System.DateTime.Now.ToString();
        if (lastUsedDate.Date == DateTime.Today)
        {
            dateText.text = System.DateTime.Now.ToString("HH:mm") + " "  + DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
        } 
        else if (lastUsedDate.Date == DateTime.Today.AddDays(-1))
        {
            dateText.text = "Yesterday";
        } 
        else 
        {
            dateText.text = lastUsedDate.Date.ToString("MMMM") + " " + lastUsedDate.Date.ToString("dd") + GetDaySuffix(System.DateTime.DaysInMonth(lastUsedDate.Year, lastUsedDate.Month)) + ", " + lastUsedDate.Year.ToString();


        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This string function will return a string. Notice the return.
    // Int is the type of variable for day
    string GetDaySuffix(int day)
    {
        int test = 5;
        float test1 = 2;
        test = Int32.Parse(test1.ToString());
        switch (day)
        {
            case 1:
            case 21:
            case 31:
                return "st";
            case 2:
            case 22:
                return "nd";
            case 3:
            case 23:
                return "rd";
            default:
                return "th";
        }
    }
}
