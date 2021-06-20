using UnityEngine;

public class PlayerSingle : Player
{
    public static PlayerSingle II;
    public static new PlayerSingle My {
        get 
        {
            return Player.My as PlayerSingle;
        }
        set
        {
            Player.My = value;
        }
    }
    protected override void Start()
    {
        GameObject[] figuresGameObjects;
        figuresGameObjects = InstantiateFigures();
        Figures = new Figure[figuresGameObjects.Length];
        for (var i = 0; i < figuresGameObjects.Length; i++)
            Figures[i] = figuresGameObjects[i].GetComponent<Figure>();
    }
    protected override GameObject[] InstantiateFigures()
    {
        GameObject[] figures = new GameObject[COUNT_FIGURES];
        for (var i = 0; i < COUNT_FIGURES; i++)
        {
            figures[i] = Instantiate(_figurePrefab, transform.position, _figurePrefab.transform.rotation,transform).gameObject;
            figures[i].transform.localScale -= new Vector3(SCALE_FIGURE, SCALE_FIGURE, SCALE_FIGURE) * i;
            figures[i].transform.localPosition = _figurePrefab.transform.localPosition + new Vector3(i * 1.4f - i * 4 * SCALE_FIGURE, 0, 0);
            figures[i].GetComponent<Figure>().Strength = COUNT_FIGURES - i;
        }
        return figures;
    }
    public void ItsMyTurn(bool deactivateOthers)
    {
        IsMyTurn = true;
        if (deactivateOthers)
            if (this == II)
                My.IsMyTurn = false;
            else
                II.IsMyTurn = false;
    }
    public void ItsNotMyTurn(bool activateOthers)
    {
        IsMyTurn = false;
        if (activateOthers)
            if (this == II)
                My.IsMyTurn = true;
            else
                II.IsMyTurn = true;
    }
}
