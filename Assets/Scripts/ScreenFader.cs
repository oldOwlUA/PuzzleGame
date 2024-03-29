﻿using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{

    public enum FadeState { In, Out, Stop, InEnd, OutEnd }

    Texture colorTexture;
    Color fadeColor = Color.white;

    [HideInInspector] public float fadeBalance;

    public FadeState fadeState;

    public float fadeSpeed;     // Скорость стремления баланса

    public float fromInDelay;    // Мнимые задержки перед началом процесса затемнение/расцветания
    public float fromOutDelay;



    void Start()
    {

    }



    void Awake()
    {
        DefState();

        Texture2D nullTexture = new Texture2D(1, 1) as Texture2D;
        nullTexture.SetPixel(0, 0, Color.white);
        nullTexture.Apply();

        colorTexture = (Texture)nullTexture;

        fadeBalance = (1 + fromInDelay);



    }

    void Update()
    {

        fadeColor.a = fadeBalance;

        if (fadeBalance > (1 + fromInDelay))
        {
            fadeBalance = (1 + fromInDelay);
            fadeState = FadeState.InEnd;
        }

        if (fadeBalance < -(0 + fromOutDelay))
        {
            fadeBalance = -(0 + fromOutDelay);
            fadeState = FadeState.OutEnd;
        }


        switch (fadeState)
        {

            case FadeState.In:
                fadeBalance += Time.deltaTime * fadeSpeed;
                break;

            case FadeState.Out:
                fadeBalance -= Time.deltaTime * fadeSpeed;
                break;

            case FadeState.Stop:
                fadeBalance -= 0;
                break;

            case FadeState.InEnd:
                fadeBalance = (1 + fromInDelay);
                break;

            case FadeState.OutEnd:
                fadeBalance = -(0 + fromOutDelay);
                break;
        }

    }


    void OnGUI()
    {

        GUI.depth = -2;
        GUI.color = fadeColor;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), colorTexture, ScaleMode.StretchToFill, true);

    }

    public void DefState()
    {
        fadeSpeed = 0.5f;
        fadeState = FadeState.OutEnd;
    }

    

    public void On_Click()
    {

        
        if ((fadeState == FadeState.OutEnd))
        {
            fadeSpeed = 0.5f;
            fadeState = FadeState.In;
        }
     
    }

    public void Off_Click()
    {
        
        if (fadeState == FadeState.InEnd)
        {
            fadeSpeed = 0.5f;
            fadeState = FadeState.Out;
        }
        
    }

}
