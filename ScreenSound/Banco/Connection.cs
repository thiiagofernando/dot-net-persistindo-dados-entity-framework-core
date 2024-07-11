using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

public class Connection
{
    private string connectionString = "Data Source=localhost;Database=entity-framework;User ID=sa;Password=Mudar@1234;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public SqlConnection ObterConexao()
    {
        return new SqlConnection(connectionString);
    }

    public IEnumerable<Artista> Listar()
    {
        var lista = new List<Artista>();
        using var con = ObterConexao();
        con.Open();
        const string sql = "select * from Artistas";
        var command = new SqlCommand(sql,con);
        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            var nomeArtista = Convert.ToString(reader["Nome"]);
            var bioArtista = Convert.ToString(reader["Bio"]);
            var idArtista = Convert.ToInt32(reader["Id"]);
            var fotoArtista = Convert.ToString(reader["FotoPerfil"]);
            var artista = new Artista(nomeArtista,bioArtista,idArtista,fotoArtista);
            lista.Add(artista);
        }

        return lista;
    }
}