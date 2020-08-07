using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string Left = "Left";
    private const string Right = "Right";
    private const string Jump = "Jump";
    
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private GameObject instructionPrefab;
    [SerializeField] private GameObject canvas;
    [SerializeField] private int redDinoLayerNumber = 10;
    [SerializeField] private AudioClip jumpSound;
    
    private Rigidbody2D rigidBody;
    private List<GameObject> instructions;
    private Animator animator;
    private CapsuleCollider2D feet;
    private BoxCollider2D boxCollider2D;
    private GameObject aButtonDown;
    private GameObject dButtonDown;
    private LevelData levelData;

    //private float gameTimer = 0f;
    //private bool rewinding = false;
    private bool movingLeft;
    private bool movingRight;
    private bool jump;
    void Start()
    {
        instructions = new List<GameObject>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feet = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        levelData = FindObjectOfType<LevelData>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!levelData.Rewinding)
            {
                levelData.Rewinding = true;
                gameObject.layer = redDinoLayerNumber;
                animator.SetTrigger("Rewind");
            }
            else
            {
                FindObjectOfType<LevelController>().RestartLevel();
            }
            
        }
        if (!levelData.Rewinding)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var instruction = Instantiate(instructionPrefab);
                instruction.GetComponent<Instruction>().Initialize(levelData.GameTime, Jump, true);
                instructions.Add(instruction);
                Jumping();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                var instruction = Instantiate(instructionPrefab);
                instruction.GetComponent<Instruction>().Initialize(levelData.GameTime, Left, true);
                instructions.Add(instruction);
                aButtonDown = instruction;
                movingLeft = true;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                var instruction = Instantiate(instructionPrefab);
                instruction.GetComponent<Instruction>().Initialize(levelData.GameTime, Left, false, aButtonDown);
                instructions.Add(instruction);
                movingLeft = false;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                var instruction = Instantiate(instructionPrefab);
                instruction.GetComponent<Instruction>().Initialize(levelData.GameTime, Right, true);
                instructions.Add(instruction);
                dButtonDown = instruction;
                movingRight = true;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                var instruction = Instantiate(instructionPrefab);
                instruction.GetComponent<Instruction>().Initialize(levelData.GameTime, Right, false, dButtonDown);
                instructions.Add(instruction);
                movingRight = false;
            }
        }
        else
        {
            foreach (GameObject instructionObject in instructions)
            {
                var instruction = instructionObject.GetComponent<Instruction>();
                if (!instruction.Played)
                {
                    if (instruction.ButtonTime >= levelData.GameTime)
                    {
                        instruction.Played = true;
                        switch (instruction.Button)
                        {
                            case Jump:
                                Jumping();
                                break;
                            case Left:
                                movingRight = !instruction.ButtonDown;
                                break;
                            case Right:
                                movingLeft = !instruction.ButtonDown;
                                break;
                        }
                    }
                }
            }
        } 
        Walk();
        animator.SetBool("Jumping", !feet.IsTouchingLayers(groundLayers));
    }

    private void Jumping()
    {
        if (feet.IsTouchingLayers(groundLayers))
        {
            rigidBody.velocity = Vector2.up * jumpSpeed;
            AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position);
        }
        
    }

    private void Walk()
    {
        float runDirection = 0f;
        if (!feet.IsTouchingLayers(groundLayers) && boxCollider2D.IsTouchingLayers(groundLayers))
        {
            return;
        }
        if (movingRight ^ movingLeft)
        {
            if (movingLeft)
            {
                runDirection = -1f;
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            if (movingRight)
            {
                runDirection = 1f;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        rigidBody.velocity = new Vector2(runDirection * runSpeed, rigidBody.velocity.y);
    }
}
