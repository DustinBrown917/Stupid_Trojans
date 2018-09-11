using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour {

    [SerializeField]
    private GameObject homeMenu;

    public void ShowHomeMenu()
    {
        GameManager.Instance.PauseGame();
        homeMenu.SetActive(true);
    }
}
