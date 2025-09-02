using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.Db;

namespace Test.Domain.Entidades;

[TestClass]
public class AdministradorServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }

    private Administrador CriarAdministradorFake()
    {
        return new Administrador
        {
            Email = "teste@teste.com",
            Senha = "teste",
            Perfil = "Adm"
        };
    }

    [TestMethod]
    public void TestandoSalvarAdministrador()
    {
        // Arrange
        using var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = CriarAdministradorFake();
        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);

        // Assert
        var lista = administradorServico.Todos(1).ToList();
        Assert.AreEqual(1, lista.Count);
        Assert.AreEqual(adm.Email, lista[0].Email);
        Assert.AreEqual(adm.Perfil, lista[0].Perfil);
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        // Arrange
        using var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = CriarAdministradorFake();
        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);
        var admDoBanco = administradorServico.BuscaPorId(adm.Id);

        // Assert
        Assert.IsNotNull(admDoBanco);
        Assert.AreEqual(adm.Id, admDoBanco?.Id);     // compara o ID gerado
        Assert.AreEqual(adm.Email, admDoBanco?.Email);
        Assert.AreEqual(adm.Perfil, admDoBanco?.Perfil);
    }
}
