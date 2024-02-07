using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public GameObject fan;
    public float fanRotateSpeed = 10f;

    [Header("Camera Positions")]
    public Transform cameraPosition1;
    public Transform cameraPosition2;
    public Transform cameraPosition3;
    public Transform cameraPosition4;

    [Header("Interactions")]
    public GameObject radio;
    public GameObject doorBaranda;
    public GameObject doorMain;
    public GameObject doorToilet;
    public GameObject switchLight;
    public GameObject wardrobe;
    public GameObject light;
    public Material lightMat;

    [Header("Cat Positions")]
    public Animator roomAnimator;
    public Animator catAnimator;
    public Animator wardrobeAnimator;
    public GameObject cat;
    public Transform catMainDoorPos;
    public Transform catBathroomPos;
    public Transform catWardrobePos;
    public Transform catBarandaPos;

    public float moveSpeed = 5f;
    public float rotationSpeed = 45f;
    public GameObject bed;
    public GameObject mirror;
    public GameObject table;
    public GameObject bookshelf;

    public GameObject activeObject;

    public GameObject noObjectWarning;

    public Vector3 bedPosition = new Vector3(-2.418f,0.002124447f,-2.299f);
    public Vector3 tablePosition = new Vector3(-1.817082f, 0f, -0.3899713f);
    public Vector3 bookShelfPosition = new Vector3(-1.091421f, 0f, -3.31982f);
    public Vector3 mirrorPosition = new Vector3(-2.652724f, -0.001940844f, -0.1574409f);

    private bool editMode = true;
    public GameObject editMenu;
    public GameObject playMenu;
    public GameObject resetButton;
    public GameObject removeButton;
    public GameObject changeButton;

    // Start is called before the first frame update
    void Start()
    {
        lightMat.SetColor("_EmissionColor", Color.white);
        lightMat.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        fan.transform.Rotate(Vector3.forward, fanRotateSpeed * Time.deltaTime);

        if (editMode)
        {
            editMenu.SetActive(true);
            playMenu.SetActive(false);
            camera.transform.GetComponent<MouseLook>().enabled = false;
            player.SetActive(false);
            cat.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                camera.transform.position = cameraPosition1.position;
                camera.transform.rotation = cameraPosition1.rotation;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                camera.transform.position = cameraPosition2.position;
                camera.transform.rotation = cameraPosition2.rotation;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                camera.transform.position = cameraPosition3.position;
                camera.transform.rotation = cameraPosition3.rotation;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                camera.transform.position = cameraPosition4.position;
                camera.transform.rotation = cameraPosition4.rotation;
            }

            if (activeObject != null)
            {
                noObjectWarning.SetActive(false);
                resetButton.SetActive(true);
                removeButton.SetActive(true);
                if (activeObject.transform.GetComponent<ObjectChangeScript>().childCount > 1)
                {
                    changeButton.SetActive(true);
                }
                else
                {
                    changeButton.SetActive(false);
                }

                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                float rotateInput = Input.GetAxis("Rotate");

                // Calculate movement direction
                Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
                float rotationAmount = rotateInput * rotationSpeed * Time.deltaTime;

                // Normalize the movement vector to ensure consistent speed in all directions
                movement.Normalize();

                // Move the object based on the input
                activeObject.transform.Translate(movement * moveSpeed * Time.deltaTime);
                activeObject.transform.Rotate(Vector3.up, rotationAmount);

            }
            else
            {
                noObjectWarning.SetActive(true);
                resetButton.SetActive(false);
                removeButton.SetActive(false);
                changeButton.SetActive(false);
            }
        }
        else
        {
            editMenu.SetActive(false);
            playMenu.SetActive(true);
            player.SetActive(true);
            cat.SetActive(true);
            camera.transform.GetComponent<MouseLook>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                camera.transform.parent = null;
                camera.transform.GetComponent<MouseLook>().enabled = false;
                camera.transform.position = cameraPosition1.position;
                camera.transform.rotation = cameraPosition1.rotation;
                editMode = true;
            }

            //Player position check
            if(player.transform.position.x > -2.5f)
            {
                roomAnimator.SetBool("BarandaOff", true);
                catAnimator.SetBool("Baranda_Anim_Off", true);
            }

            if(player.transform.position.x > 0 || player.transform.position.x < -4.3f || player.transform.position.z > 0 || player.transform.position.z < -3.47f || 
                player.transform.position.y < -0.5)
            {
                player.transform.position = new Vector3(-0.448f, 1.3f, -0.422f);
                player.transform.rotation = Quaternion.Euler(0f, -145f, 0f);
            }
        }
    }

    public void WalkButton()
    {
        editMode = false;
        camera.transform.parent = player.transform;
        camera.transform.localPosition = new Vector3(0f, 0.65f, 0f);
        player.transform.position = new Vector3(-0.448f, 1.3f, -0.422f);
        player.transform.rotation = Quaternion.Euler(0f, -145f, 0f);
    }

    public void BedButton()
    {
        activeObject = bed;
    }
    public void TableButton()
    {
        activeObject = table;
    }
    public void BookShelfButton()
    {
        activeObject = bookshelf;
    }
    public void MirrorButton()
    {
        activeObject = mirror;
    }

    public void ResetButton()
    {
        if(activeObject == bed)
        {
            activeObject.transform.position = bedPosition;
            activeObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }else if (activeObject == table)
        {
            activeObject.transform.position = tablePosition;
            activeObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }else if (activeObject == mirror)
        {
            activeObject.transform.position = mirrorPosition;
            activeObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }else if (activeObject == bookshelf)
        {
            activeObject.transform.position = bookShelfPosition;
            activeObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    public void AddRemoveButton()
    {
        if (activeObject.activeSelf)
        {
            activeObject.SetActive(false);
        }
        else
        {
            activeObject.SetActive(true);
        }
    }

    public void ChangeButton()
    {
        activeObject.transform.GetComponent<ObjectChangeScript>().ChangedObject();
    }

    public void Interaction(GameObject obj) 
    {
        if(obj == doorBaranda)
        {
            if (roomAnimator.GetCurrentAnimatorStateInfo(0).IsName("Baranda_Open"))
            {

            }
            else
            {
                roomAnimator.Play("Baranda_Open");
                roomAnimator.SetBool("BarandaOff", false);
                cat.transform.position = catBarandaPos.position;
                cat.transform.rotation = catBarandaPos.rotation;
                catAnimator.SetBool("Baranda_Anim_Off", false);
                catAnimator.Play("CatBaranda_anim");
            }
            
        }
        else if(obj == doorMain)
        {
            if (roomAnimator.GetCurrentAnimatorStateInfo(0).IsName("MainDoor_Open"))
            {

            }
            else
            {
                roomAnimator.Play("MainDoor_Open");
                cat.transform.position = catMainDoorPos.position;
                cat.transform.rotation = catMainDoorPos.rotation;
                catAnimator.Play("CatMainDoor_anim");
            }
        }
        else if(obj == doorToilet)
        {
            if (roomAnimator.GetCurrentAnimatorStateInfo(0).IsName("BathRoomOpen"))
            {

            }
            else
            {
                roomAnimator.Play("BathRoomOpen");
                cat.transform.position = catBathroomPos.position;
                cat.transform.rotation = catBathroomPos.rotation;
                catAnimator.Play("CatBathroomDoor_anim");
            }
        }
        else if(obj == switchLight)
        {
            if (light.activeSelf)
            {
                light.SetActive(false);
                lightMat.SetColor("_EmissionColor", Color.black);
                lightMat.EnableKeyword("_EMISSION");
            }
            else
            {
                light.SetActive(true);
                lightMat.SetColor("_EmissionColor", Color.white);
                lightMat.EnableKeyword("_EMISSION");
            }
        }
        else if(obj == wardrobe)
        {
            if (wardrobeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Wardrobe_Open"))
            {

            }
            else
            {
                wardrobeAnimator.Play("Wardrobe_Open");
                cat.transform.position = catWardrobePos.position;
                cat.transform.rotation = catWardrobePos.rotation;
                catAnimator.Play("CatWardrobe_Anim");
            }
        }else if(obj == radio)
        {
            if (radio.transform.GetComponent<AudioSource>().enabled)
            {
                radio.transform.GetComponent<AudioSource>().enabled = false;
            }
            else
            {
                radio.transform.GetComponent<AudioSource>().enabled = true;
            }
        }
    }
}
