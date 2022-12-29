# Gerenciador De Contas

Este projeto é um serviço REST criado ASP.NET com C# e Banco de Dados SQL (SQLServer): 

Utiliza mecanismos e técnicas como: 
- Entity Framework Core 
- Injeção de dependêcias;
- Comunicação com banco de dados Relacional;
- .NET 6;
- Testes Unitários;
- AutoMapper - Mapeamento de objetos;
- DTOs - Objeto de Transferência de Dados;



# Importante ao Clonar o Projeto:

1. No projeto GerenciadorDeContas é necessário alterar o arquivo appsettings: 

     Os parâmetros de DataBase da ConnectionStrings devem ser alterados conforme configuração local do seu SQL. 
     
     Exemplo: 
     
     ![image](https://user-images.githubusercontent.com/38474570/209890578-018ee10a-9eda-40be-80ca-722ff10e3b0b.png)

     * o nome do banco de dados pode ser à sua escolha;
     * Encrypt=False pode ser mantida sem alteração;
     * Os demais dados de conexão devem os mesmos definidos na instalação da sua instância SQL.
     
 <b> Antes de executar o projeto, na primeira vez é necessário rodar o seguinte código no Package Manager Console, do Visual Studio:  </b> esse comando irá criar o BD localmente na sua máquina.
 
 > Update-Database -Context GerenciadorDeContasDbContext
 
 ![image](https://user-images.githubusercontent.com/38474570/209890958-af47a62d-4d61-4b5a-8675-dcd4ab87bf26.png)

 Ao rodar o comando, você perceberá que a tabela foi criada. 
 
 ![image](https://user-images.githubusercontent.com/38474570/209890925-cc95369b-7579-42d2-8843-9a8f27d495f2.png)
 
 Inclusive você pode verificar acessando um gerenciador de banco de dados como o Sql Server Management Studio.
 
 ![image](https://user-images.githubusercontent.com/38474570/209891068-08520e34-d2c4-4ce8-8303-4d29b80984a3.png)

 Pronto, feito isso rode o projeto no VisualStudio e você conseguirá realizar a interação nos endpoints do serviço comunicando com o banco de dados local em sua máquina.

![image](https://user-images.githubusercontent.com/38474570/209891263-db9cfa0a-a3e7-4322-b6da-00b2f76695c0.png)

