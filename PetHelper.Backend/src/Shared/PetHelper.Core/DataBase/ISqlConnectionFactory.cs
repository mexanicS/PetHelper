using System.Data;

namespace PetHelper.Core.DataBase;

public interface ISqlConnectionFactory
{
    public IDbConnection CreateConnection();
}