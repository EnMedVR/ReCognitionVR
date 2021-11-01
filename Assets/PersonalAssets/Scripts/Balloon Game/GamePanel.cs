using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    private Canvas canvas = null;
    private GameMenuManager menuManager = null;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Setup(GameMenuManager menuManager)
    {
        this.menuManager = menuManager;
        Hide();
    }

    public void Show()
    {
        canvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        canvas.gameObject.SetActive(false);
    }
}
