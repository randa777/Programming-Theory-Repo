using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Cube clickedObject;
    public GameObject colorSlider;
    public GameObject[] listObj;
    private Slider sliderR;
    private Slider sliderG;
    private Slider sliderB;
    void Start()
    {
        sliderR = colorSlider.transform.Find("SliderR").GetComponent<Slider>();
        sliderG = colorSlider.transform.Find("SliderG").GetComponent<Slider>();
        sliderB = colorSlider.transform.Find("SliderB").GetComponent<Slider>();
        sliderR.onValueChanged.AddListener(SetRColor);
        sliderG.onValueChanged.AddListener(SetGColor);
        sliderB.onValueChanged.AddListener(SetBColor);
        colorSlider.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            DetectClick();
        }
    }

    void DetectClick()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (clickedObject != null)
            {
                // object is selected
                if (!Physics.Raycast(ray, out hit) || !hit.collider.CompareTag("Object"))
                {
                    // none is clicked
                    clickedObject.isClicked = false;
                    clickedObject = null;
                    colorSlider.SetActive(false);
                }
            }
        }
    }
    
    void SetRColor(float colorR)
    {
        Color newColor = clickedObject.color;
        newColor.r = colorR;
        ChangeColor(newColor);

    }

    void SetGColor(float colorG)
    {
        Color newColor = clickedObject.color;
        newColor.g = colorG;
        ChangeColor(newColor);

    }

    void SetBColor(float colorB)
    {
        Color newColor = clickedObject.color;
        newColor.b = colorB;
        ChangeColor(newColor);

    }

    // update object color
    void ChangeColor(Color newColor)
    {
        if (clickedObject != null)
        {
            clickedObject.ChangeColor(newColor);
        }
    }

    // update slider value from object color
    public void SetColorSlider(Color color)
    {
        sliderR.value = color.r;
        sliderG.value = color.g;
        sliderB.value = color.b;
    }

    public void AddObject()
    {
        Vector3 pos = Vector3.zero;
        pos.y = 5;

        int index = Random.Range(0, listObj.Length);
        Instantiate(listObj[index], pos, listObj[index].transform.rotation);
    }
}