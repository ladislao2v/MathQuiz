using Code.Services.LevelSelectorService;
using Code.Services.StaticDataService;
using Zenject;

namespace Code.Installers.Global
{
    public class LevelSelectorInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindSceneLoader();

        private void BindSceneLoader() =>
            Container
                .BindInterfacesAndSelfTo<LevelSelector>()
                .AsSingle();
    }
}