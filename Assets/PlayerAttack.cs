using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public int maxCombo = 3;
    private int currentCombo = 0;
    public float comboResetTime = 2f;
    private float comboTimer;

    public Transform attackPoint; // 攻撃発生点
    public float attackRange = 0.5f; // 攻撃範囲
    public LayerMask enemyLayers; // 敵を判定するレイヤー
    public int attackDamage = 10; // 攻撃ダメージ

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

        // トリガーをセットしてアニメーションを再生
        animator.SetTrigger($"Attack{currentCombo}Trigger");

        // 攻撃範囲内の敵を検出してダメージを与える処理を追加
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            MobStatus enemyStatus = enemy.GetComponent<MobStatus>();
            if (enemyStatus != null)
            {
                // 敵のMobStatusコンポーネントのDamageメソッドを呼び出してダメージを与える
                enemyStatus.Damage(attackDamage);
            }
        }
    }

    // 攻撃範囲をエディタ上で視覚化する（デバッグ用）
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

//{
//    private Animator animator; // Animatorコンポーネント
//    private int attackCombo = 0; // 現在のコンボ段階を表す
//    public float comboResetTime = 2.0f; // コンボをリセットする時間
//    private float comboTimer = 0.0f; // コンボ状態の継続時間を計測するタイマー

//    void Start()
//    {
//        animator = GetComponent<Animator>(); // Animatorコンポーネントを取得
//    }

//    void Update()
//    {
//        // コンボタイマーを更新
//        if (comboTimer > 0)
//        {
//            comboTimer -= Time.deltaTime;
//            if (comboTimer <= 0)
//            {
//                attackCombo = 0; // タイマーが切れたらコンボ状態をリセット
//            }
//        }

//        if (Input.GetButtonDown("Fire1"))
//        {
//            PerformComboAttack();
//        }
//    }

//    void PerformComboAttack()
//    {
//        comboTimer = comboResetTime; // コンボタイマーをリセット
//        attackCombo++;
//        if (attackCombo > 3) attackCombo = 1; // 3回攻撃したら1回目に戻る

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