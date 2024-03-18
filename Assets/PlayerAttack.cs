using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public int maxCombo = 3;
    private int currentCombo = 0;
    public float comboResetTime = 2f;
    private float comboTimer;

    public Transform attackPoint; // �U�������_
    public float attackRange = 0.5f; // �U���͈�
    public LayerMask enemyLayers; // �G�𔻒肷�郌�C���[
    public int attackDamage = 10; // �U���_���[�W

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                currentCombo = 0;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        comboTimer = comboResetTime;
        currentCombo = (currentCombo % maxCombo) + 1;

        // �g���K�[���Z�b�g���ăA�j���[�V�������Đ�
        animator.SetTrigger($"Attack{currentCombo}Trigger");

        // �U���͈͓��̓G�����o���ă_���[�W��^���鏈����ǉ�
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            MobStatus enemyStatus = enemy.GetComponent<MobStatus>();
            if (enemyStatus != null)
            {
                // �G��MobStatus�R���|�[�l���g��Damage���\�b�h���Ăяo���ă_���[�W��^����
                enemyStatus.Damage(attackDamage);
            }
        }
    }

    // �U���͈͂��G�f�B�^��Ŏ��o������i�f�o�b�O�p�j
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

//{
//    private Animator animator; // Animator�R���|�[�l���g
//    private int attackCombo = 0; // ���݂̃R���{�i�K��\��
//    public float comboResetTime = 2.0f; // �R���{�����Z�b�g���鎞��
//    private float comboTimer = 0.0f; // �R���{��Ԃ̌p�����Ԃ��v������^�C�}�[

//    void Start()
//    {
//        animator = GetComponent<Animator>(); // Animator�R���|�[�l���g���擾
//    }

//    void Update()
//    {
//        // �R���{�^�C�}�[���X�V
//        if (comboTimer > 0)
//        {
//            comboTimer -= Time.deltaTime;
//            if (comboTimer <= 0)
//            {
//                attackCombo = 0; // �^�C�}�[���؂ꂽ��R���{��Ԃ����Z�b�g
//            }
//        }

//        if (Input.GetButtonDown("Fire1"))
//        {
//            PerformComboAttack();
//        }
//    }

//    void PerformComboAttack()
//    {
//        comboTimer = comboResetTime; // �R���{�^�C�}�[�����Z�b�g
//        attackCombo++;
//        if (attackCombo > 3) attackCombo = 1; // 3��U��������1��ڂɖ߂�

//        switch (attackCombo)
//        {
//            case 1:
//                animator.SetTrigger("Attack");
//                break;
//            case 2:
//                animator.SetTrigger("SecondAttack");
//                break;
//            case 3:
//                animator.SetTrigger("ThirdAttack");
//                break;
//        }
//    }
//}