namespace BasicDefinitions
{
    class BaseResult { }
    class BaseResultGeneric<T> { }

    // concrete type
    class ResultConcrete<T> : BaseResult { }

    //closed constructed type
    class ResultClosed<T> : BaseResultGeneric<int> { }

    //open constructed type
    class ResultOpen<T> : BaseResultGeneric<T> { }

    //No error
    class Result1 : BaseResultGeneric<int> { }

    //Generates an error
    //class Result2 : BaseResultGeneric<T> {}

    //Generates an error
    //class Result3 : T {}

    class BaseResultMultiple<T, U> { }

    //No error
    class Result4<T> : BaseResultMultiple<T, int> { }

    //No error
    class Result5<T, U> : BaseResultMultiple<T, U> { }

    //Generates an error
    //class Result6<T> : BaseResultMultiple<T, U> {}
}