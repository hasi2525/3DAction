using System.Collections;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    [SerializeField]
    private float attackCooldown = 0.5f;
    [SerializeField] 
    private Collider attackCollider;

    private MobStatus _status;

    public GameObject hitEffectPrefab; // ヒットエフェクトのPrefab
    public float attackRange = 10f; // 攻撃の射程距離
    void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    /// <summary>
    /// 攻撃可能な状態なら攻撃する
    /// </summary>
    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return;

        _status.GoToAttackStateIfPossible();
    }

    /// <summary>
    /// 特定の対象が攻撃範囲に入れば攻撃する
    /// </summary>
    /// <param name="collider">攻撃範囲に入った対象のコライダー</param>
    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();

    }

    /// <summary>
    /// 攻撃開始時に呼ばれ、攻撃用コライダーを有効
    /// </summary>
    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }

    public void PerformAttack()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Player")); // "Enemy"と"Player"レイヤーのみを対象とする
        RaycastHit hit;
        Vector3 attackOrigin = transform.position; // 攻撃の発生源
        Vector3 attackDirection = transform.forward; // 攻撃の方向

        if (Physics.Raycast(attackOrigin, attackDirection, out hit, attackRange, layerMask))
        {
            // レイキャストがヒットした位置に少しオフセットを加えてエフェクトを生成
            GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitEffect, 0.5f);
        }
    }

    /// <summary>
    /// 攻撃コライダーが対象にヒットしたら呼ばれ、対象にダメージを与える
    /// </summary>
    /// <param name="collider">攻撃がヒットした対象のコライダー</param>
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
    /// 攻撃が終了した時に呼ばれ、攻撃用コライダーを無効にし、クールダウンを開始する
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