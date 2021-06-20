using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Text WinLoseText;
    [SerializeField]
    private Button ButtonReset;

    public void MessageWinLose(bool win)
    {
        ButtonReset.gameObject.SetActive(true);
        WinLoseText.gameObject.SetActive(true);
        if (win)
            WinLoseText.text = "������";
        else
            WinLoseText.text = "���������";
    }
    public void MessageDraw()
    {
        ButtonReset.gameObject.SetActive(true);
        WinLoseText.gameObject.SetActive(true);
        WinLoseText.text = "�����";
    }

}
