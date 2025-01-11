using System.Text; //Pour Encoding

namespace General.Basics.Extensions;

public static class StreamExtension
{
    public static async Task<string> GetAsStringAsync(this Stream stream, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        using StreamReader streamReader = new(stream, encoding);

        var result = await streamReader.ReadToEndAsync();

        return result;
    }
}
