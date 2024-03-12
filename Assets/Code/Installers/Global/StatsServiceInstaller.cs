using Code.Services.StatsService;
using Zenject;

namespace Code.Installers.Global
{
    public class StatsServiceInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindScoreService();

        private void BindScoreService() =>
            Container
                .BindInterfacesAndSelfTo<StatsService>()
                .AsSingle();
    }
}