//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    public Transform player; // プレイヤーのTransform
//    public float rotateSpeed = 5f; // カメラの回転速度
//    public float distanceFromPlayer = 2f; // プレイヤーからの距離
//    public Vector3 offset; // プレイヤーからカメラまでのオフセット

//    private float currentZoom = 10f; // 現在のズームレベル
//    private float pitch = 2f; // カメラの傾き（プレイヤーの上方から見下ろす角度）

//    void Start()
//    {
//        // カメラの初期位置を設定
//        offset = new Vector3(0, pitch, -distanceFromPlayer);
//        transform.position = player.position + offset;
//        transform.LookAt(player.position + Vector3.up * pitch);
//    }

//    void LateUpdate()
//    {
//        // プレイヤーの周りをカメラが回転する
//        RotateCamera();
//        CursorVisible();
//    }

//    void RotateCamera()
//    {
//        // マウスの入力に基づいて回転量を計算
//        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
//        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

//        // カメラをプレイヤーの周りで回転
//        transform.RotateAround(player.position, Vector3.up, horizontal);
//        transform.RotateAround(player.position, transform.right, -vertical);

//        // カメラを常にプレイヤーを向けるように調整
//        transform.LookAt(player.position + Vector3.up * pitch);

//        // カメラの位置をプレイヤーとのオフセットで更新
//        transform.position = player.position - transform.forward * distanceFromPlayer + Vector3.up * pitch;
//    }

//    private void CursorVisible()
//    {
//        //Altキーでマウスカーソルを消す
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
        //カメラ回転
        Rotate();
        CursorVisible();
    }

    void LateUpdate()
    {
        //追尾
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
    //カーソル表示、非表示切り替え
    private void CursorVisible()
    {
        //Altキーでマウスカーソルを消す
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