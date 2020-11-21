using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    // public Button test;
    // Start is called before the first frame update
    void Start()
    {
        // test.onClick.AddListener(AtoZButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitButtonClicked() 
    {
        menu.SetActive(false);
    }

    public void AtoZButtonClicked()
    {
        ModelPanel[] panelArray = FindObjectsOfType<ModelPanel>();
        List<string> modelPanelList = new List<string>();
        foreach (ModelPanel item in panelArray)
        {
            modelPanelList.Add(item.name);
        }

        modelPanelList.Sort();
        int position = 0;
        foreach (string item in modelPanelList)
        {
            print(item);
            foreach (ModelPanel panelItem in panelArray)
            {
                if (panelItem.name == item)
                {
                    print(panelItem.name + " " + item + " " + position);
                    panelItem.transform.SetSiblingIndex(position);
                }
            }
            position++;
        }

    }

    public void ZtoAButtonClicked()
    {
        ModelPanel[] panelArray = FindObjectsOfType<ModelPanel>();
        List<string> modelPanelList = new List<string>();
        foreach (ModelPanel item in panelArray)
        {
            modelPanelList.Add(item.name);
        }
        modelPanelList.Sort();
        int position = modelPanelList.Count-1;
        foreach (string item in modelPanelList)
        {
            print(item);
            foreach (ModelPanel panelItem in panelArray)
            {
                if (panelItem.name == item)
                {
                    print(panelItem.name + " " + item + " " + position);
                    panelItem.transform.SetSiblingIndex(position);
                }
            }
            position--;
        }

    }

    public void NewOld()
    {
        ModelPanel[] panelArray = FindObjectsOfType<ModelPanel>();
        List<string> modelPanelList = new List<string>();
        foreach (ModelPanel item in panelArray)
        {
            modelPanelList.Add(item.name);
        }
        modelPanelList.Sort();
        int position = modelPanelList.Count - 1;
        foreach (string item in modelPanelList)
        {
            print(item);
            foreach (ModelPanel panelItem in panelArray)
            {
                if (panelItem.name == item)
                {
                    print(panelItem.name + " " + item + " " + position);
                    panelItem.transform.SetSiblingIndex(position);
                }
            }
            position--;
        }
    }

    public void OldNew()
    {

    }

}
