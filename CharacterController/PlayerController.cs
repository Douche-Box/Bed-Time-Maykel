using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    NewControls newControls;
    public Transform playerTransform;
    public Rigidbody playerRigidbody;

    public GameObject escapeMenu;
    public GameObject canvas2;
    public bool wantPause;
    public GameObject camsObject;

    [Header("statistics")]
    #region
    [SerializeField]
    private float maxWalkSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private Vector3 move;
    [SerializeField]
    #endregion
    [Header("Inputs")]
    #region
    public Vector2 walkInput;
    public bool placeInput;
    public bool interactInput;
    public bool rotateInputR;
    public bool rotateInputL;
    public bool startwaveInput;
    public bool escapeInput;
    private object keycode;


    #endregion

    private void OnEnable()
    {
        if (newControls == null)
        {
            newControls = new NewControls();

            newControls.Movement.Walking.performed += w => walkInput = w.ReadValue<Vector2>();
            newControls.Movement.Walking.canceled += i => walkInput = new Vector2(0, 0);

            newControls.Movement.Escape.performed += esc => escapeInput = true;
            newControls.Movement.Escape.canceled += esc => escapeInput = false;

            newControls.Building.Placement.performed += e => placeInput = true;
            newControls.Building.Placement.canceled += f => placeInput = false;

            newControls.Building.Interacting.performed += es => interactInput = true;
            newControls.Building.Interacting.canceled += ef => interactInput = false;

            newControls.Building.RotatingR.performed += er => rotateInputR = true;
            newControls.Building.RotatingR.canceled += ex => rotateInputR = false;

            newControls.Building.RotatingL.performed += ers => rotateInputL = true;
            newControls.Building.RotatingL.canceled += erx => rotateInputL = false;

            newControls.Building.StartWave.performed += aj => startwaveInput = true;
            newControls.Building.StartWave.canceled += ajs => startwaveInput = false;
        }
        newControls.Enable();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        EscapeMenu();
    }

    private void Movement()
    {
        move.x = walkInput.x;
        move.z = walkInput.y;

        playerTransform.Translate(move * walkSpeed * Time.deltaTime);
    }

    public void EscapeMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (wantPause)
            {
                resume();
            }
            else
            {
                wantPause = true;
                escapeMenu.SetActive(wantPause);
                canvas2.SetActive(!wantPause);
                camsObject.GetComponent<Cams>().inMenu = true;
            }
        }

        if (!wantPause)
        {
            Time.timeScale = 1;
        }
        else if (wantPause)
        {
            Time.timeScale = 0;
        }
    }

    public void resume()
    {
        escapeMenu.SetActive(false);
        camsObject.GetComponent<Cams>().inMenu = false;
        canvas2.SetActive(true);
        wantPause = false;
    }
}