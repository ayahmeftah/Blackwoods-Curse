using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject pnl_options;
    public GameObject pnl_title;
    public GameObject pnl_settings;
    public GameObject pnl_credits; 

    public void ShowMainMenu()
    {
        pnl_settings.SetActive(false);
        pnl_credits.SetActive(false);
        pnl_options.SetActive(true);
        pnl_title.SetActive(true);
    }

    public void ShowSettings()
    {
        pnl_options.SetActive(false);
        pnl_title.SetActive(false);
        pnl_settings.SetActive(true);
        pnl_credits.SetActive(false);
    }

    public void ShowCredits()
    {
        pnl_options.SetActive(false);
        pnl_title.SetActive(true);
        pnl_settings.SetActive(false);
        pnl_credits.SetActive(true);
    }
}
