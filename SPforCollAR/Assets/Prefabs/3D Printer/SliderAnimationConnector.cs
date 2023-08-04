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
        if(sliderEventData.NewValue == 1)
        {
            animator.SetFloat("slidervalue", 0.99f);
        }
        else
        {
            animator.SetFloat("slidervalue", sliderEventData.NewValue);
        }
    }
}
