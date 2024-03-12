using Code.Services.AnswerCorrectnessService;
using Zenject;

namespace Code.Installers.Global
{
    public class AnswerCorrectnessServiceInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindProgressData();

        private void BindProgressData() =>
            Container
                .BindInterfacesAndSelfTo<AnswerCorrectnessService>()
                .AsSingle();
    }
}