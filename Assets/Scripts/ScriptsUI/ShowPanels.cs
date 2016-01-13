using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsTint;
	public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject pausePanel;

    public void ShowGamePanel()
    {
        gamePanel.SetActive(true);
    }

    public void HideGamePanel()
    {
        gamePanel.SetActive(false);
    }


    public void ShowOptionsPanel()
	{
		optionsTint.SetActive(true);
	}

	public void HideOptionsPanel()
	{
		optionsTint.SetActive(false);
	}

	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
	
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}

	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}
}
