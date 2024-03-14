using UnityEngine;

/// <summary>
/// �v���C���[�̑�����Ǘ�����N���X�B
/// </summary>
public class PlayerController3 : MonoBehaviour
{
    //Animator�R���|�[�l���g
    [SerializeField]
    private Animator animator;
    // �v���C���[�̊�{�ړ����x
    [SerializeField]
    private float moveSpeed = 3;

    private Transform _transform;
    //private Vector3 _moveVelocity; 
    // �v���C���[�̏�Ԃ��Ǘ�����R���|�[�l���g
    private PlayerStatus _status; 
    // �G�ւ̍U�����Ǘ�����R���|�[�l���g
    private MobAttack _attack; 
    private Rigidbody rb; 


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
    }

    private void Update()
    {
        AttackMove();
    }

    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// �v���C���[�̈ړ�����
    /// </summary>
    private void Move()
    {
        // �ړ����\�Ȃ�ړ��������s��
        if (_status.IsMoveble)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            // �L�[�{�[�h����̓��͂��擾
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // �J�����̐��ʕ����Ɋ�Â��āA�v���C���[�̌�����ς���
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // �v���C���[���ړ������Ɍ�����
                _transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                // �����Ă��邩�ǂ����ɉ����Ĉړ����x��ύX
                float currentSpeed = isRunning ? moveSpeed * 2 : moveSpeed;

                // �v���C���[���ړ�������
                rb.velocity = new Vector3(moveDir.x * currentSpeed, rb.velocity.y, moveDir.z * currentSpeed);

                // �ړ����x�Ƒ����Ă����Ԃ�animator�ɔ��f������
                animator.SetFloat("MoveSpeed", direction.magnitude * (isRunning ? 2 : 1));
                animator.SetBool("RunMoveSpeed", isRunning);
            }
            else
            {
                // ���͂��Ȃ��ꍇ�͓������~�߂�
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                animator.SetFloat("MoveSpeed", 0);
                animator.SetBool("RunMoveSpeed", false);
            }
        }
    }

    /// <summary>
    /// �U������
    /// </summary>
    void AttackMove()
    {
        // �U���{�^���������ꂽ��U��
        if (Input.GetButtonDown("Fire1"))
        {
            //�U���A�j���[�V�������ȂǂɃv���C���[�����������Ȃ��悤�ɂ���
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            _attack.AttackIfPossible();
        }
    }
}