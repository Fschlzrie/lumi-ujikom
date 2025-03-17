using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [System.Serializable]
    public class Panel
    {
        public string name;  // Nama panel
        public GameObject panelObject;  // GameObject panel
    }

    public List<Panel> panels; // List panel yang dikelola

    /// <summary>
    /// Menampilkan panel berdasarkan nama.
    /// Panel lain akan otomatis disembunyikan.
    /// </summary>
    /// <param name="panelName">Nama panel yang ingin ditampilkan</param>
    public void ShowPanel(string panelName)
    {
        foreach (var panel in panels)
        {
            if (panel.name == panelName)
            {
                panel.panelObject.SetActive(true);
                PauseGame();
            }
            else
            {
                panel.panelObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Menyembunyikan semua panel.
    /// </summary>
    public void HideAllPanels()
    {
        foreach (var panel in panels)
        {
            panel.panelObject.SetActive(false);
        }
        ResumeGame();
    }

    /// <summary>
    /// Menyembunyikan panel tertentu berdasarkan nama.
    /// </summary>
    /// <param name="panelName">Nama panel yang ingin disembunyikan</param>
    public void HidePanel(string panelName)
    {
        foreach (var panel in panels)
        {
            if (panel.name == panelName)
            {
                panel.panelObject.SetActive(false);
                break;
            }
        }
        if (!IsAnyPanelActive())
        {
            ResumeGame();
        }
    }

     /// <summary>
    /// Pause game (menghentikan waktu dalam game).
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resume game (melanjutkan waktu dalam game).
    /// </summary>
    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Mengecek apakah ada panel yang aktif.
    /// </summary>
    /// <returns>True jika ada panel aktif, false jika tidak ada</returns>
    private bool IsAnyPanelActive()
    {
        foreach (var panel in panels)
        {
            if (panel.panelObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
