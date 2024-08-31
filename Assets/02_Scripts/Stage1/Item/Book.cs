using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    Managers gm;
    UIManager ui;
    public GameObject bookShelf;
    public GameObject door;

    Vector3 movedPosition = new Vector3(-13.239f, 0.615f, 3.643f);

    // Start is called before the first frame update
    void Start()
    {
        gm = Managers.Instance;
        ui = UIManager.Instance;
    }

    private void OnMouseDown()
    {
        // 다이어리를 얻은 상태로 책을 클릭하면 책장이 옮겨지고 문이 나옴
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(gm.isCharacterNearby(this.gameObject))
            {
                if (gm.isGetDiary1 && hit.collider.gameObject == this.gameObject)
                {
                    door.SetActive(true);
                    bookShelf.transform.position = movedPosition;
                    gm.isBookshelfMoved = true;

                    ui.ShowDialog();
                }
            }
        }
    }
}