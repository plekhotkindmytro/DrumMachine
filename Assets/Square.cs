using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class Square : MonoBehaviour
{
    private AudioSource sound;

    public bool activated = false;
    
    public void setSound(AudioSource sound) {
        this.sound = sound;
    }

    public AudioSource getSound()
    {
        return sound;
    }

    public Color activeColor;
    public Color passiveColor;
    public Color tickColor;

    public void Activate()
    {
        activated = !activated;
        if (activated)
        {
            this.GetComponent<Renderer>().material.color = activeColor;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = passiveColor;
        }
    }
}
