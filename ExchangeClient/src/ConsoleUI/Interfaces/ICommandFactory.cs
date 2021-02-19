namespace ConsoleUI.Interfaces
{
    public interface ICommandFactory
    {
        ICommand GetCommand(string key);
    }
}