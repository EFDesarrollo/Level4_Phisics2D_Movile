using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    MusicController controller;

    public void OnClicGetAndPlayAction()
    {
        controller = GameObject.FindGameObjectsWithTag("Music")[0].GetComponent<MusicController>();
        controller.OnPlayDoPerformance();
    }
    public void OnClicGetAndPlayMenu()
    {
        controller = GameObject.FindGameObjectsWithTag("Music")[0].GetComponent<MusicController>();
        controller.OnStartDoPerformance();
    }
}
