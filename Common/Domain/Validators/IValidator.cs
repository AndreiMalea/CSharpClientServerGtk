namespace Common.Domain.Validators
{
    public interface IValidator<E>
    {
        void Validate(E e);
    }
}