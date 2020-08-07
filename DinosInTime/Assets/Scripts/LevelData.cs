using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public float GameTime = Mathf.Epsilon;
    public float FinalTime = Mathf.Epsilon;
    public bool Rewinding = false;
    void Start()
    {
        
    }
    void Update()
    {
        if (!Rewinding)
        {
            GameTime += Time.deltaTime;
            FinalTime = GameTime;
        }
        else
        {
            if (GameTime > 0f)
            {
                GameTime -= Time.deltaTime;
            }
            else
            {
                GameTime = 0f;
            }
        }
    }
}
