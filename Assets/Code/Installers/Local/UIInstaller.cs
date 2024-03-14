using Code.UI.Discription;
using Code.UI.Gameplay;
using Code.UI.Menu;
using Code.UI.Options;
using Code.UI.Stats;
using UnityEngine;
using Zenject;

namespace Code.Installers.Local
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private MenuOverlay _menu;
        [SerializeField] private GameplayOverlay _gameplayOverlay;
        [SerializeField] private RecordOverlay _recordOverlay;
        [SerializeField] private PolicyOverlay _policyOverlay;
        [SerializeField] private OptionsOverlay _optionsOverlay;

        public override void InstallBindings()
        {
            BindMenu();
            BindGameplayOverlay();
            BindRecordOverlay();
            BindPolicyOverlay();
            BindOptionsOverlay();
        }

        private void BindMenu() =>
            Container
                .Bind<MenuOverlay>()
                .FromInstance(_menu)
                .AsSingle();

        private void BindGameplayOverlay() =>
            Container
                .Bind<GameplayOverlay>()
                .FromInstance(_gameplayOverlay)
                .AsSingle();
        
        private void BindRecordOverlay() =>
            Container
                .Bind<RecordOverlay>()
                .FromInstance(_recordOverlay)
                .AsSingle();
        
        private void BindPolicyOverlay() =>
            Container
                .Bind<PolicyOverlay>()
                .FromInstance(_policyOverlay)
                .AsSingle();
        
        private void BindOptionsOverlay() =>
            Container
                .Bind<OptionsOverlay>()
                .FromInstance(_optionsOverlay)
                .AsSingle();
    }
}