using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translator : Singleton<Translator>
{
    private int _langId;
    private List<TranslatableText> _listText = new List<TranslatableText>();
    private string[,] _lineText =
    {
        {
            "push pops\nthe same\ncolours\nof the\nportal",
            "toch the portal\nto cange color",
            "speed up\nby swipe up",
            "YOU FAILED",
            "LOSE\nSCORE",
            "SAVE\nSCORE"
        },

        {
            "лопай пупырки\nв цвет\nпортала",
            "жми на портал\nчтобы сменить цвет",
            "свайпай вверх\nчтобы\nускориться",
            "ПОРАЖЕНИЕ",
            "ПОТЕРЯЙ\nОЧКИ",
            "СОХРАНИ\nОЧКИ"
        }
    };
  
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                PlayerPrefs.SetInt("Language", 1);
            else
                PlayerPrefs.SetInt("Language", 0);
        }
        SelectLanguage(PlayerPrefs.GetInt("Language"));
    }
    public void SelectLanguage(int id = 0)
    {
        _langId = id;
        UpdateTexts();
    }
    public string GetText(int textKey)
    {
        return _lineText[_langId, textKey];
    }
    public void Add(TranslatableText text)
    {
        _listText.Add(text);
    }
    public void Remove(TranslatableText text)
    {
        _listText.Remove(text);
    }
    public void UpdateTexts()
    {
        for(int i=0; i < _listText.Count; i++)
        {
            _listText[i].SetText(_lineText[_langId, _listText[i].TextID]);
        }
    }
}
