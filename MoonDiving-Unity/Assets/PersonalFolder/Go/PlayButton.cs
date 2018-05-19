using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム開始用ボタン
/// </summary>
public class PlayButton : MonoBehaviour
{
    private bool _isGazed = false;
    private float _elapsedTime = 0f;

    public UnityEvent OnStartPlay;

    [SerializeField]
    private float _trigerTime = 2.0f;

    void Update()
    {
        if (_isGazed)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _trigerTime)
            {
                if (OnStartPlay != null)
                {
                    OnStartPlay.Invoke();
                }
            }
        }
    }

    public void EnterPointer()
    {
        _isGazed = true;
    }

    public void ExitPointer()
    {
        _isGazed = false;
        _elapsedTime = 0f;
    }
}
