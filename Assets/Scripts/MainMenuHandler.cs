using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class MainMenuHandler : MonoBehaviour {
    public void StartPractice()
    {
        Application.LoadLevel("training"); 
    }
}
