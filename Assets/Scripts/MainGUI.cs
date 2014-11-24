using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour {
    private Rect _windowRect = new Rect(30,30, Screen.width / 3f, Screen.height);
    void OnGUI()
    {
        _windowRect = GUI.Window(0, _windowRect, WindowFunction, "Buttons");
    }

    void WindowFunction(int windowID)
    {
        if (GUI.Button(new Rect(0,0,50,50), "Start Game"))
        {
            Debug.Log("Start Game");
            GameObject carcontrollerthing = GameObject.Find("GameControllers/CardController");
            CardController cardDealer = carcontrollerthing.GetComponent<CardController>();
            cardDealer.DealCards();
        }
    }
}
