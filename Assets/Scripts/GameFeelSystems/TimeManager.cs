using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.05f;
    public float slowDownLength = 2f;
    private bool isStoped = false;

    private void Update()
    {
        if (isStoped) return;
        Time.timeScale += (1f / slowDownLength)*Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
    public void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
    public void PauseTime()
    {
        Time.timeScale = 0f;
        isStoped = true;
    }
    public void PlayTime()
    {
        DoSlowMotion();
        isStoped = false;
    }
}
