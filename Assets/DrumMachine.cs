using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumMachine : MonoBehaviour
{
    public GridGenerator gridGenerator;
    public double bpm = 90.0F;
    double nextTick = 0.0F; // The next tick in dspTime
    bool ticked = false;

    bool ticked4 = false;
    bool ticked8 = false;
    bool ticked16 = false;

    double nextTick4 = 0.0F;
    double nextTick8 = 0.0F;
    double nextTick16 = 0.0F;

    float tick16Divider = 4;
    int tickIndex = 0;
    private bool playing = false;


    void Start()
    {
        tickIndex = 0 + gridGenerator.labelWidthInColumns;
        if (PlayerPrefs.HasKey("bpm"))
        {
            bpm = PlayerPrefs.GetInt("bpm");
        }

    }

    void LateUpdate() 
    {
        if (!ticked && nextTick >= AudioSettings.dspTime)
        {
            ticked = true;
            BroadcastMessage("OnTick");
        }
    }

    void OnTick()
    {
        
        for (int rowIndex = 0; rowIndex < gridGenerator.numberByHeight; rowIndex++)
        {
            Square square = gridGenerator.matrix[rowIndex, tickIndex];
            if (square.activated)
            {
                square.getSound().Play();
            }

            square.GetComponent<Renderer>().material.color = square.tickColor;

            int prevTickIndex = tickIndex > 0 + gridGenerator.labelWidthInColumns ? tickIndex - 1 : gridGenerator.numberByWidth + gridGenerator.labelWidthInColumns - 1;


                Square prevSquare = gridGenerator.matrix[rowIndex, prevTickIndex];
                if (prevSquare.activated)
                {
                    prevSquare.GetComponent<Renderer>().material.color = prevSquare.activeColor;
                }
                else
                {
                    prevSquare.GetComponent<Renderer>().material.color = prevSquare.passiveColor;
                }
            
        }

        increaseTickIndex();
    }
    private void FixedUpdate()
    {
        if(gridGenerator.matrix != null)
        {
            if(!playing)
            {
                startPlaying();
                playing = true;
            } else 
            {
                continuePlaying();
            }
            

        }
        
    }


    private void continuePlaying()
    {
        
            double timePerTick = 60.0f / bpm / tick16Divider;
            double dspTime = AudioSettings.dspTime;

            while (dspTime >= nextTick)
            {
                ticked = false;
                nextTick += timePerTick;
            }

        
        
    }

    private void startPlaying()
    {
        
        
        double startTick = AudioSettings.dspTime;

        nextTick = startTick + (60.0 / bpm) / tick16Divider;


        
    }

    private void increaseTickIndex()
    {
        tickIndex = tickIndex >= gridGenerator.numberByWidth + gridGenerator.labelWidthInColumns - 1 ? 0 + gridGenerator.labelWidthInColumns : tickIndex + 1;
    }
}
