using UnityEngine;

// ChatGPT
public class GameManager : MonoBehaviour
{
    public Cube clickedObject;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            DetectClick();
        }
    }

    void DetectClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (clickedObject != null)
        {
            if (!Physics.Raycast(ray, out hit) || !hit.collider.CompareTag("Object"))
            {
                // 何にも当たらなかった場合（空間がクリックされた）
                clickedObject.isClicked = false;
                clickedObject = null;
            }
        }
    }
}