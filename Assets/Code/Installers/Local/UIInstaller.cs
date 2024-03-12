using Code.UI.Discription;
using Code.UI.Gameplay;
using Code.UI.Menu;
using Code.UI.Stats;
using UnityEngine;
using Zenject;

namespace Code.Installers.Local
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private MenuOverlay _menu;
        [SerializeField] private GameplayOverlay _gameplayOverlay;
        [SerializeField] private StatsOverlay _statsOverlay;
        [SerializeField] private DiscriptionOverlay _discriptionOverlay;

        public override void InstallBindings()
        {
            BindMenu();
            BindGameplayOverlay();
            BindStatsOverlay();
            BindDiscriptionOverlay();
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
        
        private void BindStatsOverlay() =>
            Container
                .Bind<StatsOverlay>()
                .FromInstance(_statsOverlay)
                .AsSingle();
        
        private void BindDiscriptionOverlay() =>
            Container
                .Bind<DiscriptionOverlay>()
                .FromInstance(_discriptionOverlay)
                .AsSingle();
    }
}