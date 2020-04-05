using System.Threading.Tasks;

namespace DiscordBot.Bot.StateChangeHandlers
{
    public interface IStateChangeHandler
    {
        Task Initialize();
        Task OnStateChanged();
    }
}