/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月27日
 *　更新日：2022年01月29日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.動作追加...神谷朋輝
 *　2.テイム関連の処理...楊志庄
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineConcierge.Fish
{
    [AddComponentMenu("_SubmarineConcierge/Fish/FishWild")]
    public class FishWild : Fish
    {
        public float moveSpeed;    // 魚の移動速度

        public Shader maskShader;

        private FishManager manager;
        private bool isTaming = false;
        private bool isAnimating = false;
        //private GameObject tameEffectPrefab;

        public void Init(FishIndividualData fish, FishData data, FishManager manager)
        {
            base.Init(fish, data);
            this.manager = manager;
            //tameEffectPrefab = Resources.Load<GameObject>("Effects/ParticleHeart");
        }

        private void Start()
        {
            // 右向きならスプライトを逆転させる
            if (moveSpeed > 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        private void Update()
        {
            if (isTaming)
            {
#if UNITY_EDITOR || UNITY_STONDALONE
                if (Input.GetMouseButtonUp(0))
                {
                    EndTame();
                }
#endif
#if UNITY_IOS
                                if (Input.touchCount==0)
                                {
                                    EndTame();
                                }
#endif
                return;
            }


            if (isAnimating)
                return;

            // 魚を等速で動かす
            this.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);

            // 画面外に出たら消滅
            if (this.transform.position.x >= 20 || this.transform.position.x <= -20)
            {
                manager.RemoveWildFish(this);
                Destroy(this.gameObject);
            }
        }

        private IEnumerator Tame()
        {
            GameObject effect = Instantiate(manager.tameEffectPrefab, transform);
            manager.tameSound.Play();
            while (fish.friendship < data.tamePP)
            {
                if (!isTaming)
                {
                    Destroy(effect);
                    manager.tameSound.Stop();
                    yield break;
                }
                Debug.Log("Taming " + fish.friendship + " / " + data.tamePP);
                if (SaveData.PPSaveData.UsePP(1))
                {
                    fish.friendship++;
                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    Destroy(effect);
                    manager.tameSound.Stop();
                    EndTame();
                    yield break;
                }
            }
            Destroy(effect);
            manager.tameSound.Stop();
            TameSucceeded();
            yield break;
        }

        public void StartTame()
        {
            if (isTaming)
                return;
            if (fish.friendship >= data.tamePP)
                return;
            isTaming = true;
            StartCoroutine(Tame());
        }

        public void EndTame()
        {
            if (!isTaming)
                return;
            isTaming = false;
        }

        private IEnumerator TamedAnimation()
        {
            isAnimating = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Material material = new Material(maskShader);
            sr.material = material;
            float progress = 1f;
            manager.tameChargeSound.Play();
            while (progress > 0f)
            {
                progress -= 0.01f;
                sr.material.SetFloat("Progress", progress);
                yield return new WaitForSeconds(0.02f);
            }
            manager.tameChargeSound.Stop();
            manager.tameSuccessSound.Play();
            yield return new WaitForSeconds(0.5f);
            GameObject obj = Instantiate(data.prefabTamed, transform.position, Quaternion.identity);
            obj.transform.parent = transform.parent;
            obj.transform.localScale = new Vector3(moveSpeed > 0 ? -1f : 1f, 1f, 1f);
            FishTamed f = obj.GetComponent<FishTamed>();
            f.Init(fish, data, false);
            manager.Tame(fish.id, f);
            SaveData.SaveDataManager.fishSaveData.Add(f.fish);
            SaveData.SaveDataManager.fishFormationSaveData.Add(f.fish);
            manager.RemoveWildFish(this);
            Destroy(gameObject);
            isAnimating = false;
            yield break;
        }

        private void TameSucceeded()
        {
            StartCoroutine(TamedAnimation());
            Debug.Log("Tame Success!");
        }
    }
}
