using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

// theory 1.Inheritance
public class Cube : MonoBehaviour
{
    public Color color { get; private set; } // theorr 3.encupsulation
    public float moveSpeed = 3;
    public float rotateSpeed = 180;
    public float time = 3;
    public bool isClicked = false;
    private Vector3 moveDirection;
    private float bounce = 15f;
    private GameManager gameManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(ObjectTurn());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClicked)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            CheckBounceLine();
        }
        else
        {
            ObjectClicked();
        }
        
    }

    // theory 2.polymorphism
    void ObjectClicked()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    // theory 4.abstraction
    void CheckBounceLine()
    {
        if (transform.position.x > bounce || transform.position.x < -bounce)
        {
            moveDirection.x *= -1;
        }

        if (transform.position.z > bounce || transform.position.z < -bounce)
        {
            moveDirection.z *= -1;
        }
        transform.forward = moveDirection;
    }

    IEnumerator ObjectTurn()
    {
        while (true)
        {
            if (!isClicked)
            {
                moveDirection = new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)).normalized;
                transform.forward = moveDirection;
            }
            yield return new WaitForSeconds(time);
        }
    }

    void OnMouseDown()
    {
        gameManager.clickedObject = this;
        if (!isClicked)
        {
            isClicked = true;
        }
    }


}
