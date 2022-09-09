using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.touches != null && Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    CheckClickAndActivate(touch.position);
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            CheckClickAndActivate(Input.mousePosition);
        }
    }

    private void CheckClickAndActivate(Vector2 position)
    {

        Debug.Log("CheckClickAndActivate: " + position);
        Camera cam = Camera.main;

        //Raycast depends on camera projection mode
        Vector2 origin = Vector2.zero;
        Vector2 dir = Vector2.zero;

        if (cam.orthographic)
        {
            origin = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            origin = ray.origin;
            dir = ray.direction;
        }

        RaycastHit2D hit = Physics2D.Raycast(origin, dir);

        //Check if we hit anything
        if (hit.collider && hit.collider.gameObject)
        {
            Debug.Log("Name: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.name.Equals("BackButton"))
            {
                SceneManager.LoadScene("MenuScene");
            }
            else
            {
                Square square = hit.collider.gameObject.GetComponent<Square>();
                square.Activate();
            }
            

        }
    }
}
