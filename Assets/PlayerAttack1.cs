using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator; // Animator�R���|�[�l���g
    private int attackCombo = 0; // ���݂̃R���{�i�K��\��
    public float comboResetTime = 2.0f; // �R���{�����Z�b�g���鎞��
    private float comboTimer = 0.0f; // �R���{��Ԃ̌p�����Ԃ��v������^�C�}�[

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator�R���|�[�l���g���擾
    }

    void Update()
    {
        // �R���{�^�C�}�[���X�V
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                attackCombo = 0; // �^�C�}�[���؂ꂽ��R���{��Ԃ����Z�b�g
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            PerformComboAttack();
        }
    }

    void PerformComboAttack()
    {
        comboTimer = comboResetTime; // �R���{�^�C�}�[�����Z�b�g
        attackCombo++;
        if (attackCombo > 3) attackCombo = 1; // 3��U��������1��ڂɖ߂�

        switch (attackCombo)
        {
            case 1:
                animator.SetTrigger("Attack");
                break;
            case 2:
                animator.SetTrigger("SecondAttack");
                break;
            case 3:
                animator.SetTrigger("ThirdAttack");
                break;
        }
    }
}
