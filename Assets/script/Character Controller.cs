using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // プレイヤーの移動用変数
    float x, z;

    // プレイヤーの移動速度
    float speed = 0.1f;

    // カメラの参照
    public GameObject cam;

    // カメラとキャラクターの回転を管理する変数
    Quaternion cameraRot, characterRot;

    // マウス感度
    float Xsensitivity = 3f, Ysensitivity = 3f;

    // カーソルロック状態を管理するフラグ
    bool cursorLock = true;

    // カメラの回転角度制限
    float minX = -90f, maxX = 90f;

    // アニメーターの参照
    public Animator animator;

    void Start()
    {
        // カメラとキャラクターの初期回転を取得
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
    }

    // 毎フレーム呼び出される処理
    void Update()
    {
        // マウスの入力を取得
        float xRot = Input.GetAxis("Mouse X") * Xsensitivity;
        float yRot = Input.GetAxis("Mouse Y") * Ysensitivity;

        // カメラとキャラクターの回転を更新
        cameraRot *= Quaternion.Euler(-yRot, 0, 0); // カメラの上下回転
        characterRot *= Quaternion.Euler(0, xRot, 0); // キャラクターの左右回転

        // カメラの回転を制限
        cameraRot = ClampRotation(cameraRot);

        // カメラとキャラクターの回転を適用
        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        // カーソルロック状態を更新
        UpdateCursorLock();


    }

    // プレイヤーの移動処理
    void FixedUpdate()
    {
        // 入力に基づいて移動量を計算
        x = Input.GetAxisRaw("Horizontal") * speed; // 左右移動
        z = Input.GetAxisRaw("Vertical") * speed;   // 前後移動

        // プレイヤーの位置を更新
        transform.position += transform.forward * z + transform.right * x;
    }

    // カーソルロック状態を管理する処理
    public void UpdateCursorLock()
    {
        // Escapeキーでカーソルロックを切り替え
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = !cursorLock;
        }
        // 左クリックでカーソルロックを有効化
        else if (Input.GetMouseButtonDown(0))
        {
            cursorLock = true;
        }

        // カーソルロック状態を適用
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked; // カーソルをロック
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // カーソルを解放
        }
    }

    // カメラの回転を制限する処理
    public Quaternion ClampRotation(Quaternion q)
    {
        // クォータニオンを正規化
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        // X軸の回転角度を計算
        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        // X軸の回転角度を制限
        angleX = Mathf.Clamp(angleX, minX, maxX);

        // 制限後の角度をクォータニオンに変換
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
