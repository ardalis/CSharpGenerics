// Sample from https://stackoverflow.com/a/52884586/13729
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public class Command : ICommand
    {
        private Func<ICommand, object> _execFunc = null;
        protected Func<ICommand, object> ExecFunc { get { return _execFunc; } }

        public object Execute()
        {
            return _execFunc(this);
        }

        public Command(Func<ICommand, object> execFunc)
        {
            if (execFunc == null) throw new ArgumentNullException("execFunc");
            _execFunc = execFunc;
        }
    }

    public class Command<TResult> : Command, ICommand<TResult> where TResult : class
    {
        new protected Func<ICommand<TResult>, TResult> ExecFunc => (ICommand<TResult> cmd) => (TResult)base.ExecFunc(cmd);

        TResult ICommand<TResult>.Execute()
        {
            return ExecFunc(this);
        }

        public Command(Func<ICommand<TResult>, TResult> execFunc) : base((ICommand c) => (object)execFunc((ICommand<TResult>)c))
        {
        }
    }

    public class ConcatCommand : Command<string>
    {
        private IEnumerable<string> _inputs;
        public IEnumerable<String> Inputs => _inputs;

        public ConcatCommand(IEnumerable<String> inputs) :
            base((ICommand<string> c) => (string)String.Concat(((ConcatCommand)c).Inputs))
        {
            if (inputs == null) throw new ArgumentNullException("inputs");
            _inputs = inputs;
        }
    }
}