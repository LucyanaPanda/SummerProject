using System.Collections.Generic;
using Lucyana.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lucyana.Scenes
{
    public class SceneController : Singleton<SceneController>
    {
        [SerializeField] private Scene currentScene;
        [SerializeField] private List<string> additivesScenesLoaded =  new ();

        public override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadSceneAdditive("PlayerUI");
        }

        public override void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            base.OnDestroy();
        }

        private void LoadSceneAdditive(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        private void OnSceneLoaded(Scene scene,  LoadSceneMode mode)
        {
            if (mode == LoadSceneMode.Additive)
            {
                if (additivesScenesLoaded.Contains(scene.name))
                {
                    SceneManager.UnloadSceneAsync(scene);
                    Debug.LogWarning($"Scene Loaded: {scene.name} but it's a duplicate so unloading scene from the scene list.");
                    return;
                }
                additivesScenesLoaded.Add(scene.name);
            }
            else
            { 
                currentScene = scene;
                Debug.Log($"Current Scene: {currentScene.name}");
            }
        }
    }
}
