namespace BasicDefinitions
{
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-classes

    class BaseResult { }
    class BaseResultGeneric<T> { }

    // generic inherit from concrete type (aka non-generic)
    class ResultFromConcrete<T> : BaseResult { }

    // generic inherit from closed constructed type
    class ResultFromClosed<T> : BaseResultGeneric<int> { }

    // generic inherit from open constructed type
    class ResultFromOpen<T> : BaseResultGeneric<T> { }

    // concrete inherit from closed constructed type
    class IntResult : BaseResultGeneric<int> { }

    // concrete cannot inherit from open constructed class
    //class Result2 : BaseResultGeneric<T> {} // ERROR

    // concrete cannot inherit simply from T
    //class Result3 : T {} // ERROR

    class BaseResultMultiple<T, U> { }

    // Single-variable generic can inherit from multi-variable if some types are closed
    class IntResult4<T> : BaseResultMultiple<T, int> { }

    // Closed constructed types(in this case int) can't be set on the derived type
    //class IntResult5<T,int> : BaseResultGeneric<T, int> {} // ERROR

    // Multi-generic class can inherit from multi-generic class as long as all types are defined
    class MultiResult5<T, U> : BaseResultMultiple<T, U> { }

    // Leaving undefined generic types on base and not on defined class produces an error
    //class Result6<T> : BaseResultMultiple<T, U> {} // ERROR
}