# Testes do AdministradorServico

Este repositório contém testes unitários para a classe `AdministradorServico` de um projeto Minimal API.  
Os testes foram melhorados para serem mais claros, robustos e independentes do valor fixo do ID.

---

## 📌 Funcionalidades testadas

1. **Salvar Administrador**  
   - Testa a inclusão de um administrador no banco de dados.
   - Verifica se o registro foi criado corretamente, incluindo `Email` e `Perfil`.

2. **Buscar Administrador por ID**  
   - Testa a busca de um administrador pelo seu ID.
   - Verifica se o objeto retornado possui o mesmo ID e os mesmos dados do objeto salvo.

---

## 🛠 Melhorias implementadas

- **Isolamento do DbContext**  
  Garantia de liberação correta do contexto com `using var context`.

- **Criação de dados fake reutilizáveis**  
  Método `CriarAdministradorFake()` para evitar duplicação de código.

- **Asserts mais completos**  
  Verificação de `Email` e `Perfil`, não apenas da contagem de registros.

- **Testes independentes de ID fixo**  
  Compara o ID gerado pelo banco com o ID do objeto salvo, tornando os testes mais confiáveis.

---

## ⚙️ Como rodar os testes

Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/en-us/download) instalado.

No terminal, dentro da pasta do projeto:

```bash
# Restaurar dependências
dotnet restore

# Rodar todos os testes
dotnet test
