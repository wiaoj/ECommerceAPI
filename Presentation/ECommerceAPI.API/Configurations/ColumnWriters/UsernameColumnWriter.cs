using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace ECommerceAPI.API.Configurations.ColumnWriters;

public class UsernameColumnWriter : ColumnWriterBase {
    public UsernameColumnWriter() : base(NpgsqlDbType.Varchar) {

    }

    public UsernameColumnWriter(NpgsqlDbType dbType, Int32? columnLength = null) : base(dbType, columnLength) {}

    public override Object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null) {
        var (username, value) = logEvent.Properties.FirstOrDefault(x => x.Key.Equals("user_name"));

        return value?.ToString() ?? null;
    }
}