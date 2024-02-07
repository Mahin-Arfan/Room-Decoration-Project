using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public Camera camera;
    public GameManager gameManager;
    private float interactTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        interactTime += Time.deltaTime;
        if (Input.GetButton("Fire1") && interactTime > 0.5f)
        {
            interactTime = 0f;
            RaycastHit hit;

            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 1f))
            {
                Debug.Log(hit.transform.name);
                GameObject hitObject = hit.collider.gameObject;
                gameManager.Interaction(hitObject);
            }
        }
    }
}
