// Sample from https://stackoverflow.com/a/52884586/13729
using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace Generic.Commands
{
    public interface ICommand
    {
        object Execute();
    }

    public interface ICommand<TResult> : ICommand
    {
        new TResult Execute();
    }











    // concrete Command class implements non-generic interface
    public class Command : ICommand
    {
        protected Func<ICommand, object> ExecFunc { get; }

        public object Execute()
        {
            return ExecFunc(this);
        }

        public Command(Func<ICommand, object> execFunc)
        {
            ExecFunc = Guard.Against.Null(execFunc, nameof(execFunc));
        }
    }







    // generic class inherits from non-generic
    public class Command<TResult> : Command, ICommand<TResult> where TResult : class
    {
        new protected Func<ICommand<TResult>, TResult> ExecFunc => (ICommand<TResult> cmd) => (TResult)base.ExecFunc(cmd);

        TResult ICommand<TResult>.Execute()
        {
            return ExecFunc(this);
        }

        public Command(Func<ICommand<TResult>, TResult> execFunc) :
            base((ICommand c) => (object)execFunc((ICommand<TResult>)c))
        {
        }
    }







    public class ConcatCommand : Command<string>
    {
        public IEnumerable<String> Inputs { get; }

        public ConcatCommand(IEnumerable<String> inputs) :
            base((ICommand<string> c) => (string)String.Concat(((ConcatCommand)c).Inputs))
        {
            Inputs = Guard.Against.Null(inputs, nameof(inputs));
        }
    }











    public class CollectCommand : Command
    {
        public IEnumerable<object> Inputs { get; }
        public CollectCommand(IEnumerable<object> inputs) :
            base((ICommand c) => new List<object>(((CollectCommand)c).Inputs))
        {
            Inputs = Guard.Against.Null(inputs, nameof(inputs));
        }
    }
}