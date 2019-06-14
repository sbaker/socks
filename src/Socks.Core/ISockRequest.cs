using System.IO;

namespace Socks
{
    public interface ISockRequest
    {
        ISockContext Context { get; }

        int Length { get; }

        T ReadAs<T>();

        string ReadAsString();

        Stream ReadAsStream();
    }
}