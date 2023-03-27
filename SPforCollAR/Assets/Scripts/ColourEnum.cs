using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColourEnum
{
    public enum PlayerColorEnum
    {
        Random,
        Blue,
        Green,
        Yellow,
        Orange,
        Red,
        Purple
    }
    public Color GetColour(int value)
    {
        switch (value)
        {
            case 0:
                return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            case 1:
                return new Color32(62, 62, 188, 255);
            case 2:
                return new Color32(33, 190, 0, 255);
            case 3:
                return new Color32(255, 235, 0, 255);
            case 4:
                return new Color32(255, 165, 0, 255);
            case 5:
                return new Color32(217, 0, 13, 255);
            case 6:
                return new Color32(175, 0, 255, 255);
            default:
                return Color.white;
        }
    }

    public string GetName(int index)
    {
        return PlayerColorEnum.GetName(typeof(PlayerColorEnum), index);
    }

    public List<string> GetNames()
    {
        string[] stringNames = PlayerColorEnum.GetNames(typeof(PlayerColorEnum));
        return stringNames.ToList();
    }
}
