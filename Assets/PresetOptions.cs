using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PresetOptions : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private const string presetKey = "preset";
    void Start()
    {
        if (PlayerPrefs.HasKey(presetKey))
        {
            dropdown.value = PlayerPrefs.GetInt(presetKey);
        }
    }

    public void ChangePreset()
    {
        PlayerPrefs.SetInt(presetKey, dropdown.value);
    }

}
