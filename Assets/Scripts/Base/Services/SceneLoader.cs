﻿using System;
using System.Collections;
using Base.Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Services
{
    public class SceneLoader : IService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }

        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

            while (waitNextScene.isDone == false)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}