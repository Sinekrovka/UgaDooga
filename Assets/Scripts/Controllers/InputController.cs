using System;
using TMPro;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Action<int, int, bool> gridParams;
    public Action<bool> mixed;
    [SerializeField] private TextMeshProUGUI textX;
    [SerializeField] private TextMeshProUGUI textY;
    
    private int startPositionX = 4;
    private int startPositionY = 4;


    private void Start()
    {
        gridParams?.Invoke(startPositionX, startPositionY, false);
    }

    public void Spawn()
    {
        startPositionX = Convert.ToInt32(textX.text);
        startPositionY = Convert.ToInt32(textY.text);
        gridParams?.Invoke(startPositionX, startPositionY, false);
    }

    public void Mixed()
    {
        mixed?.Invoke(true);
    }
}
