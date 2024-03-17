using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float rotateSpeed = 5;
    public float moveOffset = 0;
    private Vector3 targetPosition;
    void Awake()
    {
        targetPosition = player.transform.position;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //�J������]
        Rotate();
        CursorVisible();
    }

    void LateUpdate()
    {
        //�ǔ�
        Move();
    }

    private void Move()
    {
        transform.position += player.transform.position - targetPosition;
        targetPosition = player.transform.position;
    }
    private void Rotate()
    {
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed, Input.GetAxis("Mouse Y") * -rotateSpeed, 0);

        transform.RotateAround(player.transform.position, Vector3.up, angle.x);
        transform.RotateAround(player.transform.position, transform.right, angle.y);
    }
    //�J�[�\���\���A��\���؂�ւ�
    private void CursorVisible()
    {
        //Alt�L�[�Ń}�E�X�J�[�\��������
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}