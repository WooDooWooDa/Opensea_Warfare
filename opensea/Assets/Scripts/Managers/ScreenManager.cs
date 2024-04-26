using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using UI;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public enum ScreenLayer
    {
        Base,
        Main,
        Dialogue,
        Popup
    }
    
    public class ScreenStack
    {
        public ScreenLayer Layer = ScreenLayer.Base;
        public ScreenName Name;
        public BaseScreen Screen;
    }
    
    public class ScreenManager : Manager
    {
        private Dictionary<ScreenLayer, Transform> m_screenLayers = new Dictionary<ScreenLayer, Transform>();
        private readonly Dictionary<ScreenName, ScreenInformations> m_screensInformations = new Dictionary<ScreenName, ScreenInformations>();
        private Stack<ScreenStack> m_screenStack = new Stack<ScreenStack>();
        
        private ScreenConfig m_config;

        public override void Initialize()
        {
            base.Initialize();
            m_config = (ScreenConfig)ManagerConfig;

            foreach (var info in m_config.Screens) {
                if (m_screensInformations.ContainsKey(info.ScreenName)) {
                    debugger.LogError("Screen of type : " + info.ScreenName + ", has already a screen defined");
                    continue;
                }
                m_screensInformations.Add(info.ScreenName, info);
            }

            foreach (ScreenLayer layer in Enum.GetValues(typeof(ScreenLayer))) {
                if (layer == ScreenLayer.Base) continue;
                var newLayer = Instantiate(m_config.Layer, transform);
                newLayer.name = layer.ToString();
                m_screenLayers.Add(layer, newLayer.transform);
            }
            
            IsInitialize = true;
        }
        
        public BaseScreen OpenScreen(ScreenName screenName, OpenInfo openInfo = null)
        {
            if (m_screensInformations.TryGetValue(screenName, out var screenInfo)) {
                if (screenInfo.Screen == null) {
                    debugger.LogWarning("No screen object associated with this screen type.");
                    return null;
                }

                var screen = Instantiate(screenInfo.Screen, transform);

                if (m_screenStack.Count > 0) {
                    var lastScreen = m_screenStack.Peek();
                    if (lastScreen.Layer != ScreenLayer.Base) {
                        lastScreen.Screen.Enable(false);
                    }
                }

                if (m_screenLayers.TryGetValue(screenInfo.ScreenLayer, out var parentLayer)) {
                    screen.transform.SetParent(parentLayer);
                }

                openInfo ??= new OpenInfo();
                openInfo.Informations = screenInfo;

                screen.Open(openInfo);
                m_screenStack.Push(new ScreenStack
                {
                    Name = screenName,
                    Layer = screenInfo.ScreenLayer,
                    Screen = screen
                });

                //Events.Screen.FireScreenOpened(screenType);
                debugger.Log("Opening screen : " + screen.name);
                return screen;
            } else {
                debugger.LogWarning("Trying to open a screen type not yet added to the config");
                return null;
            }
        }
        
        public void CloseScreen(ScreenName screenName)
        {
            debugger.LogWarning("No previous screen, stack is empty.");
        }

        public void Back()
        {
            if (m_screenStack.Peek().Layer == ScreenLayer.Base) {
                debugger.LogWarning("No previous screen, stack is empty.");
                return;
            }

            var screenStack = m_screenStack.Pop();
            screenStack.Screen.Close();

            debugger.LogWarning("No previous screen, stack is empty.");
        }

        private bool IsOpen(ScreenName screenName, out ScreenStack screen)
        {
            screen = m_screenStack.FirstOrDefault(screenStack => screenStack.Name == screenName);
            return screen!.Layer != ScreenLayer.Base;
        }
    }
}