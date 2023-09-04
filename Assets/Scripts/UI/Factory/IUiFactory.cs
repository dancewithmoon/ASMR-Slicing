using Base.States;
using UI.Screens;

namespace UI.Factory
{
    public interface IUiFactory : IPreloadedInLoadLevel
    {
        void CreateUIRoot();
        BaseScreen CreateStartScreen();
    }
}