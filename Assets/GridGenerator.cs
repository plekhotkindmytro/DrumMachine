
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridGenerator : MonoBehaviour
{
    public AudioSource hiHat;
    public AudioSource snare;
    public AudioSource bass;
    public AudioSource ride;
    public AudioSource crash;
    public AudioSource highTom;
    public AudioSource mediumTom;
    public AudioSource floorTom;

    private AudioSource[] soundList;
    private string[] soundTitles = new string[] 
    {
        "HiHat", "Snare", "Bass", "Ride", "Crash", "Tom 1", "Tom 2", "Tom 3"
    };

    public Square squarePrefab;
    public GameObject squareWithTextPrefab;
    public GameObject backButtonPrefab;

    public byte numberByWidth = 16;
    public byte numberByHeight = 8;
    public byte labelWidthInColumns = 3;
    public Square[,] matrix;
    public float offset = 0.05f;
    
    
    private float borderOffset = 2.0f;

    private const string presetKey = "preset";
    private const string themeKey = "theme";
    private Int32 preset;
    private Int32 theme;

    void Start()
    {
        AudioSource[] soundList  = { hiHat, snare, bass, ride, crash, highTom, mediumTom, floorTom };
        this.soundList = soundList;
        Array.Reverse<AudioSource>(this.soundList);
        Array.Reverse<string>(this.soundTitles);

        matrix = new Square[numberByHeight, numberByWidth + labelWidthInColumns];
        bool cameraMoved = false;

        setPrefs();


        for (byte rowIndex = 0; rowIndex < numberByHeight; rowIndex++)
        {
            
            setTitle(rowIndex);
            // float yOffset = y == 0 ? 0 : offset;
            for (byte columnIndex = (byte)(0 + labelWidthInColumns); columnIndex < numberByWidth + labelWidthInColumns; columnIndex++)
            {
                // float xOffset = x == 0 ? 0 : offset;
                Square square = Instantiate(squarePrefab);
                setSound(square, rowIndex);

                scaleSquare(square.gameObject, offset, borderOffset);
                if (!cameraMoved)
                {
                    moveCamera(square.gameObject);
                    cameraMoved = true;
                }

                setColor(square, columnIndex);
                setMusicPattern(square, rowIndex, columnIndex);
                

                setSquarePosition(square, rowIndex, columnIndex);

                matrix[rowIndex, columnIndex] = square;
            }
        }

        setBackButton();
    }

    private void setBackButton()
    {
        GameObject backButton = Instantiate(backButtonPrefab);
        backButton.name = "BackButton";
        byte columnIndex = (byte)(numberByWidth + labelWidthInColumns);
        byte rowIndex = numberByHeight;
        float xPos = matrix[0, labelWidthInColumns].gameObject.transform.localScale.x + (borderOffset) / 2;
        float yPos = matrix[0, labelWidthInColumns].gameObject.transform.localScale.y * rowIndex + offset * (rowIndex) + (borderOffset) / 2;
        backButton.transform.position = new Vector3(xPos, yPos);
        
    }

    private void setPrefs()
    {
        if (PlayerPrefs.HasKey(presetKey))
        {
            preset = PlayerPrefs.GetInt(presetKey);
        }

        if (PlayerPrefs.HasKey(themeKey))
        {
            theme = PlayerPrefs.GetInt(themeKey);
        }
    }

    private void setTitle(byte rowIndex) {
        GameObject squareWithText = Instantiate(squareWithTextPrefab);
        scaleSquareWithLabel(squareWithText, offset, borderOffset);
        setSquareWithLabelPosition(squareWithText, rowIndex);
        squareWithText.transform.GetChild(0).GetComponent<TextMeshPro>().text = soundTitles[rowIndex].ToUpper();
    }

    private void setMusicPattern(Square square, byte rowIndex, byte columnIndex)
    {
        if (PlayerPrefs.HasKey(presetKey))
        {
            preset = PlayerPrefs.GetInt(presetKey);
        }

        if (preset.Equals(0))
        {
            setPopRockPattern(square, rowIndex, columnIndex);
        }
        else if (preset.Equals(1))
        {
            setHipHopPattern(square, rowIndex, columnIndex);
        }
        else if (preset.Equals(2))
        {
            setHeavyMetalPattern(square, rowIndex, columnIndex);
        }
        else if (preset.Equals(3))
        {
            setFunkPattern(square, rowIndex, columnIndex);
        }
    }

    private void setPopRockPattern(Square square, byte rowIndex, byte columnIndex)
    {
        int hiHatRow = numberByHeight - 1;
        int snareRow = numberByHeight - 2;
        int bassRow = numberByHeight - 3;

        List<int> hiHatColumnIndexes = new List<int>() { 0, 2, 4, 6, 8, 10, 12, 14};
        List<int> snareColumnIndexes = new List<int>() { 4, 12 };
        List<int> bassColumnIndexes = new List<int>() { 0, 8, 10};

        if ((rowIndex == hiHatRow && hiHatColumnIndexes.Contains(columnIndex - labelWidthInColumns))
            || (rowIndex == snareRow && snareColumnIndexes.Contains(columnIndex - labelWidthInColumns))
            || (rowIndex == bassRow
            && bassColumnIndexes.Contains(columnIndex - labelWidthInColumns)))
        {
            square.Activate();
        } 
    }


    private void setFunkPattern(Square square, byte rowIndex, byte columnIndex)
    {
        int hiHatRow = numberByHeight - 1;
        int snareRow = numberByHeight - 2;
        int bassRow = numberByHeight - 3;

        List<int> hiHatColumnIndexes = new List<int>() { 0, 2, 4, 6, 8, 10, 12, 14 };
        List<int> snareColumnIndexes = new List<int>() { 4, 7, 9, 12, 13 };
        List<int> bassColumnIndexes = new List<int>() { 0, 10, 15 };

        if ((rowIndex == hiHatRow && hiHatColumnIndexes.Contains(columnIndex - labelWidthInColumns))
            || (rowIndex == snareRow && snareColumnIndexes.Contains(columnIndex - labelWidthInColumns))
            || (rowIndex == bassRow
            && bassColumnIndexes.Contains(columnIndex - labelWidthInColumns)))
        {
            square.Activate();
        }
    }

    private void setHeavyMetalPattern(Square square, byte rowIndex, byte columnIndex)
    {
        int rideRow = numberByHeight - 4;
        int snareRow = numberByHeight - 2;
        int bassRow = numberByHeight - 3;

        List<int> rideColumnIndexes = new List<int>() { 0, 2, 4, 6, 8, 10, 12, 14 };
        List<int> snareColumnIndexes = new List<int>() { 4, 12, 14};
        List<int> bassColumnIndexes = new List<int>() { 0, 2, 3, 6, 7, 8, 9, 15 };

        if ((rowIndex == rideRow && rideColumnIndexes.Contains(columnIndex - labelWidthInColumns))
            || (rowIndex == snareRow && snareColumnIndexes.Contains(columnIndex - labelWidthInColumns))
            || (rowIndex == bassRow
            && bassColumnIndexes.Contains(columnIndex - labelWidthInColumns)))
        {
            square.Activate();
        }
    }

    private void setHipHopPattern(Square square, byte rowIndex, byte columnIndex)
    {
        int hiHatRow = numberByHeight - 1;
        int snareRow = numberByHeight - 2;
        int bassRow = numberByHeight - 3;

        List<int> hiHatColumnIndexes = new List<int>() { 1, 2, 4, 6, 8, 10, 12, 14 };
        List<int> snareColumnIndexes = new List<int>() { 4, 12 };
        List<int> bassColumnIndexes = new List<int>() { 0, 2, 6, 9 };

        if ((rowIndex == hiHatRow && hiHatColumnIndexes.Contains(columnIndex - labelWidthInColumns))
            || (rowIndex == snareRow && snareColumnIndexes.Contains(columnIndex - labelWidthInColumns))
            || (rowIndex == bassRow
            && bassColumnIndexes.Contains(columnIndex - labelWidthInColumns)))
        {
            square.Activate();
        }
    }

    private void startDrumMachine()
    {
       // TODO??? 
    }

    private void setColor(Square square, byte columnIndex) {

        
        Color activeColor = new Color32(22, 22, 255, 255);
        Color passiveColor = new Color32(77, 255, 55, 255);
        Color passiveColorFor4th = new Color32(250, 250, 50, 255);
        Color tickColor = new Color32(255, 105, 180, 255);

        if (theme.Equals(1))
        {
            activeColor = new Color32(240, 6, 153, 255);
            passiveColor = new Color32(247, 208, 2, 255);
            passiveColorFor4th = new Color32(1, 142, 66, 255);
            tickColor = new Color32(69, 78, 158, 255);
        }
        else if (theme.Equals(2))
        {
            activeColor = new Color32(143, 131, 137, 255);
            passiveColor = new Color32(230, 239, 233, 255);
            passiveColorFor4th = new Color32(194, 234, 186, 255);
            tickColor = new Color32(167, 196, 160, 255);
        }
        else if (theme.Equals(3)) 
        {
            activeColor = new Color32(95, 10, 135, 255);
            passiveColor = new Color32(234, 191, 203, 255);
            passiveColorFor4th = new Color32(193, 145, 161, 255);
            tickColor = new Color32(47, 0, 79, 255);
        }
        else if (theme.Equals(4))
        {
            activeColor = new Color32(46, 37, 50, 255);
            passiveColor = new Color32(251, 251, 251, 255);
            passiveColorFor4th = new Color32(210, 210, 210, 255);
            tickColor = new Color32(158, 71, 112, 255);
        }

        square.passiveColor = passiveColor;
        square.activeColor = activeColor;
        square.tickColor = tickColor;


        List<int> every4Index = new List<int> { 0, 4, 8, 12 };
        if (square.activated)
        {
            square.GetComponent<Renderer>().material.color = square.activeColor;
        } 
        else if (every4Index.Contains(columnIndex - labelWidthInColumns))
        {
            square.passiveColor = passiveColorFor4th;
            square.GetComponent<Renderer>().material.color = passiveColorFor4th;
        }
        else
        {
            square.GetComponent<Renderer>().material.color = square.passiveColor;
        }
    }

    private void setSound(Square square, byte rowIndex)
    {
        square.setSound(soundList[rowIndex]);
    }

    private void scaleSquare(GameObject square, float offset, float borderOffset)
    {

        float newScaleX = square.transform.localScale.x;
        float newScaleY = square.transform.localScale.y;
        newScaleX = (Camera.main.aspect * Camera.main.orthographicSize * 2 - borderOffset - offset * (numberByWidth + labelWidthInColumns - 1)) / (numberByWidth + labelWidthInColumns);
        newScaleY = (Camera.main.orthographicSize * 2  - borderOffset - offset * (numberByHeight - 1)) / numberByHeight;
        square.transform.localScale = new Vector3(newScaleX, newScaleY, square.transform.localScale.z);
    }

    private void scaleSquareWithLabel(GameObject square, float offset, float borderOffset)
    {
        float newScaleX = square.transform.localScale.x;
        float newScaleY = square.transform.localScale.y;
        newScaleX = (Camera.main.aspect * Camera.main.orthographicSize * 2 - borderOffset - offset * (numberByWidth + labelWidthInColumns - 1)) / (numberByWidth + labelWidthInColumns);
        newScaleY = (Camera.main.orthographicSize * 2 - borderOffset - offset*(numberByHeight - 1)) / numberByHeight;
        newScaleX = (newScaleX + offset) *(labelWidthInColumns);
        square.transform.localScale = new Vector3(newScaleX, newScaleY, square.transform.localScale.z);
    }

    private void setSquarePosition(Square square, byte rowIndex, byte columnIndex)
    {
        float xPos = square.transform.localScale.x * columnIndex + offset * (columnIndex) + (borderOffset) / 2;
        float yPos = square.transform.localScale.y * rowIndex + offset * (rowIndex) + (borderOffset) / 2;
        square.transform.position = new Vector3(xPos, yPos);
    }
    private void setSquareWithLabelPosition(GameObject square, byte rowIndex)
    {
        float xPos = (square.transform.localScale.x - offset)/ labelWidthInColumns + (borderOffset) / 2f;
        float yPos = square.transform.localScale.y * rowIndex + offset * (rowIndex) + (borderOffset) / 2;
        square.transform.position = new Vector3(xPos, yPos);
    }

    private void moveCamera(GameObject square)
    {
        float xSquareScale = square.transform.localScale.x;
        float ySquareScale = square.transform.localScale.y;
        float newCameraX = Camera.main.orthographicSize*Camera.main.aspect - xSquareScale/2;
        float newCameraY = Camera.main.orthographicSize  - ySquareScale/2;
        Camera.main.transform.position = new Vector3(newCameraX, newCameraY, Camera.main.transform.position.z);

    }

    private void OnApplicationQuit()
    {
        // TODO
    }


}
