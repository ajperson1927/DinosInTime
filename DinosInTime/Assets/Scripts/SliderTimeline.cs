using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTimeline : MonoBehaviour
{
    [SerializeField] private float height = -4f;
    [SerializeField] private float totalWidth = 18f;
    [SerializeField] private float zPos = -5f;

    private LevelData levelData;
    void Start()
    {
        levelData = FindObjectOfType<LevelData>();
    }
    void Update()
    {
        transform.position = new Vector3(levelData.GameTime / levelData.FinalTime * totalWidth - totalWidth / 2f, height, zPos);
    }
}
