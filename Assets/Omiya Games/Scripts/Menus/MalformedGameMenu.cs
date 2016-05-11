﻿using UnityEngine;
using UnityEngine.UI;

namespace OmiyaGames
{

    ///-----------------------------------------------------------------------
    /// <copyright file="CreditsMenu.cs" company="Omiya Games">
    /// The MIT License (MIT)
    /// 
    /// Copyright (c) 2014-2015 Omiya Games
    /// 
    /// Permission is hereby granted, free of charge, to any person obtaining a copy
    /// of this software and associated documentation files (the "Software"), to deal
    /// in the Software without restriction, including without limitation the rights
    /// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    /// copies of the Software, and to permit persons to whom the Software is
    /// furnished to do so, subject to the following conditions:
    /// 
    /// The above copyright notice and this permission notice shall be included in
    /// all copies or substantial portions of the Software.
    /// 
    /// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    /// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    /// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    /// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    /// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    /// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    /// THE SOFTWARE.
    /// </copyright>
    /// <author>Taro Omiya</author>
    /// <date>5/18/2015</date>
    ///-----------------------------------------------------------------------
    /// <summary>
    /// Scrolling credits. You can retrieve this menu from the singleton script,
    /// <code>MenuManager</code>.
    /// </summary>
    /// <seealso cref="MenuManager"/>
    public class MalformedGameMenu : IMenu
    {
        public enum Reason
        {
            None = -1,
            CannotConfirmGenuine = 0,
            IsNotGenuine,
            CannotConfirmDomain,
            IsIncorrectDomain
        }

        [Header("Components")]
        [SerializeField]
        Button defaultButton = null;
        [SerializeField]
        ScrollRect scrollable = null;
        [SerializeField]
        RectTransform content = null;

        System.Action<float> checkInput = null;

        public override Type MenuType
        {
            get
            {
                return Type.ManagedMenu;
            }
        }

        public override GameObject DefaultUi
        {
            get
            {
                return defaultButton.gameObject;
            }
        }

        public override void Show(System.Action<IMenu> stateChanged)
        {
            // Call base function
            base.Show(stateChanged);

            // Unlock the cursor
            //SceneManager.CursorMode = CursorLockMode.None;

            // Check if we've previously binded to the singleton's update function
            if (checkInput != null)
            {
                Singleton.Instance.OnUpdate -= checkInput;
                checkInput = null;
            }

            // Bind to Singleton's update function
            checkInput = new System.Action<float>(CheckForAnyKey);
            Singleton.Instance.OnUpdate += checkInput;
        }

        public override void Hide()
        {
            bool wasVisible = (CurrentState == State.Visible);

            // Call base function
            base.Hide();

            if (wasVisible == true)
            {
                // Lock the cursor to what the scene is set to
                SceneTransitionManager manager = Singleton.Get<SceneTransitionManager>();
                //SceneManager.CursorMode = manager.CurrentScene.LockMode;

                // Unbind to Singleton's update function
                if (checkInput != null)
                {
                    Singleton.Instance.OnUpdate -= checkInput;
                    checkInput = null;
                }

                // Return to the menu
                manager.LoadMainMenu();
            }
        }

        public void UpdateReason(Reason reason)
        {
            // Grab the web checker
            WebLocationChecker webChecker = null;
            if (Singleton.Instance.IsWebplayer == true)
            {
                webChecker = Singleton.Get<WebLocationChecker>();
            }

            // FIXME: do something!
            switch (reason)
            {
                case Reason.CannotConfirmGenuine:
                    break;
                case Reason.IsNotGenuine:
                    break;
                case Reason.CannotConfirmDomain:
                    if (webChecker != null)
                    {

                    }
                    break;
                case Reason.IsIncorrectDomain:
                    if (webChecker != null)
                    {

                    }
                    break;
            }
        }

        void CheckForAnyKey(float deltaTime)
        {
            if (Input.anyKeyDown == true)
            {
                Hide();
            }
        }
    }
}