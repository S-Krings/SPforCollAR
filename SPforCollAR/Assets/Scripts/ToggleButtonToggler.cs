using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonToggler : MonoBehaviour
{
    [SerializeField] Interactable toggleSwitch;

    public void OnClickToggleToggleButton()
    {
        toggleSwitch.IsToggled = !toggleSwitch.IsToggled;
    }
}
