using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThemeOptions : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    private const string themeKey = "theme";
    void Start()
    {
        if (PlayerPrefs.HasKey(themeKey))
        {
            dropdown.value = PlayerPrefs.GetInt(themeKey);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt(themeKey, dropdown.value);
    }
}
