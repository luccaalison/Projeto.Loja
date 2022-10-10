# Projeto Tecnico LOJA API

### Como rodar o projeto

1. Configurar os arquivos `appsettings.json` e colocar a string de conexão com o banco de dados
```JSON
 "developer": "Server=IP_OR_URL;Database=NOME_DO_BANCO_DE_DADOS;TrustServerCertificate=true;User Id=USUARIO_LOGIN_SQLSQSERVER;Password=SENHA_LOGIN;"
```

2. Rodar as migrações para criar as tabelas

      2.1. Rodar o comando no console de gerenciamento de pacotes nuget
      ```cmd
      Update-Database
      ```

      2.2. Ou rodar no terminal na pasta do projeto o comando com o dotnet
      ```cmd
      dotnet ef database update
      ```

3. Validar se foi criado as tabelas no banco de dados
4. Rodar o projeto pelo `visual Studio` ou usando o comando abaixo em um terminal nas pasta do projeto
```cmd
dotnet run
```

#### URL SWAGGER: https://localhost:7163/swagger/index.html
