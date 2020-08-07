using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Instruction : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite spaceSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    [Header("Timeline Positions")]
    [SerializeField] private float totalWidth = 18f;
    [SerializeField] private float spaceHeight = -3f;
    [SerializeField] private float leftHeight = -4f;
    [SerializeField] private float rightHeight = -5f;
    [Header("Line Renderer")] 
    [SerializeField] private Material material;
    [SerializeField] private Gradient gradient;
    [SerializeField] private float thickness;
    
    
    private GameObject pairedButton;
    private LineRenderer lineRenderer;
    private LevelData levelData;

    private float height;

    private void Start()
    {
    }

    public void Initialize(float inButtonTime, string inButton, bool inButtonDown)
    {
        ButtonTime = inButtonTime;
        Button = inButton;
        ButtonDown = inButtonDown;
        levelData = FindObjectOfType<LevelData>();
        switch (inButton)
        {
            case "Jump":
                GetComponent<SpriteRenderer>().sprite = spaceSprite;
                height = spaceHeight;
                break;
            case "Left":
                GetComponent<SpriteRenderer>().sprite = leftSprite;
                height = leftHeight;
                break;
            case "Right":
                GetComponent<SpriteRenderer>().sprite = rightSprite;
                height = rightHeight;
                break;
        }
        Update();
    }

    public void Initialize(float inButtonTime, string inButton, bool inButtonDown, GameObject inGameObject)
    {
        ButtonTime = inButtonTime;
        Button = inButton;
        ButtonDown = inButtonDown;
        levelData = FindObjectOfType<LevelData>();
        pairedButton = inGameObject;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.colorGradient = gradient;
        lineRenderer.material = material;
        lineRenderer.widthMultiplier = thickness;
        switch (inButton)
        {
            case "Jump":
                GetComponent<SpriteRenderer>().sprite = spaceSprite;
                height = spaceHeight;
                break;
            case "Left":
                GetComponent<SpriteRenderer>().sprite = leftSprite;
                height = leftHeight;
                break;
            case "Right":
                GetComponent<SpriteRenderer>().sprite = rightSprite;
                height = rightHeight;
                break;
        }
        Update();
    }

    public float ButtonTime { get; private set; }

    public string Button { get; private set; }

    public bool ButtonDown { get; private set; }

    public bool Played { get; set; }

    private void Update()
    {
        transform.position = new Vector3((ButtonTime / levelData.FinalTime) * totalWidth - totalWidth / 2f , height, -1f);
        if (pairedButton)
        {
            lineRenderer.SetPositions(new Vector3[]{transform.position, pairedButton.transform.position});
        }
    }
}
