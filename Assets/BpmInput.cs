using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BpmInput : MonoBehaviour
{
    
    public TMP_InputField input;
    
    void Start()
    {
        input = GetComponent<TMP_InputField>();
        if (PlayerPrefs.HasKey("bpm"))
        {
            input.text = PlayerPrefs.GetInt("bpm").ToString();
        }
    }

    // Update is called once per frame
    public void SetBpm()
    {
        Int32.TryParse(input.text, out int bpm);
        PlayerPrefs.SetInt("bpm", bpm);
    }
}
