using System.Collections;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    [SerializeField]
    private float attackCooldown = 0.5f;
    [SerializeField] 
    private Collider attackCollider;

    private MobStatus _status;

    public GameObject hitEffectPrefab; // �q�b�g�G�t�F�N�g��Prefab
    public float attackRange = 10f; // �U���̎˒�����
    void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    /// <summary>
    /// �U���\�ȏ�ԂȂ�U������
    /// </summary>
    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return;

        _status.GoToAttackStateIfPossible();
    }

    /// <summary>
    /// ����̑Ώۂ��U���͈͂ɓ���΍U������
    /// </summary>
    /// <param name="collider">�U���͈͂ɓ������Ώۂ̃R���C�_�[</param>
    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();

    }

    /// <summary>
    /// �U���J�n���ɌĂ΂�A�U���p�R���C�_�[��L��
    /// </summary>
    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }

    public void PerformAttack()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Player")); // "Enemy"��"Player"���C���[�݂̂�ΏۂƂ���
        RaycastHit hit;
        Vector3 attackOrigin = transform.position; // �U���̔�����
        Vector3 attackDirection = transform.forward; // �U���̕���

        if (Physics.Raycast(attackOrigin, attackDirection, out hit, attackRange, layerMask))
        {
            // ���C�L���X�g���q�b�g�����ʒu�ɏ����I�t�Z�b�g�������ăG�t�F�N�g�𐶐�
            GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitEffect, 0.5f);
        }
    }

    /// <summary>
    /// �U���R���C�_�[���ΏۂɃq�b�g������Ă΂�A�ΏۂɃ_���[�W��^����
    /// </summary>
    /// <param name="collider">�U�����q�b�g�����Ώۂ̃R���C�_�[</param>
    public void OnHitAttack(Collider collider)
    {
        MobStatus targetPlayer = collider.GetComponent<MobStatus>();
        if (targetPlayer != null)
        {
            targetPlayer.Damage(0);
            PerformAttack();
        }
    }

    /// <summary>
    /// �U�����I���������ɌĂ΂�A�U���p�R���C�_�[�𖳌��ɂ��A�N�[���_�E�����J�n����
    /// </summary>
    public void OnAttackFinished()
    {
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible();
    }
}