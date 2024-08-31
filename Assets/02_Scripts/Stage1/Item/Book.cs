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
        // ���̾�� ���� ���·� å�� Ŭ���ϸ� å���� �Ű����� ���� ����
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