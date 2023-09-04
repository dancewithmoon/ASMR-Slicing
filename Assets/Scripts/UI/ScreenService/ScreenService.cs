using System;
using System.Collections.Generic;
using Base.States;
using UI.Factory;
using UI.Screens;

namespace UI.ScreenService
{
    public class ScreenService : IScreenService, ICleanUp
    {
        private readonly Dictionary<ScreenId, BaseScreen> _screens = new();
        private readonly IUiFactory _uiFactory;

        public ScreenService(IUiFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void CleanUp() => _screens.Clear();

        public BaseScreen Open(ScreenId screenId)
        {
            BaseScreen screen = Get(screenId);
            screen.gameObject.SetActive(true);
            return screen;
        }

        public void Close(ScreenId screenId) => 
            Get(screenId).gameObject.SetActive(false);

        public BaseScreen Get(ScreenId screenId)
        {
            if (_screens.TryGetValue(screenId, out BaseScreen screen) == false || screen == null)
            {
                screen = CreateScreen(screenId);
                _screens[screenId] = screen;
            }

            return screen;
        }

        private BaseScreen CreateScreen(ScreenId screenId)
        {
            return screenId switch
            {
                ScreenId.StartScreen => _uiFactory.CreateStartScreen(),
                _ => throw new ArgumentOutOfRangeException(nameof(screenId), screenId, null)
            };
        }
    }
}