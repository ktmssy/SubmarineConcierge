/******************************
 *
 *　作成者：
 *　作成日：
 *　更新日：
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.
 *　2.
 *　3.
 *
 ******************************/

using SubmarineConcierge.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SubmarineConcierge
{
    public class HoldToReset : MonoBehaviour
    {
        Button button;

        const float delay = 0.5f;
        float timer;
        int count = 0;
        bool isProcessing = false;

        private void ResetSaveData()
        {
            SaveDataManager.Reset();
            SaveDataManager.Load();
            PPSaveData.gained = 125;
            PPSaveData.hold = 0;
            PPTimeSaveData.time = System.DateTime.Now;
            SceneManager.LoadScene("SplashScene");
        }

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Process);
        }

        private void Update()
        {
            if (!isProcessing)
                return;

            timer += Time.deltaTime;

            if (timer > delay)
            {
                timer = 0f;
                count = 0;
                isProcessing = false;
            }
        }

        private void Process()
        {
            if (!isProcessing)
            {
                count = 0;
                isProcessing = true;
            }
            timer = 0f;
            if (++count >= 7)
            {
                Debug.Log("Lucky Seven!");
                timer = 0f;
                count = 0;
                isProcessing = false;
                ResetSaveData();
            }

        }
    }
}
