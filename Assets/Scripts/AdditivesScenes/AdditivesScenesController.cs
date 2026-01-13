using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LucyanaPanda.AdditivesScenes
{
    //To Updtae if any scene are added in order
    public enum AdditiveScene
    {
        InventoryUi,
    }

    public class AdditivesScenesController : MonoBehaviour
    {
        public static AdditivesScenesController Instance;
        public List<string> additivesScenes;

        private void Awake()
        {
            if (Instance != null) { Destroy(Instance); }
            Instance = this;

            LoadAdditivesScenes();
        }

        public void LoadAdditivesScenes()
        {
            foreach (string scene in additivesScenes)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        }

        public void DeleteDuplicataScene(AdditiveScene sceneToDelete)
        {
            int count = 0;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == additivesScenes[(int)sceneToDelete])
                {
                    if (count == 0)
                        count++;
                    else
                        SceneManager.UnloadScene(scene);
                }
            }
        }
    }
}
