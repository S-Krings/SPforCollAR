using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class DropdownSetter : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    //[SerializeField] ColourEnum colorEnum;
    [SerializeField] Sprite dot, rainbowdot;
    [SerializeField] GameObject gameObjectToToggle;

    private bool updatePending = false;
    private ColourEnum colourEnum = new ColourEnum();

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(colourEnum.GetNames());

        
        Debug.Log(dropdown.options[1].text);
    }

    public void OnChangeUpdate()
    { 
        int newValue = dropdown.value;
        GameObject g = transform.Find("Colour").gameObject;
        Image image = g.GetComponentInChildren<Image>();

        if (newValue == 0)
        {
            image.sprite = rainbowdot;
            image.color = Color.white;
        }
        else
        {
            if (image.sprite == rainbowdot) image.sprite = dot;
            image.color = colourEnum.GetColour(newValue);
        }
    }

    public void OnClickCheck()
    {
        Debug.Log("OnclickCheck");
        updatePending = true;
    }

    private void setDropdownOptions()
    {
        Debug.Log("DropdownList found");
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            Debug.Log("Found go: " + GameObject.Find("Item " + i + ": " + colourEnum.GetName(i)));
            GameObject g = GameObject.Find("Item " + i + ": " + colourEnum.GetName(i));
            foreach (Image image in g.GetComponentsInChildren<Image>())
            {
                if (image.transform.gameObject.name.Equals("Item Colour"))
                {
                    if (i == 0)
                    {
                        image.sprite = rainbowdot;
                    }
                    else
                    {
                        if (image.sprite == rainbowdot) image.sprite = dot;
                        image.color = colourEnum.GetColour(i);
                    }
                    break;
                }
            }
        }
    }

    private void Update()
    {
        if (updatePending)
        {
            if (GameObject.Find("Dropdown List") != null)
            {
                setDropdownOptions();
                updatePending = false;
            }
        }
    }

    public void ToggleCoveredGameObject(bool visible)
    {
        gameObjectToToggle.SetActive(visible);
    }
}
