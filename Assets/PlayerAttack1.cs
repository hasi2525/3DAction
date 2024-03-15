using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator; // Animatorコンポーネント
    private int attackCombo = 0; // 現在のコンボ段階を表す
    public float comboResetTime = 2.0f; // コンボをリセットする時間
    private float comboTimer = 0.0f; // コンボ状態の継続時間を計測するタイマー

    void Start()
    {
        animator = GetComponent<Animator>(); // Animatorコンポーネントを取得
    }

    void Update()
    {
        // コンボタイマーを更新
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                attackCombo = 0; // タイマーが切れたらコンボ状態をリセット
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            PerformComboAttack();
        }
    }

    void PerformComboAttack()
    {
        comboTimer = comboResetTime; // コンボタイマーをリセット
        attackCombo++;
        if (attackCombo > 3) attackCombo = 1; // 3回攻撃したら1回目に戻る

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
