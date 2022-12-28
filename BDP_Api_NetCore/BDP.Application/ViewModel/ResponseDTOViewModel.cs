using BDP.Application.Enum;

namespace BDP.Application.ViewModel
{
    public class ResponseDTOViewModel<T>
    {
        public Status Status { get; set; }

        public string Message { get; set; }

        public T Value { get; set; }
    }
}
