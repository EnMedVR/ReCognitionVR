using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public GamePanel currentPanel = null;

    private List<GamePanel> panelHistory = new List<GamePanel>();

    private void Start()
    {
        SetupPanels();
    }

    private void SetupPanels()
    {
        GamePanel[] panels = GetComponentsInChildren<GamePanel>();

        foreach (GamePanel panel in panels)
        {
            panel.Setup(this);
        }
        currentPanel.Show();
    }


    public void GoToPrevious()
    {
        if (panelHistory.Count == 0)
        {
            return;
        }

        int lastIndex = panelHistory.Count - 1;
        SetCurrent(panelHistory[lastIndex]);
        panelHistory.RemoveAt(lastIndex);
    }

    public void PanelReset()
    {
        if (panelHistory.Count == 0)
        {
            return;
        }

        int lastIndex = panelHistory.Count - 1;
        SetCurrent(panelHistory[0]);
        panelHistory.RemoveAt(lastIndex);
    }

    public void SetCurrentWithHistory(GamePanel newPanel)
    {
        panelHistory.Add(currentPanel);
        SetCurrent(newPanel);
    }

    private void SetCurrent(GamePanel newPanel)
    {
        currentPanel.Hide();

        currentPanel = newPanel;
        currentPanel.Show();
    }
}
