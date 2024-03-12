//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    public Transform player; // �v���C���[��Transform
//    public float rotateSpeed = 5f; // �J�����̉�]���x
//    public float distanceFromPlayer = 2f; // �v���C���[����̋���
//    public Vector3 offset; // �v���C���[����J�����܂ł̃I�t�Z�b�g

//    private float currentZoom = 10f; // ���݂̃Y�[�����x��
//    private float pitch = 2f; // �J�����̌X���i�v���C���[�̏�����猩���낷�p�x�j

//    void Start()
//    {
//        // �J�����̏����ʒu��ݒ�
//        offset = new Vector3(0, pitch, -distanceFromPlayer);
//        transform.position = player.position + offset;
//        transform.LookAt(player.position + Vector3.up * pitch);
//    }

//    void LateUpdate()
//    {
//        // �v���C���[�̎�����J��������]����
//        RotateCamera();
//        CursorVisible();
//    }

//    void RotateCamera()
//    {
//        // �}�E�X�̓��͂Ɋ�Â��ĉ�]�ʂ��v�Z
//        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
//        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

//        // �J�������v���C���[�̎���ŉ�]
//        transform.RotateAround(player.position, Vector3.up, horizontal);
//        transform.RotateAround(player.position, transform.right, -vertical);

//        // �J��������Ƀv���C���[��������悤�ɒ���
//        transform.LookAt(player.position + Vector3.up * pitch);

//        // �J�����̈ʒu���v���C���[�Ƃ̃I�t�Z�b�g�ōX�V
//        transform.position = player.position - transform.forward * distanceFromPlayer + Vector3.up * pitch;
//    }

//    private void CursorVisible()
//    {
//        //Alt�L�[�Ń}�E�X�J�[�\��������
//        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
//        {
//            Cursor.visible = true;
//            Cursor.lockState = CursorLockMode.None;
//        }
//        else if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
//        {
//            Cursor.visible = false;
//            Cursor.lockState = CursorLockMode.Locked;
//        }
//    }
//}


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