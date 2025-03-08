using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static bool isNoteOpen = false;

    void Update() {
        int width = Screen.width;
        int height = Screen.height;

        // Pastikan resolusi selalu genap
        if (width % 2 != 0) width++; 
        if (height % 2 != 0) height++; 

        Screen.SetResolution(width, height, false); // false = windowed mode
    }

}