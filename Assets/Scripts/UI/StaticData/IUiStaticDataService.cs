using Base.States;
using UI.Screens;
using UI.ScreenService;

namespace UI.StaticData
{
    public interface IUiStaticDataService : IPreloadedInBootstrap
    {
        BaseScreen GetScreen(ScreenId screenId);
    }
}