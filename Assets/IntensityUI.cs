using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntensityUI : MonoBehaviour
{
    public static IntensityUI intensityUI;
    public float vSliderValue;
    public GUIStyle style;
    public GUIStyle thumb;
    private void Awake()
    {
        intensityUI = this;
    }
    
    public void DrawIntensity()
    {
       

        int width = 100;
        int height = 25;
        int left = Screen.width / 2 - width / 2;
        int top = Screen.height - height - 10;
        
        // GUIStyle StatesLabel = new GUIStyle(GUI.skin.label)
        // {
        //     alignment = TextAnchor.MiddleLeft,
        //     margin = new RectOffset(),
        //     padding = new RectOffset(),
        //     fontSize = 15,
        //     fontStyle = FontStyle.Bold
        // };
        //
        // GUIStyle styleThumb = new GUIStyle(GUI.skin.label)
        // {
        //     alignment = TextAnchor.MiddleLeft,
        //     margin = new RectOffset(),
        //     padding = new RectOffset(),
        //     fontSize = 15,
        //     fontStyle = FontStyle.Bold
        // };


        if ( GUI.Button( new Rect( left, top, width, height ), "text" ) )
        {
            
        }
 
        // vSliderValue = GUI.VerticalSlider(new Rect(25, 25, 100, 30), vSliderValue, 10.0f, 0.0f, StatesLabel, styleThumb);
        vSliderValue = GUI.VerticalSlider(new Rect(25, 25, 50, 50), vSliderValue, 10.0f, 0.0f, style, thumb);
        // if (GUI.VerticalSlider())
        // {
        //     
        // }
    }

 
}
