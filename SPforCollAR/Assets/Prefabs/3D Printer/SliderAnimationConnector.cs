using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderAnimationConnector : MonoBehaviour
{
    [SerializeField] private Animator animator;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void onValueUpdated(SliderEventData sliderEventData)
    {
        animator.SetFloat("slidervalue", sliderEventData.NewValue);
    }
}
