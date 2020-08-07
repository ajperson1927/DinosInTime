using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] Color normalColor = Color.blue;
    [SerializeField] Color rewindColor = Color.red;
    
    private SpriteRenderer spriteRenderer;
    private LevelData levelData;

    private bool normalChanged;
    private bool rewindChanged;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        levelData = FindObjectOfType<LevelData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelData.Rewinding)
        {
            if (!rewindChanged)
            {
                rewindChanged = true;
                normalChanged = false;
                spriteRenderer.color = rewindColor;
                transform.localScale = new Vector3(-1f,transform.localScale.y, transform.localScale.z);
            }
            float newZ = transform.eulerAngles.z + rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newZ);
            
        }
        else
        {
            if (!normalChanged)
            {
                normalChanged = true;
                rewindChanged = false;
                spriteRenderer.color = normalColor;
                transform.localScale = new Vector3(1f,transform.localScale.y, transform.localScale.z);
            }
            float newZ = transform.eulerAngles.z - rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newZ);
        }
    }
}
