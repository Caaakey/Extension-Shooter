using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YourName.SurvivalShooter.Interactions
{
    public class StageSelector : MonoBehaviour
    {
        public void OnEnable()
        {
            PlayerStatus.Get.Shooting.enabled = false;
            PlayerStatus.Get.Movement.enabled = false;
        }

        public void OnDisable()
        {
            PlayerStatus.Get.Shooting.enabled = true;
            PlayerStatus.Get.Movement.enabled = true;
        }

        //public void Start()
        //{
        //    string data = Resources.Load<TextAsset>("SpawnData/Stage1").text;
        //    SpawnMonster(data);
        //}

        public void OnStageSelect(string fileName)
        {
            StartCoroutine(UpdateStartStage(fileName));
        }

        private static IEnumerator UpdateStartStage(string fileName)
        {
            PlayerStatus.Get.Shooting.enabled = false;
            PlayerStatus.Get.Movement.enabled = false;

            SpawnManager.Get.SetTextData = Resources.Load<TextAsset>($"SpawnData/{fileName}").text;

            var currentScene = SceneManager.GetActiveScene();
            var newAsyncOperator = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            while (!newAsyncOperator.isDone)
                yield return null;

            var player = PlayerStatus.Get.Movement.gameObject;
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByBuildIndex(1));

            player.transform.position = Vector3.zero;
            player.transform.rotation = Quaternion.identity;
            PlayerStatus.Get.Shooting.enabled = true;
            PlayerStatus.Get.Movement.enabled = true;
            PlayerStatus.Get.Inventory.Weapon.RepaintMagazine();

            SpawnManager.Get.StartSpawn();

            var oldAsyncOperator = SceneManager.UnloadSceneAsync(currentScene);
            while (!oldAsyncOperator.isDone)
                yield return null;

            yield break;
        }

    }
}
