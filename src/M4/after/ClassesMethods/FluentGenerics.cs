using System.Collections.Generic;

namespace FluentGenerics
{
    class Process
    {
        void Execute() { }
    }
    class Process<TIn, TOut>
    {
        TOut Execute(TIn input)
        {
            return default(TOut);
        }
    }
    class Process<TIn>
    {
        void Execute(TIn input) { }
    }

    // can't compile due to conflict with Process<TIn>
    // class Process<TOut>
    // {
    //     TOut Execute()
    //     {
    //         return default(TOut);
    //     }
    // }

    public static class FluentProcess
    {
        public static class WithInput<TIn>
        {
            public abstract class WithOutput<TOut>
            {
                public abstract TOut Execute(TIn input);
            }
            public abstract class WithoutOutput
            {
                public abstract void Execute(TIn input);
            }
        }
        public static class WithoutInput
        {
            public abstract class WithOutput<TOut>
            {
                public abstract TOut Execute();
            }
            public abstract class WithoutOutput
            {
                public abstract void Execute();
            }
        }
    }


    // usage
    class TranslateXToY : FluentProcess
                            .WithInput<string>
                            .WithOutput<string>
    {
        public override string Execute(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    class ListItems : FluentProcess
                        .WithoutInput
                        .WithOutput<IEnumerable<string>>
    {
        public override IEnumerable<string> Execute()
        {
            throw new System.NotImplementedException();
        }
    }

    class DeleteItem : FluentProcess
                        .WithInput<int>
                        .WithoutOutput
    {
        public override void Execute(int input)
        {
            throw new System.NotImplementedException();
        }
    }

    class ResetAllData : FluentProcess
                            .WithoutInput
                            .WithoutOutput
    {
        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}