using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour
{
    // theory 3.encupsulation
    private Color m_color;
    public Color color
    {
        get {return m_color;}
        set {
            float r = Mathf.Clamp(value.r, 0f, 1f);
            float g = Mathf.Clamp(value.g, 0f, 1f);
            float b = Mathf.Clamp(value.b, 0f, 1f);

            m_color = new Color(r, g, b);
        }
    }
    public float moveSpeed = 3;
    public float rotateSpeed = 180;
    public float time = 3;
    public bool isClicked = false;
    private Vector3 moveDirection;
    private float bounce = 15f;
    private GameManager gameManager;
    private MeshRenderer cubeRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubeRenderer = GetComponent<MeshRenderer>();
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

    public void ChangeColor(Color newColor)
    {
        color = newColor;
        Material mat = cubeRenderer.material;
        mat.color = m_color;
    }

    protected virtual void ObjectClicked()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    // theory 4.abstraction
    private void CheckBounceLine()
    {
        Vector3 newPos = transform.position;
        if (transform.position.x > bounce || transform.position.x < -bounce)
        {
            moveDirection.x *= -1;
            newPos.x = transform.position.x > 0 ? bounce : -bounce;
        }

        if (transform.position.z > bounce || transform.position.z < -bounce)
        {
            moveDirection.z *= -1;
            newPos.z = transform.position.z > 0 ? bounce : -bounce;
        }

        if (!isClicked)
        {
            transform.forward = moveDirection;
        }
        transform.position = newPos;
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
        if (gameManager.clickedObject != null)
        {
            gameManager.clickedObject.isClicked = false;
        }

        gameManager.clickedObject = this;
        if (!isClicked)
        {
            isClicked = true;
        }
        gameManager.colorSlider.SetActive(true);
        gameManager.SetColorSlider(cubeRenderer.material.color);
    }


}
