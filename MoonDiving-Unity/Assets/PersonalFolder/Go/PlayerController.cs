using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoonVR
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rigid;
        private float _elapsedTime;

        [Header("スラスターを射出する感覚")]
        [SerializeField]
        private float _interval = 2.0f;

        [Header("スラスターを射出する力")]
        [SerializeField]
        private float _power = 2.0f;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // For Debug
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Move();
            }

            if (_elapsedTime >= _interval)
            {
                Move();
                _elapsedTime = 0;
            }
            else
            {
                _elapsedTime += Time.deltaTime;
            }

            // For Debug
            if(GvrController.AppButtonDown)
            {
                LoadMainScene();
            }
        }

        /// <summary>
        /// スラスターを射出して移動する
        /// </summary>
        private void Move()
        {
            _rigid.AddForce(Camera.main.transform.forward * _power, ForceMode.Impulse);
        }


        /// <summary>
        /// tag説明
        /// CheckPoint : ゲーム中に拾うオブジェクト
        /// Goal : ゴールとなるオブジェクト
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("CheckPoint"))
            {
                Destroy(other.gameObject);
            }
            else if(other.CompareTag("Goal"))
            {
                LoadMainScene();
            }
        }

        // For Debug
        private void LoadMainScene()
        {
            SceneManager.LoadScene("SampleMian");
        }
    }
}