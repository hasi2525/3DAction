using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(EnemyStatus))]

/// <summary>
/// 敵の移動を制御するスクリプト
/// </summary>
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask;

    private NavMeshAgent _agent;

    private RaycastHit[] _raycastHits = new RaycastHit[10];

    private EnemyStatus _status;

    void Start()
    {
        //NavMeshAgentを取得
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();
    }

    /// <summary>
    /// 特定のオブジェクトを検出したときに呼び出されるメソッド
    /// </summary>
    /// <param name="collider">検出されたコライダー</param>
    public void OnDetectObject(Collider collider)
    {
        if(! _status.IsMoveble)
        {
            _agent.isStopped = true;
            return;
        }



        // 検出されたオブジェクトがプレイヤーである場合、その位置を目的地に設定
        if (collider.CompareTag("Player"))
        {
            //自分とプレイヤーの位置差
            Vector3 positionDiff = collider.transform.position - transform.
            position;
            //プレイヤーとの距離を計算
            float distance = positionDiff.magnitude;
            //プレイヤーの方向
            Vector3 direction = positionDiff.normalized;

            float hitCount = Physics.RaycastNonAlloc(transform.position,
            direction, _raycastHits, distance, raycastLayerMask);

            Debug.Log("hitCount:" + hitCount);
            if(hitCount == 0)
            {
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                //見失ったら停止
                _agent.isStopped = true;
            }
        }
    }

    /// <summary>
    /// NavMeshAgentの目的地を設定します
    /// </summary>
    /// <param name="destination">目的地の座標</param>
    private void SetDestinationToPlayer(Vector3 destination)
    {
        _agent.destination = destination;
    }
}