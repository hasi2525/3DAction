using UnityEngine;

/// <summary>
/// プレイヤーの基本的な状態を管理する抽象クラス
/// </summary>
public abstract class MobStatus : MonoBehaviour
{
    // 状態定義
    protected enum StateEnum
    {
        // 通常
        Normal,
        // 攻撃中
        Attack,
        //回避中
        Rolling,
        // 死亡
        Die
    }

    // ライフの最大値
    [SerializeField] 
    private float lifeMax = 10;

    // 移動可能か
    public bool IsMoveble =>   StateEnum.Normal == _state;

    // 攻撃可能か
    public bool IsAttackable => StateEnum.Normal == _state;

    //　回避中か
    public bool IsRolling => StateEnum.Normal == _state;

    // ライフの最大値
    public float LifeMax => lifeMax;

    // ライフの値
    public float Life => _life;

    protected Animator _animator;
    private StateEnum _state = StateEnum.Normal;
    private float _life;

    protected virtual void Start()
    {
        // 初期はライフ満タン
        _life = lifeMax;
        _animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// キャラクターが倒れた時に呼ばれるメソッド
    /// </summary>
    protected virtual void OnDie()
    {
        // オーバーライドして特定のアクションを実行する
    }

    /// <summary>
    /// 指定値のダメージを受けるメソッド
    /// </summary>
    /// <param name="damage">受けるダメージの量</param>
    public void Damage(int damage)
    {
        if (_state == StateEnum.Die) return;

        _life -= damage;

        if (_life > 0) return;

        _state = StateEnum.Die;
        _animator.SetTrigger("Die");
        OnDie();
    }

    /// <summary>
    /// 攻撃可能な状態に移行するメソッド
    /// </summary>
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 通常の状態に移行するメソッド
    /// </summary>
    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;

        _state = StateEnum.Normal;
    }   
    ///　<summary>
    ///　回避の状態に移行するメソッド
    /// </summary>
 　   public void GoToRollingStateIfPossible()
   　 {
        if (!IsRolling) return;

        _state = StateEnum.Rolling;
        _animator.SetTrigger("Rolling");
   　 }
}