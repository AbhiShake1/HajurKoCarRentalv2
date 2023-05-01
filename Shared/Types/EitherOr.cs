// ReSharper disable InconsistentNaming

namespace HajurKoCarRental.Shared.Types;

public readonly struct Either<L, R>
{
    private readonly L left;
    private readonly R right;

    private Either(L left)
    {
        this.left = left;
        right = default!;
        IsLeft = true;
    }

    private Either(R right)
    {
        this.right = right;
        left = default!;
        IsLeft = false;
    }

    public static Either<L, R> Left(L value)
    {
        return new Either<L, R>(value);
    }

    public static Either<L, R> Right(R value)
    {
        return new Either<L, R>(value);
    }

    public bool IsLeft { get; }

    public bool IsRight => !IsLeft;

    public L LeftValue =>
        IsLeft ? left : throw new InvalidOperationException("Cannot access Left value when instance is Right");

    public R RightValue =>
        !IsLeft ? right : throw new InvalidOperationException("Cannot access Right value when instance is Left");

    public Either<LNew, R> MapLeft<LNew>(Func<L, LNew> map)
    {
        return IsLeft ? Either<LNew, R>.Left(map(LeftValue)) : Either<LNew, R>.Right(RightValue);
    }

    public Either<L, RNew> MapRight<RNew>(Func<R, RNew> map)
    {
        return IsLeft ? Either<L, RNew>.Left(LeftValue) : Either<L, RNew>.Right(map(RightValue));
    }

    public void Fold(Action<L> left, Action<R> right)
    {
        if (IsLeft)
            left(LeftValue);
        else
            right(RightValue);
    }
}

// since this should be just a plugin to the original function (hacky + no explicit generics), its kept in extensions   
public static class EitherExtensions
{
    public static Either<LNew, RNew> Map<L, R, LNew, RNew>(
        this Either<L, R> either,
        Func<L, LNew> left,
        Func<R, RNew> right)
    {
        return either.IsLeft
            ? Either<LNew, RNew>.Left(left(either.LeftValue))
            : Either<LNew, RNew>.Right(right(either.RightValue));
    }

    public static Either<LNew, R> MapLeft<L, R, LNew>(
        this Either<L, R> either,
        Func<L, LNew> map)
    {
        return either.IsLeft
            ? Either<LNew, R>.Left(map(either.LeftValue))
            : Either<LNew, R>.Right(either.RightValue);
    }

    public static Either<L, RNew> MapRight<L, R, RNew>(
        this Either<L, R> either,
        Func<R, RNew> map)
    {
        return either.IsLeft
            ? Either<L, RNew>.Left(either.LeftValue)
            : Either<L, RNew>.Right(map(either.RightValue));
    }
    
    public static Either<L, R> Join<L, R>(
        this Either<Either<L, R>, R> either)
    {
        return either.IsLeft
            ? either.LeftValue
            : Either<L, R>.Right(either.RightValue);
    }
}